using System;
using System.Runtime.Serialization;

namespace MESModel
{
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
