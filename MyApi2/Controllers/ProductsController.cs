using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Models;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public ProductsController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/products
        [HttpGet]
        public IActionResult Get()
        {
            var ls_sql = @"
                SELECT [A].[C_id],
	                [A].[Name],
	                [A].[Name_origin],
	                [A].[Name_short],
	                [A1].[C_type_name],
	                [A].[Intro],
	                [A].[Remark],
	                [A].[Sdate],
	                [A].[Edate],
	                [B].[P_id],
	                [B].[Name],
	                [B].[C_Name]
                FROM [Company] AS [A]
                LEFT JOIN [Company_type] AS [A1] ON [A].[C_type] = [A1].[C_type]
                INNER JOIN [Product] AS [B] ON [B].[C_id] = [A].[C_id]

                order by [A].[C_id], [B].[P_id];
                ";
            var device = _test10Context.Product.FromSqlRaw(ls_sql);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        // GET api/products/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var device = _test10Context.Device
                    .FromSqlRaw("SELECT Device_id,FullName,ShortName FROM Device WHERE device_id = {0}", id)
                    .Select(d => new
                    {
                        d.Device_id,
                        d.FullName,
                        d.ShortName,
                    })
                    .FirstOrDefault();

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
