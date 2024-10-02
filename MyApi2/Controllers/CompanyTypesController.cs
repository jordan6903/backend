using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/company_type")]
    [ApiController]
    public class CompanyTypesController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public CompanyTypesController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/company_type
        [HttpGet]
        public ActionResult<IEnumerable<CompanyTypesDto>> Get(string? searchword, string? UseYN)
        {
            var result = from a in _GalDBContext.Company_type
                         orderby a.Sort
                         select new
                         {
                             C_type = a.C_type,
                             C_type_name = a.C_type_name,
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
                    a => a.C_type_name.Contains(searchword)
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

        // GET api/company_type/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CompanyTypesDto>> GetSingle(int id)
        {
            var result = from a in _GalDBContext.Company_type
                         orderby a.Sort
                         select new
                         {
                             C_type = a.C_type,
                             C_type_name = a.C_type_name,
                             Remark = a.Remark,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(a => a.C_type == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/company_type
        /*上傳json格式
        {
            "C_type": 4,
            "C_type_name": "test",
            "Remark": "test1",
            "use_yn": false,
            "sort": 0
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] CompanyTypesDto value)
        {
            var isExists = _GalDBContext.Company_type.Any(a => a.C_type == value.C_type);

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Company_type insert = new Company_type
                {
                    C_type = value.C_type,
                    C_type_name = value.C_type_name,
                    Remark = value.Remark,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Company_type.Add(insert);
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

        // PUT api/company_type/{id}
        /*上傳json格式
        {
            "C_type": 4,
            "C_type_name": "test",
            "Remark": "test1",
            "use_yn": false,
            "sort": 0
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CompanyTypesDto value)
        {
            var result = (from a in _GalDBContext.Company_type
                          where a.C_type == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.C_type = value.C_type;
                    result.C_type_name = value.C_type_name;
                    result.Remark = value.Remark;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _GalDBContext.Company_type.Update(result);
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

        // DELETE api/company_type/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Company_type
                          where a.C_type == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _GalDBContext.Company_type.Remove(result);
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
