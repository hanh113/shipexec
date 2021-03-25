using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace wmsReport.WebApi
{
    public class CallApi
    {
        public static T postApi<T>(string path, object o, string basePath = "")
        {
            HttpClient client = new HttpClient();
            basePath = string.IsNullOrEmpty(basePath) ? @"http://10.171.16.201:9877/" : basePath;
            client.BaseAddress = new Uri(basePath);

            var result = client.PostAsJsonAsync("api/TopStar/PostOrderToTopStar", o).Result;
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return result.Content.ReadAsAsync<T>().Result;
                case HttpStatusCode.BadRequest:
                    var jobj = result.Content.ReadAsAsync<JObject>().Result;
                    throw new Exception(result.StatusCode.ToString() + "\t" + jobj["Message"].ToString());
                default:
                    throw new Exception(result.StatusCode.ToString());
            }
        }
    }
}
