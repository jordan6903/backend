﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/staff")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public StaffsController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/staff
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<StaffsDto>> Get(string? searchword, int? staff_typeid)
        {
            var result = from a in _GalDBContext.Staff
                         join b in _GalDBContext.Product on a.P_id equals b.P_id
                         join c in _GalDBContext.Staff_info on a.Staff_id equals c.Staff_id
                         join d in _GalDBContext.Staff_type on a.Staff_typeid equals d.Staff_typeid
                         orderby a.P_id, a.Staff_typeid
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = b.Name,
                             Staff_id = a.Staff_id,
                             Staff_Name = c.Name,
                             Staff_typeid = a.Staff_typeid,
                             Staff_type_Name = d.Name,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(searchword) ||
                         a.P_Name.Contains(searchword) ||
                         a.Staff_id.Contains(searchword) ||
                         a.Staff_Name.Contains(searchword)
                );
            }

            if (staff_typeid != null)
            {
                result = result.Where(
                    a => a.Staff_typeid == staff_typeid
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/staff/mainpage
        [HttpGet("mainpage")]
        [Authorize]
        public ActionResult<IEnumerable<StaffsDto>> mainpage(string? searchword, int? staff_typeid, int page = 1, int pageSize = 10)
        {
            var result = from a in _GalDBContext.Staff
                         join b in _GalDBContext.Product on a.P_id equals b.P_id
                         join c in _GalDBContext.Staff_info on a.Staff_id equals c.Staff_id
                         join d in _GalDBContext.Staff_type on a.Staff_typeid equals d.Staff_typeid
                         orderby a.P_id, a.Staff_typeid
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = b.Name,
                             Staff_id = a.Staff_id,
                             Staff_Name = c.Name,
                             Staff_typeid = a.Staff_typeid,
                             Staff_type_Name = d.Name,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(searchword) ||
                         a.P_Name.Contains(searchword) ||
                         a.Staff_id.Contains(searchword) ||
                         a.Staff_Name.Contains(searchword)
                );
            }

            if (staff_typeid != null)
            {
                result = result.Where(
                    a => a.Staff_typeid == staff_typeid
                );
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

        // GET api/staff/{id}
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<StaffsDto>> GetSingle(int? id)
        {
            var result = from a in _GalDBContext.Staff
                         join b in _GalDBContext.Product on a.P_id equals b.P_id
                         join c in _GalDBContext.Staff_info on a.Staff_id equals c.Staff_id
                         join d in _GalDBContext.Staff_type on a.Staff_typeid equals d.Staff_typeid
                         orderby a.P_id, a.Staff_typeid
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = b.Name,
                             Staff_id = a.Staff_id,
                             Staff_Name = c.Name,
                             Staff_typeid = a.Staff_typeid,
                             Staff_type_Name = d.Name,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null)
            {
                result = result.Where(
                    a => a.Id == id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/staff/getbypid
        [HttpGet("getbypid")]
        [Authorize]
        public ActionResult<IEnumerable<StaffsDto>> GetByPid(string id)
        {
            var result = from a in _GalDBContext.Staff
                         join b in _GalDBContext.Product on a.P_id equals b.P_id
                         join c in _GalDBContext.Staff_info on a.Staff_id equals c.Staff_id
                         join d in _GalDBContext.Staff_type on a.Staff_typeid equals d.Staff_typeid
                         orderby a.P_id, a.Staff_typeid
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             P_Name = b.Name,
                             Staff_id = a.Staff_id,
                             Staff_Name = c.Name,
                             Staff_typeid = a.Staff_typeid,
                             Staff_type_Name = d.Name,
                             Remark = a.Remark,
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

        // POST api/staff
        /*上傳json格式
        {
            "P_id": "A000000014",
            "Staff_id": "S000000001",
            "Staff_typeid": 1,
            "Remark": "test"
        }
        */
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] StaffsDto value)
        {
            try
            {
                Staff insert = new Staff
                {
                    P_id = value.P_id,
                    Staff_id = value.Staff_id,
                    Staff_typeid = value.Staff_typeid,
                    Remark = value.Remark,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Staff.Add(insert);
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

        // PUT api/staff/{id}
        /*上傳json格式
        {
            "P_id": "A000000014",
            "Staff_id": "S000000001",
            "Staff_typeid": 1,
            "Remark": "test"
        }
        */
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] StaffsDto value)
        {
            var result = (from a in _GalDBContext.Staff
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
                    result.Staff_id = value.Staff_id;
                    result.Staff_typeid = value.Staff_typeid;
                    result.Remark = value.Remark;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;

                    _GalDBContext.Staff.Update(result);
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

        // DELETE api/staff/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var result = (from a in _GalDBContext.Staff
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
                    _GalDBContext.Staff.Remove(result);
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
