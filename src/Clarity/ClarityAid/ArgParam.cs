using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityAid
{
    /// <summary>
    /// 入力パラメータ管理データ
    /// </summary>
    class ArgParam
    {

        class KeyValueSet
        {
            public string Key;
            public string Value;
        }

        #region メンバ変数

        /// <summary>
        /// これのモード
        /// </summary>
        public EAidMode Mode = EAidMode.Max;

        /// <summary>
        /// これのパラメータ一式
        /// </summary>
        private List<KeyValueSet> ParamVec;

        /// <summary>
        /// 元ネタ引数を丸ごと保存
        /// </summary>
        protected string[] Arg = null;
        #endregion

        /// <summary>
        /// 処理の解析
        /// </summary>
        /// <param name="arg"></param>
        public bool Analyze(string[] arg)
        {
            try
            {
                //元ネタを保存
                this.Arg = arg;

                //モードの解析
                this.Mode = this.AnalyzeAidMode(arg[0]);
                if (this.Mode == EAidMode.Max)
                {
                    return false;
                }

                //パラメータ一の解析
                this.ParamVec = this.AnalyzeParam(arg);
                if (this.ParamVec == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 指定のパラメータを取得
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> GetParameter(string key)
        {
            //この二つは同じだが、どっちの書き方の方がよいのだろうか？
            //var data = from f in this.ParamVec where f.Key == key select f.Value;
            //this.ParamVec.Where(x => x.Key == key).Select(x => x.Value).ToList();

            var data = from f in this.ParamVec where f.Key == key select f.Value;
            return data.ToList();
        }

        /// <summary>
        /// 入力一式の取得
        /// </summary>
        /// <returns></returns>
        public List<string> GetInputList()
        {
            return this.GetParameter("-i");
        }

        /// <summary>
        /// 出力ファイルパスの作成
        /// </summary>
        /// <param name="param">パラメータ</param>
        /// <param name="filename">ファイル名</param>
        /// <returns></returns>
        public string CreateOutputFile(string filename)
        {
            List<string> olist = this.GetParameter("-o");
            if (olist.Count <= 0)
            {
                return filename;
            }

            return olist[0] + "\\" + filename;
        }

        /// <summary>
        /// 対象のパラメータが存在するかを確認する。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CheckExists(string key)
        {
            int c = this.GetParameter(key).Count;
            if (c <= 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 整数値パラメータの取得
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>複数ある場合は最初の一つ</remarks>
        public int? GetParameterInt(string key)
        {
            try
            {
                string s = this.GetParameter(key)[0];
                return Convert.ToInt32(s);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 処理モードの確定
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
                ("ta", EAidMode.TextureAnime),
                ("im", EAidMode.ImageMerge),
                ("es", EAidMode.Structure),
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


        /// <summary>
        /// Kyeの解析
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private List<KeyValueSet> AnalyzeParam(string[] args)
        {
            //奇数でない場合 = mode + パラメータセットなので必ず偶数 よって足して奇数でないなら問題有
            if ((args.Length % 2) == 0)
            {
                return null;
            }

            //[0]=modeなので1から
            int index = 1;
            List<KeyValueSet> anslist = new List<KeyValueSet>();

            //二つの引数のsetを作成し、全て保存する
            while (args.Length > index)
            {
                KeyValueSet ans = new KeyValueSet();
                ans.Key = args[index].Trim();
                index++;
                
                ans.Value = args[index].Trim();
                index++;

                anslist.Add(ans);
            }

            return anslist;
        }

    }
}
