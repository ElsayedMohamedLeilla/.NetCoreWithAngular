using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Data;
using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.GenericRepository;
using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Helper;
using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Models;

namespace Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersContext _context;
        private readonly IGenericRepository<Table_Users> _repo;

        public UsersController(UsersContext context, IGenericRepository<Table_Users> repo)
        {
            _context = context;
            _repo = repo;
        }

        
        [HttpGet]
        public IEnumerable<Table_Users> GetUsers()
        {
            return _context.Table_Users.OrderBy(p => p.UserId);
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> Getuser([FromRoute] int id)
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

            return Ok(user);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Putuser([FromRoute] int id, [FromBody] Table_Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                _repo.Update(user);
                var save = await _repo.SaveAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpPost]
        public async Task<IActionResult> Postuser([FromBody] Table_Users user)
        {
            user.AddedDate = DateTime.Now;
            user.IsEmailConfirmed = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            

            var allUsers = _context.Table_Users.ToList();
            var token = DateTime.Now.AddDays(DateTime.Now.Millisecond).ToString().GetHashCode().ToString("x");

            while (allUsers.FirstOrDefault(s => s.EmailConfirmationToken == token) != null)
            {
                token = DateTime.Now.AddDays(DateTime.Now.Millisecond).ToString().GetHashCode().ToString("x");
            }

            user.EmailConfirmationToken = token;


            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var Link = baseUrl + "/api/Confirm/" + user.EmailConfirmationToken;

           
            var mail = new Email(user, Link);
            mail.SendEmail();


            _repo.Add(user);
            var save = await _repo.SaveAsync(user);

            return CreatedAtAction("Getuser", new { id = user.UserId }, user);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteuser([FromRoute] int id)
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

            _repo.Delete(user);
            var save = await _repo.SaveAsync(user);

            return Ok(user);
        }

        private bool userExists(int id)
        {
            return _context.Table_Users.Any(e => e.UserId == id);
        }

        
    }
}
