using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SportStore.Models;
using WpfApp;

namespace Request
{
    
    public class Requests
    {
        public static async Task<ReturnAutor> Autorization(string login, string password)
        {
            var sock = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
            };

            var client = new HttpClient(sock);

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7102/user/login/{login}/{password}");

            if (response.IsSuccessStatusCode) 
            {
                var ra = await client.GetFromJsonAsync<ReturnAutor>($"https://localhost:7102/user/login/{login}/{password}");
                
                return ra;

            }
            else
            {
                throw new Exception("Error retrieving user data");
            }
            
        }   
    } 
}
