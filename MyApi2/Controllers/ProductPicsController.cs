using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/product_pic")]
    [ApiController]
    public class ProductPicsController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public ProductPicsController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/product_pic
        [HttpGet]
        public ActionResult<IEnumerable<ProductPicsDto>> Get(string? searchword, string? UseYN, string? type_id)
        {
            var result = from a in _test10Context.Product_Pic
                         join b in _test10Context.Website_Type on a.Type_id equals b.Type_id
                         orderby a.P_id, a.Sort
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             Type_id = a.Type_id,
                             Type_Name = b.Name,
                             Name = a.Name,
                             Url = a.Url,
                             width = a.width, 
                             height = a.height,
                             Remark = a.Remark,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(searchword)
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

            if (type_id != null)
            {
                result = result.Where(
                    a => a.Type_id.Contains(type_id)
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/product_pic/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ProductPicsDto>> GetSingle(string id)
        {
            var result = from a in _test10Context.Product_Pic
                         join b in _test10Context.Website_Type on a.Type_id equals b.Type_id
                         orderby a.P_id, a.Sort
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             Type_id = a.Type_id,
                             Type_Name = b.Name,
                             Name = a.Name,
                             Url = a.Url,
                             width = a.width,
                             height = a.height,
                             Remark = a.Remark,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
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

        // POST api/product_pic
        /*上傳json格式
        {
            "P_id": "A000000001",
            "Type_id": "P04",
            "Name": "test",
            "Url": "https://www.ptt.cc/bbs/miHoYo/M.1715871687.A.E89.html",
            "width": 10,
            "height": 20,
            "Remark": "test123",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ProductPicsDto value)
        {
            try
            {
                Product_Pic insert = new Product_Pic
                {
                    P_id = value.P_id,
                    Type_id = value.Type_id,
                    Name = value.Name,
                    Url = value.Url,
                    Remark = value.Remark,
                    width = value.width,
                    height = value.height,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Product_Pic.Add(insert);
                _test10Context.SaveChanges();

                // 回傳成功訊息
                return Ok(new { message = "資料上傳成功" });
            }
            catch (Exception ex)
            {
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return BadRequest(new { message = "資料上傳失敗", error = ex.Message });
            }
        }

        // PUT api/product_pic/{id}
        /*上傳json格式
        {
            "P_id": "A000000001",
            "Type_id": "P04",
            "Name": "test",
            "Url": "https://www.ptt.cc/bbs/miHoYo/M.1715871687.A.E89.html",
            "width": 10,
            "height": 20,
            "Remark": "test123",
            "Use_yn": false,
            "Sort": 0
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductPicsDto value)
        {
            var result = (from a in _test10Context.Product_Pic
                          where a.Id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.P_id = value.P_id;
                    result.Type_id = value.Type_id;
                    result.Name = value.Name;
                    result.Url = value.Url;
                    result.Remark = value.Remark;
                    result.width = value.width;
                    result.height = value.height;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Product_Pic.Update(result);
                    _test10Context.SaveChanges();

                    // 回傳成功訊息
                    return Ok(new { message = "資料更新成功" });
                }
                catch (Exception ex)
                {
                    // 捕捉錯誤並回傳詳細的錯誤訊息
                    return BadRequest(new { message = "資料更新失敗", error = ex.Message });
                }
            }
        }

        // DELETE api/product_pic/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _test10Context.Product_Pic
                          where a.Id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _test10Context.Product_Pic.Remove(result);
                    _test10Context.SaveChanges();

                    // 回傳成功訊息
                    return Ok(new { message = "資料刪除成功" });
                }
                catch (Exception ex)
                {
                    // 捕捉錯誤並回傳詳細的錯誤訊息
                    return BadRequest(new { message = "資料刪除失敗", error = ex.Message });
                }
            }
        }
    }
}
