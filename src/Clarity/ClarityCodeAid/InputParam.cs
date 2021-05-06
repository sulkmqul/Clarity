using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ClarityCodeAid
{
    /// <summary>
    /// 入力パラメータクラス
    /// </summary>
    public class InputParam
    {
        /// <summary>
        /// 処理モード
        /// </summary>
        public EAidMode AidMode = EAidMode.Max;

        /// <summary>
        /// 入力パラメータ一式
        /// </summary>
        public List<string> InputList = new List<string>();

        /// <summary>
        /// 検索鍵
        /// </summary>
        public List<string> ExtKeyList = new List<string>();

        /// <summary>
        /// 出力パス
        /// </summary>
        public string OutputPath = "";


        /// <summary>
        /// 対象がフォルダか否かをチェックする
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool CheckDirectory(string s)
        {
            return Directory.Exists(s);
        }

        /// <summary>
        /// InputListをExtKeyを元に解析し、処理対象となるファイル一覧を得る。ファイルの場合はそのまま
        /// </summary>
        /// <returns></returns>
        public List<string> CreateInputFileList()
        {
            List<string> anslist = Clarity.Util.ClarityUtil.ListupSpecifiedFiles(this.InputList, this.ExtKeyList);

            return anslist;

        }



        /// <summary>
        /// 起動引数の解析
        /// </summary>
        /// <param name="args">処理</param>
        /// <returns></returns>
        public void AnalyzeParam(string[] args)
        {
            Dictionary<int, List<string>> paramdic = new Dictionary<int, List<string>>();

            (string key, int index)[] analyzelist = {
                ("i", 0),
                ("k", 1),
                ("o", 2),                
            };

            //初期化をしておく
            foreach (var ids in analyzelist)
            {
                paramdic.Add(ids.index, new List<string>());
            }


            //AidModeの読み込み
            this.AidMode = this.AnalyzeAidMode(args[0]);
            if (this.AidMode == EAidMode.Max)
            {
                throw new Exception("mode argument Error!!");
            }
            

            int i = 1;
            int index = 0;
            while (i < args.Length)
            {
                //処理文字取得
                string s = args[i];

                //param check
                string cs = this.CheckParam(s);
                if (cs.Length > 0)
                {
                    //パラメータであった
                    var a = analyzelist.ToList().Find((k) =>
                    {
                        return k.key.Equals(cs);
                    });
                    if (a.key == null)
                    {
                        throw new Exception(string.Format("{0}:無効なパラメータです", cs));
                    }
                    
                    index = a.index;
                    i++;
                    continue;
                }

                //ここまで来たらAddする
                bool kf = paramdic.ContainsKey(index);
                if (kf == false)
                {
                    paramdic.Add(index, new List<string>());
                }

                paramdic[index].Add(s);

                i++;

            }


            try
            {
                //入力要素の適応
                this.InputList = paramdic[0];
                this.ExtKeyList = paramdic[1];
                this.OutputPath = paramdic[2][0];
            }
            catch (Exception exx)
            {
                throw new Exception("パラメータ引数に問題があります");
            }

        }


        /// <summary>
        /// パラメータ先頭文字か否かをチェック ret=空文字パラメータでない。　
        /// </summary>
        /// <param name="s">解析文字列</param>
        /// <returns></returns>
        private string CheckParam(string s)
        {
            string ans = "";

            //先頭 - であるならパラメータと解釈する
            int p = s.IndexOf("-");
            if (p != 0)
            {
                return "";
            }

            //-をすべて消し、残った文字を返却(-と--の区別を無くすため)
            ans = s.Replace("-", "");
            return ans;

        }

        
        /// <summary>
        /// 処理モードの取得
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private EAidMode AnalyzeAidMode(string param)
        {
            (string s, EAidMode mode)[] modevec = {
                ("te", EAidMode.Texture),
                ("ve", EAidMode.Vertex),
                ("sh", EAidMode.Shader),
                ("so", EAidMode.Sound),
                ("ta", EAidMode.TexAnime),
                ("im", EAidMode.ImageMerge),
                ("cs", EAidMode.ClaritySetting),
            };


            //指定モードに一致するものを検索
            var a = modevec.ToList().Find((v) =>
            {
                return v.s.Equals(param);
            });

            //無かった            
            if (a.s == null)
            {
                return EAidMode.Max;
            }

            return a.mode;
            


        }
    }

}
