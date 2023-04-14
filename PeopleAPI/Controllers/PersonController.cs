using Common;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Image;
using System;
using static Common.Utils;

namespace PeopleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private PersonService PersonService { get; set; }
        private ImageService ImageService { get; set; }

        public PersonController(PersonService personService, ImageService imageService)
        {
            PersonService = personService;
            ImageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var people = await this.PersonService.GetAll();
                return Ok(people);
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
                var count = await this.PersonService.GetCount();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOneById(Guid id)
        {
            try
            {
                var person = await this.PersonService.GetOneById(id);
                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Person person)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                Guid Id = Guid.NewGuid();

                ImageProperties imageProperties = Utils.ConvertImageBase64StringToByteArr(person.PhotoUrl);
                string photoUrl = await ImageService.UploadFile("person", Id, imageProperties.FileExtension, imageProperties.ImageBytes);

                var newPerson = new Person
                {
                    Id = Id,
                    Name = person.Name,
                    Email = person.Email,
                    PhoneNumber = person.PhoneNumber,
                    PhotoUrl = photoUrl,
                    Birthday = person.Birthday,
                    CountryId = person.CountryId,
                    StateId = person.StateId,
                };

                int result = await PersonService.Create(newPerson);

                if (result == 0)
                    return BadRequest();

                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Person person)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            try
            {
                var result = await this.PersonService.Update(person);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await this.PersonService.Delete(id);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
