﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/staff_info")]
    [ApiController]
    public class StaffInfosController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public StaffInfosController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/staff_info
        [HttpGet]
        public ActionResult<IEnumerable<StaffInfoDto>> Get(string? searchword)
        {
            var result = from a in _test10Context.Staff_info
                         orderby a.Id
                         select new
                         {
                             Id = a.Id,
                             Staff_id = a.Staff_id,
                             Name = a.Name,
                             Content = a.Content,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.Name.Contains(searchword) ||
                         a.Content.Contains(searchword) ||
                         a.Staff_id.Contains(searchword)
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/staff_info/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<StaffInfoDto>> GetSingle(string id)
        {
            var result = from a in _test10Context.Staff_info
                         orderby a.Id
                         select new
                         {
                             Id = a.Id,
                             Staff_id = a.Staff_id,
                             Name = a.Name,
                             Content = a.Content,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(a => a.Staff_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/staff_info/getmaxstaffid
        [HttpGet("getmaxstaffid")]
        public ActionResult<string> GetMaxStaffId()
        {
            var result = (from a in _test10Context.Staff_info
                         orderby a.Staff_id descending
                         select a.Staff_id).FirstOrDefault();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/staff_info
        /*上傳json格式
        {
            "Staff_id": "S000000006",
            "Name": "test",
            "Content": "test1"
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] StaffInfoDto value)
        {
            var isExists = _test10Context.Staff_info.Any(a => a.Staff_id == value.Staff_id);

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Staff_info insert = new Staff_info
                {
                    Staff_id = value.Staff_id,
                    Name = value.Name,
                    Content = value.Content,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Staff_info.Add(insert);
                _test10Context.SaveChanges();

                // 回傳成功訊息
                return Ok(new { message = "Y#資料上傳成功" });
            }
            catch (Exception ex)
            {
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return BadRequest(new { message = "N#資料上傳失敗", error = ex.Message });
            }

        }

        // PUT api/staff_info/{id}
        /*上傳json格式
        {
            "Staff_id": "S000000006",
            "Name": "test",
            "Content": "test1"
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] StaffInfoDto value)
        {
            var result = (from a in _test10Context.Staff_info
                          where a.Staff_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.Staff_id = value.Staff_id;
                    result.Name = value.Name;
                    result.Content = value.Content;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Staff_info.Update(result);
                    _test10Context.SaveChanges();

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

        // DELETE api/staff_info/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = (from a in _test10Context.Staff_info
                          where a.Staff_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _test10Context.Staff_info.Remove(result);
                    _test10Context.SaveChanges();

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
