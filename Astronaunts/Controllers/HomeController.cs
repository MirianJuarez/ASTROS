using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAstronautData()
        {
            string url = "http://api.open-notify.org/astros.json";
            HttpClient client = new HttpClient();
            AstronautResponse astronautData = null;

            try
            {
                var result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    astronautData = JsonConvert.DeserializeObject<AstronautResponse>(json);
                }
                else
                {
                    return Content("Error en la llamada a la API: " + result.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return Content("Excepción: " + ex.Message);
            }

            return PartialView("_AstronautData", astronautData);
        }

        public class AstronautResponse
        {
            public string Message { get; set; }
            public int Number { get; set; }
            public Person[] People { get; set; }
        }

        public class Person
        {
            public string Name { get; set; }
            public string Craft { get; set; }
        }
    }
}