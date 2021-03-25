using System;
using System.Data;

namespace MESGridView
{
	public class Cache
	{
		private static int RowsPerPage;

		private Cache.DataPage[] cachePages;

		private IDataPageRetriever dataSupply;

		public Cache(IDataPageRetriever dataSupplier, int rowsPerPage)
		{
			this.dataSupply = dataSupplier;
			Cache.RowsPerPage = rowsPerPage;
			this.LoadFirstTwoPages();
		}

		private int GetIndexToUnusedPage(int rowIndex)
		{
			int num;
			if ((rowIndex <= this.cachePages[0].HighestIndex ? true : rowIndex <= this.cachePages[1].HighestIndex))
			{
				num = (this.cachePages[0].LowestIndex - rowIndex >= this.cachePages[1].LowestIndex - rowIndex ? 0 : 1);
			}
			else
			{
				num = (rowIndex - this.cachePages[0].HighestIndex >= rowIndex - this.cachePages[1].HighestIndex ? 0 : 1);
			}
			return num;
		}

		private bool IfPageCached_ThenSetElement(int rowIndex, int columnIndex, ref string element)
		{
			bool flag;
			if (this.IsRowCachedInPage(0, rowIndex))
			{
				element = this.cachePages[0].table.Rows[rowIndex % Cache.RowsPerPage][columnIndex].ToString();
				flag = true;
			}
			else if (!this.IsRowCachedInPage(1, rowIndex))
			{
				flag = false;
			}
			else
			{
				element = this.cachePages[1].table.Rows[rowIndex % Cache.RowsPerPage][columnIndex].ToString();
				flag = true;
			}
			return flag;
		}

		private bool IsRowCachedInPage(int pageNumber, int rowIndex)
		{
			return (rowIndex > this.cachePages[pageNumber].HighestIndex ? false : rowIndex >= this.cachePages[pageNumber].LowestIndex);
		}

		private void LoadFirstTwoPages()
		{
			DataTable dataTable = new DataTable();
			Cache.DataPage dataPage = new Cache.DataPage(this.dataSupply.SupplyPageOfData(Cache.DataPage.MapToLowerBoundary(0), Cache.RowsPerPage, ref dataTable), 0);
			Cache.DataPage dataPage1 = new Cache.DataPage(dataTable, Cache.RowsPerPage);
			this.cachePages = new Cache.DataPage[] { dataPage, dataPage1 };
		}

		private string RetrieveData_CacheIt_ThenReturnElement(int rowIndex, int columnIndex)
		{
			DataTable dataTable = this.dataSupply.SupplyPageOfData(Cache.DataPage.MapToLowerBoundary(rowIndex), Cache.RowsPerPage);
			this.cachePages[this.GetIndexToUnusedPage(rowIndex)] = new Cache.DataPage(dataTable, rowIndex);
			return this.RetrieveElement(rowIndex, columnIndex);
		}

		public string RetrieveElement(int rowIndex, int columnIndex)
		{
			string str;
			string str1 = null;
			str = (!this.IfPageCached_ThenSetElement(rowIndex, columnIndex, ref str1) ? this.RetrieveData_CacheIt_ThenReturnElement(rowIndex, columnIndex) : str1);
			return str;
		}

		public struct DataPage
		{
			public DataTable table;

			private int lowestIndexValue;

			private int highestIndexValue;

			public int HighestIndex
			{
				get
				{
					return this.highestIndexValue;
				}
			}

			public int LowestIndex
			{
				get
				{
					return this.lowestIndexValue;
				}
			}

			public DataPage(DataTable table, int rowIndex)
			{
				this.table = table;
				this.lowestIndexValue = Cache.DataPage.MapToLowerBoundary(rowIndex);
				this.highestIndexValue = Cache.DataPage.MapToUpperBoundary(rowIndex);
			}

			public static int MapCurrentPageIndex(int rowIndex)
			{
				int num;
				int num1 = rowIndex / Cache.RowsPerPage;
				if (rowIndex % Cache.RowsPerPage <= 0)
				{
					num = num1;
				}
				else
				{
					int num2 = num1 + 1;
					num1 = num2;
					num = num2;
				}
				return num;
			}

			public static int MapToLowerBoundary(int rowIndex)
			{
				return rowIndex / Cache.RowsPerPage * Cache.RowsPerPage;
			}

			private static int MapToUpperBoundary(int rowIndex)
			{
				int lowerBoundary = Cache.DataPage.MapToLowerBoundary(rowIndex) + Cache.RowsPerPage - 1;
				return lowerBoundary;
			}
		}
	}
}