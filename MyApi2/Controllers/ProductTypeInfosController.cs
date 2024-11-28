using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/product_type_info")]
    [ApiController]
    public class ProductTypeInfosController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public ProductTypeInfosController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/product_type_info
        [HttpGet]
        public ActionResult<IEnumerable<ProductTypeInfosDto>> Get(string? searchword, string? UseYN, int? P_type)
        {
            var result = from a in _GalDBContext.Product_type_info
                         join b in _GalDBContext.Product_type_class on a.P_type_class equals b.P_type_class
                         orderby a.P_type_class, a.Sort
                         select new
                         {
                             P_type_id = a.P_type_id,
                             P_type_class = a.P_type_class,
                             P_type_name = b.Name,
                             FullName = a.FullName,
                             ShortName = a.ShortName,
                             Content = a.Content,
                             FullName_JP = a.FullName_JP,
                             FullName_EN = a.FullName_EN,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.FullName.Contains(searchword) ||
                         a.FullName_JP.Contains(searchword) ||
                         a.FullName_EN.Contains(searchword) ||
                         a.ShortName.Contains(searchword) ||
                         a.P_type_id.Contains(searchword)
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

            if (P_type != null)
            {
                result = result.Where(a => a.P_type_class == P_type);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/product_type_info/mainpage
        [HttpGet("mainpage")]
        public ActionResult<IEnumerable<ProductTypeInfosDto>> mainpage(string? searchword, string? UseYN, int? P_type, int page = 1, int pageSize = 10)
        {
            var result = from a in _GalDBContext.Product_type_info
                         join b in _GalDBContext.Product_type_class on a.P_type_class equals b.P_type_class
                         orderby a.P_type_class, a.Sort
                         select new
                         {
                             P_type_id = a.P_type_id,
                             P_type_class = a.P_type_class,
                             P_type_name = b.Name,
                             FullName = a.FullName,
                             ShortName = a.ShortName,
                             Content = a.Content,
                             FullName_JP = a.FullName_JP,
                             FullName_EN = a.FullName_EN,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.FullName.Contains(searchword) ||
                         a.FullName_JP.Contains(searchword) ||
                         a.FullName_EN.Contains(searchword) ||
                         a.ShortName.Contains(searchword) ||
                         a.P_type_id.Contains(searchword)
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

            if (P_type != null)
            {
                result = result.Where(a => a.P_type_class == P_type);
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

        // GET api/product_type_info/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ProductTypeInfosDto>> GetSingle(string id)
        {
            var result = from a in _GalDBContext.Product_type_info
                         join b in _GalDBContext.Product_type_class on a.P_type_class equals b.P_type_class
                         orderby a.P_type_class, a.Sort
                         select new
                         {
                             P_type_id = a.P_type_id,
                             P_type_class = a.P_type_class,
                             P_type_name = b.Name,
                             FullName = a.FullName,
                             ShortName = a.ShortName,
                             Content = a.Content,
                             FullName_JP = a.FullName_JP,
                             FullName_EN = a.FullName_EN,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(a => a.P_type_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/product_type_info
        /*上傳json格式
        {
            "P_type_id": "A0016",
            "P_type_class": 1,
            "FullName": "111",
            "ShortName": "222",
            "FullName_JP": "333",
            "FullName_EN": "444",
            "Content": "test1",
            "use_yn": false,
            "sort": 0
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ProductTypeInfosDto value)
        {
            var isExists = _GalDBContext.Product_type_info.Any(a => a.P_type_id == value.P_type_id);

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Product_type_info insert = new Product_type_info
                {
                    P_type_id = value.P_type_id,
                    P_type_class = value.P_type_class,
                    FullName = value.FullName,
                    ShortName = value.ShortName,
                    FullName_JP = value.FullName_JP,
                    FullName_EN = value.FullName_EN,
                    Content = value.Content,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Product_type_info.Add(insert);
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

        // PUT api/product_type_info/{id}
        /*上傳json格式
        {
            "P_type_id": "A0016",
            "P_type_class": 1,
            "FullName": "111",
            "ShortName": "222",
            "FullName_JP": "333",
            "FullName_EN": "444",
            "Content": "test1",
            "use_yn": false,
            "sort": 0
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ProductTypeInfosDto value)
        {
            var result = (from a in _GalDBContext.Product_type_info
                          where a.P_type_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.P_type_id = value.P_type_id;
                    result.P_type_class = value.P_type_class;
                    result.FullName = value.FullName;
                    result.ShortName = value.ShortName;
                    result.FullName_JP = value.FullName_JP;
                    result.FullName_EN = value.FullName_EN;
                    result.Content = value.Content;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _GalDBContext.Product_type_info.Update(result);
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

        // DELETE api/product_type_info/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = (from a in _GalDBContext.Product_type_info
                          where a.P_type_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _GalDBContext.Product_type_info.Remove(result);
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
