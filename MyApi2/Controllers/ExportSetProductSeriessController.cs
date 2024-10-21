using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //// GET api/export_set_product_series/{id}
        //[HttpGet("{id}")]
        //public ActionResult<IEnumerable<ExportSetProductSeriesDto>> GetSingle(int id)
        //{
        //    var result = from a in _GalDBContext.Export_set_Product_series
        //                 orderby a.Export_batch
        //                 select new
        //                 {
        //                     Id = a.Id,
        //                     Export_batch = a.Export_batch,
        //                     Name = a.Name,
        //                     C_id = a.C_id,
        //                     P_id = a.P_id,
        //                     Use_yn = a.Use_yn,
        //                     Sort = a.Sort,
        //                     Upd_user = a.Upd_user,
        //                     Upd_date = a.Upd_date,
        //                     Create_dt = a.Create_dt,
        //                 };

        //    if (id != null)
        //    {
        //        result = result.Where(
        //            a => a.Id == id
        //        );
        //    }

        //    if (result == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}

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
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Export_set_Product_series.Add(insert);
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
