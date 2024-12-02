using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;
using MyApi2.Services;

namespace MyApi2.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;
        private readonly TokenService _TokenService;

        public LoginController(GalDBContext GalDBContext, TokenService TokenService)
        {
            _GalDBContext = GalDBContext;
            _TokenService = TokenService;
        }


        // GET api/login/gettoken/id
        [HttpGet("gettoken/{id}")]
        public string GetToken(string id)
        {
            string token = _TokenService.GenerateToken(id);
            return token;
        }

        // GET api/login/check
        /*
        {
          "username": "string",
          "password": "string"
        } 
        */
        [HttpPost("check")]
        public ActionResult<IEnumerable<AuthenticationDto>> Check([FromBody] LoginDto value)
        {
            var result = (from a in _GalDBContext.Account_info
                          join b in _GalDBContext.Account_per on a.Account_id equals b.Account_id
                          where a.Account_id == value.username && b.Password == value.password
                          select a).SingleOrDefault();

            if (result == null)
            {
                return Ok(new AuthenticationDto 
                { 
                    IsAuthSuccessful = false, 
                    ErrorMessage = "帳號或密碼錯誤" 
                });
            }
            else
            {
                string token = _TokenService.GenerateToken(value.username);
                return Ok(new AuthenticationDto 
                { 
                    IsAuthSuccessful = true, 
                    Token = token, 
                    User = value.username,
                    ErrorMessage = ""
                });
            }
        }
    }
}
