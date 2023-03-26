using Clarity;
using Clarity.Engine.Texture;
using ClarityMovement.MotionFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.ComponentModel.Com2Interop;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClarityMovement
{
    /// <summary>
    /// プロジェクトファイル管理
    /// </summary>
    internal class CmProjectFile
    {
        const string BASE_TAG = "Base";
        const string SRC_IMAGE_TAG = "SrcImage";
        const string TAG_TAG = "Tag";


        /// <summary>
        /// プロジェクトの保存
        /// </summary>
        /// <param name="project">保存対象</param>
        /// <param name="filepath">保存ファイルパス</param>
        /// <returns></returns>
        public async Task SaveProject(CmProject project, string filepath)
        {
            XElement root = new XElement("ClarityMovementProject");

            //基本情報
            var basetag = this.SaveBase(project);
            root.Add(basetag);

            //使用画像
            var srctag = this.SaveSrcImage(project);
            root.Add(srctag);

            //タグ
            var tags = this.SaveTag(project);
            root.Add(tags);

            //保存
            await Task.Run(() =>
            {
                using (FileStream fp = new FileStream(filepath, FileMode.Create))
                {
                    //ちゃんと見栄えが良い形式で出力する
                    using (XmlTextWriter xtw = new XmlTextWriter(fp, Encoding.UTF8))
                    {
                        XDocument doc = new XDocument();
                        doc.Add(root);
                        xtw.Formatting = Formatting.Indented;
                        doc.Save(xtw);
                    }

                }
            });
        }

        /// <summary>
        /// Projectの読み込み
        /// </summary>
        /// <param name="filepath">読み込みファイルパス</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public CmProject LoadProject(string filepath)
        {
            try
            {
                CmProject ans = new CmProject();

                //ファイルの読み込み
                XElement root = XElement.Load(filepath);

                {
                    //基本情報の読み込み
                    var basetag = root.Element(BASE_TAG);
                    if (basetag == null)
                    {
                        throw new Exception("BaseTagE読み込み失敗");
                    }
                    this.ReadBaseTag(basetag, ans);
                }


                //元画像
                {
                    var srctag = root.Element(SRC_IMAGE_TAG);
                    if (srctag == null)
                    {
                        throw new Exception("SrcImageTag読み込み失敗");
                    }
                    this.ReadSrcImageTag(srctag, ans);
                }


                //タグ情報の読み込み
                {
                    var tagtag = root.Element(TAG_TAG);
                    if (tagtag == null)
                    {
                        throw new Exception("Tag読み込み失敗");
                    }
                    this.ReadDataTag(tagtag, ans);
                }

                return ans;
            }
            catch(Exception ex)
            {
                throw new Exception("LoadProject", ex);
            }

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        #region 読み込み

        /// <summary>
        /// 基本情報の読み取り
        /// </summary>
        /// <param name="btag">Baseタグ内容</param>
        /// <param name="data">読み込み場所</param>
        private void ReadBaseTag(XElement btag, CmProject data)
        {
            data.FrameRate = Convert.ToInt32(btag.Element("FrameRate")?.Value);
            
            string[] svec = ((btag.Element("RenderingSize")?.Value) ?? "").Split(",");
            data.RenderingSize = new System.Numerics.Vector2(Convert.ToSingle(svec[0]), Convert.ToSingle(svec[1]));

            data.MaxFrame = Convert.ToInt32(btag.Element("MaxFrame")?.Value);
        }

        /// <summary>
        /// 元画像の読み込み
        /// </summary>
        /// <param name="itag"></param>
        /// <param name="data"></param>
        private void ReadSrcImageTag(XElement itag, CmProject data)
        {
            var pathlist = itag.Elements("ImageFilePath").Select(x => x.Value).ToList();
            
            //存在確認
            pathlist.ForEach(x =>
            {
                bool f = File.Exists(x);
                if (f == false)
                {
                    throw new Exception("SrcImagePath is not exists");

                }
            });

            //読み込み
            data.ImageDataMana.AddImage(pathlist);
        }


        /// <summary>
        /// タグ情報の読み込み
        /// </summary>
        /// <param name="rtag"></param>
        /// <param name="data"></param>
        private void ReadDataTag(XElement rtag, CmProject data)
        {
            //画像タグ
            {
                var itaglist = rtag.Elements("ImageTag");

                //画像タグの読み込み
                foreach(var itag in itaglist)
                {
                    var d = this.ReadImageTag(itag, data.ImageDataMana);
                    data.ModifierList.Add(d);
                }

            }

            //データタグ
            {
                var taglist = rtag.Elements("DataTag");
                foreach (var tag in taglist)
                {
                    var d = this.ReadDataTag(tag);
                    data.ModifierList.Add(d);
                }
            }

        }

        /// <summary>
        /// イメージタグの読み込み
        /// </summary>
        /// <param name="itag">画像タグ</param>
        /// <param name="mana">イメージ</param>
        /// <returns></returns>
        private FrameImageModifier ReadImageTag(XElement itag, ImageDataManager mana)
        {
            FrameImageModifier ans = new FrameImageModifier();
            ans.Frame = Convert.ToInt32(itag.Element("Frame")?.Value);
            ans.FrameSpan = Convert.ToInt32(itag.Element("FrameSpan")?.Value);
            
            //パスの取得、パスから対象データ割り出し
            string path = itag.Element("Path")?.Value ?? "";
            int? id = mana.identifiedImageByFilePath(path);
            if(id == null)
            {
                throw new Exception($"ImageTagPath [{path}] is not exists");
            }
            ans.ImageDataID = id.Value;

            return ans;
            
        }

        /// <summary>
        /// データタグの読み込み
        /// </summary>
        /// <param name="rtag"></param>
        /// <returns></returns>
        private FrameTagModifier ReadDataTag(XElement rtag)
        {
            FrameTagModifier ans = new FrameTagModifier();
            ans.Frame = Convert.ToInt32(rtag.Element("Frame")?.Value);
            ans.FrameSpan = Convert.ToInt32(rtag.Element("FrameSpan")?.Value);            
            //全タグを読み込み、clarity形式を探す
            ClaritySettingCoreReader cr = new ClaritySettingCoreReader();
            bool rok = false;
            var elemlist = rtag.Elements();
            foreach (var tag in elemlist)
            {
                ClarityMotionTag? mda = cr.Analyze<ClarityMotionTag>(tag);
                if(mda != null)
                {
                    ans.Data = mda;
                    rok = true;
                    break;
                }
            }
            if(rok == false)
            {
                throw new Exception("DataTag is not contain clarity type");
            }

            //TagIDの設定
            ans.TagId = Convert.ToInt32(rtag.Element("Id")?.Value);

            return ans;
        }
        #endregion



        #region 書き込み        
        /// <summary>
        /// 基礎情報の保存
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        private XElement SaveBase(CmProject proj)
        {
            XElement ans = new XElement(BASE_TAG);

            //フレームレート
            ans.Add(new XElement("FrameRate", proj.FrameRate));

            //サイズ
            ans.Add(new XElement("RenderingSize", $"{proj.RenderingSize.X},{proj.RenderingSize.Y}"));

            //最大フレーム数
            ans.Add(new XElement("MaxFrame", proj.MaxFrame));

            return ans;
        }

        /// <summary>
        /// 元画像の保存
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        private XElement SaveSrcImage(CmProject proj)
        {
            XElement ans = new XElement(SRC_IMAGE_TAG);

            var srclist = proj.ImageDataMana.GetImageList();

            srclist.ForEach(x =>
            {
                ans.Add(new XElement("ImageFilePath", x.FilePath));
            });

            return ans;
        }

        /// <summary>
        /// タグの保存
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        private XElement SaveTag(CmProject proj)
        {
            XElement ans = new XElement(TAG_TAG);

            {                
                //画像の書き出し
                var itaglist = proj.ModifierList.Where(x => x.TagType == ETagType.Image).Select(y => (FrameImageModifier)y).ToList();
                itaglist.ForEach(x =>
                {
                    XElement itag = new XElement("ImageTag");
                    itag.Add(new XElement("Frame", x.Frame));
                    itag.Add(new XElement("FrameSpan", x.FrameSpan));
                    var idata = proj.ImageDataMana.GetImage(x.ImageDataID);
                    itag.Add(new XElement("Path", idata.FilePath));


                    ans.Add(itag);
                });
            }
            {
                //tagの書き出し
                ClaritySettingCoreWriter cw = new ClaritySettingCoreWriter();
                var taglist = proj.ModifierList.Where(x => x.TagType == ETagType.Tag).Select(y => (FrameTagModifier)y).ToList();
                taglist.ForEach(x =>
                {
                    XElement tag = new XElement("DataTag");
                    tag.Add(new XElement("Frame", x.Frame));
                    tag.Add(new XElement("FrameSpan", x.FrameSpan));
                    tag.Add(cw.CreateElement(x.Data));
                    tag.Add(new XElement("Id", x.TagId));

                    ans.Add(tag);
                });
            }


            return ans;
        }
        #endregion
    }
}
