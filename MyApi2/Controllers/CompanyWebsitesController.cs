using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/company_website")]
    [ApiController]
    public class CompanyWebsitesController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public CompanyWebsitesController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/company_website
        [HttpGet]
        public ActionResult<IEnumerable<CompanyWebsitesDto>> Get(string? searchword, string? UseYN, string? type_id)
        {
            var result = from a in _test10Context.Company_Website
                         join b in _test10Context.Website_Type on a.Type_id equals b.Type_id
                         orderby a.C_id, a.Sort
                         select new
                         {
                             Id = a.Id,
                             C_id = a.C_id,
                             Type_id = a.Type_id,
                             Type_Name = b.Name,
                             Name = a.Name,
                             Url = a.Url,
                             Remark = a.Remark,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.C_id.Contains(searchword)
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

            if (type_id != null)
            {
                result = result.Where(
                    a => a.Type_id == type_id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/company_website/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CompanyWebsitesDto>> Get(string id)
        {
            var result = from a in _test10Context.Company_Website
                         join b in _test10Context.Website_Type on a.Type_id equals b.Type_id
                         orderby a.C_id, a.Sort
                         select new
                         {
                             Id = a.Id,
                             C_id = a.C_id,
                             Type_id = a.Type_id,
                             Type_Name = b.Name,
                             Name = a.Name,
                             Url = a.Url,
                             Remark = a.Remark,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.C_id == id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/company_website
        /*上傳json格式
        {
            "C_id": "C000000002",
            "Type_id": "C02",
            "Name": "test",
            "Url": "https://x.com/august_soft",
            "Remark": "test123",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] CompanyWebsitesDto value)
        {
            try
            {
                Company_Website insert = new Company_Website
                {
                    C_id = value.C_id,
                    Type_id = value.Type_id,
                    Name = value.Name,
                    Url = value.Url,
                    Remark = value.Remark,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Company_Website.Add(insert);
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

        // PUT api/company_website/{id}
        /*上傳json格式
        {
            "C_id": "C000000002",
            "Type_id": "C02",
            "Name": "test",
            "Url": "https://x.com/august_soft",
            "Remark": "test123",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CompanyWebsitesDto value)
        {
            var result = (from a in _test10Context.Company_Website
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
                    result.C_id = value.C_id;
                    result.Type_id = value.Type_id;
                    result.Name = value.Name;
                    result.Url = value.Url;
                    result.Remark = value.Remark;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Company_Website.Update(result);
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

        // DELETE api/company_website/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _test10Context.Company_Website
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
                    _test10Context.Company_Website.Remove(result);
                    _test10Context.SaveChanges();

                    // 回傳成功訊息
                    return Ok(new { message = "資料刪除成功" });
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
                    return BadRequest(new { message = "資料刪除失敗", error = ex.Message });
                }
            }
        }
    }
}
