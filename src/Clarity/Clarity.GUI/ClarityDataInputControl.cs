using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity;

namespace Clarity.GUI
{
    /// <summary>
    /// ClarityDataを適切に入れるControl
    /// </summary>
    public partial class ClarityDataInputControl : UserControl
    {
        public ClarityDataInputControl()
        {
            InitializeComponent();
        }

        //./データまとめコントロール
        private class DataControl
        {
            public ComboBox TypeCombo = new ComboBox();
            public TextBox ValueText = new TextBox();


            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="index">データindex</param>            
            public DataControl(int index)
            {
                //開始y位置
                int start = 34;

                //offset
                int offset = 29;

                //このコントロールの表示位置を計算
                int ypos = start + (offset * index);

                this.TypeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
                this.TypeCombo.FormattingEnabled = true;
                this.TypeCombo.Location = new Point(63, ypos);
                this.TypeCombo.Name = "comboBoxType";
                this.TypeCombo.Size = new Size(114, 23);
                this.TypeCombo.TabIndex = 10;


                this.ValueText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                this.ValueText.Location = new Point(183, ypos);
                this.ValueText.Name = "textBoxValue";
                this.ValueText.Size = new Size(365, 23);
                this.ValueText.TabIndex = 12;


                //型選択肢の設定
                EClaritySettingDataType[] typevec = (EClaritySettingDataType[])Enum.GetValues(typeof(EClaritySettingDataType));
                this.TypeCombo.Items.Clear();
                foreach (var tv in typevec)
                {
                    //無効な型は排除
                    if (tv == EClaritySettingDataType.Array || tv == EClaritySettingDataType.MAX)
                    {
                        continue;
                    }

                    this.TypeCombo.Items.Add(tv);
                }
                this.TypeCombo.SelectedIndex = 0;
            }


            /// <summary>
            /// コントロールの追加
            /// </summary>
            /// <param name="con">追加場所</param>
            public void AddControl(Control con)
            {
                con.Controls.Add(this.TypeCombo);
                con.Controls.Add(this.ValueText);
            }

            /// <summary>
            /// コントロールの削除
            /// </summary>
            /// <param name="con">削除場所</param>
            public void RemoveControl(Control con)
            {
                con.Controls.Remove(this.TypeCombo);
                con.Controls.Remove(this.ValueText);
            }

            /// <summary>
            /// 入力値の取得
            /// </summary>
            /// <returns></returns>
            public (EClaritySettingDataType, string) GetInput()
            {
                //入力の取得
                EClaritySettingDataType dt = (EClaritySettingDataType)this.TypeCombo.SelectedItem;
                string value = this.ValueText.Text.Trim();

                return (dt, value);
            }

            /// <summary>
            /// データの表示
            /// </summary>
            /// <param name="type"></param>
            /// <param name="value"></param>
            public void SetData(EClaritySettingDataType type, string value)
            {
                //初期データ表示
                this.ValueText.Text = value;
                this.TypeCombo.SelectedItem = type;
            }
        }


        /// <summary>
        /// データ一覧
        /// </summary>
        private List<DataControl> DataControlList { get; init; } = new List<DataControl>();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// データの表示
        /// </summary>
        /// <param name="data">表示データ</param>
        public void DispData(ClaritySettingCoreLine data)
        {
            //既存の削除
            this.ClearControl();

            this.textBoxTagName.Text = data.TagName;
            
            //配列以外ならそのまま表示
            if(data.DataType != EClaritySettingDataType.Array)
            {
                EClaritySettingDataType dt = data.DataType;
                string val = this.GetDataString(data.Data);
                this.AddControl((dt, val));
                return;
            }

            //ここまできたら配列なのでsubtypeを取得して変換
            EClaritySettingDataType sdt = data.SubDataType;

            object[] vec = (object[])data.Data;
            foreach(object obj in vec)
            {
                string val = this.GetDataString(obj);
                this.AddControl((sdt, val));
            }

        }


        /// <summary>
        /// データの取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T? GetInputData<T>() where T : ClaritySettingCoreLine, new()
        {
            //データ入力なし
            if(this.DataControlList.Count <= 0)
            {
                return null;
            }

            //タグの取得とデフォルト定義
            string tag = this.textBoxTagName.Text.Trim();
            if(tag.Length <= 0)
            {
                tag = "tag";
            }

            //入力値の解釈クラス作成
            ClaritySettingCoreReader cr = new ClaritySettingCoreReader();
            

            //データが配列でないとき
            if (this.DataControlList.Count == 1)
            {
                var idata = this.DataControlList.First().GetInput();
                return cr.Analyze<T>(idata.Item1, idata.Item2, tag);
            }

            //入力一覧の取得
            var ilist = this.DataControlList.Select(x => x.GetInput()).ToList();

            //typeはすべて同一でないなら読み込めない
            int n = ilist.Select(x => x.Item1).Distinct().Count();
            if(n != 1)
            {
                return null;
            }
            
            var valist = ilist.Select(x => x.Item2).ToList();

            //配列の読み込み
            return cr.Analyze<T>(ilist[0].Item1, valist, tag);

        }



        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// コントロールの追加
        /// </summary>
        /// <param name="src">元ネタ</param>
        private void AddControl((EClaritySettingDataType, string)? src =null)
        {
            int index = this.DataControlList.Count;
            //作成
            DataControl con = new DataControl(index);
            if (src != null)
            {
                con.SetData(src.Value.Item1, src.Value.Item2);
            }

            //追加処理
            con.AddControl(this);
         
            //管理へ追加
            this.DataControlList.Add(con);

            
            
        }

        /// <summary>
        /// コントロールの削除
        /// </summary>
        private void RemoveControl()
        {
            //一番最後の取得
            DataControl data = this.DataControlList.Last();

            //コントロールの解除
            data.RemoveControl(this);
            this.DataControlList.Remove(data);
        }

        /// <summary>
        /// 全コントロールの削除
        /// </summary>
        private void ClearControl()
        {
            //コントロールの解除            
            this.DataControlList.ForEach(x => { x.RemoveControl(this); });
            this.DataControlList.Clear();
        }


        /// <summary>
        /// 値の文字列を取得する。arrayは対象外
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetDataString(object data)
        {
            //vectorをtostringすると<>が入るので対策
            return data.ToString()?.Replace("<", "").Replace(">", "") ?? "";
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClarityDataInputControl_Load(object sender, EventArgs e)
        {
            //初期コントロールの追加
            this.AddControl();
        }

        /// <summary>
        /// 追加ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.AddControl();
        }

        /// <summary>
        /// 削除ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            this.RemoveControl();
        }
    }
}
