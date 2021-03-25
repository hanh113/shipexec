using System;
using System.Data;

namespace MESGridView
{
	public interface IDataPageRetriever
	{
		DataTable SupplyPageOfData(int lowerPageBoundary, int rowsPerPage);

		DataTable SupplyPageOfData(int intlowerPageBoundary, int rowsPerPage, ref DataTable dt);
	}
}