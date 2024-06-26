﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity
{
    /// <summary>
    /// Singleton基底
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseClaritySingleton<T> where T : BaseClaritySingleton<T>
    {
        protected BaseClaritySingleton()
        {

        }


        /// <summary>
        /// 本体
        /// </summary>
        protected static T? Instance = null;

        public static T? Manager
        {
            get
            {
                return Instance;
            }
        }

        public static T Mana
        {
            get
            {
                if(Instance == null)
                {
                    throw new Exception($"Manager {typeof(T)} is not created.");
                }
                return Instance;
            }
        }


        public static void Clear()
        {
            Instance = null;
        }
    }

    /// <summary>
    /// 取得時にnewを行い、初期化忘れが存在しないsingleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseClarityConstSingleton<T> : BaseClaritySingleton<T> where T : BaseClarityConstSingleton<T>, new()
    {
        protected BaseClarityConstSingleton() : base()
        {
        }

        public static new T Mana
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new T();
                    Instance.Initialize();
                }
                return Instance;
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        protected virtual void Initialize()
        {

        }
    }

    /// <summary>
    /// ゲーム内資源管理クラス基底
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseClarityFactroy<T, U> : BaseClaritySingleton<T>, IDisposable where T : BaseClaritySingleton<T> where U : IDisposable
    {
        protected BaseClarityFactroy() : base()
        {

        }

        /// <summary>
        /// マイナスを予約、0は使いたくなので1から
        /// </summary>
        protected const int CustomStartIndex = 1;


        /// <summary>
        /// 管理ディクショナリ
        /// </summary>
        protected Dictionary<int, U> ManaDic = new Dictionary<int, U>();



        /// <summary>
        /// ユーザー定義データのクリア
        /// </summary>
        internal void ClearUserData()
        {
            int[] indexvec = this.ManaDic.Keys.ToArray();
            foreach (int index in indexvec)
            {
                if (index < CustomStartIndex)
                {
                    continue;
                }

                this.ManaDic[index].Dispose();
                this.ManaDic.Remove(index);
            }
        }


        /// <summary>
        /// データの開放
        /// </summary>
        /// <param name="cf">デフォルトデータのクリア可否 true=デフォルトデータまでクリアする</param>
        protected void ClearManageDic(bool cf = false)
        {
            //ユーザー定義だけクリア
            if (cf == false)
            {
                this.ClearUserData();
                return;
            }

            //完全クリア
            foreach (U mdata in this.ManaDic.Values)
            {
                mdata.Dispose();

            }
            this.ManaDic.Clear();
        }


        /// <summary>
        /// リソースの削除
        /// </summary>
        public void Dispose()
        {
            this.ClearManageDic(true);
        }
    }
}
