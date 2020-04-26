using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Data;
using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.GenericRepository;
using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Models;

namespace Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmController : ControllerBase
    {
        private readonly UsersContext _context;
        private readonly IGenericRepository<Table_Users> _repo;

        public ConfirmController(UsersContext context, IGenericRepository<Table_Users> repo)
        {
            _context = context;
            _repo = repo;
        }


       

        [HttpGet("{token}")]
        public ContentResult ConfirmEmail(string token)
        {

            var getUserWithToken = _context.Table_Users.FirstOrDefault(s => s.EmailConfirmationToken == token);
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            if (getUserWithToken == null)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = "<html><body><div style='text-align: center; margin-top: 100px; font-size: 40px; font-weight:bolder; color: red; '>User With This Token Not Found !! " + "Navigate To Our WebSite: <a href=' " + baseUrl + "'>" + baseUrl + "</a></div></body></html>"
                };
            }else if (getUserWithToken.IsEmailConfirmed)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = "<html><body><div style='text-align: center; margin-top: 100px; font-size: 40px; font-weight:bolder; color: blue; '> User Already Confirmed ( " + getUserWithToken.Name + " ) !! " + "Navigate To Our WebSite: <a href=' " + baseUrl + "'>" + baseUrl + "</a></div></body></html>"
                };
            }
            else
            {
                getUserWithToken.IsEmailConfirmed = true;
                _repo.Update(getUserWithToken);
                _repo.SaveAsync(getUserWithToken);
                
                
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = "<html><body><div style='text-align: center; margin-top: 100px; font-size: 40px; font-weight:bolder; color: green; '>Congrateulations !! ( "+ getUserWithToken.Name + " ) You Are Confirmed !! Navigate To Our WebSite: <a href='" + baseUrl + "'>" + baseUrl + "</a></div></body></html>"
                };
            }



        }




    }
}
