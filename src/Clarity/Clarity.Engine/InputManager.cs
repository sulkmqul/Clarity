using Clarity.DLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
//using Vortice.XInput;
//using Vortice.DirectInput;


namespace Clarity.Engine
{
    /// <summary>
    /// 使用するキーの定義
    /// </summary>
    public class GameKey
    {
        public const int Up = 1 << 0;
        public const int Down = 1 << 1;
        public const int Left = 1 << 2;
        public const int Right = 1 << 3;

        public const int Button1 = 1 << 4;
        public const int Button2 = 1 << 5;
        public const int Button3 = 1 << 6;
        public const int Button4 = 1 << 7;
        public const int Button5 = 1 << 8;
        public const int Button6 = 1 << 9;
        public const int Button7 = 1 << 10;


        //--------------------------------------------------------
        //同時押し関連

        //全ボタン
        public const int AllButton = Button1 | Button2 | Button3 | Button4 | Button5 | Button6 | Button7;

        public const int LeftRightButton = Left | Right;
        public const int UpDownButton = Left | Up;
        public const int LeftUpButton = Left | Up;
        public const int RightDownButton = Right | Down;


        public const int AllMoveButton = Left | Right | Up | Down;
    }



    /// <summary>
    /// 入力種別
    /// </summary>
    enum EInputManageType
    {
        Keyboard,
        GamePadXInput,
        GamePadDInput,
    }

