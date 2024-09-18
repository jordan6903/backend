﻿using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/product_score")]
    [ApiController]
    public class ProductScoresController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public ProductScoresController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/product_score
        [HttpGet]
        public ActionResult<IEnumerable<ProductScoresDto>> Get(string? searchword, int? type_id)
        {
            var result = from a in _test10Context.Product_score
                         join b in _test10Context.Product_score_type on a.Type_id equals b.Type_id
                         join c in _test10Context.Product on a.P_id equals c.P_id
                         orderby a.P_id, a.Type_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = c.Name,
                             Type_id = a.Type_id,
                             Type_Name = b.Name,
                             Score = a.Score,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(searchword) ||
                         a.P_Name.Contains(searchword)
                );
            }

            if (type_id != null)
            {
                result = result.Where(
                    a => a.Type_id == type_id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/product_score/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<StaffsDto>> GetSingle(string id)
        {
            var result = from a in _test10Context.Product_score
                         join b in _test10Context.Product_score_type on a.Type_id equals b.Type_id
                         join c in _test10Context.Product on a.P_id equals c.P_id
                         orderby a.P_id, a.Type_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = c.Name,
                             Type_id = a.Type_id,
                             Type_Name = b.Name,
                             Score = a.Score,
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

        // POST api/product_score
        /*上傳json格式
        {
            "P_id": "A000000003",
            "Type_id": 1,
            "Score": 60
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ProductScoresDto value)
        {
            try
            {
                Product_score insert = new Product_score
                {
                    P_id = value.P_id,
                    Type_id = value.Type_id,
                    Score = value.Score,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Product_score.Add(insert);
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

        // PUT api/product_score/{id}
        /*上傳json格式
        {
            "P_id": "A000000003",
            "Type_id": 1,
            "Score": 60
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductScoresDto value)
        {
            var result = (from a in _test10Context.Product_score
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
                    result.Score = value.Score;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Product_score.Update(result);
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

        // DELETE api/product_score/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _test10Context.Product_score
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
                    _test10Context.Product_score.Remove(result);
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
