using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Image;
using Services;
using static Common.Utils;

namespace PeopleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private FriendshipService FriendshipService{ get; set; }

        public FriendshipController(FriendshipService friendshipService)
        {
            FriendshipService = friendshipService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var friendships = await this.FriendshipService.GetAll();
                return Ok(friendships);
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
                var friendship = await this.FriendshipService.GetOneById(id);
                return Ok(friendship);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Friendship friendship)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                Guid Id = Guid.NewGuid();

                var newFriendship = new Friendship
                { 
                    Id = Id,
                    APersonId = friendship.APersonId,
                    BPersonId = friendship.BPersonId,
                };

                int result = await FriendshipService.Create(newFriendship);

                if (result == 0)
                    return BadRequest();

                return Ok();
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
                var result = await this.FriendshipService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
