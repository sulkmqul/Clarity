using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.File;

namespace Clarity.Engine
{
    /// <summary>
    /// 初期設定パラメータ
    /// </summary>
    public class ClarityInitialParameter
    {

        /// <summary>
        /// 変換マップ
        /// </summary>
        Dictionary<int, ClaritySettingData> ManaDic = null;

        /// <summary>
        /// ファイルの読み込み
        /// </summary>
        /// <param name="pathlist"></param>
        public void LoadFile(List<string> pathlist)
        {
            this.ManaDic = new Dictionary<int, ClaritySettingData>();

            List<ClaritySettingData> srclist = new List<ClaritySettingData>();
            pathlist.ForEach(x =>
            {

                ClaritySettingFile fp = new ClaritySettingFile();

                int rid = 0;
                string rname;
                var tlist = fp.ReadSetting(x, out rid, out rname);
                srclist.AddRange(tlist);
            });


            //扱いやすいmap形式に変換
            var dic = this.CreateMap(srclist);            

            //データを突っ込む
            dic.ToList().ForEach(x => this.ManaDic.Add(x.Key, x.Value));
            
        }


        /// <summary>
        /// 設定データを生で取得
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public ClaritySettingData GetSettingData(int rid)
        {
            return this.ManaDic[rid];
        }


        /// <summary>
        /// データの取得対象全部
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rid"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public List<T> GetData<T>(int rid, string tag)
        {
            var data = this.GetSettingData(rid);
            return data.GetDataList<T>(tag);
        }

        /// <summary>
        /// データの取得 単体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rid"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public T GetDataFirst<T>(int rid, string tag , T def)
        {
            var m = this.GetData<T>(rid, tag);
            if (m.Count <= 0)
            {
                return def;
            }
            return m.First();
        }

        /// <summary>
        /// Enumデータの取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rid"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public T GetDataEnum<T>(int rid, string tag, T def) where T: Enum
        {
            string s = this.GetDataFirst<string>(rid, tag, "");
            
            //指定型に変換可能？
            object o;
            bool f = Enum.TryParse(typeof(T), s, out o);
            if (f == false)
            {
                return def;
            }
                
            T ans = (T)Enum.Parse(typeof(T), s);
            return ans;
        }

        /// <summary>
        /// 管理Codeと番号の取得(Aid用)
        /// </summary>
        /// <returns></returns>
        internal List<(string code, int id)> CreateCodeIdList()
        {
            return this.ManaDic.Select(x => (x.Value.Code, x.Value.Id)).ToList();
        }


        

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 変換マップを作成する [root code, deta]
        /// </summary>
        /// <param name="datalist"></param>
        /// <returns></returns>
        private Dictionary<int, ClaritySettingData> CreateMap(List<ClaritySettingData> datalist)
        {
            Dictionary<int, ClaritySettingData> ansdic = new Dictionary<int, ClaritySettingData>();

            //大本Nodeを取得
            List<ClaritySettingData> rootlist = datalist.SearchRootNode();

            rootlist.ForEach(x =>
            {
                ansdic.Add(x.Id, x);
            });

            return ansdic;
        }

        /// <summary>
        /// 大本のコードを取得する
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <remarks>
        /// abc.def.cdの場合のabcだけを取得する
        /// </remarks>
        private string GetRootCode(string code)
        {
            //最初の.位置を取得
            int m = code.IndexOf(".");
            if (m < 0)
            {
                //単体ならこれがrootコード
                return code;
            }

            //.までを分離
            string ans = code.Substring(0, m);
            return ans;

        }
    }
}
