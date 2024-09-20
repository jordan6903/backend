using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/account_per")]
    [ApiController]
    public class AccountPersController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public AccountPersController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        // GET: api/account_per
        [HttpGet]
        public ActionResult<IEnumerable<AccountPersDto>> Get(string? searchword, int? Permission_id)
        {
            var result = from a in _test10Context.Account_per
                         join b in _test10Context.Account_info on a.Account_id equals b.Account_id
                         join c in _test10Context.Permission_set on a.Permission_id equals c.Permission_id
                         orderby a.Permission_id, a.Account_id
                         select new
                         {
                             Id = a.Id,
                             Account_id = a.Account_id,
                             Account_Name = b.Name,
                             Permission_id = a.Permission_id,
                             Permission_Name = c.Name,
                             Password = a.Password,
                             Password_encrypt = a.Password_encrypt,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.Account_id.Contains(searchword) ||
                         a.Account_Name.Contains(searchword)
                );
            }

            if (Permission_id != null)
            {
                result = result.Where(
                    a => a.Permission_id == Permission_id
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/account_per/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<AccountPersDto>> GetSingle(string id)
        {
            var result = from a in _test10Context.Account_per
                         join b in _test10Context.Account_info on a.Account_id equals b.Account_id
                         join c in _test10Context.Permission_set on a.Permission_id equals c.Permission_id
                         orderby a.Permission_id, a.Account_id
                         select new
                         {
                             Id = a.Id,
                             Account_id = a.Account_id,
                             Account_Name = b.Name,
                             Permission_id = a.Permission_id,
                             Permission_Name = c.Name,
                             Password = a.Password,
                             Password_encrypt = a.Password_encrypt,
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

        // POST api/account_per
        /*上傳json格式
        {
            "Account_id": "user01",
            "Permission_id": 2,
            "Password": "test2",
            "Password_encrypt": "test1",
            "Remark": "test1"
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] AccountPersDto value)
        {
            try
            {
                Account_per insert = new Account_per
                {
                    Account_id = value.Account_id,
                    Permission_id = value.Permission_id,
                    Password = value.Password,
                    Password_encrypt = value.Password_encrypt,
                    Remark = value.Remark,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Account_per.Add(insert);
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

        // PUT api/account_per/{id}
        /*上傳json格式
        {
            "Account_id": "user01",
            "Permission_id": 2,
            "Password": "test2",
            "Password_encrypt": "test1",
            "Remark": "test1"
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] AccountPersDto value)
        {
            var result = (from a in _test10Context.Account_per
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
                    result.Permission_id = value.Permission_id;
                    result.Password = value.Password;
                    result.Password_encrypt = value.Password_encrypt;
                    result.Remark = value.Remark;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Account_per.Update(result);
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

        // DELETE api/account_per/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = (from a in _test10Context.Account_per
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
                    _test10Context.Account_per.Remove(result);
                    _test10Context.SaveChanges();

                    // 回傳成功訊息
                    return Ok(new { message = "資料刪除成功" });
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
                    return BadRequest(new { message = "資料刪除失敗", error = ex.Message });
                }
            }
        }
    }
}
