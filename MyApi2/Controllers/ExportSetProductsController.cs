using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/export_set_product")]
    [ApiController]
    public class ExportSetProductsController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public ExportSetProductsController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/export_set_product
        [HttpGet]
        public ActionResult<IEnumerable<ExportSetProductsDto>> Get(int? id, string? UseYN)
        {
            var result = from a in _test10Context.Export_set_Product
                         orderby a.Export_batch
                         select new
                         {
                             Id = a.Id,
                             Export_batch = a.Export_batch,
                             C_id = a.C_id,
                             P_id = a.P_id,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            //var result = from a in _test10Context.Export_set_batch
            //             join b in _test10Context.Export_set_Company on a.Export_batch equals b.Export_batch
            //             join c in _test10Context.Export_set_Product on new { b.Export_batch, b.C_id } equals new { c.Export_batch, c.C_id }
            //             orderby a.Export_batch, b.C_id, c.P_id
            //             select new
            //             {
            //                 Export_batch = a.Export_batch,
            //                 C_id = b.C_id,
            //                 P_id = c.P_id,
            //                 Use_yn = a.Use_yn,
            //                 Sort = c.Sort,
            //                 Upd_user = a.Upd_user,
            //                 Upd_date = a.Upd_date,
            //                 Create_dt = a.Create_dt,
            //             };

            if (id != null)
            {
                result = result.Where(
                    a => a.Export_batch == id
                );
            }

            if (UseYN != null)
            {
                if (UseYN == "Y")
                {
                    result = result.Where(a => a.Use_yn == true);
                }
                else if (UseYN == "N")
                {
                    result = result.Where(a => a.Use_yn == false);
                }
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/export_set_product/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ExportSetProductsDto>> GetSingle(int id)
        {
            var result = from a in _test10Context.Export_set_Product
                         orderby a.Export_batch
                         select new
                         {
                             Id = a.Id,
                             Export_batch = a.Export_batch,
                             C_id = a.C_id,
                             P_id = a.P_id,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.Id == id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/export_set_product
        /*上傳json格式
        {
            "Export_batch": 2,
            "C_id": "C000000001",
            "P_id": "A000000003",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ExportSetProductsDto value)
        {
            try
            {
                Export_set_Product insert = new Export_set_Product
                {
                    Export_batch = value.Export_batch,
                    C_id = value.C_id,
                    P_id = value.P_id,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Export_set_Product.Add(insert);
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

        // PUT api/export_set_product/{id}
        /*上傳json格式
        {
            "Export_batch": 2,
            "C_id": "C000000001",
            "P_id": "A000000003",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ExportSetProductsDto value)
        {
            var result = (from a in _test10Context.Export_set_Product
                          where a.Id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.Export_batch = value.Export_batch;
                    result.C_id = value.C_id;
                    result.P_id = value.P_id;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Export_set_Product.Update(result);
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

        // DELETE api/export_set_product/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _test10Context.Export_set_Product
                          where a.Id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _test10Context.Export_set_Product.Remove(result);
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
