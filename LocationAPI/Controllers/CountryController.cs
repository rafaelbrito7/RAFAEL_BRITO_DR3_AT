using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Image;
using System;
using static Common.Utils;

namespace LocationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private CountryService CountryService { get; set; }
        private ImageService ImageService { get; set; }

        public CountryController(CountryService countryService, ImageService imageService)
        {
            CountryService = countryService;
            ImageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var countries = await CountryService.GetAll();
                return Ok(countries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("count")]
        public async Task<ActionResult> GetCount()
        {
            try
            {
                var count = await this.CountryService.GetCount();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneById(Guid id)
        {
            try
            {
                var country = await CountryService.GetOneById(id);
                return Ok(country);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Country country)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                Guid Id = Guid.NewGuid();

                ImageProperties imageProperties = Utils.ConvertImageBase64StringToByteArr(country.PhotoUrl);
                string photoUrl = await ImageService.UploadFile("country", Id, imageProperties.FileExtension, imageProperties.ImageBytes);

                var newCountry = new Country
                {
                    Id = Id,
                    Name = country.Name,
                    PhotoUrl = photoUrl,
                };

                int result = await CountryService.Create(newCountry);

                if (result == 0)
                    return BadRequest();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Country country)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var result = await this.CountryService.Update(country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await this.CountryService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
