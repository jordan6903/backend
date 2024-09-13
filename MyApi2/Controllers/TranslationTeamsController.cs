using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/translation_team")]
    [ApiController]
    public class TranslationTeamsController : ControllerBase
    {
        // GET: api/translation_team
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/translation_team/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/translation_team
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/translation_team/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/translation_team/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
