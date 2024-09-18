using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/voice_type")]
    [ApiController]
    public class VoiceTypesController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public VoiceTypesController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/voice_type
        [HttpGet]
        public ActionResult<IEnumerable<VoiceTypesDto>> Get(string? searchword, string? UseYN)
        {
            var result = from a in _test10Context.Voice_type
                         orderby a.Sort
                         select new
                         {
                             Voice_id = a.Voice_id,
                             Name = a.Name,
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
                         a.Content.Contains(searchword)
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

        // GET api/voice_type/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<VoiceTypesDto>> Get(int id)
        {
            var result = from a in _test10Context.Voice_type
                         orderby a.Sort
                         select new
                         {
                             Voice_id = a.Voice_id,
                             Name = a.Name,
                             Content = a.Content,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(a => a.Voice_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/voice_type
        /*上傳json格式
        {
            "Voice_id": 4,
            "Name": "test",
            "Content": "test1",
            "use_yn": false,
            "sort": 0
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] VoiceTypesDto value)
        {
            var isExists = _test10Context.Voice_type.Any(a => a.Voice_id == value.Voice_id);

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Voice_type insert = new Voice_type
                {
                    Voice_id = value.Voice_id,
                    Name = value.Name,
                    Content = value.Content,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Voice_type.Add(insert);
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

        // PUT api/voice_type/{id}
        /*上傳json格式
        {
            "Voice_id": 4,
            "Name": "test",
            "Content": "test1",
            "use_yn": false,
            "sort": 0
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VoiceTypesDto value)
        {
            var result = (from a in _test10Context.Voice_type
                          where a.Voice_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.Voice_id = value.Voice_id;
                    result.Name = value.Name;
                    result.Content = value.Content;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Voice_type.Update(result);
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

        // DELETE api/voice_type/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _test10Context.Voice_type
                          where a.Voice_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _test10Context.Voice_type.Remove(result);
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
