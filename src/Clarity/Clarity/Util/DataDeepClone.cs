using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Clarity
{
    public static class DataDeepClone
    {
        /// <summary>
        /// 拡張メソッド　ディープコピーつまり参照オブジェクトまで丸ごとのコピーを作成する
        /// <remarks>これでコピーするクラスには[Serializable]をつけること</remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src">元</param>
        /// <returns>クローンした物体</returns>
        public static T DeepClone<T>(this T src)
        {
            object ans = null;

            using (MemoryStream mst = new MemoryStream())
            {
                /*
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(mst, src);
                mst.Position = 0;
                ans = bf.Deserialize(mst);*/

                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(mst, src);

                mst.Position = 0;
                ans = xs.Deserialize(mst);

                
            }

            return (T)ans;
        }
    }
}
