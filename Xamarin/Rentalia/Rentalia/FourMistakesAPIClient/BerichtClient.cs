using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Rentalia.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Rentalia.FourMistakesAPIClient
{
    class BerichtClient
    {
        private readonly HttpClient Client;

        private readonly string Url;

        public BerichtClient()
        {
            Url = "http://fourmistakesapi.azurewebsites.net/api/bericht/";
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Bericht[] Get(string code)
        {
            string content = Client.GetStringAsync(Url + $"/{code}").Result;
            JArray json = JArray.Parse(content);
            return json.ToObject<Bericht[]>();
        }

        public bool Post(Bericht bericht)
        {
            var json = JsonConvert.SerializeObject(bericht);
            var response = Client.PostAsync(Url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Put(Bericht bericht)
        {
            var json = JsonConvert.SerializeObject(bericht);
            var response = Client.PutAsync(Url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Bericht bericht)
        {
            var json = JsonConvert.SerializeObject(bericht);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, Url) { Content = new StringContent(json, Encoding.UTF8, "application/json") };
            var response = Client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
