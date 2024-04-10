using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity
{
    /// <summary>
    /// アプリケーション構成ファイル基底クラス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseAppConfigManager<T> : BaseClarityConstSingleton<T> where T : BaseAppConfigManager<T>, new ()
    {
        #region 変換関数
        /// <summary>
        /// 設定値をboolで取得
        /// </summary>
        /// <param name="key">取得AppCinfigのKey</param>
        /// <param name="def">デフォルト値</param>
        /// <returns></returns>
        protected bool GetBool(string key, bool def = false)
        {
            bool ans = true;

            try
            {
                //値を取得して変換                
                string s = ConfigurationManager.AppSettings[key] ?? "";
                int n = Convert.ToInt32(s);
                if (n == 0)
                {
                    ans = false;
                }
            }
            catch (Exception ex)
            {
                return def;
            }


            return ans;
        }

        /// <summary>
        /// 設定値をintで取得
        /// </summary>
        /// <param name="key">取得AppCinfigのKey</param>
        /// <param name="def">デフォルト値</param>
        /// <returns></returns>
        protected int GetInt(string key, int def = 0)
        {
            int ans = def;

            try
            {
                //値を取得して変換
                string s = ConfigurationManager.AppSettings[key] ?? "";
                ans = Convert.ToInt32(s);
            }
            catch (Exception ex)
            {
                return def;
            }
            return ans;
        }

        /// <summary>
        /// 設定値を文字列で取得
        /// </summary>
        /// <param name="key">取得AppCinfigのKey</param>
        /// <param name="def">デフォルト値</param>
        /// <returns></returns>
        protected string GetString(string key, string def = "")
        {
            string? ans = def;

            try
            {
                ans = ConfigurationManager.AppSettings[key];
                if (ans == null)
                {
                    return def;
                }

            }
            catch (Exception ex)
            {
                return def;
            }

            return ans;
        }


        /// <summary>
        /// Double値の取得
        /// </summary>
        /// <returns>The double.</returns>
        /// <param name="key">Key.</param>
        /// <param name="def">Def.</param>
        protected double GetDouble(string key, double def = 0.0)
        {
            double ans = def;

            try
            {
                //値を取得して変換
                string s = ConfigurationManager.AppSettings[key] ?? "";
                ans = Convert.ToDouble(s);
            }
            catch (Exception ex)
            {
                return def;
            }
            return ans;
        }


        /// <summary>
        /// float値の取得
        /// </summary>
        /// <returns>The double.</returns>
        /// <param name="key">Key.</param>
        /// <param name="def">Def.</param>
        protected float GetFloat(string key, float def = 0.0f)
        {
            float ans = def;

            try
            {
                //値を取得して変換
                string s = ConfigurationManager.AppSettings[key] ?? "";
                ans = Convert.ToSingle(s);
            }
            catch (Exception ex)
            {
                return def;
            }
            return ans;
        }
        #endregion
    }
}
