using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/translation_team_info")]
    [ApiController]
    public class TranslationTeamInfosController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public TranslationTeamInfosController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/translation_team_info
        [HttpGet]
        public ActionResult<IEnumerable<TranslationTeamInfosDto>> Get(string? searchword)
        {
            var result = from a in _test10Context.Translation_team_info
                         orderby a.T_id
                         select new
                         {
                             Id = a.Id,
                             T_id = a.T_id,
                             Name = a.Name,
                             Content = a.Content,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.T_id.Contains(searchword) ||
                         a.Name.Contains(searchword)  
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/translation_team_info/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<TranslationTeamInfosDto>> GetSingle(string id)
        {
            var result = from a in _test10Context.Translation_team_info
                         orderby a.T_id
                         select new
                         {
                             Id = a.Id,
                             T_id = a.T_id,
                             Name = a.Name,
                             Content = a.Content,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(a => a.T_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/translation_team_info
        /*上傳json格式
        {
            "T_id": "T00000",
            "Name": "測試",
            "Content": "test1"
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] TranslationTeamInfosDto value)
        {
            var isExists = _test10Context.Translation_team_info.Any(a => a.T_id == value.T_id);

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Translation_team_info insert = new Translation_team_info
                {
                    T_id = value.T_id,
                    Name = value.Name,
                    Content = value.Content,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Translation_team_info.Add(insert);
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

        // PUT api/translation_team_info/{id}
        /*上傳json格式
        {
            "T_id": "T00000",
            "Name": "測試",
            "Content": "test1"
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] TranslationTeamInfosDto value)
        {
            var result = (from a in _test10Context.Translation_team_info
                          where a.T_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.T_id = value.T_id;
                    result.Name = value.Name;
                    result.Content = value.Content;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Translation_team_info.Update(result);
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

        // DELETE api/translation_team_info/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = (from a in _test10Context.Translation_team_info
                          where a.T_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _test10Context.Translation_team_info.Remove(result);
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
