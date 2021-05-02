using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.DirectInput;
using Clarity.DLL;

namespace Clarity
{
    /// <summary>
    /// キーの定義
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
    /// 入力管理
    /// </summary>
    internal class InputManager : BaseClaritySingleton<InputManager>
    {
        private InputManager()
        {
        }

        public static void Create()
        {
            Instance = new InputManager();
            Instance.Init();
        }


        #region メンバ変数        
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

        /// <summary>
        /// キーボード入力チェックデータ {VK_Key, GameKey}のセット
        /// </summary>
        private Dictionary<int, int> KeyboardDic = null;


        #region GamePad用
        /// <summary>
        /// DirectInputクラス
        /// </summary>
        private DirectInput DXInput = null;

        /// <summary>
        /// 入力機器一覧
        /// </summary>
        private List<DeviceInstance> InputDeviceList = new List<DeviceInstance>();

        /// <summary>
        /// Pad入力用 これがnullならPAD入力なし。
        /// </summary>
        private Joystick GamePad = null;

        /// <summary>
        /// PAD入力チェックデータ {JoystickOffset, GameKey}のセット 
        /// </summary>
        private Dictionary<JoystickOffset, int> GamePadButtonDic = null;

        /// <summary>
        /// ゲームパッドの現在の状態 trueで押している DirectInputはON/OFFの値しか来ないため。
        /// {GameKey, bool}
        /// </summary>
        private Dictionary<int, bool> GamePadStateDic = null;

        /// <summary>
        /// DirectInputでStickが離されている（中心位置）の値 これより大きいor小さいで押されている。
        /// </summary>
        private const int StrickCenterValue = 32767;

        /// <summary>
        /// キーの遊び。これ以上で反応する
        /// </summary>
        private const int StickPlay = 10000;
        #endregion
        #endregion



        /// <summary>
        /// キーボードの初期化
        /// </summary>
        private void InitKeybord()
        {
            //入力チェックデータの作製
            this.KeyboardDic = new Dictionary<int, int>();

            //ここはファイルでの初期化が望ましい。           

            //矢印
            this.KeyboardDic.Add(User32.VK_LEFT, GameKey.Left);
            this.KeyboardDic.Add(User32.VK_UP, GameKey.Up);
            this.KeyboardDic.Add(User32.VK_RIGHT, GameKey.Right);
            this.KeyboardDic.Add(User32.VK_DOWN, GameKey.Down);

            //ボタン
            this.KeyboardDic.Add(User32.VK_Z, GameKey.Button1);
            this.KeyboardDic.Add(User32.VK_X, GameKey.Button2);
            this.KeyboardDic.Add(User32.VK_C, GameKey.Button3);
            this.KeyboardDic.Add(User32.VK_V, GameKey.Button4);


            this.KeyboardDic.Add(User32.VK_Q, GameKey.Button7);
        }






