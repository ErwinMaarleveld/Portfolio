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
    class GebruikerClient
    {
        private readonly HttpClient Client;

        private readonly string Url;

        public GebruikerClient()
        {
            Url = "http://fourmistakesapi.azurewebsites.net/api/gebruiker/";
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Gebruiker Get(string code)
        {
            try
            {
                string content = Client.GetStringAsync(Url + $"/{code}").Result;
                JObject json = JObject.Parse(content);
                return json.ToObject<Gebruiker>();
            }
            catch (AggregateException)
            {
                return null;
            }
        }

        public bool Post(Gebruiker gebruiker)
        {
            var json = JsonConvert.SerializeObject(gebruiker);
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

        public bool Put(Gebruiker gebruiker)
        {
            var json = JsonConvert.SerializeObject(gebruiker);
            var response = Client.PutAsync(Url + $"/{gebruiker.GCode}", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Gebruiker gebruiker)
        {
            var response = Client.DeleteAsync(Url + $"/{gebruiker.GCode}").Result;
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
            var response = Client.DeleteAsync(Url + $"/{code}").Result;
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
