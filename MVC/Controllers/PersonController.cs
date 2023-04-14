using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MVC.Controllers
{
    public class PersonController : Controller
    {
        // GET: PersonController
        public ActionResult Index()
        {
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync("https://localhost:7038/api/person").Result;

            if (response.IsSuccessStatusCode == false)
            {
                return View(response);
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<List<Person>>(jsonString);

            return View(result);
        }

        // GET: PersonController/Details/5
        public ActionResult Details(Guid id)
        {
            var httpClient = new HttpClient();

            var response = httpClient.GetAsync($"https://localhost:7038/api/person/{id}").Result;

            if(response.IsSuccessStatusCode == false)
            {
                return View(response);
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Person>(jsonString);

            return View(result);
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name, Email, PhoneNumber, Birthday, CountryId, StateId")]Person model, IFormFile PhotoUrl)
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

                var person = new Person
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PhotoUrl = model.PhotoUrl,
                    Birthday = model.Birthday,
                    StateId = model.StateId,
                    CountryId = model.CountryId,
                };

                var json = JsonSerializer.Serialize<Person>(model);
                HttpContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                HttpClient httpClient = new HttpClient();

                var response = httpClient.PostAsync($"https://localhost:7038/api/person", content).Result;

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

        // GET: PersonController/Edit/5
        public ActionResult Edit(Guid id)
        {
            HttpClient httpClient = new HttpClient();

            var response = httpClient.GetAsync($"https://localhost:7038/api/person/{id}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                return View(response);
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Person>(jsonString);

            return View(result);
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Person model)
        {
            try
            {
                var json = JsonSerializer.Serialize<Person>(model);
                HttpContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                HttpClient httpClient = new HttpClient();

                var response = httpClient.PutAsync($"https://localhost:7038/api/person/{id}", content).Result;

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

        // GET: PersonController/Delete/5
        public ActionResult Delete(Guid id)
        {
            HttpClient httpClient = new HttpClient();

            var response = httpClient.GetAsync($"https://localhost:7038/api/person/{id}").Result;

            if (response.IsSuccessStatusCode == false)
            {
                return View(response);
            }

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Person>(jsonString);

            return View(result);
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var response = httpClient.DeleteAsync($"https://localhost:7038/api/person/{id}").Result;

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
