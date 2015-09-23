using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace HyBy.Trading.BusinessFramework
{
    public class HttpClientHelper
    {
        const string baseAddress = "http://10.10.30.26:8090/";
        public static List<T> GetEntityList<T>(string api)
        {
            return GetEntityList<T>(baseAddress, api);
            #region oldcode
            //List<T> list = new List<T>();
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(baseAddress);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    // New code:
            //    try
            //    {
            //        HttpResponseMessage response = client.GetAsync(api).Result;

            //        if (response.IsSuccessStatusCode)
            //        {
            //            list = response.Content.ReadAsAsync<List<T>>().Result;
            //            //Product product = response.Content.ReadAsAsync<Product>().Result;
            //        }


            //        // HTTP POST
            //        //var gizmo = new Product() { Name = "Gizmo", Price = 100, Category = "Widget" };
            //        //response = client.PostAsJsonAsync("api/products", gizmo);
            //        //if (response.IsSuccessStatusCode)
            //        //{
            //        //    Uri gizmoUrl = response.Headers.Location;

            //        //    // HTTP PUT
            //        //    gizmo.Price = 80;   // Update price
            //        //    response = client.PutAsJsonAsync(gizmoUrl, gizmo);

            //        //    // HTTP DELETE
            //        //    response = client.DeleteAsync(gizmoUrl);
            //        //}

            //        return list;
            //    }
            //    catch (HttpRequestException e)
            //    {
            //        throw new Exception(e.Message);
            //    }
            #endregion
        }
        public static List<T> GetEntityList<T>(string url, string api)
        {
            List<T> list = new List<T>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // New code:
                try
                {
                    HttpResponseMessage response = client.GetAsync(api).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<T>>().Result;
                        //Product product = response.Content.ReadAsAsync<Product>().Result;
                    }

                    // HTTP POST
                    //var gizmo = new Product() { Name = "Gizmo", Price = 100, Category = "Widget" };
                    //response = client.PostAsJsonAsync("api/products", gizmo);
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    Uri gizmoUrl = response.Headers.Location;

                    //    // HTTP PUT
                    //    gizmo.Price = 80;   // Update price
                    //    response = client.PutAsJsonAsync(gizmoUrl, gizmo);

                    //    // HTTP DELETE
                    //    response = client.DeleteAsync(gizmoUrl);
                    //}

                    return list;
                }
                catch (HttpRequestException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public static T GetEntity<T>(string api)
        {
            return GetEntity<T>(baseAddress, api);
        }
        public static T GetEntity<T>(string url,string api)
        {
            T entity = default(T);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // New code:
                try
                {
                    HttpResponseMessage response = client.GetAsync(api).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        entity = response.Content.ReadAsAsync<T>().Result;
                    }

                    return entity;
                }
                catch (HttpRequestException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData">post数据</param>
        /// <returns></returns>
        public static string PostResponse(string url, string postData)
        {
            //MessageHelper.WriteLog("post:" + url + ";postData:" + postData);
            //HttpContent httpContent = new StringContent(postData);
            //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //HttpClient httpClient = new HttpClient();

            //MessageHelper.WriteLog("-----111111------------");

            //HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

            //MessageHelper.WriteLog("response.IsSuccessStatusCode:" + response.IsSuccessStatusCode);

            //if (response.IsSuccessStatusCode)
            //{
            //    string result = response.Content.ReadAsStringAsync().Result;
            //    return result;
            //}

            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] data = encoding.GetBytes(postData);

            HttpWebRequest myRequest =
             (HttpWebRequest)WebRequest.Create(url);

            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();

            // Send the data.
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            // Get response
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default);
            
            string content = reader.ReadToEnd();

            return content;
        }
    }
}
