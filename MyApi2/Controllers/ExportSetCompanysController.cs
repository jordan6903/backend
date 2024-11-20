using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/export_set_company")]
    [ApiController]
    public class ExportSetCompanysController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public ExportSetCompanysController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/export_set_company
        [HttpGet]
        public ActionResult<IEnumerable<ExportSetCompanysDto>> Get(int? id, string? UseYN)
        {
            var result = from a in _GalDBContext.Export_set_Company
                         orderby a.Export_batch
                         select new
                         {
                             Id = a.Id,
                             Export_batch = a.Export_batch,
                             C_id = a.C_id,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Title = a.Title,
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

        // GET api/export_set_company/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ExportSetCompanysDto>> GetSingle(int id, string? UseYN)
        {
            var result = from a in _GalDBContext.Export_set_Company
                         join b in _GalDBContext.Company on a.C_id equals b.C_id
                         orderby a.Export_batch, a.Sort
                         select new
                         {
                             Id = a.Id,
                             Export_batch = a.Export_batch,
                             C_id = a.C_id,
                             C_Name = b.Name,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Title = a.Title,
                             DragShow = false,
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

        // GET api/export_set_company/GetById/{id}
        [HttpGet("getbyid/{id}")]
        public ActionResult<IEnumerable<ExportSetCompanysDto>> GetById(int id)
        {
            var result = from a in _GalDBContext.Export_set_Company
                         join b in _GalDBContext.Company on a.C_id equals b.C_id
                         orderby a.Export_batch, a.Sort
                         select new
                         {
                             Id = a.Id,
                             Export_batch = a.Export_batch,
                             C_id = a.C_id,
                             C_Name = b.Name,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Title = a.Title,
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

        // GET: api/export_set_company/view
        [HttpGet("view/{id}")]
        public ActionResult<IEnumerable<ExportViews>> GetView(int id, string? UseYN)
        {
            var result = from a in _GalDBContext.Export_set_Company
                         join a1 in _GalDBContext.Company on a.C_id equals a1.C_id
                         join b in _GalDBContext.Export_set_Product_series on a.Id equals b.ESC_id into Series
                         orderby a.Export_batch, a.Sort
                         select new
                         {
                             Id = a.Id,
                             Export_batch = a.Export_batch,
                             C_id = a.C_id,
                             C_Name = a1.Name,
                             C_Name_origin = a1.Name_origin,
                             C_Name_short = a1.Name_short,
                             C_Intro = a1.Intro,
                             C_Remark = a1.Remark,
                             Url = "",
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Title = a.Title,
                             Repeat_chk = true,
                             Count_chk = false,
                             Count_export = 0,
                             Count_exportall = 0,
                             Count_all = 0,
                             Series_data = Series.Select(b => new ExportViews2
                             {
                                 Id = b.Id,
                                 Name = b.Name,
                                 Use_yn = b.Use_yn, 
                                 Sort = b.Sort,
                                 P_data = "",
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

        // GET: api/export_set_company/viewp
        [HttpGet("viewp/{id}")]
        public ActionResult<IEnumerable<ExportViewsP>> GetViewP(int id, string? UseYN)
        {
            var result = from a in _GalDBContext.Export_set_Product
                         join a1 in _GalDBContext.Product on a.P_id equals a1.P_id
                         join a2 in _GalDBContext.Company on a1.C_id equals a2.C_id
                         join b in _GalDBContext.Export_set_Product_series on a.ESPS_id equals b.Id
                         join c in _GalDBContext.Export_set_Company on b.ESC_id equals c.Id
                         orderby c.Export_batch
                         select new
                         {
                             Export_batch = c.Export_batch,
                             Id = a.Id,
                             ESPS_id = a.ESPS_id,
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

        // GET: api/export_set_company/viewcount/{id}
        [HttpGet("viewcount/{id}")]
        public ActionResult<IEnumerable<ExportViewsCount>> GetViewCount(int id)
        {
            var result = from a in _GalDBContext.Export_set_Company
                         orderby a.Sort
                         select new
                         {
                             Id = a.Id,
                             C_id = a.C_id,
                             Export_batch = a.Export_batch,
                             Sort = a.Sort,

                             // Count_export: 已編排數
                             Count_export = (from b in _GalDBContext.Export_set_Product_series
                                             join c in _GalDBContext.Export_set_Product on b.Id equals c.ESPS_id
                                             where b.ESC_id == a.Id
                                             select b).Count(),

                             // Count_exportALL: 可編排數
                             Count_exportALL = (from d in _GalDBContext.Company
                                                join e in _GalDBContext.Product on d.C_id equals e.C_id
                                                where d.C_id == a.C_id &&
                                                      (from f in _GalDBContext.Translation_team
                                                       where f.P_id == e.P_id
                                                       select f).Any()
                                                select d).Count(),

                             // Count_ALL: 所有遊戲總數
                             Count_ALL = (from d in _GalDBContext.Company
                                          join e in _GalDBContext.Product on d.C_id equals e.C_id
                                          where d.C_id == a.C_id
                                          select d).Count()
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.Export_batch == id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/export_set_company
        /*上傳json格式
        {
            "Export_batch": 2,
            "C_id": "C000000004",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ExportSetCompanysDto value)
        {
            var isExists = _GalDBContext.Export_set_Company.Any(
                    a => a.Export_batch == value.Export_batch &&
                         a.C_id == value.C_id
                );

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Export_set_Company insert = new Export_set_Company
                {
                    Export_batch = value.Export_batch,
                    C_id = value.C_id,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Title = value.Title,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Export_set_Company.Add(insert);
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

        // POST api/export_set_company/several
        /*上傳json格式
        [
            {
                "Export_batch": 2,
                "C_id": "C000000004",
                "Use_yn": false,
                "Sort": 0
            },
            {
                "Export_batch": 2,
                "C_id": "C000000005",
                "Use_yn": true,
                "Sort": 1
            }
        ]
        */
        [HttpPost("several")]
        public IActionResult Post([FromBody] List<ExportSetCompanysDto> values)
        {
            try
            {
                // 儲存每個成功插入的資料的 ID與c_id
                List<int> insertedIds = new List<int>();
                List<string> insertedcIds = new List<string>();

                // 遍歷每一筆資料，新增到資料庫中
                foreach (var value in values)
                {
                    var isExists = _GalDBContext.Export_set_Company.Any(
                        a => a.Export_batch == value.Export_batch &&
                             a.C_id == value.C_id
                    );

                    if (isExists)
                    {
                        return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
                    }

                    Export_set_Company insert = new Export_set_Company
                    {
                        Export_batch = value.Export_batch,
                        C_id = value.C_id,
                        Use_yn = value.Use_yn,
                        Sort = value.Sort,
                        Title = value.Title,
                        Upd_user = value.Upd_user,
                        Upd_date = DateTime.Now,
                        Create_dt = DateTime.Now,
                    };

                    _GalDBContext.Export_set_Company.Add(insert);
                    _GalDBContext.SaveChanges(); // 提交單筆更改，以便生成自動遞增的 ID
                    insertedIds.Add(insert.Id); // 存入新插入的 ID
                    insertedcIds.Add(value.C_id);
                }

                // 一次性保存所有更改
                //_GalDBContext.SaveChanges();

                // 回傳成功訊息
                return Ok(new { message = "Y#資料上傳成功", ids = insertedIds, c_id = insertedcIds });
            }
            catch (Exception ex)
            {
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return BadRequest(new { message = "N#資料上傳失敗", error = ex.Message });
            }
        }


        // PUT api/export_set_company/{id}
        /*上傳json格式
        {
            "Export_batch": 2,
            "C_id": "C000000004",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ExportSetCompanysDto value)
        {
            var result = (from a in _GalDBContext.Export_set_Company
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
                    result.C_id = value.C_id;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Title = value.Title;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;

                    _GalDBContext.Export_set_Company.Update(result);
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

        // PUT api/export_set_company/several
        /*上傳json格式
        [
            {
                "id": 1
                "Sort": 0
            },
            {
                "id": 2
                "Sort": 0
            },
        ]
        */
        [HttpPut("several")]
        public IActionResult Put([FromBody] List<ExportSetCompanysDto> values)
        {
            try
            {
                // 遍歷每一筆資料，新增到資料庫中
                foreach (var value in values)
                {
                    var result = (from a in _GalDBContext.Export_set_Company
                                  where a.Id == value.Id
                                  select a).SingleOrDefault();

                    if (result == null)
                    {
                        //return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
                    }
                    else
                    {
                        result.Use_yn = value.Use_yn;
                        result.Sort = value.Sort;
                        result.Upd_user = value.Upd_user;
                        result.Upd_date = DateTime.Now;

                        _GalDBContext.Export_set_Company.Update(result);
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

        // DELETE api/export_set_company/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Export_set_Company
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
                    var result3 = (from c in _GalDBContext.Export_set_Product
                                  join b in _GalDBContext.Export_set_Product_series on c.ESPS_id equals b.Id
                                  where b.ESC_id == id
                                  select c).Distinct();
                    if (result3.Any())
                    {
                        _GalDBContext.Export_set_Product.RemoveRange(result3);
                    }

                    var result2 = (from b in _GalDBContext.Export_set_Product_series
                                  where b.ESC_id == id
                                  select b).Distinct();

                    if (result2.Any())
                    {
                        _GalDBContext.Export_set_Product_series.RemoveRange(result2);
                    }

                    _GalDBContext.Export_set_Company.Remove(result);
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

        // DELETE api/export_set_company/deletebybatch/{batch}
        [HttpDelete("deletebybatch/{batch}")]
        public IActionResult DeleteByBatch(int batch)
        {
            var result = (from a in _GalDBContext.Export_set_Company
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
                    var result3 = (from c in _GalDBContext.Export_set_Product
                                   join b in _GalDBContext.Export_set_Product_series on c.ESPS_id equals b.Id
                                   join a in _GalDBContext.Export_set_Company on b.ESC_id equals a.Id
                                   where a.Export_batch == batch
                                   select c).Distinct();
                    if (result3.Any())
                    {
                        _GalDBContext.Export_set_Product.RemoveRange(result3);
                    }

                    var result2 = (from b in _GalDBContext.Export_set_Product_series
                                   join a in _GalDBContext.Export_set_Company on b.ESC_id equals a.Id
                                   where a.Export_batch == batch
                                   select b).Distinct();

                    if (result2.Any())
                    {
                        _GalDBContext.Export_set_Product_series.RemoveRange(result2);
                    }

                    _GalDBContext.Export_set_Company.RemoveRange(result);
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
