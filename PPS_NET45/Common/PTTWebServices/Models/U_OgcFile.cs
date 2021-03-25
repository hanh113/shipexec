using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTTWebServices.Models
{
     public class U_OgcFile
    {
        /// <summary>
        /// 出货项次
        /// </summary>
        public string ogc03 { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public string ogc09 { get; set; }
        /// <summary>
        /// 储位
        /// </summary>
        public string ogc091 { get; set; }

        //public string ogc092 { get; set; }
        /// <summary>
        ///数量 
        /// </summary>
        public string ogc12 { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string ogc15 { get; set; }

    }
}
