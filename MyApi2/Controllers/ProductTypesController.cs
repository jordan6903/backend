using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/product_type")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public ProductTypesController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/product_type
        [HttpGet]
        public ActionResult<IEnumerable<ProductTypesDto>> Get(string? searchword, string? P_type_id)
        {
            var result = from a in _test10Context.Product_type
                         join b in _test10Context.Product on a.P_id equals b.P_id
                         join c in _test10Context.Product_type_info on a.P_type_id equals c.P_type_id
                         orderby a.P_id, a.P_type_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = b.Name,
                             P_type_id = a.P_type_id,
                             P_type_Name = c.FullName,
                             Remark = a.Remark,
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

            if (P_type_id != null)
            {
                result = result.Where(
                    a => a.P_type_id == P_type_id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/product_type/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ProductTypesDto>> GetSingle(int id)
        {
            var result = from a in _test10Context.Product_type
                         join b in _test10Context.Product on a.P_id equals b.P_id
                         join c in _test10Context.Product_type_info on a.P_type_id equals c.P_type_id
                         orderby a.P_id, a.P_type_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = b.Name,
                             P_type_id = a.P_type_id,
                             P_type_Name = c.FullName,
                             Remark = a.Remark,
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

        // POST api/product_type
        /*上傳json格式
        {
            "P_id": "A000000006",
            "P_type_id": "A0003",
            "Remark": ""
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ProductTypesDto value)
        {
            try
            {
                Product_type insert = new Product_type
                {
                    P_id = value.P_id,
                    P_type_id = value.P_type_id,
                    Remark = value.Remark,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Product_type.Add(insert);
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

        // PUT api/product_type/{id}
        /*上傳json格式
        {
            "P_id": "A000000006",
            "P_type_id": "A0003",
            "Remark": ""
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductTypesDto value)
        {
            var result = (from a in _test10Context.Product_type
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
                    result.P_type_id = value.P_type_id;
                    result.Remark = value.Remark;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Product_type.Update(result);
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

        // DELETE api/product_type/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _test10Context.Product_type
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
                    _test10Context.Product_type.Remove(result);
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
