using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/rating")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public RatingsController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RatingsDto>> Get(string? searchword, string? UseYN, int? Rating_type)
        {
            var result = from a in _GalDBContext.Rating
                         join b in _GalDBContext.Rating_type on a.Rating_type equals b.Rating_type1
                         orderby a.Rating_type, a.Sort
                         select new
                         {
                             Rating_id = a.Rating_id,
                             Rating_type = a.Rating_type,
                             Name = a.Name,
                             ShortName = a.ShortName,
                             Name_JP = a.Name_JP,
                             Name_EN = a.Name_EN,
                             Content = a.Content,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Type_Name = b.Name,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.Name.Contains(searchword) ||
                         a.ShortName.Contains(searchword) ||
                         a.Name_JP.Contains(searchword) ||
                         a.Name_EN.Contains(searchword)
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

            if (Rating_type != null)
            {
                result = result.Where(a => a.Rating_type == Rating_type);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("mainpage")]
        public ActionResult<IEnumerable<RatingsDto>> mainpage(string? searchword, string? UseYN, int? Rating_type, int page = 1, int pageSize = 10)
        {
            var result = from a in _GalDBContext.Rating
                         join b in _GalDBContext.Rating_type on a.Rating_type equals b.Rating_type1
                         orderby a.Rating_type, a.Sort
                         select new
                         {
                             Rating_id = a.Rating_id,
                             Rating_type = a.Rating_type,
                             Name = a.Name,
                             ShortName = a.ShortName,
                             Name_JP = a.Name_JP,
                             Name_EN = a.Name_EN,
                             Content = a.Content,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Type_Name = b.Name,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.Name.Contains(searchword) ||
                         a.ShortName.Contains(searchword) ||
                         a.Name_JP.Contains(searchword) ||
                         a.Name_EN.Contains(searchword)
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

            if (Rating_type != null)
            {
                result = result.Where(a => a.Rating_type == Rating_type);
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

        // GET api/rating
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<DevicesDto>> Get(int id)
        {
            var result = from a in _GalDBContext.Rating
                         join b in _GalDBContext.Rating_type on a.Rating_type equals b.Rating_type1
                         orderby a.Rating_type, a.Sort
                         select new
                         {
                             Rating_id = a.Rating_id,
                             Rating_type = a.Rating_type,
                             Name = a.Name,
                             ShortName = a.ShortName,
                             Name_JP = a.Name_JP,
                             Name_EN = a.Name_EN,
                             Content = a.Content,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             Type_Name = b.Name,
                         };

            if (id != null)
            {
                result = result.Where(a => a.Rating_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }



        // POST api/rating
        /*上傳json格式
        {
            "rating_id": 15,
            "rating_type": 0,
            "name": "test",
            "shortName": "test1",
            "name_JP": "11",
            "name_EN": "",
            "content": "",
            "use_yn": false,
            "sort": 999
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] RatingsDto value)
        {
            var isExists = _GalDBContext.Rating.Any(a => a.Rating_id == value.Rating_id);

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Rating insert = new Rating
                {
                    Rating_id = value.Rating_id,
                    Rating_type = value.Rating_type,
                    Name = value.Name,
                    ShortName = value.ShortName,
                    Name_JP = value.Name_JP,
                    Name_EN = value.Name_EN,
                    Content = value.Content,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Rating.Add(insert);
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

        // PUT api/rating/{id}
        /*上傳json格式
        {
            "rating_id": 15,
            "rating_type": 0,
            "name": "test1",
            "shortName": "test2",
            "name_JP": "11",
            "name_EN": "",
            "content": "",
            "use_yn": false,
            "sort": 999
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RatingsDto value)
        {
            var result = (from a in _GalDBContext.Rating
                          where a.Rating_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.Rating_id = value.Rating_id;
                    result.Rating_type = value.Rating_type;
                    result.Name = value.Name;
                    result.ShortName = value.ShortName;
                    result.Name_JP = value.Name_JP;
                    result.Name_EN = value.Name_EN;
                    result.Content = value.Content;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;

                    _GalDBContext.Rating.Update(result);
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

        // DELETE api/rating/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Rating
                          where a.Rating_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _GalDBContext.Rating.Remove(result);
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
