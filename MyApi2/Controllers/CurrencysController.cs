﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/currency")]
    [ApiController]
    public class CurrencysController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public CurrencysController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/currency
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<CurrencysDto>> Get(string? searchword, string? UseYN)
        {
            var result = from a in _GalDBContext.Currency
                         orderby a.Sort
                         select new
                         {
                             Currency_id = a.Currency_id,
                             FullName = a.FullName,
                             ShortName = a.ShortName,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.FullName.Contains(searchword) ||
                         a.ShortName.Contains(searchword) ||
                         a.Currency_id.Contains(searchword)
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

        // GET: api/currency
        [HttpGet("mainpage")]
        [Authorize]
        public ActionResult<IEnumerable<CurrencysDto>> mainpage(string? searchword, string? UseYN, int page = 1, int pageSize = 10)
        {
            var result = from a in _GalDBContext.Currency
                         orderby a.Sort
                         select new
                         {
                             Currency_id = a.Currency_id,
                             FullName = a.FullName,
                             ShortName = a.ShortName,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.FullName.Contains(searchword) ||
                         a.ShortName.Contains(searchword) || 
                         a.Currency_id.Contains(searchword)
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

        // GET api/currency/{id}
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<CurrencysDto>> Get(string id)
        {
            var result = from a in _GalDBContext.Currency
                         orderby a.Sort
                         select new
                         {
                             Currency_id = a.Currency_id,
                             FullName = a.FullName,
                             ShortName = a.ShortName,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(a => a.Currency_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/currency
        /*上傳json格式
        {
            "Currency_id": "TET",
            "FullName": "test",
            "shortName": "test1",
            "use_yn": false,
            "sort": 0
        }
        */
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CurrencysDto value)
        {
            var isExists = _GalDBContext.Currency.Any(a => a.Currency_id == value.Currency_id);

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Currency insert = new Currency
                {
                    Currency_id = value.Currency_id,
                    FullName = value.FullName,
                    ShortName = value.ShortName,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Currency.Add(insert);
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

        // PUT api/currency/{id}
        /*上傳json格式
        {
            "Currency_id": "TET",
            "FullName": "test",
            "shortName": "test1",
            "use_yn": false,
            "sort": 0
        }
        */
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(string id, [FromBody] CurrencysDto value)
        {
            var result = (from a in _GalDBContext.Currency
                          where a.Currency_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.Currency_id = value.Currency_id;
                    result.FullName = value.FullName;
                    result.ShortName = value.ShortName;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;

                    _GalDBContext.Currency.Update(result);
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

        // DELETE api/currency/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(string id)
        {
            var result = (from a in _GalDBContext.Currency
                          where a.Currency_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _GalDBContext.Currency.Remove(result);
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
