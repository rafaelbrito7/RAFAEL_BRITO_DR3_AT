using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;

namespace MVC.Controllers
{
    public class CountryController : Controller
    {
        
        public ActionResult Index()
        {
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync("https://localhost:7167/api/country").Result;

            if (response.IsSuccessStatusCode == false)
            {
                return View(response);
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<List<Country>>(jsonString);

            return View(result);
        }

        // GET: CountryController/Details/5
        public ActionResult Details(Guid id)
        {
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync($"https://localhost:7167/api/country/{id}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                return View(response);
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Country>(jsonString);

            return View(result);
        }

        // GET: CountryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name, PhotoUrl")]Country model, IFormFile PhotoUrl)
        {
            if (ModelState.IsValid == false)
                return View(model);

            try
            {
                if (PhotoUrl != null && PhotoUrl.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await PhotoUrl.CopyToAsync(stream);
                        var fileExtension = Path.GetExtension(PhotoUrl.FileName).TrimStart('.');
                        var imageBase64 = $"data:image/{fileExtension};base64,{Convert.ToBase64String(stream.ToArray())}";
                        model.PhotoUrl = imageBase64;
                    }
                }

                var country = new Country
                {
                    Name = model.Name,
                    PhotoUrl = model.PhotoUrl,
                };

                var json = JsonSerializer.Serialize<Country>(model);
                HttpContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                HttpClient httpClient = new HttpClient();

                var response = httpClient.PostAsync($"https://localhost:7167/api/country", content).Result;

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

        // GET: CountryController/Edit/5
        public ActionResult Edit(Guid id)
        {
            HttpClient httpClient = new HttpClient();

            var response = httpClient.GetAsync($"https://localhost:7167/api/country/{id}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                return View(response);
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Country>(jsonString);

            return View(result);
        }

        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Country model)
        {
            try
            {
                var json = JsonSerializer.Serialize<Country>(model);
                HttpContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                HttpClient httpClient = new HttpClient();

                var response = httpClient.PutAsync($"https://localhost:7167/api/country/{id}", content).Result;

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

        // GET: CountryController/Delete/5
        public ActionResult Delete(Guid id)
        {
            HttpClient httpClient = new HttpClient();

            var response = httpClient.GetAsync($"https://localhost:7167/api/country/{id}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                return View(response);
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Country>(jsonString);

            return View(result);
        }

        // POST: CountryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var response = httpClient.DeleteAsync($"https://localhost:7167/api/country/{id}").Result;

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
