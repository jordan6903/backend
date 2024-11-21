using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApi2.Dtos;
using MyApi2.Models;
using System.Data.Common;
using System.Linq;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/translation_team")]
    [ApiController]
    public class TranslationTeamsController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public TranslationTeamsController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/translation_team
        [HttpGet]
        public ActionResult<IEnumerable<TranslationTeamsDto>> Get(string? c_search, string? p_search, string? t_search, int? type_id)
        {
            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Translation_team on a.P_id equals b.P_id into TT
                         join c in _GalDBContext.Company on a.C_id equals c.C_id
                         orderby a.C_id, a.P_id
                         select new
                         {
                             P_id = a.P_id,
                             P_Name = a.Name,
                             P_CName = a.C_Name,
                             C_id = a.C_id,
                             C_Name = c.Name,
                             T_batch_data = TT.Select(b => new TTviewsDto1
                             {
                                 Id = b.Id,
                                 T_batch = b.T_batch,
                                 Type_id = b.Type_id,
                                 Remark = b.Remark,
                                 Type_Name = (from c in _GalDBContext.Translation_team_type
                                              where c.Type_id == b.Type_id
                                              select c.Name).FirstOrDefault(),
                                 TT_info = (from d in _GalDBContext.Translation_team_batch
                                            where b.Id == d.TT_id
                                            select new TTviewsDto2
                                            {
                                                Id = d.Id,
                                                TT_Id = d.TT_id,
                                                T_id = d.T_id,
                                                T_Name = (from e in _GalDBContext.Translation_team_info
                                                          where e.T_id == d.T_id
                                                          select e.Name).FirstOrDefault(),
                                                P_id = "",
                                                T_batch = 0
                                            }).ToList()
                             }).ToList(),
                         };

            if (c_search != null)
            {
                result = result.Where(
                    a => a.C_id.Contains(c_search) ||
                         a.C_Name.Contains(c_search)
                );
            }

            if (p_search != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(p_search) ||
                         a.P_Name.Contains(p_search) ||
                         a.P_CName.Contains(p_search)
                );
            }

            var resultList = result.AsEnumerable().Where(a => a.T_batch_data != null && a.T_batch_data.Any()); //過濾掉空集合

            if (t_search != null)
            {
                resultList = resultList.Where(
                    a => a.T_batch_data.Any(
                        b => b.TT_info != null && b.TT_info.Any(
                            c => (c.T_id != null && c.T_id.Contains(t_search)) ||
                                 (c.T_Name != null && c.T_Name.Contains(t_search)))
                        )
                );
            }

            if (type_id != null)
            {
                resultList = resultList.Where(
                    a => a.T_batch_data.Any(b => b.Type_id == type_id)
                );
            }

            if (resultList == null)
            {
                return NotFound();
            }

            return Ok(resultList.Take(300));
        }

        // GET: api/translation_team
        [HttpGet]
        [Route("normal")]
        public ActionResult<IEnumerable<TranslationTeamsDto>> Getnormal(string? searchword, string? searchword2, int? type_id)
        {
            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Translation_team on a.P_id equals b.P_id
                         join c in _GalDBContext.Translation_team_batch on b.Id equals c.TT_id
                         join d in _GalDBContext.Translation_team_info on c.T_id equals d.T_id
                         join e in _GalDBContext.Translation_team_type on b.Type_id equals e.Type_id
                         orderby a.P_id
                         select new
                         {
                             P_id = a.P_id,
                             P_Name = a.Name,
                             P_CName = a.C_Name,
                             T_batch = b.T_batch,
                             T_id = c.T_id,
                             T_Name = d.Name,
                             Type_id = b.Type_id,
                             Type_Name = e.Name,
                             Remark = b.Remark,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(searchword) ||
                         a.P_Name.Contains(searchword) ||
                         a.P_CName.Contains(searchword)
                );
            }

            if (searchword2 != null)
            {
                result = result.Where(
                    a => a.T_id.Contains(searchword2) ||
                         a.T_Name.Contains(searchword2)
                );
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

        // GET api/translation_team/single/{id}
        [HttpGet("single/{id}")]
        public ActionResult<IEnumerable<TranslationTeamsDto>> GetSingle(string id)
        {
            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Translation_team on a.P_id equals b.P_id into TT
                         join c in _GalDBContext.Company on a.C_id equals c.C_id
                         orderby a.P_id
                         select new
                         {
                             P_id = a.P_id,
                             P_Name = a.Name,
                             P_CName = a.C_Name,
                             C_id = a.C_id,
                             C_Name = c.Name,
                             T_batch_data = TT.Select(b => new TTviewsDto1
                             {
                                 Id = b.Id,
                                 T_batch = b.T_batch,
                                 Type_id = b.Type_id,
                                 Remark = b.Remark,
                                 Type_Name = (from c in _GalDBContext.Translation_team_type
                                              where c.Type_id == b.Type_id
                                              select c.Name).FirstOrDefault(),
                                 TT_info = (from d in _GalDBContext.Translation_team_batch
                                            where b.Id == d.TT_id
                                            select new TTviewsDto2
                                            {
                                                T_id = d.T_id,
                                                T_Name = (from e in _GalDBContext.Translation_team_info
                                                          where e.T_id == d.T_id
                                                          select e.Name).FirstOrDefault(),
                                            }).ToList()
                             }).ToList(),
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(id)
                );
            }

            var resultList = result.AsEnumerable();

            if (resultList == null)
            {
                return NotFound();
            }

            return Ok(resultList);
        }

        // GET api/translation_team/singlebyid/{id}
        [HttpGet("singlebyid/{id}")]
        public ActionResult<IEnumerable<TranslationTeamsDto>> GetSingleById(int id)
        {
            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Translation_team on a.P_id equals b.P_id into TT
                         join c in _GalDBContext.Company on a.C_id equals c.C_id
                         orderby a.P_id
                         select new
                         {
                             P_id = a.P_id,
                             P_Name = a.Name,
                             P_CName = a.C_Name,
                             C_id = a.C_id,
                             C_Name = c.Name,
                             T_batch_data = TT.Select(b => new TTviewsDto1
                             {
                                 Id = b.Id,
                                 T_batch = b.T_batch,
                                 Type_id = b.Type_id,
                                 Remark = b.Remark,
                                 Type_Name = (from c in _GalDBContext.Translation_team_type
                                              where c.Type_id == b.Type_id
                                              select c.Name).FirstOrDefault(),
                                 TT_info = (from d in _GalDBContext.Translation_team_batch
                                            where b.Id == d.TT_id
                                            select new TTviewsDto2
                                            {
                                                T_id = d.T_id,
                                                T_Name = (from e in _GalDBContext.Translation_team_info
                                                          where e.T_id == d.T_id
                                                          select e.Name).FirstOrDefault(),
                                            }).ToList()
                             }).ToList(),
                         };

            var resultList = result.AsEnumerable();

            if (id != null)
            {
                resultList = resultList.Where(
                    a => a.T_batch_data.Any( b => b.Id == id)
                );
            }

            if (resultList == null)
            {
                return NotFound();
            }

            return Ok(resultList);
        }

        // GET api/translation_team/getnewid
        [HttpGet("getnewid")]
        public ActionResult<string> GetMaxTbatch()
        {
            try
            {
                var maxId = _GalDBContext.Translation_team
                            .Max(t => t.Id);

                return Ok(maxId);
            }
            catch (Exception ex)
            {
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return Ok(0);
            }
        }

        // GET api/translation_team/getmaxtbatch/{id}
        [HttpGet("getmaxtbatch/{id}")]
        public ActionResult<string> GetMaxTbatch(string id)
        {
            try
            {
                var maxTbatch = _GalDBContext.Translation_team
                            .Where(t => t.P_id == id)
                            .Max(t => t.T_batch);

                return Ok(maxTbatch);
            }
            catch (Exception ex)
            {
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return Ok(0);
            }
        }

        // GET api/translation_team/deletechk/{id}
        [HttpGet("deletechk/{id}")]
        public ActionResult<IEnumerable<TranslationTeamsDto>> DeleteChk(string id)
        {
            var result = from a in _GalDBContext.Translation_team
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             Type_id = a.Type_id,
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.P_id == id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/translation_team
        /*上傳json格式
        {
            "P_id": "A000000001",
            "T_batch": 2,
            "Type_id": 2,
            "Remark": "test"
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] TranslationTeamsDto value)
        {
            var isExists = _GalDBContext.Translation_team.Any(
                a => a.P_id == value.P_id &&
                     a.T_batch == value.T_batch
             );

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Translation_team insert = new Translation_team
                {
                    P_id = value.P_id,
                    T_batch = value.T_batch,
                    Type_id = value.Type_id,
                    Remark = value.Remark,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Translation_team.Add(insert);
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

        // PUT api/translation_team/{id}
        /*上傳json格式
        {
            "P_id": "A000000001",
            "T_batch": 2,
            "Type_id": 2,
            "Remark": "test"
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TranslationTeamsDto value)
        {
            var result = (from a in _GalDBContext.Translation_team
                          where a.Id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.P_id = value.P_id;
                    result.T_batch = value.T_batch;
                    result.Type_id = value.Type_id;
                    result.Remark = value.Remark;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _GalDBContext.Translation_team.Update(result);
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

        // DELETE api/translation_team/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Translation_team
                          where a.Id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _GalDBContext.Translation_team.Remove(result);
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
