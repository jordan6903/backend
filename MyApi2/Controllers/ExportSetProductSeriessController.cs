using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/export_set_product_series")]
    [ApiController]
    public class ExportSetProductSeriessController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public ExportSetProductSeriessController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/export_set_product_series/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ExportSetProductSeriesDto>> GetSingle(int id)
        {
            var result = from a in _GalDBContext.Export_set_Company
                         join b in _GalDBContext.Export_set_Product_series on a.Id equals b.ESC_id
                         orderby b.Sort
                         select new
                         {
                             esc_id = b.ESC_id,
                             esps_Id = b.Id,
                             Name = b.Name,
                             Use_yn = b.Use_yn,
                             Sort = b.Sort,
                             Product_data = "",
                             Upd_user = b.Upd_user,
                             Upd_date = b.Upd_date,
                             Create_dt = b.Create_dt,
                             Add_word = b.Add_word,
                             Add_word_Use_yn = b.Add_word_Use_yn,
                         };

            result = result.Where(
                a => a.esc_id == id
            );

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/export_set_product_series/getbyid/{id}
        [HttpGet("getbyid/{id}")]
        public ActionResult<IEnumerable<ExportSetProductSeriesDto>> GetById(int id)
        {
            var result = from a in _GalDBContext.Export_set_Product_series
                         orderby a.Sort
                         select new
                         {
                             esc_id = a.ESC_id,
                             esps_Id = a.Id,
                             Name = a.Name,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Product_data = "",
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             Add_word = a.Add_word,
                             Add_word_Use_yn = a.Add_word_Use_yn,
                         };

            result = result.Where(
                a => a.esps_Id == id
            );

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/export_set_product_series/getall
        [HttpGet("getall")]
        public ActionResult<IEnumerable<ExportSetProductSeriesDto>> GetAll()
        {
            var result = from b in _GalDBContext.Export_set_Product_series
                         orderby b.Sort
                         select new
                         {
                             esc_id = b.ESC_id,
                             esps_Id = b.Id,
                             Name = b.Name,
                             Use_yn = b.Use_yn,
                             Sort = b.Sort,
                             Product_data = "",
                             Upd_user = b.Upd_user,
                             Upd_date = b.Upd_date,
                             Create_dt = b.Create_dt,
                             Add_word = b.Add_word,
                             Add_word_Use_yn = b.Add_word_Use_yn,
                         };

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/export_set_product_series
        /*上傳json格式
        {
            "ESC_id": 2,
            "Name" : "test",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ExportSetProductSeriesDto value)
        {
            try
            {
                Export_set_Product_series insert = new Export_set_Product_series
                {
                    ESC_id = value.esc_id,
                    Name = value.Name,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Add_word = "",
                    Add_word_Use_yn = true,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Export_set_Product_series.Add(insert);
                _GalDBContext.SaveChanges();

                // 取得自動產生的 ID
                long newId = insert.Id;

                // 回傳成功訊息和新 ID
                return Ok(new { message = "Y#資料上傳成功", id = newId });
            }
            catch (Exception ex)
            {
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return BadRequest(new { message = "N#資料上傳失敗", error = ex.Message });
            }
        }

        // POST api/export_set_product_series/copy
        /*上傳json格式
        {
            "ESC_id": 2,
            "Name" : "test",
            "Use_yn": false,
            "Sort": 0,
            "Add_word": "",
            "Add_word_Use_yn": true
        }
        */
        [HttpPost("copy")]
        public IActionResult PostCopy([FromBody] ExportSetProductSeriesDto value)
        {
            try
            {
                Export_set_Product_series insert = new Export_set_Product_series
                {
                    ESC_id = value.esc_id,
                    Name = value.Name,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Add_word = value.Add_word,
                    Add_word_Use_yn = value.Add_word_Use_yn,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Export_set_Product_series.Add(insert);
                _GalDBContext.SaveChanges();

                // 取得自動產生的 ID
                long newId = insert.Id;

                // 回傳成功訊息和新 ID
                return Ok(new { message = "Y#資料上傳成功", id = newId });
            }
            catch (Exception ex)
            {
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return BadRequest(new { message = "N#資料上傳失敗", error = ex.Message });
            }
        }

        // PUT api/export_set_product_series/{id}
        /*上傳json格式
        {
            "ESC_id": 2,
            "Name" : "test",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ExportSetProductSeriesDto value)
        {
            var result = (from a in _GalDBContext.Export_set_Product_series
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
                    result.ESC_id = value.esc_id;
                    result.Name = value.Name;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;

                    _GalDBContext.Export_set_Product_series.Update(result);
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

        // PUT api/export_set_product_series/putaddword/{id}
        /*上傳json格式
        {
            "ESC_id": 2,
            "Name" : "test",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPut("putaddword/{id}")]
        public IActionResult PutAddword(int id, [FromBody] ExportSetProductSeriesDto value)
        {
            var result = (from a in _GalDBContext.Export_set_Product_series
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
                    result.Add_word = value.Add_word;
                    result.Add_word_Use_yn = value.Add_word_Use_yn;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;

                    _GalDBContext.Export_set_Product_series.Update(result);
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

        // PUT api/export_set_product_series/{id}
        /*上傳json格式
        [
            {
                "esps_id": 1
                "Sort": 0
            },
            {
                "esps_id": 2
                "Sort": 0
            },
        ]
        */
        [HttpPut("several")]
        public IActionResult Put([FromBody] List<ExportSetProductSeries2Dto> values)
        {
            try
            {
                // 遍歷每一筆資料，新增到資料庫中
                foreach (var value in values)
                {
                    var result = (from a in _GalDBContext.Export_set_Product_series
                                  where a.Id == value.esps_Id
                                  select a).SingleOrDefault();

                    if (result == null)
                    {
                        //return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
                    }
                    else
                    {
                        result.Sort = value.Sort;
                        result.Upd_user = value.Upd_user;
                        result.Upd_date = DateTime.Now;

                        _GalDBContext.Export_set_Product_series.Update(result);
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

        // DELETE api/export_set_product_series/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Export_set_Product_series
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
                    var result2 = from a in _GalDBContext.Export_set_Product
                                  where a.ESPS_id == id
                                  select a;

                    if (result2.Any())
                    {
                        _GalDBContext.Export_set_Product.RemoveRange(result2);
                    }

                    _GalDBContext.Export_set_Product_series.Remove(result);
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
