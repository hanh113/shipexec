using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTTWebServices.Models
{
     public class ResponseModel
    {
      /// <summary>
      /// 成功否
      /// </summary>
       public bool YN { get; set; }

       /// <summary>
       /// 成功时返回单号
       /// </summary>
       public string FromNo {get;set;}

       /// <summary>
       /// 异常类型
       /// </summary>
       public string type { get; set; }
       /// <summary>
       /// 失败代码
       /// </summary>
       public string errcode { get; set; }

       /// <summary>
       /// 失败描述
       /// </summary>
       public string errDesc { get; set; }


    }
}
