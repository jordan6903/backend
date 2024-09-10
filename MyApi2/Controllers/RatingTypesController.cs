using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/rating_type")]
    [ApiController]
    public class RatingTypesController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public RatingTypesController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/rating_type
        [HttpGet]
        public ActionResult<IEnumerable<RatingTypesDto>> Get(string? searchword, string? UseYN)
        {
            var result = from a in _test10Context.Rating_type
                         orderby a.Sort
                         select new
                         {
                             Rating_type = a.Rating_type1,
                             Name = a.Name,
                             ShortName = a.ShortName,
                             Content = a.Content,
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
                         a.ShortName.Contains(searchword)
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

        // GET api/rating_type/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<RatingTypesDto>> Get(int id)
        {
            var result = from a in _test10Context.Rating_type
                         orderby a.Sort
                         select new
                         {
                             Rating_type = a.Rating_type1,
                             Name = a.Name,
                             ShortName = a.ShortName,
                             Content = a.Content,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(a => a.Rating_type == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/rating_type
        /*上傳json格式
        {
            "rating_type1": 4,
            "name": "test",
            "shortName": "test1",
            "content": "123",
            "use_yn": false,
            "sort": 999
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] RatingTypesDto value)
        {
            try
            {
                Rating_type insert = new Rating_type
                {
                    Rating_type1 = value.Rating_type1,
                    Name = value.Name,
                    ShortName = value.ShortName,
                    Content = value.Content,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Rating_type.Add(insert);
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

        // PUT api/rating_type/{id}
        /*上傳json格式
        {
            "rating_type1": 4,
            "name": "test",
            "shortName": "test1",
            "content": "123",
            "use_yn": false,
            "sort": 999
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RatingTypesDto value)
        {
            var result = (from a in _test10Context.Rating_type
                          where a.Rating_type1 == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.Rating_type1 = value.Rating_type1;
                    result.Name = value.Name;
                    result.ShortName = value.ShortName;
                    result.Content = value.Content;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Rating_type.Update(result);
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

        // DELETE api/rating_type/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _test10Context.Rating_type
                          where a.Rating_type1 == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _test10Context.Rating_type.Remove(result);
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
