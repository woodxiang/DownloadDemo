using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace DownloadDemo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDownloadService
    {
        [OperationContract]
        long GetFileSize(string path);

        [OperationContract]
        Stream DownloadFile(string path, long startPosition, int length);
    }   
}
