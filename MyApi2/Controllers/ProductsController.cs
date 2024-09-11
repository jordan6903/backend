using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public ProductsController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }
        /*
        // GET: api/product
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

        // GET api/product/{id}
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
        */

        // GET: api/product
        [HttpGet]
        public ActionResult<IEnumerable<ProductsDto>> Get(string? searchword)
        {
            var result = from a in _test10Context.Product
                         join b in _test10Context.Company on a.C_id equals b.C_id
                         orderby a.C_id, a.P_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             C_id = a.C_id,
                             Name = a.Name,
                             C_Name = a.C_Name,
                             Content = a.Content,
                             Content_style = a.Content_style,
                             Play_time = a.Play_time,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             Company_name = b.Name,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(searchword) ||
                         a.Name.Contains(searchword) ||
                         a.C_Name.Contains(searchword)
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/company/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ProductsDto>> GetSingle(string id)
        {
            var result = from a in _test10Context.Product
                         join b in _test10Context.Company on a.C_id equals b.C_id
                         orderby a.C_id, a.P_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             C_id = a.C_id,
                             Name = a.Name,
                             C_Name = a.C_Name,
                             Content = a.Content,
                             Content_style = a.Content_style,
                             Play_time = a.Play_time,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             Company_name = b.Name,
                         };

            if (id != null)
            {
                result = result.Where(a => a.P_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/product
        /*上傳json格式
        {
            "P_id": "A000000000",
            "C_id": "C000000001",
            "Name": "test",
            "C_Name": "test2",
            "Content": "test3",
            "Content_style": "test4",
            "Play_time": "",
            "Remark": ""
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ProductsDto value)
        {
            try
            {
                Product insert = new Product
                {
                    P_id = value.P_id,
                    C_id = value.C_id,
                    Name = value.Name,
                    C_Name = value.C_Name,
                    Content = value.Content,
                    Content_style = value.Content_style,
                    Play_time = value.Play_time,
                    Remark = value.Remark,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Product.Add(insert);
                _test10Context.SaveChanges();

                // 回傳成功訊息
                return Ok(new { message = "資料上傳成功" });
            }
            catch (Exception ex)
            {
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return BadRequest(new { message = "資料上傳失敗", error = ex.Message });
            }
        }

        // PUT api/product/{id}
        /*上傳json格式
        {
            "P_id": "A000000000",
            "C_id": "C000000001",
            "Name": "test",
            "C_Name": "test2",
            "Content": "test3",
            "Content_style": "test4",
            "Play_time": "",
            "Remark": ""
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ProductsDto value)
        {
            var result = (from a in _test10Context.Product
                          where a.P_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.P_id = value.P_id;
                    result.C_id = value.C_id;
                    result.Name = value.Name;
                    result.C_Name = value.C_Name;
                    result.Content = value.Content;
                    result.Content_style = value.Content_style;
                    result.Play_time = value.Play_time;
                    result.Remark = value.Remark;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Product.Update(result);
                    _test10Context.SaveChanges();

                    // 回傳成功訊息
                    return Ok(new { message = "資料更新成功" });
                }
                catch (Exception ex)
                {
                    // 捕捉錯誤並回傳詳細的錯誤訊息
                    return BadRequest(new { message = "資料更新失敗", error = ex.Message });
                }
            }
        }

        // DELETE api/product/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = (from a in _test10Context.Product
                          where a.P_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _test10Context.Product.Remove(result);
                    _test10Context.SaveChanges();

                    // 回傳成功訊息
                    return Ok(new { message = "資料刪除成功" });
                }
                catch (Exception ex)
                {
                    // 捕捉錯誤並回傳詳細的錯誤訊息
                    return BadRequest(new { message = "資料刪除失敗", error = ex.Message });
                }
            }
        }
    }
}
