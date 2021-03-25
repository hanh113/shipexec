using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MESModel
{
    [DataContract]
    public class DALModel
    {
        [DataMember]
        public bool IsSuccess { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public DataSet DS { get; set; }
    }

    [DataContract]
    public class FileObject
    {
        [DataMember]
        public string fileName;

        [DataMember]
        public string version;

        [DataMember]
        public DateTime fileAge;

        public FileObject(string fileName, string version, DateTime fileAge)
        {
            this.fileName = fileName;
            this.version = version;
            this.fileAge = fileAge;
        }
    }

    [DataContract]
    public class ClientObject
    {
        [DataMember]
        public string computerName;

        [DataMember]
        public string userNo;

        [DataMember]
        public string userName;

        [DataMember]
        public DateTime loginTime;

        [DataMember]
        public int Port;

        public ClientObject(string computerName, string userNo, string userName, DateTime loginTime, int Port)
        {
            this.computerName = computerName;
            this.userNo = userNo;
            this.userName = userName;
            this.loginTime = loginTime;
            this.Port = Port;
        }
    }
}
