using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DownloadDemo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class DownloadService : IDownloadService
    {
        public Stream DownloadFile(string path, long startPosition, int length)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            string filePath = Path.Combine(@"\\labdataserver2\LabData\Dingshan_Data_SINOPEC_Chengdu", path);

            try
            {
                Stream s = File.OpenRead(filePath);

                s.Position = startPosition;

                byte[] buffer = new byte[length];
                s.Read(buffer, 0, length);

                MemoryStream ms = new MemoryStream(buffer);
                ms.Position = 0;
                return ms;
            }
            catch
            {
                return null;
            }
        }

        public long GetFileSize(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            string filePath = Path.Combine(@"\\labdataserver2\LabData\Dingshan_Data_SINOPEC_Chengdu", path);

            try
            {
                var fi = new FileInfo(filePath);
                return fi.Length;
            }
            catch
            {
                return 0;
            }
        }
    }
}
