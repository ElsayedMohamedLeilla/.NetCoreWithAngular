
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Data;
using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.GenericRepository;
using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Helper;
using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Models;

namespace Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SendConfirmController : ControllerBase
    {
        private readonly UsersContext _context;
        private readonly IGenericRepository<Table_Users> _repo;

        public SendConfirmController(UsersContext context, IGenericRepository<Table_Users> repo)
        {
            _context = context;
            _repo = repo;
        }


        
        [HttpGet("{id}")]
        public async Task<IActionResult> SendConfirmationEmail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Table_Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var Link = baseUrl + "/api/Confirm/" + user.EmailConfirmationToken;
            var mail = new Email(user, Link);
            mail.SendEmail();



            return Ok(user);
        }

        




    }
}
