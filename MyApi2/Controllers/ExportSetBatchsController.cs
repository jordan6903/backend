using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/export_set_batch")]
    [ApiController]
    public class ExportSetBatchsController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public ExportSetBatchsController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/export_set_batch
        [HttpGet]
        public ActionResult<IEnumerable<ExportSetBatchsDto>> Get(string? searchword, string? UseYN)
        {
            var result = from a in _test10Context.Export_set_batch
                         orderby a.Export_batch
                         select new
                         {
                             Export_batch = a.Export_batch,
                             Content = a.Content,
                             Use_yn = a.Use_yn,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.Content.Contains(searchword)
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

        // GET api/export_set_batch/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ExportSetBatchsDto>> GetSingle(int id)
        {
            var result = from a in _test10Context.Export_set_batch
                         orderby a.Export_batch
                         select new
                         {
                             Export_batch = a.Export_batch,
                             Content = a.Content,
                             Use_yn = a.Use_yn,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
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

        // POST api/export_set_batch
        /*上傳json格式
        {
            "Export_batch": 3,
            "Content": "test",
            "Use_yn": false
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ExportSetBatchsDto value)
        {
            try
            {
                Export_set_batch insert = new Export_set_batch
                {
                    Export_batch = value.Export_batch,
                    Content = value.Content,
                    Use_yn = value.Use_yn,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Export_set_batch.Add(insert);
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

        // PUT api/export_set_batch/{id}
        /*上傳json格式
        {
            "Export_batch": 3,
            "Content": "test",
            "Use_yn": false
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ExportSetBatchsDto value)
        {
            var result = (from a in _test10Context.Export_set_batch
                          where a.Export_batch == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.Content = value.Content;
                    result.Use_yn = value.Use_yn;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Export_set_batch.Update(result);
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

        // DELETE api/export_set_batch/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _test10Context.Export_set_batch
                          where a.Export_batch == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _test10Context.Export_set_batch.Remove(result);
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
