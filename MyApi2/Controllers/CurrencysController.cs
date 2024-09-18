using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/currency")]
    [ApiController]
    public class CurrencysController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public CurrencysController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/currency
        [HttpGet]
        public ActionResult<IEnumerable<CurrencysDto>> Get(string? searchword, string? UseYN)
        {
            var result = from a in _test10Context.Currency
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

        // GET api/currency/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CurrencysDto>> Get(string id)
        {
            var result = from a in _test10Context.Currency
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
        public IActionResult Post([FromBody] CurrencysDto value)
        {
            var isExists = _test10Context.Currency.Any(a => a.Currency_id == value.Currency_id);

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
                _test10Context.Currency.Add(insert);
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
        public IActionResult Put(string id, [FromBody] CurrencysDto value)
        {
            var result = (from a in _test10Context.Currency
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
                    result.Create_dt = DateTime.Now;

                    _test10Context.Currency.Update(result);
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

        // DELETE api/currency/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = (from a in _test10Context.Currency
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
                    _test10Context.Currency.Remove(result);
                    _test10Context.SaveChanges();

                    // 回傳成功訊息
                    return Ok(new { message = "Y#資料刪除成功" });
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
