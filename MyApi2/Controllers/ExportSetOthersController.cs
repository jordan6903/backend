using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/export_set_other")]
    [ApiController]
    public class ExportSetOthersController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public ExportSetOthersController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/export_set_other
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<ExportSetOthersDtos>> Get(int? id, string? UseYN)
        {
            var result = from a in _GalDBContext.Export_set_Other
                         orderby a.Export_batch
                         select new
                         {
                             Id = a.Id,
                             Export_batch = a.Export_batch,
                             Name = a.Name,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Series_data = "",
                             snumber = 0,
                             pnumber = 0,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.Export_batch == id
                );
            }

            if (UseYN != null)
            {
                if (UseYN == "Y")
                {
                    result = result.Where(a => a.Use_yn == true);
                }
                else if (UseYN == "N")
                {
                    result = result.Where(a => a.Use_yn == false);
                }
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/export_set_other/{id}
        [HttpGet("getbyid/{id}")]
        [Authorize]
        public ActionResult<IEnumerable<ExportSetOthersDtos>> GetById(int id)
        {
            var result = from a in _GalDBContext.Export_set_Other
                         orderby a.Export_batch, a.Sort
                         select new
                         {
                             Id = a.Id,
                             Export_batch = a.Export_batch,
                             Name = a.Name,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            result = result.Where(
                a => a.Id == id
            );

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/export_set_other/view
        [HttpGet("view/{id}")]
        [Authorize]
        public ActionResult<IEnumerable<ExportViewsOther>> GetView(int id, string? UseYN)
        {
            var result = from a in _GalDBContext.Export_set_Other
                         join b in _GalDBContext.Export_set_Other_series on a.Id equals b.ESO_id into Series
                         orderby a.Export_batch, a.Sort
                         select new
                         {
                             Id = a.Id,
                             Export_batch = a.Export_batch,
                             Name = a.Name,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Count_chk = true,
                             Count_export = 0,
                             Count_exportall = 0,
                             Count_all = 0,
                             Series_data = Series.Select(b => new ExportViews2Other
                             {
                                 Id = b.Id,
                                 Name = b.Name,
                                 Use_yn = b.Use_yn,
                                 Sort = b.Sort,
                                 P_data = "",
                                 Add_word = b.Add_word,
                                 Add_word_Use_yn = b.Add_word_Use_yn,
                             }).ToList(),
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.Export_batch == id
                );
            }

            if (UseYN != null)
            {
                if (UseYN == "Y")
                {
                    result = result.Where(a => a.Use_yn == true);
                }
                else if (UseYN == "N")
                {
                    result = result.Where(a => a.Use_yn == false);
                }
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/export_set_other/viewmainpage
        [HttpGet("viewmainpage/{id}")]
        [Authorize]
        public ActionResult<IEnumerable<ExportViewsOther>> GetViewmainpage(int id, string? UseYN, int page = 1, int pageSize = 10)
        {
            var result = from a in _GalDBContext.Export_set_Other
                         join b in _GalDBContext.Export_set_Other_series on a.Id equals b.ESO_id into Series
                         orderby a.Export_batch, a.Sort
                         select new
                         {
                             Id = a.Id,
                             Export_batch = a.Export_batch,
                             Name = a.Name,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Count_chk = true,
                             Count_export = 0,
                             Count_exportall = 0,
                             Count_all = 0,
                             Series_data = Series.Select(b => new ExportViews2Other
                             {
                                 Id = b.Id,
                                 Name = b.Name,
                                 Use_yn = b.Use_yn,
                                 Sort = b.Sort,
                                 P_data = "",
                                 Add_word = b.Add_word,
                                 Add_word_Use_yn = b.Add_word_Use_yn,
                             }).ToList(),
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.Export_batch == id
                );
            }

            if (UseYN != null)
            {
                if (UseYN == "Y")
                {
                    result = result.Where(a => a.Use_yn == true);
                }
                else if (UseYN == "N")
                {
                    result = result.Where(a => a.Use_yn == false);
                }
            }

            if (result == null)
            {
                return NotFound();
            }

            // 分頁處理
            var totalRecords = result.Count(); // 總記錄數
            var data = result.Skip((page - 1) * pageSize).Take(pageSize).ToList(); // 分頁數據

            // 回傳資料
            return Ok(new
            {
                TotalRecords = totalRecords, // 總記錄數
                Data = data                 // 分頁資料
            });
        }

        // GET: api/export_set_other/viewp
        [HttpGet("viewp/{id}")]
        [Authorize]
        public ActionResult<IEnumerable<ExportViewsPOther>> GetViewP(int id, string? UseYN)
        {
            var result = from a in _GalDBContext.Export_set_Other_Product
                         join a1 in _GalDBContext.Product on a.P_id equals a1.P_id
                         join a2 in _GalDBContext.Company on a1.C_id equals a2.C_id
                         join b in _GalDBContext.Export_set_Other_series on a.ESOS_id equals b.Id
                         join c in _GalDBContext.Export_set_Other on b.ESO_id equals c.Id
                         orderby c.Export_batch
                         select new
                         {
                             Export_batch = c.Export_batch,
                             Id = a.Id,
                             esos_id = a.ESOS_id,
                             C_id = a2.C_id,
                             C_Name = a2.Name,
                             P_id = a.P_id,
                             P_Name = a1.Name,
                             P_CName = a1.C_Name,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.Export_batch == id
                );
            }

            if (UseYN != null)
            {
                if (UseYN == "Y")
                {
                    result = result.Where(a => a.Use_yn == true);
                }
                else if (UseYN == "N")
                {
                    result = result.Where(a => a.Use_yn == false);
                }
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/export_set_other
        /*上傳json格式
        {
            "Export_batch": 1,
            "Name": "測試",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] ExportSetOthersDtos value)
        {
            var isExists = _GalDBContext.Export_set_Other.Any(
                    a => a.Export_batch == value.Export_batch &&
                         a.Name == value.Name
                );

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Export_set_Other insert = new Export_set_Other
                {
                    Export_batch = value.Export_batch,
                    Name = value.Name,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Export_set_Other.Add(insert);
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

        // POST api/export_set_other/several
        /*上傳json格式
        [
            {
                "Export_batch": 1,
                "Name": "測試",
                "Use_yn": false,
                "Sort": 0
            },
            {
                "Export_batch": 1,
                "Name": "測試123",
                "Use_yn": false,
                "Sort": 0
            }
        ]
        */
        [HttpPost("several")]
        [Authorize]
        public IActionResult Post([FromBody] List<ExportSetOthersDtos> values)
        {
            try
            {
                // 儲存每個成功插入的資料的 ID與c_id
                List<int> insertedIds = new List<int>();
                List<string> insertednames = new List<string>();

                // 遍歷每一筆資料，新增到資料庫中
                foreach (var value in values)
                {
                    var isExists = _GalDBContext.Export_set_Other.Any(
                        a => a.Export_batch == value.Export_batch &&
                             a.Name == value.Name
                    );

                    if (isExists)
                    {
                        return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
                    }

                    Export_set_Other insert = new Export_set_Other
                    {
                        Export_batch = value.Export_batch,
                        Name = value.Name,
                        Use_yn = value.Use_yn,
                        Sort = value.Sort,
                        Upd_user = value.Upd_user,
                        Upd_date = DateTime.Now,
                        Create_dt = DateTime.Now,
                    };

                    _GalDBContext.Export_set_Other.Add(insert);
                    _GalDBContext.SaveChanges(); // 提交單筆更改，以便生成自動遞增的 ID
                    insertedIds.Add(insert.Id); // 存入新插入的 ID
                    insertednames.Add(value.Name);
                }

                // 一次性保存所有更改
                //_GalDBContext.SaveChanges();

                // 回傳成功訊息
                return Ok(new { message = "Y#資料上傳成功", ids = insertedIds, name = insertednames });
            }
            catch (Exception ex)
            {
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return BadRequest(new { message = "N#資料上傳失敗", error = ex.Message });
            }
        }

        // PUT api/export_set_other/{id}
        /*上傳json格式
        {
            "Export_batch": 1,
            "Name": "測試",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] ExportSetOthersDtos value)
        {
            var result = (from a in _GalDBContext.Export_set_Other
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
                    result.Export_batch = value.Export_batch;
                    result.Name = value.Name;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;

                    _GalDBContext.Export_set_Other.Update(result);
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

        // PUT api/export_set_other/several
        /*上傳json格式
        [
            {
                "id": 1,
                "Sort": 1
            },
            {
                "id": 2,
                "Sort": 2
            }
        ]
        */
        [HttpPut("several")]
        [Authorize]
        public IActionResult Put([FromBody] List<ExportSetOthersDtos> values)
        {
            try
            {
                // 遍歷每一筆資料，新增到資料庫中
                foreach (var value in values)
                {
                    var result = (from a in _GalDBContext.Export_set_Other
                                  where a.Id == value.Id
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

                        _GalDBContext.Export_set_Other.Update(result);
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

        // DELETE api/export_set_other/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Export_set_Other
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
                                   join a in _GalDBContext.Export_set_Other on b.ESO_id equals a.Id
                                   where a.Id == id
                                   select c).Distinct();
                    if (result3.Any())
                    {
                        _GalDBContext.Export_set_Other_Product.RemoveRange(result3);
                    }

                    var result2 = (from b in _GalDBContext.Export_set_Other_series
                                   join a in _GalDBContext.Export_set_Other on b.ESO_id equals a.Id
                                   where a.Id == id
                                   select b).Distinct();

                    if (result2.Any())
                    {
                        _GalDBContext.Export_set_Other_series.RemoveRange(result2);
                    }

                    _GalDBContext.Export_set_Other.RemoveRange(result);
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

        // DELETE api/export_set_other/deletebybatch/{batch}
        [HttpDelete("deletebybatch/{batch}")]
        [Authorize]
        public IActionResult DeleteByBatch(int batch)
        {
            var result = (from a in _GalDBContext.Export_set_Other
                          where a.Export_batch == batch
                          select a).Distinct();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該batch" });
            }
            else
            {
                try
                {
                    var result3 = (from c in _GalDBContext.Export_set_Other_Product
                                   join b in _GalDBContext.Export_set_Other_series on c.ESOS_id equals b.Id
                                   join a in _GalDBContext.Export_set_Other on b.ESO_id equals a.Id
                                   where a.Export_batch == batch
                                   select c).Distinct();
                    if (result3.Any())
                    {
                        _GalDBContext.Export_set_Other_Product.RemoveRange(result3);
                    }

                    var result2 = (from b in _GalDBContext.Export_set_Other_series
                                   join a in _GalDBContext.Export_set_Other on b.ESO_id equals a.Id
                                   where a.Export_batch == batch
                                   select b).Distinct();

                    if (result2.Any())
                    {
                        _GalDBContext.Export_set_Other_series.RemoveRange(result2);
                    }

                    _GalDBContext.Export_set_Other.RemoveRange(result);
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
