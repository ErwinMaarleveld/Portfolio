using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rentalia.Data;
using System.Net.Http.Headers;

namespace Rentalia.FourMistakesAPIClient
{
    class AanbiedingClient
    {
        private readonly HttpClient Client;

        private readonly string Url;

        public AanbiedingClient()
        {
            Url = "http://fourmistakesapi.azurewebsites.net/api/aanbieding/";
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Aanbieding[] Get()
        {
            string content = Client.GetStringAsync(Url).Result;
            JArray json = JArray.Parse(content);
            return json.ToObject<Aanbieding[]>();
        }

        public Aanbieding Get(string code)
        {
            string content = Client.GetStringAsync(Url + $"/{code}").Result;
            JObject json = JObject.Parse(content);
            return json.ToObject<Aanbieding>();
        }

        public bool Post(Aanbieding aanbieding)
        {
            var json = JsonConvert.SerializeObject(aanbieding);
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

        public bool Put(Aanbieding aanbieding)
        {
            var json = JsonConvert.SerializeObject(aanbieding);
            var response = Client.PutAsync(Url + $"/{aanbieding.ACode}", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Aanbieding aanbieding)
        {
            var response = Client.DeleteAsync(Url + $"/{aanbieding.ACode}").Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(string code)
        {
            var response = Client.DeleteAsync(Url + "/" + code).Result;
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
