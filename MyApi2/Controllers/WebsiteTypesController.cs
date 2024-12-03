using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/website_type")]
    [ApiController]
    public class WebsiteTypesController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public WebsiteTypesController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/website_type
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<WebsiteTypesDto>> Get(string? searchword, string? UseYN)
        {
            var result = from a in _GalDBContext.Website_Type
                         orderby a.Sort
                         select new
                         {
                             Type_id = a.Type_id,
                             Name = a.Name,
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
                    a => a.Name.Contains(searchword) ||
                         a.Type_id.Contains(searchword)
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

            return Ok(result.Take(500));
        }

        // GET: api/website_type/mainpage
        [HttpGet("mainpage")]
        [Authorize]
        public ActionResult<IEnumerable<WebsiteTypesDto>> mainpage(string? searchword, string? UseYN, int page = 1, int pageSize = 10)
        {
            var result = from a in _GalDBContext.Website_Type
                         orderby a.Sort
                         select new
                         {
                             Type_id = a.Type_id,
                             Name = a.Name,
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
                    a => a.Name.Contains(searchword) ||
                         a.Type_id.Contains(searchword)
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

            // 分頁處理
            var totalRecords = result.Count(); // 總記錄數
            var data = result.Skip((page - 1) * pageSize).Take(pageSize).ToList(); // 分頁數據

            // 回傳資料
            return Ok(new
            {
                TotalRecords = totalRecords, // 總記錄數
                Data = data                 // 分頁資料
            });
        }

        // GET api/website_type/{id}
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<WebsiteTypesDto>> GetSingle(string id)
        {
            var result = from a in _GalDBContext.Website_Type
                         orderby a.Sort
                         select new
                         {
                             Type_id = a.Type_id,
                             Name = a.Name,
                             Remark = a.Remark,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(a => a.Type_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/website_type
        /*上傳json格式
        {
            "Type_id": "C03",
            "Name": "test",
            "Remark": "test1",
            "use_yn": false,
            "sort": 0
        }
        */
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] WebsiteTypesDto value)
        {
            var isExists = _GalDBContext.Website_Type.Any(a => a.Type_id == value.Type_id);

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Website_Type insert = new Website_Type
                {
                    Type_id = value.Type_id,
                    Name = value.Name,
                    Remark = value.Remark,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Website_Type.Add(insert);
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

        // PUT api/website_type/{id}
        /*上傳json格式
        {
            "Type_id": "C03",
            "Name": "test",
            "Remark": "test1",
            "use_yn": false,
            "sort": 0
        }
        */
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(string id, [FromBody] WebsiteTypesDto value)
        {
            var result = (from a in _GalDBContext.Website_Type
                          where a.Type_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.Type_id = value.Type_id;
                    result.Name = value.Name;
                    result.Remark = value.Remark;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;

                    _GalDBContext.Website_Type.Update(result);
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

        // DELETE api/website_type/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(string id)
        {
            var result = (from a in _GalDBContext.Website_Type
                          where a.Type_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _GalDBContext.Website_Type.Remove(result);
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
