using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/product_score")]
    [ApiController]
    public class ProductScoresController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public ProductScoresController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/product_score
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<ProductScoresDto>> Get(string? searchword, int? type_id)
        {
            var result = from a in _GalDBContext.Product_score
                         join b in _GalDBContext.Product_score_type on a.Type_id equals b.Type_id
                         join c in _GalDBContext.Product on a.P_id equals c.P_id
                         orderby a.P_id, a.Type_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = c.Name,
                             Type_id = a.Type_id,
                             Type_Name = b.Name,
                             Score = a.Score,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(searchword) ||
                         a.P_Name.Contains(searchword)
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

        // GET api/product_score/{id}
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<StaffsDto>> GetSingle(string id)
        {
            var result = from a in _GalDBContext.Product_score
                         join b in _GalDBContext.Product_score_type on a.Type_id equals b.Type_id
                         join c in _GalDBContext.Product on a.P_id equals c.P_id
                         orderby a.P_id, a.Type_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = c.Name,
                             Type_id = a.Type_id,
                             Type_Name = b.Name,
                             Score = a.Score,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
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

        // POST api/product_score
        /*上傳json格式
        {
            "P_id": "A000000003",
            "Type_id": 1,
            "Score": 60
        }
        */
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] ProductScoresDto value)
        {
            var isExists = _GalDBContext.Product_score.Any(
                    a => a.P_id == value.P_id &&
                         a.Type_id == value.Type_id
                );

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Product_score insert = new Product_score
                {
                    P_id = value.P_id,
                    Type_id = value.Type_id,
                    Score = value.Score,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Product_score.Add(insert);
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

        // PUT api/product_score/{id}
        /*上傳json格式
        {
            "P_id": "A000000003",
            "Type_id": 1,
            "Score": 60
        }
        */
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] ProductScoresDto value)
        {
            var result = (from a in _GalDBContext.Product_score
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
                    result.Type_id = value.Type_id;
                    result.Score = value.Score;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;

                    _GalDBContext.Product_score.Update(result);
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

        // DELETE api/product_score/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Product_score
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
                    _GalDBContext.Product_score.Remove(result);
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
