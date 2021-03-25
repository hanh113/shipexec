using System;
using System.Runtime.Serialization;

namespace MESModel
{
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
}
