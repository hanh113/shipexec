using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientUtilsDll
{
    public static class ExtenderObject
    {
        public static IEnumerable<T> DatatableToObject<T>(this DataTable dt)
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(
                JsonConvert.SerializeObject(dt)
               );
        }

        public static string TL(this string str)
        {
            var l = ClientUtils.Language.FirstOrDefault(xx => xx.LANGCODE == str || xx.CN_DESC == str);

            var ret =
            l == null ? str : l.LANGFUNC.Equals("Controls") ?
            (ClientUtils.fClientLang == "EN-US" ? l.EN_DESC :
        ClientUtils.fClientLang == "VN" ? l.VN_DESC : l.CN_DESC) :
     (ClientUtils.fClientLang == "EN-US" ? l.LANGCODE + ":" + l.EN_DESC :
        ClientUtils.fClientLang == "VN" ? l.LANGCODE + ":" + l.VN_DESC :
       l.LANGCODE + ":" + l.CN_DESC);
            return ret == null ? ret : ret.Replace("\\n", Environment.NewLine);
        }
        public static string TP(this string str)
        {
            //修改return msg => return msg_code+ msg by Franky 2020/1/13
            string ret = str;
            bool isNotChange = true;
            try
            {
                var list = ClientUtils.Language.Where(x => !string.IsNullOrEmpty(x.REMARK) && x.LANGFUNC != "Controls").OrderByDescending(s => s.CN_DESC.Length);
                var l = list.FirstOrDefault(x =>
                {
                    var sp = x.REMARK.Split(new[] { "/*/" }, StringSplitOptions.None).ToList();
                    return sp.Count == sp.Where(y => str.Contains(y)).Count();
                });
                if (l != null)
                {
                    var tostr = ClientUtils.fClientLang == "EN-US" ? l.LANGCODE + ":" + l.EN_DESC :
                         ClientUtils.fClientLang == "VN" ? l.LANGCODE + ":" + l.VN_DESC : l.LANGCODE + ":" + l.CN_DESC;
                    var from = l.REMARK.Split(new[] { "/*/" }, StringSplitOptions.None).ToArray();
                    var to = tostr.Split(new[] { "/*/" }, StringSplitOptions.None).ToArray();
                    for (int i = 0; i < from.Length; i++)
                    {
                        ret = ret.Replace(from[i], to[i]);
                    }
                }
                isNotChange = false;
            }
            catch
            {
            }
            return ret == null ? ret :
                isNotChange ? ret.TL() :
                    ret.Replace("\\n", Environment.NewLine);
        }



        public static List<Control> GetAllControls(this Form form)
        {
            return GetAllControls(ToList(form.Controls));
        }

        public static List<Control> ToList(Control.ControlCollection controls)
        {
            List<Control> controlList = new List<Control>();
            foreach (Control control in controls)
                controlList.Add(control);
            return controlList;
        }

        public static List<Control> GetAllControls(List<Control> inputList)
        {
            List<Control> outputList = new List<Control>();
            outputList.AddRange(inputList);
            //取出inputList中的容器
            IEnumerable<Control> containers = from control in inputList
                                              where control is GroupBox |
                         control is TabControl |
                         control is Panel |
                         control is FlowLayoutPanel |
                         control is TableLayoutPanel |
                         control is ContainerControl
                                              select control;

            foreach (Control container in containers)
            {
                outputList.AddRange(GetAllControls(ToList(container.Controls)));
            }
            return outputList;
        }

    }
}
