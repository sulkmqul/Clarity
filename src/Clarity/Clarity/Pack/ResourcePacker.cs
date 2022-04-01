using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Pack
{
    /// <summary>
    /// パックされるファイル情報
    /// </summary>
    class PackDataInfo
    {
        /// <summary>
        /// 名前(識別名)
        /// </summary>
        public string Name;
        /// <summary>
        /// ファイルサイズ
        /// </summary>
        public long Size;

        /// <summary>
        /// 元ネタファイルパス
        /// </summary>
        public string SrcPath;
    }

    /// <summary>
    /// パックファイルのヘッダー情報
    /// </summary>
    /// <remarks>
    /// パックファイルの仕様
    /// 先頭からint(4byte):データ数
    /// [256byte文字列(UTF8)、8byteでlongファイルサイズ]、これのPackFile情報セットがデータ数分
    /// 残りは上記の順番にファイルの実データが並ぶ
    /// </remarks>
    class PackDataHeader
    {
        /// <summary>
        /// パックファイル数
        /// </summary>
        public int Count;

        /// <summary>
        /// パックファイル情報
        /// </summary>
        public List<PackDataInfo> FileHeader;
    }


    /// <summary>
    /// パックされたデータ一式
    /// </summary>
    public class PackData
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string Name;
        /// <summary>
        /// ファイルサイズ
        /// </summary>
        public long FileSize;
        /// <summary>
        /// ファイルデータ一式(MermoryStreamにでも入れてあとはお好きに)
        /// </summary>
        public byte[] FileData;
    }


    /// <summary>
    /// ファイルを一つにまとめる
    /// </summary>
    public class ResourcePacker
    {
        /// <summary>
        /// 名前のサイズ
        /// </summary>
        const int NameSize = 256;

        /// <summary>
        /// ファイルのパッキング
        /// </summary>
        /// <param name="exportpath">出力ファイルパス</param>
        /// <param name="pathlist">結合物一覧</param>
        public void PackingResource(string exportpath, List<string> pathlist)
        {
            try
            {
                PackDataHeader pdata = this.CreatePackList(pathlist);

                using (FileStream fp = new FileStream(exportpath, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fp))
                    {
                        this.WritePackData(pdata, bw);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ResourcePacker.PackingResource", ex);
            }
        }


        /// <summary>
        /// ファイルの解凍
        /// </summary>
        /// <param name="filepath">読み込みpackファイルパス</param>
        /// <returns></returns>
        public List<PackData> UnPackingResource(string filepath)
        {
            List<PackData> anslist = new List<PackData>();

            using (FileStream fp = new FileStream(filepath, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fp))
                {
                    //ヘッダーの読み込み
                    List<PackDataInfo> hlist = this.ReadHeader(br);

                    //ファイルデータの読み込み
                    anslist = this.ReadPackData(br, hlist);
                }
            }



            return anslist;
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 書き込み用データの作成
        /// </summary>
        /// <param name="pathlist"></param>
        /// <returns></returns>
        private PackDataHeader CreatePackList(List<string> pathlist)
        {
            PackDataHeader ans = new PackDataHeader();

            ans.Count = pathlist.Count;

            ans.FileHeader = new List<PackDataInfo>();
            pathlist.ForEach(x =>
            {
                PackDataInfo hd = new PackDataInfo();
                hd.Name = Path.GetFileName(x);
                using (FileStream fp = new FileStream(x, FileMode.Open))
                {
                    hd.Size = fp.Length;
                }
                hd.SrcPath = x;

                ans.FileHeader.Add(hd);
            });

            return ans;
        }


        /// <summary>
        /// pack書き込み本体
        /// </summary>
        /// <param name="pdata"></param>
        /// <param name="bw"></param>
        private void WritePackData(PackDataHeader pdata, BinaryWriter bw)
        {
            //数の書き込み
            bw.Write(pdata.Count);

            //ヘッダーの書き込み
            pdata.FileHeader.ForEach(x =>
            {
                //名前
                byte[] buf = new byte[NameSize];
                Span<byte> sp = new Span<byte>(buf);
                Encoding.UTF8.GetBytes(x.Name, sp);
                bw.Write(sp);

                //サイズ
                bw.Write(x.Size);
            });

            //ファイルデータの本体の書き込み
            pdata.FileHeader.ForEach(x =>
            {
                using (FileStream fp = new FileStream(x.SrcPath, FileMode.Open))
                {
                    //ファイルの中身を一括読み込み
                    byte[] filebuf = new byte[fp.Length];
                    Span<byte> sp = new Span<byte>(filebuf);
                    fp.Read(sp);

                    //書き込み
                    bw.Write(sp);
                }
            });



        }

        /// <summary>
        /// ヘッダーの読み込み
        /// </summary>
        /// <param name="bw"></param>
        /// <returns></returns>
        private List<PackDataInfo> ReadHeader(BinaryReader br)
        {
            List<PackDataInfo> anslist = new List<PackDataInfo>();

            //データ数確定
            int datacount = br.ReadInt32();

            for (int i = 0; i < datacount; i++)
            {
                PackDataInfo ans = new PackDataInfo();

                //名前の読み込み                
                byte[] buf = new byte[NameSize];
                Span<byte> sp = new Span<byte>(buf);
                br.Read(sp);

                ans.Name = Encoding.UTF8.GetString(sp);

                //サイズ
                ans.Size = br.ReadInt64();

                //----
                anslist.Add(ans);
            }

            return anslist;
        }

        /// <summary>
        /// Packの読み込み
        /// </summary>
        /// <param name="datalist"></param>
        /// <returns></returns>
        private List<PackData> ReadPackData(BinaryReader br, List<PackDataInfo> datalist)
        {
            List<PackData> anslist = new List<PackData>();

            datalist.ForEach(x =>
            {
                PackData ans = new PackData();
                ans.Name = x.Name;
                ans.FileSize = x.Size;
                ans.FileData = new byte[x.Size];
                Span<byte> sp = new Span<byte>(ans.FileData);
                br.Read(sp);

                anslist.Add(ans);

            });

            return anslist;
        }
    }
}
