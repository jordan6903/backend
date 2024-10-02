using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/company_relation")]
    [ApiController]
    public class CompanyRelationsController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public CompanyRelationsController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/company_relation
        [HttpGet]
        public ActionResult<IEnumerable<CompanyRelationsDto>> Get(string? searchword, int? relation_id)
        {
            var result = from a in _GalDBContext.Company_relation
                         join b in _GalDBContext.Company on a.C_id equals b.C_id
                         join c in _GalDBContext.Company on a.C_id_to equals c.C_id
                         join d in _GalDBContext.Company_relation_info on a.Relation_id equals d.Relation_id
                         orderby a.C_id, a.Relation_id
                         select new
                         {
                             Id = a.Id,
                             C_id = a.C_id,
                             C_Name = b.Name,
                             C_id_to = a.C_id_to,
                             C_Name_to = c.Name,
                             Relation_id = a.Relation_id,
                             Relation_Name = d.Name,
                             Content = a.Content,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.C_id.Contains(searchword) ||
                         a.C_id_to.Contains(searchword) ||
                         a.C_Name.Contains(searchword) ||
                         a.C_Name_to.Contains(searchword)
                );
            }

            if (relation_id != null)
            {
                result = result.Where(
                    a => a.Relation_id == relation_id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/company_relation/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CompanyRelationsDto>> GetSingle(int id)
        {
            var result = from a in _GalDBContext.Company_relation
                         join b in _GalDBContext.Company on a.C_id equals b.C_id
                         join c in _GalDBContext.Company on a.C_id_to equals c.C_id
                         join d in _GalDBContext.Company_relation_info on a.Relation_id equals d.Relation_id
                         orderby a.C_id, a.Relation_id
                         select new
                         {
                             Id = a.Id,
                             C_id = a.C_id,
                             C_Name = b.Name,
                             C_id_to = a.C_id_to,
                             C_Name_to = c.Name,
                             Relation_id = a.Relation_id,
                             Relation_Name = d.Name,
                             Content = a.Content,
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

        // POST api/company_relation
        /*上傳json格式
        {
            "C_id": "C000000066",
            "C_id_to": "C000000069",
            "Relation_id": "2",
            "Content": "test1"
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] CompanyRelationsDto value)
        {
            var isExists = _GalDBContext.Company_relation.Any(
                    a => a.C_id == value.C_id &&
                         a.C_id_to == value.C_id_to &&
                         a.Relation_id == value.Relation_id
                );

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Company_relation insert = new Company_relation
                {
                    C_id = value.C_id,
                    C_id_to = value.C_id_to,
                    Relation_id = value.Relation_id,
                    Content = value.Content,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Company_relation.Add(insert);
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

        // PUT api/company_relation/{id}
        /*上傳json格式
        {
            "C_id": "C000000066",
            "C_id_to": "C000000069",
            "Relation_id": "2",
            "Content": "test1"
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CompanyRelationsDto value)
        {
            var result = (from a in _GalDBContext.Company_relation
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
                    result.C_id = value.C_id;
                    result.C_id_to = value.C_id_to;
                    result.Relation_id = value.Relation_id;
                    result.Content = value.Content;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _GalDBContext.Company_relation.Update(result);
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

        // DELETE api/company_relation/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Company_relation
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
                    _GalDBContext.Company_relation.Remove(result);
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
