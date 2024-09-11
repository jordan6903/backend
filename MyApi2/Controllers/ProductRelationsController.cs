﻿using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/product_relation")]
    [ApiController]
    public class ProductRelationsController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public ProductRelationsController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/product_relation
        [HttpGet]
        public ActionResult<IEnumerable<ProductRelationsDto>> Get(string? searchword)
        {
            var result = from a in _test10Context.Product_relation
                         join b in _test10Context.Product on a.P_id equals b.P_id
                         join c in _test10Context.Product on a.P_id_to equals c.P_id
                         join d in _test10Context.Product_relation_info on a.Relation_id equals d.Relation_id
                         orderby a.P_id, a.Relation_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = b.Name,
                             P_id_to = a.P_id_to,
                             P_Name_to = c.Name,
                             Relation_id = a.Relation_id,
                             Relation_Name = d.Name,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(searchword) ||
                         a.P_id_to.Contains(searchword) ||
                         a.P_Name.Contains(searchword) ||
                         a.P_Name_to.Contains(searchword)
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/product_relation/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ProductRelationsDto>> GetSingle(int id)
        {
            var result = from a in _test10Context.Product_relation
                         join b in _test10Context.Product on a.P_id equals b.P_id
                         join c in _test10Context.Product on a.P_id_to equals c.P_id
                         join d in _test10Context.Product_relation_info on a.Relation_id equals d.Relation_id
                         orderby a.P_id, a.Relation_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = b.Name,
                             P_id_to = a.P_id_to,
                             P_Name_to = c.Name,
                             Relation_id = a.Relation_id,
                             Relation_Name = d.Name,
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

        // POST api/product_relation
        /*上傳json格式
        {
            "P_id": "A000000324",
            "P_id_to": "A000000325",
            "Relation_id": "1",
            "Content": "test"
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ProductRelationsDto value)
        {
            try
            {
                Product_relation insert = new Product_relation
                {
                    P_id = value.P_id,
                    P_id_to = value.P_id_to,
                    Relation_id = value.Relation_id,
                    Content = value.Content,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Product_relation.Add(insert);
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

        // PUT api/product_relation/{id}
        /*上傳json格式
        {
            "P_id": "A000000324",
            "P_id_to": "A000000325",
            "Relation_id": "1",
            "Content": "test"
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductRelationsDto value)
        {
            var result = (from a in _test10Context.Product_relation
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
                    result.P_id_to = value.P_id_to;
                    result.Relation_id = value.Relation_id;
                    result.Content = value.Content;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Product_relation.Update(result);
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

        // DELETE api/product_relation/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _test10Context.Product_relation
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
                    _test10Context.Product_relation.Remove(result);
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
