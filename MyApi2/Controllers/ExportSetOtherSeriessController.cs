﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/export_set_other_series")]
    [ApiController]
    public class ExportSetOtherSeriessController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public ExportSetOtherSeriessController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/export_set_other_series/{id}
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<ExportSetOtherSeriesDto>> GetSingle(int id)
        {
            var result = from a in _GalDBContext.Export_set_Other
                         join b in _GalDBContext.Export_set_Other_series on a.Id equals b.ESO_id
                         orderby b.Sort
                         select new
                         {
                             eso_id = b.ESO_id,
                             esos_Id = b.Id,
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
                a => a.eso_id == id
            );

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/export_set_other_series/getbyid/{id}
        [HttpGet("getbyid/{id}")]
        [Authorize]
        public ActionResult<IEnumerable<ExportSetOtherSeriesDto>> GetById(int id)
        {
            var result = from a in _GalDBContext.Export_set_Other_series
                         orderby a.Sort
                         select new
                         {
                             eso_id = a.ESO_id,
                             esos_Id = a.Id,
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
                a => a.esos_Id == id
            );

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/export_set_other_series/getall
        [HttpGet("getall")]
        [Authorize]
        public ActionResult<IEnumerable<ExportSetOtherSeriesDto>> GetAll()
        {
            var result = from b in _GalDBContext.Export_set_Other_series
                         orderby b.Sort
                         select new
                         {
                             eso_id = b.ESO_id,
                             esos_Id = b.Id,
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

        // POST api/export_set_other_series
        /*上傳json格式
        {
            "ESO_id": 2,
            "Name" : "test",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] ExportSetOtherSeriesDto value)
        {
            try
            {
                Export_set_Other_series insert = new Export_set_Other_series
                {
                    ESO_id = value.eso_id,
                    Name = value.Name,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Add_word = "",
                    Add_word_Use_yn = true,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Export_set_Other_series.Add(insert);
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

        // POST api/export_set_other_series/copy
        /*上傳json格式
        {
            "ESO_id": 2,
            "Name" : "test",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPost("copy")]
        [Authorize]
        public IActionResult PostCopy([FromBody] ExportSetOtherSeriesDto value)
        {
            try
            {
                Export_set_Other_series insert = new Export_set_Other_series
                {
                    ESO_id = value.eso_id,
                    Name = value.Name,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Add_word = value.Add_word,
                    Add_word_Use_yn = value.Add_word_Use_yn,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Export_set_Other_series.Add(insert);
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

        // PUT api/export_set_other_series/{id}
        /*上傳json格式
        {
            "ESO_id": 2,
            "Name" : "test",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] ExportSetOtherSeriesDto value)
        {
            var result = (from a in _GalDBContext.Export_set_Other_series
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
                    result.ESO_id = value.eso_id;
                    result.Name = value.Name;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;

                    _GalDBContext.Export_set_Other_series.Update(result);
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

        // PUT api/export_set_other_series/putaddword/{id}
        /*上傳json格式
        {
            "ESO_id": 2,
            "Name" : "test",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPut("putaddword/{id}")]
        [Authorize]
        public IActionResult PutAddword(int id, [FromBody] ExportSetOtherSeriesDto value)
        {
            var result = (from a in _GalDBContext.Export_set_Other_series
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

                    _GalDBContext.Export_set_Other_series.Update(result);
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

        // PUT api/export_set_other_series/{id}
        /*上傳json格式
        [
            {
                "esos_id": 1,
                "Sort": 1
            },
            {
                "esos_id": 2,
                "Sort": 2
            }
        ]
        */
        [HttpPut("several")]
        [Authorize]
        public IActionResult Put([FromBody] List<ExportSetOtherSeries2Dto> values)
        {
            try
            {
                // 遍歷每一筆資料，新增到資料庫中
                foreach (var value in values)
                {
                    var result = (from a in _GalDBContext.Export_set_Other_series
                                  where a.Id == value.esos_Id
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

                        _GalDBContext.Export_set_Other_series.Update(result);
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

        // DELETE api/export_set_other_series/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Export_set_Other_series
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
                    var result3 = (from c in _GalDBContext.Export_set_Other_Product
                                   join b in _GalDBContext.Export_set_Other_series on c.ESOS_id equals b.Id
                                   where b.Id == id
                                   select c).Distinct();
                    if (result3.Any())
                    {
                        _GalDBContext.Export_set_Other_Product.RemoveRange(result3);
                    }

                    _GalDBContext.Export_set_Other_series.RemoveRange(result);
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
