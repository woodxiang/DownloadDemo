using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DownloadClient.DownloadServiceReference;

namespace DownloadClient
{
    class Program
    {
        static void Main(string[] args)
        {
            DownloadServiceClient client = new DownloadServiceClient("NetTcpBinding_IDownloadService");
            string path = "2_Whole Core Data\\丁页1HF井 DY-1HF Scanning\\DY3_2_2_94cm\\筛选数据\\DY3_2_2_94cm_512x512x1512_0p4947x0p4947x0p625_100kv_16bit_signed.raw";
            long fileSize = client.GetFileSize(path);

            int step = 32 * 1024;
            long startIndex = 0;

            var binding = client.Endpoint.Binding as NetTcpBinding;
            long maxSize = binding.MaxReceivedMessageSize;

            byte[] buffer = new byte[step];

            Stopwatch sw = new Stopwatch();

            const int timeElapsed = 1000;

            using (Stream t = File.OpenWrite("downloadFile"))
            {
                while (startIndex < fileSize)
                {
                    int l = fileSize - startIndex > step ? step : (int)(fileSize - startIndex);

                    sw.Restart();
                    Stream s = client.DownloadFile(path, startIndex, l);
                    sw.Stop();
                    if (sw.ElapsedMilliseconds > timeElapsed * 2)
                    {
                        step /= 2;
                        buffer = new byte[step];
                    }
                    else if (sw.ElapsedMilliseconds < timeElapsed / 2 && step < maxSize / 2)
                    {
                        step *= 2;
                        buffer = new byte[step];
                    }

                    s.Read(buffer, 0, (int)l);

                    t.Write(buffer, 0, (int)l);
                    startIndex += l;

                    Console.WriteLine("Download {0} bytes", startIndex);
                }
            }

        }
    }
}
