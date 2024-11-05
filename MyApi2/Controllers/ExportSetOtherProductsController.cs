using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/export_set_other_product")]
    [ApiController]
    public class ExportSetOtherProductsController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public ExportSetOtherProductsController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/export_set_other_product
        [HttpGet]
        public ActionResult<IEnumerable<ExportSetOtherProductsDto>> GetSingle(int id)
        {
            var result = from a in _GalDBContext.Export_set_Other_Product
                         join a1 in _GalDBContext.Product on a.P_id equals a1.P_id
                         join b in _GalDBContext.Export_set_Other_series on a.ESOS_id equals b.Id
                         orderby b.ESO_id, a.ESOS_id, a.Sort
                         select new
                         {
                             eso_id = b.ESO_id,
                             esos_id = a.ESOS_id,
                             esop_id = a.Id,
                             P_id = a.P_id,
                             P_Name = a1.Name,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             esp_chk = false,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.eso_id == id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/export_set_other_product/getbybatch
        [HttpGet("getbybatch")]
        public ActionResult<IEnumerable<ExportSetOtherProductsDto>> GetByBatch(int id)
        {
            var result = from a in _GalDBContext.Export_set_Other_Product
                         join a1 in _GalDBContext.Product on a.P_id equals a1.P_id
                         join b in _GalDBContext.Export_set_Other_series on a.ESOS_id equals b.Id
                         join c in _GalDBContext.Export_set_Other on b.ESO_id equals c.Id
                         orderby b.ESO_id, a.ESOS_id, a.Sort
                         select new
                         {
                             export_batch = c.Export_batch,
                             eso_id = b.ESO_id,
                             esos_id = a.ESOS_id,
                             esop_id = a.Id,
                             P_id = a.P_id,
                             P_Name = a1.Name,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.export_batch == id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/export_set_other_product
        /*上傳json格式
        {
            "ESOS_id": 2,
            "P_id": "A000000003",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ExportSetOtherProductsDto value)
        {
            try
            {
                Export_set_Other_Product insert = new Export_set_Other_Product
                {
                    ESOS_id = value.esos_id,
                    P_id = value.P_id,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Export_set_Other_Product.Add(insert);
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

        // POST api/export_set_other_product/several
        /*上傳json格式
        [
            {
                "ESOS_id": 2,
                "P_id": "A000000003",
                "Use_yn": false,
                "Sort": 0
            },
            {
                "ESOS_id": 2,
                "P_id": "A000000003",
                "Use_yn": false,
                "Sort": 0
            }
        ]
        */
        [HttpPost("several")]
        public IActionResult Post([FromBody] List<ExportSetOtherProductsDto> values)
        {
            try
            {
                // 遍歷每一筆資料，新增到資料庫中
                foreach (var value in values)
                {
                    var isExists = _GalDBContext.Export_set_Other_Product.Any(
                        a => a.ESOS_id == value.esos_id &&
                             a.P_id == value.P_id
                    );

                    if (isExists)
                    {
                        return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
                    }

                    Export_set_Other_Product insert = new Export_set_Other_Product
                    {
                        ESOS_id = value.esos_id,
                        P_id = value.P_id,
                        Use_yn = value.Use_yn,
                        Sort = value.Sort,
                        Upd_user = value.Upd_user,
                        Upd_date = DateTime.Now,
                        Create_dt = DateTime.Now,
                    };

                    _GalDBContext.Export_set_Other_Product.Add(insert);
                }

                // 一次性保存所有更改
                _GalDBContext.SaveChanges();

                // 回傳成功訊息
                return Ok(new { message = "Y#資料上傳成功" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return BadRequest(new { message = "N#資料上傳失敗", error = innerException ?? ex.Message });
            }
        }

        // PUT api/export_set_other_product/several
        /*上傳json格式
        [
            {
                "esos_id": 1,
                "esop_id": 1,
                "Sort": 1
            },
            {
                "esos_id": 1,
                "esop_id": 2,
                "Sort": 2
            }
        ]
        */
        [HttpPut("several")]
        public IActionResult Put([FromBody] List<ExportSetOtherProductsDto> values)
        {
            try
            {
                // 遍歷每一筆資料，新增到資料庫中
                foreach (var value in values)
                {
                    var result = (from a in _GalDBContext.Export_set_Other_Product
                                  where a.Id == value.esop_id
                                  select a).SingleOrDefault();

                    if (result == null)
                    {
                        //return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
                    }
                    else
                    {
                        result.ESOS_id = value.esos_id;
                        result.Sort = value.Sort;
                        result.Upd_user = value.Upd_user;
                        result.Upd_date = DateTime.Now;

                        _GalDBContext.Export_set_Other_Product.Update(result);
                    }
                }
                // 一次性保存所有更改
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

        // DELETE api/export_set_other_product/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Export_set_Other_Product
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
                    _GalDBContext.Export_set_Other_Product.Remove(result);
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
