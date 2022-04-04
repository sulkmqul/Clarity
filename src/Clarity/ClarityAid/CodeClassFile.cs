using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClarityAid
{
    public class IdData
    {
        public IdData(string name, int id)
        {
            this.IDName = name;
            this.Id = id;
        }

        /// <summary>
        /// ID名
        /// </summary>
        public string IDName = "";

        /// <summary>
        /// ID値
        /// </summary>
        public int Id = Clarity.Engine.ClarityEngine.INVALID_ID;
    }

    /// <summary>
    /// 出力クラスファイルパス
    /// </summary>
    public class CodeClassFile
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="cf">true=class false=enum</param>
        public CodeClassFile(bool cf)
        {
            this.ClassFlag = cf;
        }

        /// <summary>
        /// クラス出力可否 true=Class false=Enum
        /// </summary>
        bool ClassFlag = false;

        private string MemberTextClass = "\t\tpublic static readonly int {0} = {1};";
        private string MemberTextEnum = "\t\t{0} = {1},";

        private string MemberText
        {
            get
            {
                if (this.ClassFlag == true)
                {
                    return this.MemberTextClass;
                }
                return this.MemberTextEnum;

            }
        }

        private string SrcCode
        {
            get
            {
                if (this.ClassFlag == true)
                {
                    return Properties.Resources.ClassCode;
                }
                return Properties.Resources.EnumCode;
            }
        }


        static readonly string NewLineCode = "___NNDDLL__";

        /// <summary>
        /// 全IDを生成する
        /// </summary>
        /// <param name="datalist">書き出し変数名一覧</param>
        /// <returns></returns>
        private string GenerateIDString(List<IdData> datalist)
        {
            string ans = "";


            //とりあえず一つの文字列として生成
            foreach (IdData data in datalist)
            {
                string s = string.Format(this.MemberText, data.IDName, data.Id);
                ans += s;
                ans += CodeClassFile.NewLineCode;
            }

            return ans;
        }




        /// <summary>
        /// 書き込み
        /// </summary>
        /// <param name="filepath">書き込みファイルパス</param>
        /// <param name="classname">出力クラス名</param>
        /// <param name="datalist">出力名一式</param>
        public void Write(EAidMode mode, string filepath, string classname, List<IdData> datalist, string comment = "")
        {
            try
            {
                string src = this.SrcCode;

                //全ID文字列の作成
                string ids = this.GenerateIDString(datalist);

                //書き込み文字列の作成         
                //クラス名
                string ws = src.Replace("__CLASS_NAME__", classname);
                //コメント説明
                ws = ws.Replace("__CLASS_DESC__", comment);
                //全ID
                ws = ws.Replace("__MEMBER_CODE__", ids);

                //便利Funcの変更
                string aidfunc = AidFuncManage.GetAidFunc(mode);
                if (this.ClassFlag == true)
                {
                    aidfunc = "";   //Class出力の場合は数値なので変換自体が不要
                }
                ws = ws.Replace("__AID_FUNC__", aidfunc);

                //最後に改行を埋め込む
                ws = ws.Replace(CodeClassFile.NewLineCode, Environment.NewLine);

                //ファイルOpen
                using (FileStream fp = new FileStream(filepath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fp, System.Text.Encoding.UTF8))
                    {
                        sw.Write(ws);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("WriteText失敗", e);
            }
        }
    }


}


