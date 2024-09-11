﻿using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/account_info")]
    [ApiController]
    public class AccountInfosController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public AccountInfosController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/account_info
        [HttpGet]
        public ActionResult<IEnumerable<AccountInfosDto>> Get(string? searchword)
        {
            var result = from a in _test10Context.Account_info
                         orderby a.Account_id
                         select new
                         {
                             Account_id = a.Account_id,
                             Name = a.Name,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.Account_id.Contains(searchword) ||
                         a.Name.Contains(searchword)
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/account_info/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<AccountInfosDto>> GetSingle(string id)
        {
            var result = from a in _test10Context.Account_info
                         orderby a.Account_id
                         select new
                         {
                             Account_id = a.Account_id,
                             Name = a.Name,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(a => a.Account_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/account_info
        /*上傳json格式
        {
            "Account_id": "user01",
            "Name": "user01_name",
            "Remark": "test"
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] AccountInfosDto value)
        {
            try
            {
                Account_info insert = new Account_info
                {
                    Account_id = value.Account_id,
                    Name = value.Name,
                    Remark = value.Remark,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Account_info.Add(insert);
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

        // PUT api/account_info/{id}
        /*上傳json格式
        {
            "Account_id": "user01",
            "Name": "user01_name",
            "Remark": "test"
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] AccountInfosDto value)
        {
            var result = (from a in _test10Context.Account_info
                          where a.Account_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.Account_id = value.Account_id;
                    result.Name = value.Name;
                    result.Remark = value.Remark;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Account_info.Update(result);
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

        // DELETE api/account_info/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = (from a in _test10Context.Account_info
                          where a.Account_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _test10Context.Account_info.Remove(result);
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