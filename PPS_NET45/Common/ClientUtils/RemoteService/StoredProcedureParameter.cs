using System.Data;
using System.Runtime.Serialization;

namespace RemoteService
{
	[DataContract]
	public struct StoredProcedureParameter
	{
		[DataMember]
		public string Name;

		[DataMember]
		public OraDirection Direction;

		[DataMember]
		public OraDbType DbType;

		[DataMember]
		public string Value;

		[DataMember]
		public DataSet Cursor;

		[DataMember]
		public int Size;
	}
}
