using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/export_set_date")]
    [ApiController]
    public class ExportSetDatesController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public ExportSetDatesController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/export_set_date
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<ExportSetDatesDto>> Get(string? UseYN)
        {
            var result = from a in _GalDBContext.Export_set_date
                         orderby a.Date_mark descending
                         select new
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Date_mark = a.Date_mark,
                             Use_yn = a.Use_yn,
                         };

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

        // GET api/export_set_date/{id}
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<ExportSetDatesDto>> Get(int id)
        {
            var result = from a in _GalDBContext.Export_set_date
                         select new
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Date_mark = a.Date_mark,
                             Use_yn = a.Use_yn,
                         };

            if (id != null)
            {
                result = result.Where(a => a.Id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/export_set_date/getlast
        [HttpGet("getlast")]
        [Authorize]
        public ActionResult<DateTime> GetLast()
        {
            try
            {
                var result = _GalDBContext.Export_set_date
                            .Where(t => t.Use_yn == true)
                            .Max(t => t.Date_mark);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return Ok(0);
            }
        }

        // POST api/export_set_date
        /*上傳json格式
        {
            "Name": "test",
            "Date_mark": "2024-11-28T09:00:38.463",
            "use_yn": true
        }
        */
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] ExportSetDatesDto value)
        {
            try
            {
                Export_set_date insert = new Export_set_date
                {
                    Name = value.Name,
                    Date_mark = value.Date_mark,
                    Use_yn = value.Use_yn,
                };
                _GalDBContext.Export_set_date.Add(insert);
                _GalDBContext.SaveChanges();

                // 回傳成功訊息
                return Ok(new { message = "Y#資料上傳成功" });
            }
            catch (Exception ex)
            {
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return BadRequest(new { message = "N#資料上傳失敗", error = ex.Message });
            }

        }

        // PUT api/export_set_date/{id}
        /*上傳json格式
        {
            "Name": "test",
            "Date_mark": "2024-11-28T09:00:38.463",
            "use_yn": true
        }
        */
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] ExportSetDatesDto value)
        {
            var result = (from a in _GalDBContext.Export_set_date
                          where a.Id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.Name = value.Name;
                    result.Date_mark = value.Date_mark;
                    result.Use_yn = value.Use_yn;

                    _GalDBContext.Export_set_date.Update(result);
                    _GalDBContext.SaveChanges();

                    // 回傳成功訊息
                    return Ok(new { message = "Y#資料更新成功" });
                }
                catch (Exception ex)
                {
                    // 捕捉錯誤並回傳詳細的錯誤訊息
                    return BadRequest(new { message = "N#資料更新失敗", error = ex.Message });
                }
            }
        }

        // DELETE api/export_set_date/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Export_set_date
                          where a.Id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _GalDBContext.Export_set_date.Remove(result);
                    _GalDBContext.SaveChanges();

                    // 回傳成功訊息
                    return Ok(new { message = "Y#資料刪除成功" });
                }
                catch (DbUpdateException dbEx)
                {
                    // 解析內部例外狀況
                    var innerException = dbEx.InnerException?.Message;
                    if (innerException != null && innerException.Contains("REFERENCE"))
                    {
                        // 如果內部訊息包含外鍵約束的提示，回傳更具體的錯誤訊息
                        return Ok(new { message = "N#資料刪除失敗，此資料正在被其他表引用，無法刪除。" });
                    }
                    else
                    {
                        // 捕捉其他例外並回傳詳細錯誤訊息
                        return BadRequest(new { message = "N#資料刪除失敗", error = innerException ?? dbEx.Message });
                    }
                }
                catch (Exception ex)
                {
                    // 捕捉錯誤並回傳詳細的錯誤訊息
                    return BadRequest(new { message = "N#資料刪除失敗", error = ex.Message });
                }
            }
        }
    }
}
