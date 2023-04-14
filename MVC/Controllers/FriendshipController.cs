using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MVC.Controllers
{
    public class FriendshipController : Controller
    {
        public IActionResult Index(Guid id)
        {
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync($"https://localhost:7038/api/friendship{id}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                return View(response);
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<List<Friendship>>(jsonString);

            ViewBag.PersonId = Request.Query["personId"].ToString();

            return View(result);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("APersonId, BPersonId")] Friendship model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            try
            {
                var Friendship = new Friendship
                {
                   APersonId = model.APersonId,
                   BPersonId = model.BPersonId,
                };

                var json = JsonSerializer.Serialize<Friendship>(model);

                HttpContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));
                HttpClient httpClient = new HttpClient();

                var response = httpClient.PostAsync($"https://localhost:7038/api/friendship", content).Result;

                if (response.IsSuccessStatusCode == false)
                {
                    return View(response);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Details(Guid id)
        {
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync($"https://localhost:7038/api/friendship/{id}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                return View(response);
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Friendship>(jsonString);

            return View(result);
        }

        public ActionResult Delete(Guid id)
        {
            HttpClient httpClient = new HttpClient();

            var response = httpClient.GetAsync($"https://localhost:7038/api/friendship/{id}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                return View(response);
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Friendship>(jsonString);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var response = httpClient.DeleteAsync($"https://localhost:7038/api/friendship/{id}").Result;

                if (response.IsSuccessStatusCode == false)
                {
                    return View(response);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
