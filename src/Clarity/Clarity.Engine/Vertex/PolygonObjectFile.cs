using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.File;
using System.Numerics;
using Vortice.Mathematics;

namespace Clarity.Engine.Vertex
{
    

    /// <summary>
    /// ポリゴン情報
    /// </summary>
    internal class PolyData
    {
        /// <summary>
        /// 頂点一覧
        /// </summary>
        public List<VertexInfo> VertexList;

        /// <summary>
        /// Index一覧 3で区切ります
        /// </summary>
        public List<int> IndexList;

        

    }




    /// <summary>
    /// ポリゴンオブジェクトファイル管理クラス
    /// </summary>
    internal class PolygonObjectFile : BaseCsvFile
    {
        /// <summary>
        /// 頂点形式
        /// </summary>
        private enum EVertexFormat
        {
            Vetex,
            Color,
            Texture
        }
        /// <summary>
        /// 頂点形式情報
        /// </summary>
        private class VertexFileFormat
        {
            /// <summary>
            /// 形式
            /// </summary>
            public EVertexFormat Format;
            /// <summary>
            /// 数
            /// </summary>
            public int Count;
        }

        /// <summary>
        /// 頂点形式の読み込み
        /// </summary>
        /// <param name="sdata">フォーマット文字列</param>
        /// <returns></returns>
        private List<VertexFileFormat> ReadVertexFormat(string sdata)
        {
            List<VertexFileFormat> anslist = new List<VertexFileFormat>();

            (char, EVertexFormat)[] formattempvec = {
                ('V', EVertexFormat.Vetex),
                ('C', EVertexFormat.Color),
                ('T', EVertexFormat.Texture),
            };
            
            int pos = 0;
            while (sdata.Length > pos)
            {
                char cf = sdata[pos++];
                int length = Convert.ToInt32(sdata[pos++].ToString());

                //対象のフォーマットを抜き出し
                var n = from f in formattempvec where f.Item1 == cf select f;
                if (n.Count() <= 0)
                {
                    throw new FormatException("Load Polygon Object File Format Exception");
                }

                //成功したら設定
                var sel = n.First();
                VertexFileFormat ans = new VertexFileFormat() { Format = sel.Item2, Count = length };
                anslist.Add(ans);
            }

            return anslist;
        }


        /// <summary>
        /// 頂点情報一行の読み込み
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private VertexInfo ReadVertexInfoLine(string[] data)
        {
            VertexInfo ans = new VertexInfo();

            int p = 0;

            //頂点位置
            ans.Pos = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            for (int i = 0; i < 3; i++)
            {
                //ans.Pos[i] = Convert.ToSingle(data[p]);
                ans.Pos.SetIndex(i, Convert.ToSingle(data[p]));
                p++;
            }
            //色
            ans.Col = new Color4(1.0f, 1.0f, 1.0f, 1.0f);

            //テクスチャ
            ans.Tex = new Vector2(0.0f, 0.0f);
            for (int i = 0; i < 2; i++)
            {
                //ans.Tex[i] = Convert.ToSingle(data[p]);
                ans.Tex.SetIndex(i, Convert.ToSingle(data[p]));
                p++;
            }

            return ans;
        }


        /// <summary>
        /// フォーマットに沿った一つ分の頂点情報読み込み
        /// </summary>
        /// <param name="formatlist">フォーマット情報</param>
        /// <param name="data">読み込みデータ</param>
        /// <returns></returns>
        private VertexInfo ReadVertexInfoLine(List<VertexFileFormat> formatlist, string[] data)
        {
            VertexInfo ans = new VertexInfo();
            //初期化
            ans.Pos = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            ans.Col = new Color4(1.0f);
            ans.Tex = new Vector2(0.0f);


            //全行変換
            List<float> flist = new List<float>();
            foreach (string fsd in data)
            {
                flist.Add(Convert.ToSingle(fsd));
            }

            //Formatに沿って分配・・・あまり良いコードではないが力技で
            int rpos = 0;
            formatlist.ForEach(x =>
            {
                //今回のフォーマット数で・・・・indexアクセスが難しいのでちょっとわかりにくい真似
                switch (x.Format)
                {
                    case EVertexFormat.Vetex:
                        for (int i = 0; i < x.Count; i++)
                        {
                            ans.Pos.SetIndex(i, flist[rpos++]);
                        }
                        break;
                    case EVertexFormat.Color:
                        {
                            float[] colvec = { 1.0f, 1.0f, 1.0f, 1.0f };
                            for (int i = 0; i < x.Count; i++)
                            {
                                colvec[i] = flist[rpos++];
                            }
                            ans.Col = new Color4(colvec[0], colvec[1], colvec[2], colvec[3]);
                        }
                        break;
                    case EVertexFormat.Texture:
                        for (int i = 0; i < x.Count; i++)
                        {
                            ans.Tex.SetIndex(i, flist[rpos++]);
                        }
                        break;
                }

            });

            return ans;
        }


        /// <summary>
        /// 頂点情報の読み取り
        /// </summary>
        /// <param name="pos">読み込み位置</param>
        /// <returns></returns>
        private List<VertexInfo> ReadVertex(List<string[]> datalist, ref int pos)
        {
            List<VertexInfo> anslist = new List<VertexInfo>();

            //形式情報の読み込み
            List<VertexFileFormat> formatlist = this.ReadVertexFormat(datalist[pos][0]);
            pos++;

            //サイズ
            int size = Convert.ToInt32(datalist[pos][0]);
            pos++;
            //全データ読み込み
            for (int i = 0; i < size; i++)
            {
                VertexInfo vinfo = this.ReadVertexInfoLine(formatlist, datalist[pos]);

                anslist.Add(vinfo);
                pos++;
            }

            return anslist;
        }


        /// <summary>
        /// Indexデータの読み取り
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private List<int> ReadIndex(List<string[]> datalist, ref int pos)
        {
            List<int> anslist = new List<int>();

            //サイズ
            int size = Convert.ToInt32(datalist[pos][0]);
            pos++;
            //全データ読み込み
            for (int i = 0; i < size; i++)
            {
                //一行の解析
                string[] line = datalist[pos];

                //for (int pp = 0; pp < 3; pp++)
                for (int pp = 0; pp < line.Length; pp++)
                {
                    int n = Convert.ToInt32(line[pp]);
                    anslist.Add(n);
                }

                pos++;
            }

            return anslist;
        }




        /// <summary>
        /// データ読み込み本体
        /// </summary>
        /// <param name="csvdatalist"></param>
        /// <returns></returns>
        private PolyData LoadPolyCsv(List<string[]> csvdatalist)
        {
            PolyData ans = new PolyData();

            int pos = 0;

            //頂点情報
            ans.VertexList = this.ReadVertex(csvdatalist, ref pos);

            //index情報
            ans.IndexList = this.ReadIndex(csvdatalist, ref pos);

            return ans;
        }



        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 一覧ファイルの読み込み
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public PolyData ReadFile(string filepath)
        {
            //csvの読み込み
            List<string[]> datalist = this.ReadCsvFile(filepath);

            PolyData ans = this.LoadPolyCsv(datalist);

            return ans;
        }


        /// <summary>
        /// CSV文字列の読み込み
        /// </summary>
        /// <param name="csv"></param>
        /// <returns></returns>
        internal PolyData ReadCsvString(string csv)
        {
            List<string[]> datalist = new List<string[]>();

            //csvの読み込み
            using (System.IO.MemoryStream mst = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(csv)))
            {
                datalist = this.ReadCsvStream(mst);
            }

            PolyData ans = this.LoadPolyCsv(datalist);

            return ans;
        }

    }
}
