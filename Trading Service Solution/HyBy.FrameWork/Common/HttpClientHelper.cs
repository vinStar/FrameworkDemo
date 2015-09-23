using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace HyBy.FrameWork.Common
{
    public class HttpClientHelper
    {
        const string baseAddress = "";
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
    }
}
