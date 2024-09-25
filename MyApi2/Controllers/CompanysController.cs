using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanysController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public CompanysController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/company
        [HttpGet]
        public ActionResult<IEnumerable<CompanysDto>> Get(string? searchword, int? C_type)
        {
            var result = from a in _test10Context.Company
                         join b in _test10Context.Company_type on a.C_type equals b.C_type
                         orderby a.C_type, a.C_id
                         select new
                         {
                             Id = a.Id,
                             C_id = a.C_id,
                             C_type = a.C_type,
                             Name = a.Name,
                             Name_origin = a.Name_origin,
                             Name_short = a.Name_short,
                             Intro = a.Intro,
                             Remark = a.Remark,
                             Sdate = a.Sdate,
                             Edate = a.Edate,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             C_type_name = b.C_type_name,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.C_id.Contains(searchword) ||
                         a.Name.Contains(searchword) ||
                         a.Name_origin.Contains(searchword) ||
                         a.Name_short.Contains(searchword)
                );
            }

            if (C_type != null)
            {
                result = result.Where(a => a.C_type == C_type);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/company/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CompanysDto>> GetSingle(string id)
        {
            var result = from a in _test10Context.Company
                         join b in _test10Context.Company_type on a.C_type equals b.C_type
                         orderby a.C_type, a.C_id
                         select new
                         {
                             Id = a.Id,
                             C_id = a.C_id,
                             C_type = a.C_type,
                             Name = a.Name,
                             Name_origin = a.Name_origin,
                             Name_short = a.Name_short,
                             Intro = a.Intro,
                             Remark = a.Remark,
                             Sdate = a.Sdate,
                             Edate = a.Edate,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             C_type_name = b.C_type_name,
                         };

            if (id != null)
            {
                result = result.Where(a => a.C_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/company/getnewcid
        [HttpGet("getnewcid")]
        public ActionResult<string> GetNewcid()
        {
            var result = (from a in _test10Context.Company
                          orderby a.C_id descending
                          select a.C_id).FirstOrDefault();

            if (result == null)
            {
                return "NAN";
            }

            return result;
        }

        // POST api/company
        /*上傳json格式
        {
            "C_id": "C000000000",
            "C_type": 1,
            "Name": "test",
            "Name_origin": "test1",
            "Name_short": "test2",
            "Intro": "test3",
            "Remark": "",
            "Sdate": "",
            "Edate": ""
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] CompanysDto value)
        {
            var isExists = _test10Context.Company.Any(a => a.C_id == value.C_id);

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Company insert = new Company
                {
                    C_id = value.C_id,
                    C_type = value.C_type,
                    Name = value.Name,
                    Name_origin = value.Name_origin,
                    Name_short = value.Name_short,
                    Intro = value.Intro,
                    Remark = value.Remark,
                    Sdate = value.Sdate,
                    Edate = value.Edate,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Company.Add(insert);
                _test10Context.SaveChanges();

                // 回傳成功訊息
                return Ok(new { message = "Y#資料上傳成功" });
            }
            catch (Exception ex)
            {
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return BadRequest(new { message = "N#資料上傳失敗", error = ex.Message });
            }
        }

        // PUT api/company/{id}
        /*上傳json格式
        {
            "C_id": "C000000000",
            "C_type": 1,
            "Name": "test",
            "Name_origin": "test1",
            "Name_short": "test2",
            "Intro": "test3",
            "Remark": "",
            "Sdate": "",
            "Edate": ""
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] CompanysDto value)
        {
            var result = (from a in _test10Context.Company
                          where a.C_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.C_id = value.C_id;
                    result.C_type = value.C_type;
                    result.Name = value.Name;
                    result.Name_origin = value.Name_origin;
                    result.Name_short = value.Name_short;
                    result.Intro = value.Intro;
                    result.Remark = value.Remark;
                    result.Sdate = value.Sdate;
                    result.Edate = value.Edate;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Company.Update(result);
                    _test10Context.SaveChanges();

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

        // DELETE api/company/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = (from a in _test10Context.Company
                          where a.C_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _test10Context.Company.Remove(result);
                    _test10Context.SaveChanges();

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
