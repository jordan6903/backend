﻿using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/permission_set")]
    [ApiController]
    public class PermissionSetsController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public PermissionSetsController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/permission_set
        [HttpGet]
        public ActionResult<IEnumerable<PermissionSetsDto>> Get(string? searchword)
        {
            var result = from a in _test10Context.Permission_set
                         orderby a.Permission_id
                         select new
                         {
                             Permission_id = a.Permission_id,
                             Name = a.Name,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.Name.Contains(searchword)
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/permission_set/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<PermissionSetsDto>> GetSingle(int id)
        {
            var result = from a in _test10Context.Permission_set
                         orderby a.Permission_id
                         select new
                         {
                             Permission_id = a.Permission_id,
                             Name = a.Name,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(a => a.Permission_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/permission_set
        /*上傳json格式
        {
            "Permission_id": 5,
            "Name": "test1",
            "Remark": "test2"
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] PermissionSetsDto value)
        {
            try
            {
                Permission_set insert = new Permission_set
                {
                    Permission_id = value.Permission_id,
                    Name = value.Name,
                    Remark = value.Remark,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Permission_set.Add(insert);
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

        // PUT api/permission_set/{id}
        /*上傳json格式
        {
            "Permission_id": 5,
            "Name": "test1",
            "Remark": "test2"
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PermissionSetsDto value)
        {
            var result = (from a in _test10Context.Permission_set
                          where a.Permission_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.Permission_id = value.Permission_id;
                    result.Name = value.Name;
                    result.Remark = value.Remark;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Permission_set.Update(result);
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

        // DELETE api/permission_set/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = (from a in _test10Context.Permission_set
                          where a.Permission_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _test10Context.Permission_set.Remove(result);
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