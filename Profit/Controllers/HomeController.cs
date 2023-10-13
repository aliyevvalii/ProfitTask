using Microsoft.AspNetCore.Mvc;
using Profit.Models;
using System.Diagnostics;

namespace Profit.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Product> result = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://fakestoreapi.com/products";
                    string response = await client.GetStringAsync(apiUrl);
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(response);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Xəta baş verdi: {e.Message}");
                }
                return View(result);
            }
        }
        
    }
}