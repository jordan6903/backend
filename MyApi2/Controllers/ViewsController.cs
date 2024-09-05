using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyApi2.Controllers
{
    [Route("api/view")]
    [ApiController]
    public class ViewsController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public ViewsController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/view
        [HttpGet]
        public ActionResult<IEnumerable<ViewDto1>> Get(string? searchword, int? C_type)
        {
            var result = from a in _test10Context.Company
                         join a1 in _test10Context.Company_type on a.C_type equals a1.C_type
                         join b in _test10Context.Product on a.C_id equals b.C_id
                         orderby a.C_id, b.P_id
                         select new
                         {
                             C_id = a.C_id,
                             C_Name = a.Name,
                             Name_origin = a.Name_origin,
                             Name_short = a.Name_short,
                             C_type = a.C_type,
                             C_type_name = a1.C_type_name,
                             Intro = a.Intro,
                             Remark = a.Remark,
                             Sdate = a.Sdate,
                             Edate = a.Edate
                         };

            // Distinct 過濾重複的結果
            var distinctResult = result.Distinct().ToList();

            // 重新組合子集合 Products
            var finalResult = from a in distinctResult
                              join b in _test10Context.Product on a.C_id equals b.C_id into products
                              select new
                              {
                                  C_id = a.C_id,
                                  C_Name = a.C_Name,
                                  Name_origin = a.Name_origin,
                                  Name_short = a.Name_short,
                                  C_type = a.C_type,
                                  C_type_name = a.C_type_name,
                                  Intro = a.Intro,
                                  Remark = a.Remark,
                                  Sdate = a.Sdate,
                                  Edate = a.Edate,
                                  Products = products.Select(b => new ViewDto1_2
                                  {
                                      P_id = b.P_id,
                                      P_Name = b.Name,
                                      P_CName = b.C_Name,
                                  }).ToList(),
                              };

            if (searchword != null)
            {
                finalResult = finalResult.Where(
                    a => a.C_Name.Contains(searchword) ||
                         a.C_id.Contains(searchword) ||
                         a.Name_origin.Contains(searchword) ||
                         a.Name_short.Contains(searchword) ||
                         a.Products.Any(b => b.P_id.Contains(searchword) ||
                                             b.P_Name.Contains(searchword) ||
                                             b.P_CName.Contains(searchword))
                );
            }

            if (C_type != null)
            {
                finalResult = finalResult.Where(a => a.C_type == C_type);
            }

            if (finalResult == null)
            {
                return NotFound();
            }

            return Ok(finalResult.Take(300));
        }

        // GET api/<ViewsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ViewsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ViewsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ViewsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
