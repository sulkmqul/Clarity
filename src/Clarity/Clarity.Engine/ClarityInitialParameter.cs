using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.File;

namespace Clarity.Engine
{
    public class ClarityInitialParameterSet
    {
        /// <summary>
        /// データID
        /// </summary>
        public int Id = 0;
        /// <summary>
        /// データコード
        /// </summary>
        public string Code = "";

        /// <summary>
        /// これのタグ名
        /// </summary>
        public string TagName = "";

        /// <summary>
        /// 格納データ
        /// </summary>
        public object Data = null;


        /// <summary>
        /// データの取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetData<T>()
        {
            return (T)this.Data;
        }
    }

    /// <summary>
    /// 初期設定パラメータ
    /// </summary>
    public class ClarityInitialParameter
    {
        /// <summary>
        /// 元ネタ
        /// </summary>
        List<ClaritySettingData> SrcList = null;

        class ManageData
        {
            public string Code;
            public int Id;

            public List<ClarityInitialParameterSet> DataList = new List<ClarityInitialParameterSet>();
        }

        /// <summary>
        /// 変換マップ
        /// </summary>
        Dictionary<string, ManageData> ManaDic = null;

        /// <summary>
        /// ファイルの読み込み
        /// </summary>
        /// <param name="pathlist"></param>
        public void LoadFile(List<string> pathlist)
        {
            this.SrcList = new List<ClaritySettingData>();

            this.ManaDic = new Dictionary<string, ManageData>();

            pathlist.ForEach(x =>
            {

                ClaritySettingFile fp = new ClaritySettingFile();

                int rid = 0;
                var fl = fp.ReadSetting(x, out rid);
                this.SrcList.AddRange(fl);

                
                
            });

            //データを変換して保持
            var dic = this.CreateMap(this.SrcList);

            //データを突っ込む
            dic.ToList().ForEach(x => this.ManaDic.Add(x.Key, x.Value));

        }

        /// <summary>
        /// 対象のデータを取得
        /// </summary>
        /// <param name="rootcode">RootCode名</param>
        /// <param name="tag">rootコード以下のtag名</param>
        /// <returns>取得値</returns>
        public List<ClarityInitialParameterSet> GetTagData(string rootcode, string? tag = null)
        {
            if (this.ManaDic.ContainsKey(rootcode) == false)
            {
                return new List<ClarityInitialParameterSet>();
            }

            var datalist = this.ManaDic[rootcode].DataList;
            string code = rootcode + "." + tag;

            //return datalist.Where(x => x.Code.StartsWith(code)).ToList();
            return datalist.Where(x => x.Code == code).ToList();
        }

        /// <summary>
        /// 対象データを単体取得
        /// </summary>
        /// <param name="rootcode">RootCode名</param>
        /// <param name="tag">RootCode以下のtag名</param>
        /// <returns></returns>
        public ClarityInitialParameterSet GetTagDataFirst(string rootcode, string? tag = null)
        {
            return this.GetTagData(rootcode, tag).FirstOrDefault();
        }

        /// <summary>
        /// データの取得対象全部
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rootcode"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public List<T> GetData<T>(string rootcode, string? tag = null)
        {
            return this.GetTagData(rootcode, tag).Select(x => (T)x.Data).ToList();
        }

        /// <summary>
        /// データの取得 単体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rootcode"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public T GetDataFirst<T>(string rootcode, string tag , T def)
        {
            var m = this.GetData<T>(rootcode, tag);
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
        /// <param name="rootcode"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public T GetDataEnum<T>(string rootcode, string tag, T def) where T: Enum
        {
            string s = this.GetDataFirst<string>(rootcode, tag, "");
            
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
        private Dictionary<string, ManageData> CreateMap(List<ClaritySettingData> datalist)
        {
            Dictionary<string, ManageData> ansdic = new Dictionary<string, ManageData>();

            //Setに変換する
            List<ClarityInitialParameterSet> templist = datalist.Select(x => new ClarityInitialParameterSet() { Id = x.Id, Code = x.Code, TagName = x.TagName, Data = x.Data }).ToList();

            //rootの設定
            templist.ForEach(x =>
            {
                string rootcode = this.GetRootCode(x.Code);
                bool f = ansdic.ContainsKey(rootcode);
                if (f == false)
                {
                    ManageData data = new ManageData();
                    data.Code = rootcode;
                    data.Id = x.Id;
                    data.DataList = new List<ClarityInitialParameterSet>();
                    ansdic.Add(rootcode, data);
                }
                ansdic[rootcode].DataList.Add(x);

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