        /// <summary>
        /// DirectInputの初期化
        /// </summary>
        private void InitGamePad()
        {
            //初期化
            this.DXInput = new DirectInput();
            this.GamePad = null;
            this.InputDeviceList = new List<DeviceInstance>();

            #region ゲームPAD入力情報の作成
            this.GamePadStateDic = new Dictionary<int, bool>();
            this.GamePadButtonDic = new Dictionary<JoystickOffset, int>();


            //ボタンの値
            this.GamePadButtonDic.Add(JoystickOffset.Buttons0, GameKey.Button1);
            this.GamePadButtonDic.Add(JoystickOffset.Buttons1, GameKey.Button2);
            this.GamePadButtonDic.Add(JoystickOffset.Buttons2, GameKey.Button3);
            #endregion


            //GamePadの検索            
            foreach (DeviceInstance dins in this.DXInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            {
                //一覧へADD
                this.InputDeviceList.Add(dins);
            }
            //JoyStrickの検索
            foreach (DeviceInstance dins in this.DXInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
            {
                this.InputDeviceList.Add(dins);
            }

            //入力機器がなかった
            if (this.InputDeviceList.Count <= 0)
            {
                return;
            }


            //とりあえず適当に一つめのデバイスで作成する
            this.GamePad = new Joystick(this.DXInput, this.InputDeviceList[0].InstanceGuid);

            var effectlist = this.GamePad.GetEffects();

            this.GamePad.Properties.BufferSize = 128;
            //アクセス
            this.GamePad.Acquire();


        }


        /// <summary>
        /// 初期化関数
        /// </summary>
        /// <returns></returns>
        private bool Init()
        {
            //キーボード入力初期化
            this.InitKeybord();


            //PAD入力の初期化
            this.InitGamePad();

            return true;
        }




        /// <summary>
        /// キーボード入力の取得
        /// </summary>
        /// <returns></returns>
        private int GetKeyboardInput()
        {
            int ans = 0;

            //全データのチェック
            foreach (int vkey in this.KeyboardDic.Keys)
            {
                //入力チェック
                short ret = User32.GetAsyncKeyState(vkey);

                //押されてる？
                if ((ret & 0x8000) != 0)
                {
                    ans |= this.KeyboardDic[vkey];
                }
            }

            return ans;
        }



        /// <summary>
        /// 矢印or3Dスティックの取得
        /// </summary>
        /// <param name="updata"></param>
        private void ProcGamePadArrowStick(JoystickUpdate updata)
        {
            //キー状態を取得
            int vec = updata.Value - StrickCenterValue;

            //Console.WriteLine(updata);
            //Console.WriteLine(vec);

            //矢印左右 or Stick
            if (updata.Offset == JoystickOffset.X)
            {
                if (vec > StickPlay)
                {
                    this.GamePadStateDic[GameKey.Right] = true;
                    return;
                }
                if (vec < -StickPlay)
                {
                    this.GamePadStateDic[GameKey.Left] = true;
                    return;
                }

                //ここまできたら、どちらも有効ではない。
                this.GamePadStateDic[GameKey.Left] = false;
                this.GamePadStateDic[GameKey.Right] = false;
            }


            //矢印上下 or Stick
            if (updata.Offset == JoystickOffset.Y)
            {
                if (vec > StickPlay)
                {
                    this.GamePadStateDic[GameKey.Down] = true;
                    return;
                }
                if (vec < -StickPlay)
                {
                    this.GamePadStateDic[GameKey.Up] = true;
                    return;
                }

                //ここまできたら、どちらも有効ではない。
                this.GamePadStateDic[GameKey.Up] = false;
                this.GamePadStateDic[GameKey.Down] = false;
            }
        }


        /// <summary>
        /// GamePadボタンの入力判定
        /// </summary>
        /// <param name="updata">チェック対象</param>
        private void ProcGamePadButton(JoystickUpdate updata)
        {
            //これは処理すべきもの？
            bool ret = this.GamePadButtonDic.ContainsKey(updata.Offset);
            if (ret == false)
            {
                return;
            }

            //GameKeyの取得
            int key = this.GamePadButtonDic[updata.Offset];

            //押した、離した情報取得
            bool flag = true;
            if (updata.Value == 0)
            {
                flag = false;
            }

            //State更新
            this.GamePadStateDic[key] = flag;

        }

        /// <summary>
        /// ゲームパッド入力の取得
        /// </summary>
        /// <returns></returns>
        private int GetGamePadInput()
        {
            int ans = 0;
            if (this.GamePad == null)
            {
                return ans;
            }

            this.GamePad.Poll();

            //結論から言ってゲームパッド、十字キー（とスティック）は真ん中（なにもなし）が32767。
            //これより値が大きいなら右or下 小さいなら左or上 多少どころじゃないぐらいには遊びの値を設けるべし

            //キーは128で押されている。
            //0で押されていない。0チェックでＯＫ

            //更新の取得
            JoystickUpdate[] updatavec = this.GamePad.GetBufferedData();
            foreach (JoystickUpdate updata in updatavec)
            {
                //矢印＆スティック入力処理
                this.ProcGamePadArrowStick(updata);

                //ボタン入力処理
                this.ProcGamePadButton(updata);

            }


            //最後にStateDicから今押されているものを作成する
            foreach (int key in this.GamePadStateDic.Keys)
            {
                bool flag = this.GamePadStateDic[key];
                if (flag == true)
                {
                    ans |= key;
                }
            }


            return ans;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 入力の取得
        /// </summary>
        public void GetInput()
        {
            int oldkey = this.KeyFlag;

            //初期化
            this.KeyFlag = 0;

            //キーボード入力の取得
            int kbinput = this.GetKeyboardInput();

            //パッド入力の取得
            int padinput = this.GetGamePadInput();


            //両方の入力を有効にしておく
            this.KeyFlag = kbinput | padinput;

            //キーエッジ入力の取得・・・この計算で合ってる？
            this.KeyEdgeFlag = this.KeyFlag & ~oldkey;
            //KeyFlag = NowKey & ~OldKey;

            //キー離したエッジの取得
            this.KeyReleaseEdgeFlag = (~this.KeyFlag) & oldkey;
        }


        /// <summary>
        /// 対象の入力チェック
        /// </summary>
        /// <param name="gamekey">GameKeyの値を設定する</param>
        /// <returns></returns>
        public static bool TestKey(int gamekey)
        {
            if ((InputManager.Mana.KeyFlag & gamekey) == gamekey)
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
            if ((InputManager.Mana.KeyEdgeFlag & gamekey) == gamekey)
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
            if ((InputManager.Mana.KeyReleaseEdgeFlag & gamekey) == gamekey)
            {
                return true;
            }

            return false;
        }



        /// <summary>
        /// 開放
        /// </summary>
        public void Dispose()
        {
            //GamePad
            this.GamePad?.Dispose();

            //DirectInput
            this.DXInput?.Dispose();


        }

    }
}
