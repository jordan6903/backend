using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/export_view_other")]
    [ApiController]
    public class ExportViewsOtherController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public ExportViewsOtherController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/export_view_other/get1_1
        [HttpGet("get1_1")]
        public ActionResult<IEnumerable<ExportViewsOther1_1Dtos>> Get1_1(int id)
        {
            var result = from a in _GalDBContext.Export_set_Other
                         join b in _GalDBContext.Export_set_Other_series on a.Id equals b.ESO_id
                         join c in _GalDBContext.Export_set_Other_Product on b.Id equals c.ESOS_id
                         join c1 in _GalDBContext.Product on c.P_id equals c1.P_id
                         join c2 in _GalDBContext.Company on c1.C_id equals c2.C_id
                         orderby a.Sort, b.Sort, c.Sort
                         select new
                         {
                             Id = a.Id,
                             Export_batch = a.Export_batch,
                             C_id = c2.C_id,
                             C_Name = c2.Name,
                             C_Name_origin = c2.Name_origin,
                             eso_Sort = a.Sort,
                             esos_id = b.Id,
                             esos_Name = b.Name,
                             esos_Sort = b.Sort,
                             esop_id = c.Id,
                             esop_Sort = c.Sort,
                             P_id = "",
                             P_Name = "",
                             P_CName = "",
                             Sale_Date = "",
                             T_id = "",
                             T_team = "",
                             T_type = "",
                             Remark = "",
                             url1 = "",
                             url2 = "",
                             url3 = "",
                             url4 = "",
                             pic = "",
                             picw = 0,
                             pich = 0
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.Export_batch == id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/export_view_other/get1_2
        [HttpGet("get1_2")]
        public ActionResult<IEnumerable<ExportViewsOther1_2Dtos>> Get1_2()
        {
            //var result = from a in _GalDBContext.Export_set_Other_Product
            //             join a1 in _GalDBContext.Product on a.P_id equals a1.P_id
            //             join c1 in
            //                 (from prd in _GalDBContext.Product_Release_day
            //                  group prd by prd.P_id into g
            //                  select new
            //                  {
            //                      P_id = g.Key,
            //                      Sale_Date = g.Min(x => x.Sale_Date)
            //                  }) on a.P_id equals c1.P_id
            //             join c in _GalDBContext.Product_Release_day
            //                 on new { c1.P_id, c1.Sale_Date } equals new { c.P_id, c.Sale_Date }
            //             join d in _GalDBContext.Translation_team on a.P_id equals d.P_id
            //             orderby a.Sort
            //             select new
            //             {
            //                 esop_id = a.Id,
            //                 P_id = a.P_id,
            //                 P_Name = a1.Name,
            //                 P_CName = a1.C_Name,
            //                 Sale_Date = c.Sale_Date,
            //             };
            var result = from a in _GalDBContext.Export_set_Other_Product
                         join a1 in _GalDBContext.Product on a.P_id equals a1.P_id
                         join c in _GalDBContext.Product_Release_day on a.P_id equals c.P_id
                         join d in _GalDBContext.Translation_team on a.P_id equals d.P_id
                         where c.Official_First == true
                         orderby a.Sort
                         select new
                         {
                             esop_id = a.Id,
                             P_id = a.P_id,
                             P_Name = a1.Name,
                             P_CName = a1.C_Name,
                             Sale_Date = c.Sale_Date,
                         };

            // Distinct 過濾重複的結果
            var distinctResult = result.Distinct().ToList();

            if (distinctResult == null)
            {
                return NotFound();
            }

            return Ok(distinctResult);
        }

        // POST api/export_view_other
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/export_view_other/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/export_view_other/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
