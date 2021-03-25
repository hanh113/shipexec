using System.Data;
using System.Runtime.Serialization;

namespace MESModel
{
	[DataContract]
	public class DALModel
	{
		[DataMember]
		public bool IsSuccess
		{
			get;
			set;
		}

		[DataMember]
		public string Message
		{
			get;
			set;
		}

		[DataMember]
		public DataSet DS
		{
			get;
			set;
		}
	}
}
