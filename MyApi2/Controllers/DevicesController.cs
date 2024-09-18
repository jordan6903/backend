using Microsoft.AspNetCore.Mvc;
using MyApi2.Models;
using MyApi2.Dtos;
using System.Diagnostics;

namespace MyApi2.Controllers
{
    [Route("api/device")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly test10Context _test10Context;

        public DevicesController(test10Context test10Context)
        {
            _test10Context = test10Context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DevicesDto>> Get(string? searchword, string? UseYN)
        {
            var result = from a in _test10Context.Device
                         orderby a.Sort
                         select new
                         {
                             Device_id = a.Device_id,
                             FullName = a.FullName,
                             ShortName = a.ShortName,
                             Content = a.Content,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.Device_id.Contains(searchword) || 
                         a.FullName.Contains(searchword) ||
                         a.ShortName.Contains(searchword)
                );
            }

            if (UseYN != null)
            {
                if (UseYN == "Y")
                {
                    result = result.Where( a => a.Use_yn == true );
                }
                else if (UseYN == "N") 
                {
                    result = result.Where( a => a.Use_yn == false );
                }
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/device
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<DevicesDto>> Get(string id)
        {
            var result = from a in _test10Context.Device
                         select new
                         {
                             Device_id = a.Device_id,
                             FullName = a.FullName,
                             ShortName = a.ShortName,
                             Content = a.Content,
                             Use_yn = a.Use_yn,
                             Sort = a.Sort,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                         };

            if (id != null && id != "%")
            {
                result = result.Where(a => a.Device_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }



        // POST api/device
        /*上傳json格式
        {
            "device_id": "A0015",
            "fullName": "123",
            "shortName": "DOS",
            "content": "test",
            "use_yn": false,
            "sort": 999
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] Device value)
        {
            var isExists = _test10Context.Device.Any(a => a.Device_id == value.Device_id);

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }
            
            try
            {
                Device insert = new Device
                {
                    Device_id = value.Device_id,
                    FullName = value.FullName,
                    ShortName = value.ShortName,
                    Content = value.Content,
                    Use_yn = value.Use_yn,
                    Sort = value.Sort,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _test10Context.Device.Add(insert);
                _test10Context.SaveChanges();

                // 回傳成功訊息
                return Ok(new { message = "Y#資料上傳成功" });
            }
            catch (Exception ex)
            {
                // 捕捉錯誤並回傳詳細的錯誤訊息
                return BadRequest(new { message = "資料上傳失敗", error = ex.Message });
            }
            
        }

        // PUT api/device/{id}
        /*上傳json格式
        {
            "device_id": "A0015",
            "fullName": "123",
            "shortName": "DOS",
            "content": "test",
            "use_yn": false,
            "sort": 999
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Device value)
        {
            var result = (from a in _test10Context.Device
                         where a.Device_id == id
                         select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料更新失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    result.FullName = value.FullName;
                    result.ShortName = value.ShortName;
                    result.Content = value.Content;
                    result.Use_yn = value.Use_yn;
                    result.Sort = value.Sort;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _test10Context.Device.Update(result);
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

        // DELETE api/device/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = (from a in _test10Context.Device
                          where a.Device_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _test10Context.Device.Remove(result);
                    _test10Context.SaveChanges();

                    // 回傳成功訊息
                    return Ok(new { message = "Y#資料刪除成功" });
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
