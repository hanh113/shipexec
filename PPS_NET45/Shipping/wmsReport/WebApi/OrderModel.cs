using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmsReport.WebApi
{
    /// <summary>
    /// 订单类
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// 栈板号
        /// </summary>
        public string[] TrayCodes { get; set; }
        /// <summary>
        /// 目的储位
        /// </summary>
        public string TarLocation { get; set; }
    }

    public class StockRes
    {
        /// <summary>
        /// 托盘号
        /// </summary>
        public string traycode { get; set; }
        /// <summary>
        /// 返回处理结果状态 1成功 0失败
        /// </summary>
        public int result
        {
            get
            {
                return this.ResCode == 1 ? this.ResCode : 0;
            }
            set
            {
                ResCode = value;
            }
        }
        private int ResCode { get; set; }
        /// <summary>
        /// 处理结果
        /// </summary>
        public string message { get; set; }
    }
}
