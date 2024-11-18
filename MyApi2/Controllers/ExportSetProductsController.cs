using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/export_set_product")]
    [ApiController]
    public class ExportSetProductsController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public ExportSetProductsController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/export_set_product
        [HttpGet]
        public ActionResult<IEnumerable<ExportSetProductsDto>> GetSingle(int id)
        {
            var result = from a in _GalDBContext.Export_set_Product
                         join a1 in _GalDBContext.Product on a.P_id equals a1.P_id 
                         join a2 in _GalDBContext.Company on a1.C_id equals a2.C_id
                         join a3 in _GalDBContext.Product_Release_day on a1.P_id equals a3.P_id
                         join b in _GalDBContext.Export_set_Product_series on a.ESPS_id equals b.Id
                         join c in _GalDBContext.Translation_team on a.P_id equals c.P_id into TT
                         where a3.Official_First == true
                         orderby b.ESC_id, a.ESPS_id, a.Sort
                         select new
                         {
                             esc_id = b.ESC_id,
                             esps_id = a.ESPS_id,
                             esp_id = a.Id,
                             P_id = a.P_id,
                             P_Name = a1.Name,
                             C_id = a2.C_id,
                             C_Name = a2.Name,
                             Sale_Date = a3.Sale_Date,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             eso_chk = false,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             TT_type = TT.Select(c => new ProductsViews2Dto
                             {
                                 Type_id = c.Type_id,
                                 Type_Name = (from d in _GalDBContext.Translation_team_type
                                              where c.Type_id == d.Type_id
                                              select d.Name).FirstOrDefault(),
                             }),
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.esc_id == id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/export_set_product/getbybatch
        [HttpGet("getbybatch")]
        public ActionResult<IEnumerable<ExportSetProductsDto>> GetByBatch(int id)
        {
            var result = from a in _GalDBContext.Export_set_Product
                         join a1 in _GalDBContext.Product on a.P_id equals a1.P_id
                         join a2 in _GalDBContext.Translation_team on a.P_id equals a2.P_id into TT
                         join b in _GalDBContext.Export_set_Product_series on a.ESPS_id equals b.Id
                         join c in _GalDBContext.Export_set_Company on b.ESC_id equals c.Id
                         orderby b.ESC_id, a.ESPS_id, a.Sort
                         select new
                         {
                             export_batch = c.Export_batch,
                             esc_id = b.ESC_id,
                             esps_id = a.ESPS_id,
                             esp_id = a.Id,
                             P_id = a.P_id,
                             P_Name = a1.Name,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             eso_chk = false,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             TT_type = TT.Select(a2 => new ProductsViews2Dto
                             {
                                 Type_id = a2.Type_id,
                                 Type_Name = (from d in _GalDBContext.Translation_team_type
                                              where a2.Type_id == d.Type_id
                                              select d.Name).FirstOrDefault(),
                             })
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

        //// GET api/export_set_product/{id}
        //[HttpGet("{id}")]
        //public ActionResult<IEnumerable<ExportSetProductsDto>> GetSingle(int id)
        //{
        //    var result = from a in _GalDBContext.Export_set_Product
        //                 orderby a.Export_batch
        //                 select new
        //                 {
        //                     Id = a.Id,
        //                     Export_batch = a.Export_batch,
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

        // POST api/export_set_product
        /*上傳json格式
        {
            "ESPS_id": 2,
            "P_id": "A000000003",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ExportSetProductsDto value)
        {
            try
            {
                Export_set_Product insert = new Export_set_Product
                {
                    ESPS_id = value.esps_id,
                    P_id = value.P_id,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Export_set_Product.Add(insert);
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

        // POST api/export_set_product/several
        /*上傳json格式
        [
            {
                "ESPS_id": 2,
                "P_id": "A000000003",
                "Use_yn": false,
                "Sort": 0
            },
            {
                "ESPS_id": 2,
                "P_id": "A000000003",
                "Use_yn": false,
                "Sort": 0
            }
        ]
        */
        [HttpPost("several")]
        public IActionResult Post([FromBody] List<ExportSetProductsDto> values)
        {
            try
            {
                // 遍歷每一筆資料，新增到資料庫中
                foreach (var value in values)
                {
                    var isExists = _GalDBContext.Export_set_Product.Any(
                        a => a.ESPS_id == value.esps_id &&
                             a.P_id == value.P_id
                    );

                    if (isExists)
                    {
                        return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
                    }

                    Export_set_Product insert = new Export_set_Product
                    {
                        ESPS_id = value.esps_id,
                        P_id = value.P_id,
                        Use_yn = value.Use_yn,
                        Sort = value.Sort,
                        Upd_user = value.Upd_user,
                        Upd_date = DateTime.Now,
                        Create_dt = DateTime.Now,
                    };

                    _GalDBContext.Export_set_Product.Add(insert);
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

        // PUT api/export_set_product/several
        /*上傳json格式
        [
            {
                "esps_id": 1,
                "esp_id": 1
                "Sort": 0
            },
            {
                "esps_id": 1,
                "esp_id": 2
                "Sort": 0
            },
        ]
        */
        [HttpPut("several")]
        public IActionResult Put([FromBody] List<ExportSetProductsDto> values)
        {
            try
            {
                // 遍歷每一筆資料，新增到資料庫中
                foreach (var value in values)
                {
                    var result = (from a in _GalDBContext.Export_set_Product
                                  where a.Id == value.esp_id
                                  select a).SingleOrDefault();

                    if (result == null)
                    {
                        //return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
                    }
                    else
                    {
                        result.ESPS_id = value.esps_id;
                        result.Sort = value.Sort;
                        result.Upd_user = value.Upd_user;
                        result.Upd_date = DateTime.Now;

                        _GalDBContext.Export_set_Product.Update(result);
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

        // DELETE api/export_set_product/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Export_set_Product
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
                    _GalDBContext.Export_set_Product.Remove(result);
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
