using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/product_release_day")]
    [ApiController]
    public class ProductReleaseDaysController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public ProductReleaseDaysController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/product_release_day
        [HttpGet]
        public ActionResult<IEnumerable<ProductReleaseDaysDto>> Get(string? searchword, int? voice, string? currency, string? device, int? rating)
        {
            var result = from a in _GalDBContext.Product_Release_day
                         join b in _GalDBContext.Product on a.P_id equals b.P_id
                         join c in _GalDBContext.Voice_type on a.Voice_id equals c.Voice_id
                         join d in _GalDBContext.Currency on a.Currency_id equals d.Currency_id
                         join e in _GalDBContext.Device on a.Device_id equals e.Device_id
                         join f in _GalDBContext.Rating on a.Rating_id equals f.Rating_id
                         orderby a.P_id, a.Sale_Date
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = b.Name,
                             Name = a.Name,
                             Sale_Date = a.Sale_Date,
                             Presale_Date = a.Presale_Date,
                             Official_First = a.Official_First,
                             Price = a.Price,
                             Voice_id = a.Voice_id,
                             Voice_Name = c.Name,
                             Currency_id = a.Currency_id,
                             Currency_Name = d.ShortName,
                             Content = a.Content,
                             Device_id = a.Device_id,
                             Device_Name = e.ShortName,
                             Rating_id = a.Rating_id,
                             Rating_Name = f.ShortName,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(searchword) ||
                         a.P_Name.Contains(searchword) ||
                         a.Name.Contains(searchword)
                );
            }

            if (voice != null)
            {
                result = result.Where(
                    a => a.Voice_id == voice
                );
            }

            if (currency != null)
            {
                result = result.Where(
                    a => a.Currency_id == currency
                );
            }

            if (device != null)
            {
                result = result.Where(
                    a => a.Device_id == device
                );
            }

            if (rating != null)
            {
                result = result.Where(
                    a => a.Rating_id == rating
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.Take(300));
        }

        // GET api/product_release_day/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ProductReleaseDaysDto>> GetSingle(int id)
        {
            var result = from a in _GalDBContext.Product_Release_day
                         join b in _GalDBContext.Product on a.P_id equals b.P_id
                         join c in _GalDBContext.Voice_type on a.Voice_id equals c.Voice_id
                         join d in _GalDBContext.Currency on a.Currency_id equals d.Currency_id
                         join e in _GalDBContext.Device on a.Device_id equals e.Device_id
                         join f in _GalDBContext.Rating on a.Rating_id equals f.Rating_id
                         orderby a.P_id, a.Sale_Date
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = b.Name,
                             Name = a.Name,
                             Sale_Date = a.Sale_Date,
                             Presale_Date = a.Presale_Date,
                             Official_First = a.Official_First,
                             Price = a.Price,
                             Voice_id = a.Voice_id,
                             Voice_Name = c.Name,
                             Currency_id = a.Currency_id,
                             Currency_Name = d.FullName,
                             Content = a.Content,
                             Device_id = a.Device_id,
                             Device_Name = e.FullName,
                             Rating_id = a.Rating_id,
                             Rating_Name = f.Name,
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

        // GET api/product_release_day/getbypid
        [HttpGet("getbypid")]
        public ActionResult<IEnumerable<ProductReleaseDaysDto>> GetByPid(string id)
        {
            var result = from a in _GalDBContext.Product_Release_day
                         join b in _GalDBContext.Product on a.P_id equals b.P_id
                         join c in _GalDBContext.Voice_type on a.Voice_id equals c.Voice_id
                         join d in _GalDBContext.Currency on a.Currency_id equals d.Currency_id
                         join e in _GalDBContext.Device on a.Device_id equals e.Device_id
                         join f in _GalDBContext.Rating on a.Rating_id equals f.Rating_id
                         orderby a.P_id, a.Sale_Date
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = b.Name,
                             Name = a.Name,
                             Sale_Date = a.Sale_Date,
                             Presale_Date = a.Presale_Date,
                             Official_First = a.Official_First,
                             Price = a.Price,
                             Voice_id = a.Voice_id,
                             Voice_Name = c.Name,
                             Currency_id = a.Currency_id,
                             Currency_Name = d.FullName,
                             Content = a.Content,
                             Device_id = a.Device_id,
                             Device_Name = e.FullName,
                             Rating_id = a.Rating_id,
                             Rating_Name = f.Name,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(a => a.P_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/product_release_day
        /*上傳json格式
        {
            "P_id": "A000000001",
            "Name": "",
            "Sale_Date": "20030223",
            "Presale_Date": "",
            "Price": 0,
            "Voice_id": 0,
            "Currency_id": "JPY",
            "Content": "test1",
            "Device_id": "A0008",
            "Rating_id": 3
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ProductReleaseDaysDto value)
        {
            var isExists = _GalDBContext.Product_Release_day.Any(a => a.Id == value.Id);

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Product_Release_day insert = new Product_Release_day
                {
                    P_id = value.P_id,
                    Name = value.Name,
                    Sale_Date = value.Sale_Date,
                    Presale_Date = value.Presale_Date,
                    Official_First = value.Official_First,
                    Price = value.Price,
                    Voice_id = value.Voice_id,
                    Currency_id = value.Currency_id,
                    Content = value.Content,
                    Device_id = value.Device_id,
                    Rating_id = value.Rating_id,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Product_Release_day.Add(insert);
                _GalDBContext.SaveChanges();

                // 回傳成功訊息
                return Ok(new { message = "Y#資料上傳成功" });
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
                return BadRequest(new { message = "N#資料上傳失敗", error = ex.Message });
            }
        }

        // PUT api/product_release_day/{id}
        /*上傳json格式
        {
            "P_id": "A000000001",
            "Name": "",
            "Sale_Date": "20030223",
            "Presale_Date": "",
            "Price": 0,
            "Voice_id": 0,
            "Currency_id": "JPY",
            "Content": "test1",
            "Device_id": "A0008",
            "Rating_id": 3
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductReleaseDaysDto value)
        {
            var result = (from a in _GalDBContext.Product_Release_day
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
                    result.Name = value.Name;
                    result.Sale_Date = value.Sale_Date;
                    result.Presale_Date = value.Presale_Date;
                    result.Official_First = value.Official_First;
                    result.Price = value.Price;
                    result.Voice_id = value.Voice_id;
                    result.Currency_id = value.Currency_id;
                    result.Content = value.Content;
                    result.Device_id = value.Device_id;
                    result.Rating_id = value.Rating_id;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _GalDBContext.Product_Release_day.Update(result);
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

        // DELETE api/product_release_day/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Product_Release_day
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
                    _GalDBContext.Product_Release_day.Remove(result);
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
