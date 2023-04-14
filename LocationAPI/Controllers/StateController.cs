using Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Image;
using Services;
using System.Net.Http.Headers;
using System.Text.Json;
using Common;
using static Common.Utils;
using System.Diagnostics.Metrics;

namespace LocationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private StateService StateService { get; set; }
        private ImageService ImageService { get; set; }

        public StateController(StateService stateService, ImageService imageService)
        {
            StateService = stateService;
            ImageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var states = await StateService.GetAll();
                return Ok(states);
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
                var count = await this.StateService.GetCount();
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
                var state = await StateService.GetOneById(id);
                return Ok(state);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] State state)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                Guid Id = Guid.NewGuid();

                ImageProperties imageProperties = Utils.ConvertImageBase64StringToByteArr(state.PhotoUrl);
                string photoUrl = await ImageService.UploadFile("state", Id, imageProperties.FileExtension, imageProperties.ImageBytes);

                var newState = new State
                {
                    Id = Id,
                    Name = state.Name,
                    PhotoUrl = photoUrl,
                    CountryId = state.CountryId
                };

                int result = await StateService.Create(newState);

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
        public async Task<IActionResult> Update(Guid id, [FromBody] State state)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var result = await this.StateService.Update(state);
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
                var result = await this.StateService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
