using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;
using System.Data.Common;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/translation_team_batch")]
    [ApiController]
    public class TranslationTeamBatchsController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public TranslationTeamBatchsController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/translation_team_batch
        [HttpGet]
        public ActionResult<IEnumerable<TranslationTeamBatchsDto>> Get(string? searchword)
        {
            var result = from a in _test10Context.Translation_team_batch
                         join b in _test10Context.Translation_team_info on a.T_id equals b.T_id
                         join c in _test10Context.Translation_team on a.TT_id equals c.Id
                         join d in _test10Context.Product on c.P_id equals d.P_id
                         orderby a.P_id, a.T_batch
                         select new
                         {
                             Id = a.Id,
                             TT_id = a.TT_id,
                             P_id = c.P_id,
                             T_batch = c.T_batch,
                             P_Name = d.Name,
                             T_id = a.T_id,
                             T_Name = b.Name,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            //if (searchword != null)
            //{
            //    result = result.Where(
            //        a => a.P_id.Contains(searchword) ||
            //             a.P_Name.Contains(searchword)
            //    );
            //}

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.Take(300));
        }

        // GET api/translation_team_batch/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<TranslationTeamBatchsDto>> GetSingle(int id)
        {
            var result = from a in _test10Context.Translation_team_batch
                         join b in _test10Context.Product on a.P_id equals b.P_id
                         join c in _test10Context.Translation_team_info on a.T_id equals c.T_id
                         orderby a.P_id, a.T_batch
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = b.Name,
                             T_batch = a.T_batch,
                             T_id = a.T_id,
                             T_Name = c.Name,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(a => a.Id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/translation_team_batch
        /*上傳json格式
        {
            "P_id": "A000000001",
            "T_batch": 2,
            "T_id": "T00514"
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] TranslationTeamBatchsDto value)
        {
            try
            {
                Translation_team_batch insert = new Translation_team_batch
                {
                    P_id = value.P_id,
                    T_batch = value.T_batch,
                    T_id = value.T_id,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Translation_team_batch.Add(insert);
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

        // PUT api/translation_team_batch/{id}
        /*上傳json格式
        {
            "P_id": "A000000001",
            "T_batch": 2,
            "T_id": "T00514"
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TranslationTeamBatchsDto value)
        {
            var result = (from a in _test10Context.Translation_team_batch
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
                    result.P_id = value.P_id;
                    result.T_batch = value.T_batch;
                    result.T_id = value.T_id;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Translation_team_batch.Update(result);
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

        // DELETE api/translation_team_batch/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _test10Context.Translation_team_batch
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
                    _test10Context.Translation_team_batch.Remove(result);
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
