using Entities;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var httpClient = new HttpClient();
                var personResponse = httpClient.GetAsync("https://localhost:7038/api/person/count").Result;
                var countryResponse = httpClient.GetAsync("https://localhost:7167/api/country/count").Result;
                var stateResponse = httpClient.GetAsync("https://localhost:7167/api/state/count").Result;


                if (personResponse.IsSuccessStatusCode == false || countryResponse.IsSuccessStatusCode == false || stateResponse.IsSuccessStatusCode == false)
                {
                    return View(new { Success = false });
                }

                var personCount = await personResponse.Content.ReadAsStringAsync();
                var countryCount = await countryResponse.Content.ReadAsStringAsync();
                var stateCount = await stateResponse.Content.ReadAsStringAsync();


                return View(new { Success = true, PersonCount = personCount, CountryCount = countryCount, StateCount = stateCount });

            }
            catch (Exception ex)
            {
                return View(new { Success = false, ErrorMessage = ex.Message });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}