    /// <summary>
    /// 入力情報設定ファイル管理
    /// </summary>
    class InputSettingFile
    {
        /// <summary>
        /// ファイルの読み込み
        /// </summary>
        /// <returns></returns>
        public InputManageData ReadInputSetting(string filepath)
        {
            InputManageData ans = null;

            try
            {
                using (FileStream fp = new FileStream(filepath, FileMode.Open))
                {
                    ans = this.ReadInputSetting(fp);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ans;

        }

        /// <summary>
        /// 入力設定情報の読み込み
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public InputManageData ReadInputSetting(Stream st)
        {
            InputManageData ans = null;
            try
            {
                XElement xml = XElement.Load(st);                
                ans = this.ReadSetting(xml);
            }
            catch (Exception ex)
            {
                throw new Exception("InputSettingFile ReadInputSetting", ex);
            }

            return ans;
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 入力設定の読み込み
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private InputManageData ReadSetting(XElement xml)
        {
            InputManageData ans = new InputManageData();
            var iset = xml.Elements("InputSet");

            //必要な入力情報を取得
            //Keyboard
            {
                XElement elem = iset.Where(x => x.Attribute("type").Value == EInputManageType.Keyboard.ToString()).FirstOrDefault();
                if (elem != null)
                {
                    ans.KeyboardDic = this.ReadKeyboardSetting(elem);
                }
            }
            //Pad入力なども随時追加せよ！

            return ans;
       }

        /// <summary>
        /// キーボード設定の読み込み
        /// </summary>
        /// <returns>[vk_key, gamekey]</returns>
        private Dictionary<int, int> ReadKeyboardSetting(XElement xml)
        {
            Dictionary<int, int> ans = new Dictionary<int, int>();

            Type gtype = typeof(GameKey);
            //int a = (int)t.GetField("Button1").GetValue(null);

            Type vktype = typeof(DLL.VirtualKeyCode);

            xml.Elements("button").ToList().ForEach(x =>
            {
                //VirtualKey名
                string svkey = x.Attribute("srckey").Value;
                int vkkey = (int)vktype.GetField(svkey).GetValue(null);

                //対応キー
                string sgkey = x.Attribute("key").Value;
                int gkey = (int)gtype.GetField(sgkey).GetValue(null);

                ans.Add(vkkey, gkey);
            });

            return ans;
        }
    }



    /// <summary>
    /// 入力情報管理データ
    /// </summary>
    class InputManageData
    {
        /// <summary>
        /// キーボード入力チェックデータ {VK_Key, GameKey}のセット
        /// </summary>
        public Dictionary<int, int> KeyboardDic = null;

        /// <summary>
        /// ゲームパッド {VK_Key, GameKey}のセット
        /// </summary>
        //public Dictionary<GamepadButtons, int> GamePadDic = null;
    }

    /// <summary>
    /// 入力管理クラス
    /// </summary>
    internal class InputManager : BaseClaritySingleton<InputManager>
    {
        private InputManager()
        {
            
            
        }

        /// <summary>
        /// 入力管理の作成
        /// </summary>
        public static void Create()
        {
            Instance = new InputManager();

        }

        /// <summary>
        /// 入力情報設定
        /// </summary>
        private InputManageData InputData = new InputManageData();

        /// <summary>
        /// GameKeyの値でandを取る
        /// </summary>
        public int KeyFlag { get; private set; } = 0;

        /// <summary>
        /// キーのエッジ
        /// </summary>
        public int KeyEdgeFlag { get; private set; } = 0;

        /// <summary>
        /// キーの離したエッジ
        /// </summary>
        public int KeyReleaseEdgeFlag { get; private set; } = 0;


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 入力情報の取得
        /// </summary>
        public void GetInput()
        {
            //以前の情報をほぞの
            int oldkey = this.KeyFlag;

            //初期化
            this.KeyFlag = 0;

            //キーボード入力の取得
            int kbinput = this.GetKeyboardInput();

            //パッド入力の取得
            int padinput = 0;
            //int padinput = this.GetGamePadInput();

            //両方の入力を有効にしておく
            this.KeyFlag = kbinput | padinput;

            //キーエッジ入力の取得
            this.KeyEdgeFlag = this.KeyFlag & ~oldkey;
            //KeyFlag = NowKey & ~OldKey;

            //キー離したエッジの取得
            this.KeyReleaseEdgeFlag = (~this.KeyFlag) & oldkey;


            //表示
            //ClarityLog.WriteDebug($"key={this.KeyFlag} edge={this.KeyEdgeFlag} release={this.KeyReleaseEdgeFlag}");

            //----------------------------------------------------------------------------------------
            //XInput入力はこれで行けるが、XIput対応していないため詳細不明
            //State keystate = new State();
            //XInput.GetState(0, out keystate);
            //ClarityLog.WriteDebug($"{keystate.Gamepad}");
        }

        /// <summary>
        /// 入力設定情報の読み込み
        /// </summary>
        /// <param name="filepath">読み込み入力設定ファイルパス</param>
        public void ReadInputSetting(string filepath)
        {
            InputSettingFile fp = new InputSettingFile();
            this.InputData = fp.ReadInputSetting(filepath);
        }

        /// <summary>
        /// 入力設定情報の読み込み
        /// </summary>
        /// <param name="st"></param>
        internal void ReadInputSettingStream(Stream st)
        {
            InputSettingFile fp = new InputSettingFile();
            this.InputData = fp.ReadInputSetting(st);
        }


        /// <summary>
        /// 対象の入力チェック
        /// </summary>
        /// <param name="gamekey">GameKeyの値を設定する</param>
        /// <returns></returns>
        public static bool TestKey(int gamekey)
        {
            if ((InputManager.Mana.KeyFlag & gamekey) != 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 対象の入力エッジチェック
        /// </summary>
        /// <param name="gamekey">GameKeyの値を設定する</param>
        /// <returns></returns>
        public static bool TestKeyEdge(int gamekey)
        {
            if ((InputManager.Mana.KeyEdgeFlag & gamekey) != 0)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 対象の離したエッジチェック
        /// </summary>
        /// <param name="gamekey">GameKeyの値を設定する</param>
        /// <returns></returns>
        public static bool TestKeyReleaseEdge(int gamekey)
        {
            if ((InputManager.Mana.KeyReleaseEdgeFlag & gamekey) != 0)
            {
                return true;
            }

            return false;
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// キーボード入力の取得
        /// </summary>
        /// <returns></returns>
        private int GetKeyboardInput()
        {
            //キーボード入力する？
            if (this.InputData.KeyboardDic == null)
            {
                return 0;
            }

            var keydic = this.InputData.KeyboardDic;

            int ans = 0;
            //全データのチェック
            foreach (int vkey in keydic.Keys)
            {
                //入力チェック
                short ret = User32.GetAsyncKeyState(vkey);

                //押されてる？
                if ((ret & 0x8000) != 0)
                {
                    ans |= keydic[vkey];
                }
            }

            return ans;
        }

    }
}
