using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi2.Dtos;
using MyApi2.Models;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApi2.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public ProductsController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }
        /*
        // GET: api/product
        [HttpGet]
        public IActionResult Get()
        {
            var ls_sql = @"
                SELECT [A].[C_id],
	                [A].[Name],
	                [A].[Name_origin],
	                [A].[Name_short],
	                [A1].[C_type_name],
	                [A].[Intro],
	                [A].[Remark],
	                [A].[Sdate],
	                [A].[Edate],
	                [B].[P_id],
	                [B].[Name],
	                [B].[C_Name]
                FROM [Company] AS [A]
                LEFT JOIN [Company_type] AS [A1] ON [A].[C_type] = [A1].[C_type]
                INNER JOIN [Product] AS [B] ON [B].[C_id] = [A].[C_id]

                order by [A].[C_id], [B].[P_id];
                ";
            var device = _GalDBContext.Product.FromSqlRaw(ls_sql);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        // GET api/product/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var device = _GalDBContext.Device
                    .FromSqlRaw("SELECT Device_id,FullName,ShortName FROM Device WHERE device_id = {0}", id)
                    .Select(d => new
                    {
                        d.Device_id,
                        d.FullName,
                        d.ShortName,
                    })
                    .FirstOrDefault();

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }
        */

        // GET: api/product
        [HttpGet]
        public ActionResult<IEnumerable<ProductsDto>> Get(string? searchword, string? searchword2)
        {
            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Company on a.C_id equals b.C_id
                         orderby a.C_id, a.P_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             C_id = a.C_id,
                             Name = a.Name,
                             C_Name = a.C_Name,
                             Content = a.Content,
                             Content_style = a.Content_style,
                             Play_time = a.Play_time,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             Company_name = b.Name,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(searchword) ||
                         a.Name.Contains(searchword) ||
                         a.C_Name.Contains(searchword)
                );
            }

            if (searchword2 != null)
            {
                result = result.Where(
                    a => a.C_id.Contains(searchword2) ||
                         a.Company_name.Contains(searchword2)
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/product/limit
        [HttpGet("limit")]
        public ActionResult<IEnumerable<ProductsDto>> GetLimit(string? searchword, string? searchword2)
        {
            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Company on a.C_id equals b.C_id
                         orderby a.C_id, a.P_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             C_id = a.C_id,
                             Name = a.Name,
                             C_Name = a.C_Name,
                             Content = a.Content,
                             Content_style = a.Content_style,
                             Play_time = a.Play_time,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             Company_name = b.Name,
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(searchword) ||
                         a.Name.Contains(searchword) ||
                         a.C_Name.Contains(searchword)
                );
            }

            if (searchword2 != null)
            {
                result = result.Where(
                    a => a.C_id.Contains(searchword2) ||
                         a.Company_name.Contains(searchword2)
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.Take(300));
        }

        // GET api/company/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ProductsDto>> GetSingle(string id)
        {
            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Company on a.C_id equals b.C_id
                         orderby a.C_id, a.P_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             C_id = a.C_id,
                             Name = a.Name,
                             C_Name = a.C_Name,
                             Content = a.Content,
                             Content_style = a.Content_style,
                             Play_time = a.Play_time,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             Company_name = b.Name,
                         };

            if (id != null)
            {
                result = result.Where(a => a.P_id == id);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/product/GetByCompany/{id}
        [HttpGet("getbycompany/{id}")]
        public ActionResult<IEnumerable<ProductsDto>> GetByCompany(string id)
        {
            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Company on a.C_id equals b.C_id
                         orderby a.C_id, a.P_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             C_id = a.C_id,
                             Name = a.Name,
                             P_CName = a.C_Name,
                             C_Name = b.Name,
                             Content = a.Content,
                             Content_style = a.Content_style,
                             Play_time = a.Play_time,
                             Remark = a.Remark,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             Company_name = b.Name
                         };

            if (id != null)
            {
                result = result.Where(a => a.C_id == id);
            }

            // Distinct 過濾重複的結果
            //var distinctResult = result.Distinct().ToList();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/product/GetByCompanytt/{id}
        [HttpGet("getbycompanytt/{id}")]
        public ActionResult<IEnumerable<ProductsViewsDto>> GetByCompanyTT(string id)
        {
            //var result = from a in _GalDBContext.Product
            //             join b in _GalDBContext.Company on a.C_id equals b.C_id
            //             join c in _GalDBContext.Translation_team on a.P_id equals c.P_id into TT
            //             join d1 in
            //                 (from prd in _GalDBContext.Product_Release_day
            //                  group prd by prd.P_id into g
            //                  select new
            //                  {
            //                      P_id = g.Key,
            //                      Sale_Date = g.Min(x => x.Sale_Date)
            //                  }) on a.P_id equals d1.P_id
            //             join d in _GalDBContext.Product_Release_day
            //                 on new { d1.P_id, d1.Sale_Date } equals new { d.P_id, d.Sale_Date }
            //             orderby a.C_id, a.P_id
            //             select new
            //             {
            //                 Id = a.Id,
            //                 P_id = a.P_id,
            //                 C_id = a.C_id,
            //                 Name = a.Name,
            //                 P_CName = a.C_Name,
            //                 C_Name = b.Name,
            //                 Content = a.Content,
            //                 Content_style = a.Content_style,
            //                 Play_time = a.Play_time,
            //                 Remark = a.Remark,
            //                 Sale_date = d.Sale_Date,
            //                 Upd_user = a.Upd_user,
            //                 Upd_date = a.Upd_date,
            //                 Create_dt = a.Create_dt,
            //                 Company_name = b.Name,
            //                 eso_chk = false,
            //                 TT_type = TT.Select(c => new ProductsViews2Dto
            //                 {
            //                     Type_id = c.Type_id,
            //                     Type_Name = (from d in _GalDBContext.Translation_team_type
            //                                  where c.Type_id == d.Type_id
            //                                  select d.Name).FirstOrDefault(),
            //                 })
            //             };

            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Company on a.C_id equals b.C_id
                         join c in _GalDBContext.Translation_team on a.P_id equals c.P_id into TT
                         join d in _GalDBContext.Product_Release_day on a.P_id equals d.P_id
                         where d.Official_First == true
                         orderby a.C_id, a.P_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             C_id = a.C_id,
                             Name = a.Name,
                             P_CName = a.C_Name,
                             C_Name = b.Name,
                             Content = a.Content,
                             Content_style = a.Content_style,
                             Play_time = a.Play_time,
                             Remark = a.Remark,
                             Sale_date = d.Sale_Date,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             Company_name = b.Name,
                             eso_chk = false,
                             TT_type = TT.Select(c => new ProductsViews2Dto
                             {
                                 Type_id = c.Type_id,
                                 Type_Name = (from d in _GalDBContext.Translation_team_type
                                              where c.Type_id == d.Type_id
                                              select d.Name).FirstOrDefault(),
                             })
                         };

            if (id != null)
            {
                result = result.Where(a => a.C_id == id);
            }

            // Distinct 過濾重複的結果
            //var distinctResult = result.Distinct().ToList();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/product/getforother
        [HttpGet("getforother")]
        public ActionResult<IEnumerable<ProductsViewsDto>> GetForOther()
        {
            //var result = from a in _GalDBContext.Product
            //             join b in _GalDBContext.Company on a.C_id equals b.C_id
            //             join c in _GalDBContext.Translation_team on a.P_id equals c.P_id into TT
            //             join d1 in
            //                 (from prd in _GalDBContext.Product_Release_day
            //                  group prd by prd.P_id into g
            //                  select new
            //                  {
            //                      P_id = g.Key,
            //                      Sale_Date = g.Min(x => x.Sale_Date)
            //                  }) on a.P_id equals d1.P_id
            //             join d in _GalDBContext.Product_Release_day
            //                 on new { d1.P_id, d1.Sale_Date } equals new { d.P_id, d.Sale_Date }
            //             orderby a.C_id, a.P_id
            //             select new
            //             {
            //                 Id = a.Id,
            //                 P_id = a.P_id,
            //                 C_id = a.C_id,
            //                 Name = a.Name,
            //                 P_CName = a.C_Name,
            //                 C_Name = b.Name,
            //                 Content = a.Content,
            //                 Content_style = a.Content_style,
            //                 Play_time = a.Play_time,
            //                 Remark = a.Remark,
            //                 Sale_date = d.Sale_Date,
            //                 Upd_user = a.Upd_user,
            //                 Upd_date = a.Upd_date,
            //                 Create_dt = a.Create_dt,
            //                 Company_name = b.Name,
            //                 TT_type = TT.Select(c => new ProductsViews2Dto
            //                 {
            //                     Type_id = c.Type_id,
            //                     Type_Name = (from d in _GalDBContext.Translation_team_type
            //                                  where c.Type_id == d.Type_id
            //                                  select d.Name).FirstOrDefault(),
            //                 })
            //             };

            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Company on a.C_id equals b.C_id
                         join c in _GalDBContext.Translation_team on a.P_id equals c.P_id into TT
                         join d in _GalDBContext.Product_Release_day on a.P_id equals d.P_id
                         where d.Official_First == true
                         orderby a.C_id, a.P_id
                         select new
                         {
                             Id = a.Id,
                             P_id = a.P_id,
                             C_id = a.C_id,
                             Name = a.Name,
                             P_CName = a.C_Name,
                             C_Name = b.Name,
                             Content = a.Content,
                             Content_style = a.Content_style,
                             Play_time = a.Play_time,
                             Remark = a.Remark,
                             Sale_date = d.Sale_Date,
                             Upd_user = a.Upd_user,
                             Upd_date = a.Upd_date,
                             Create_dt = a.Create_dt,
                             Company_name = b.Name,
                             TT_type = TT.Select(c => new ProductsViews2Dto
                             {
                                 Type_id = c.Type_id,
                                 Type_Name = (from d in _GalDBContext.Translation_team_type
                                              where c.Type_id == d.Type_id
                                              select d.Name).FirstOrDefault(),
                             })
                         };

            // Distinct 過濾重複的結果
            //var distinctResult = result.Distinct().ToList();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /*
        SELECT [B].[C_id] AS [C_id], 
		        [B].[Name] AS [C_Name],
		        [A].[P_id] AS [P_id], 
		        [A].[Name] AS [P_Name], 
		        [A].[C_Name] AS [P_CName], 
		        [A].[Content] AS [P_Content], 
		        [A].[Content_style] AS [Content_style], 
		        [A].[Play_time] AS [Play_time], 
		        [A].[Remark] AS [P_Remark],

		        [C].[Id] AS [R_id],
		        [C].[Sale_Date] AS [Sale_Date],
                [C].[Presale_Date] AS [Presale_Date],
		        [C].[Price] AS [Price],
		        [C].[Content] AS [R_Content],
                [C].[Voice_id] AS [Voice_id],
		        [C1].[Name] AS [Voice_Name],
		        [C].[Currency_id] AS [Currency_id],
		        [C2].[ShortName] AS [Currency_Name],
		        [C].[Device_id] AS [Device_id],
		        [C3].[FullName] AS [Device_Name],
                [C].[Rating_id] AS [Rating_id],
		        [C4].[Name] AS [Rating_Name],

		        [D].[Id] AS [PW_id],
		        [D].[Type_id] AS [PW_Type_id],
		        [D1].[Name] AS [PW_Type_Name],
		        [D].[Name] AS [PW_Name],
		        [D].[Url] AS [PW_Url],
		        [D].[Remark] AS [PW_Remark],

		        [E].[Id] AS [PP_id],
		        [E].[Type_id] AS [PP_Type_id],
		        [E1].[Name] AS [PP_Type_Name],
		        [E].[Name] AS [PP_Name],
		        [E].[Url] AS [PP_Url],
                [E].[width] AS [PP_width],
		        [E].[height] AS [PP_height],
		        [E].[Remark] AS [PP_Remark],

		        [F].[P_type_id] AS [P_type_id],
		        [F1].[FullName] AS [P_type_Name],
		        [F].[Remark] AS [P_type_Remark],

		        [G].[P_id_to] AS [P_id_to],
		        [G1].[Name] AS [P_id_to_Name],
		        [G].[Relation_id] AS [Relation_id],
		        [G2].[Name] AS [Relation_Name],
		        [G].[Content] AS [Relation_Content],

		        [H].[Type_id] AS [PS_Type_id],
		        [H1].[Name] AS [PS_Type_Name],
		        [H].[Score] AS [PS_Score],

		        [I].[Staff_id] AS [Staff_id],
		        [I1].[Name] AS [Staff_Name],
		        [I].[Staff_typeid] AS [Staff_typeid],
		        [I2].[Name] AS [Staff_type_Name],
		        [I].[Remark] AS [Staff_Remark]

          FROM [GalDB].[dbo].[Product] AS [A]
          INNER JOIN [Company] AS [B] ON [A].[C_id]=[B].[C_id]

          LEFT JOIN [Product_Release_day] AS [C] ON [A].[P_id]=[C].[P_id] --發售日 多筆
          LEFT JOIN [Voice_type] AS [C1] ON [C].[Voice_id]=[C1].[Voice_id]
          LEFT JOIN [Currency] AS [C2] ON [C].[Currency_id]=[C2].[Currency_id]
          LEFT JOIN [Device] AS [C3] ON [C].[Device_id]=[C3].[Device_id]
          LEFT JOIN [Rating] AS [C4] ON [C].[Rating_id]=[C4].[Rating_id]

          LEFT JOIN [Product_Website] AS [D] ON [A].[P_id]=[D].[P_id] AND [D].[Use_yn]=1 --網址 多筆
          LEFT JOIN [Website_Type] AS [D1] ON [D].[Type_id]=[D1].[Type_id]

          LEFT JOIN [Product_Pic] AS [E] ON [A].[P_id]=[E].[P_id] AND [E].[Use_yn]=1 --圖片 多筆
          LEFT JOIN [Website_Type] AS [E1] ON [E].[Type_id]=[E1].[Type_id]

          LEFT JOIN [Product_type] AS [F] ON [F].[P_id]=[A].[P_id] --屬性/標籤 多筆
          LEFT JOIN [Product_type_info] AS [F1] ON [F].[P_type_id]=[F1].[P_type_id]

          LEFT JOIN [Product_relation] AS [G] ON [G].[P_id]=[A].[P_id] --關聯遊戲 多筆
          LEFT JOIN [Product] AS [G1] ON [G].[P_id]=[G1].[P_id]
          LEFT JOIN [Product_relation_info] AS [G2] ON [G].[Relation_id]=[G2].[Relation_id]

          LEFT JOIN [Product_score] AS [H] ON [H].[P_id]=[A].[P_id] --遊戲評分 多筆
          LEFT JOIN [Product_score_type] AS [H1] ON [H].[Type_id]=[H1].[Type_id]

          LEFT JOIN [Staff] AS [I] ON [I].[P_id]=[A].[P_id] --製作人員 多筆
          LEFT JOIN [Staff_info] AS [I1] ON [I].[Staff_id]=[I1].[Staff_id]
          LEFT JOIN [Staff_type] AS [I2] ON [I].[Staff_typeid]=[I2].Staff_typeid

          order by [B].[C_id], [A].[P_id] 
        */

        // GET: api/product
        [HttpGet("View1")]
        public ActionResult<IEnumerable<ProductsView1Dto>> GetView1(string? searchword, string? searchword2)
        {
            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Company on a.C_id equals b.C_id
                         //join c in _GalDBContext.Product_Release_day on a.P_id equals c.P_id into Release
                         //join d in _GalDBContext.Product_Website on a.P_id equals d.P_id into Website
                         orderby a.C_id, a.P_id
                         select new
                         {
                             C_id = b.C_id,
                             C_Name = b.Name,
                             P_id = a.P_id,
                             P_Name = a.Name,
                             P_CName = a.C_Name,
                             P_Content = a.Content,
                             Content_style = a.Content_style,
                             Play_time = a.Play_time,
                             P_Remark = a.Remark
                             //發售日 多筆
                             //Release_data = Release.Select(c => new ProductReleaseDaysDto
                             //{
                             //    Id = c.Id,
                             //    Sale_Date = c.Sale_Date,
                             //    Presale_Date = c.Presale_Date,
                             //    Price = c.Price,
                             //    Voice_id = c.Voice_id,
                             //    Voice_Name = (from c1 in _GalDBContext.Voice_type
                             //                 where c1.Voice_id == c.Voice_id
                             //                  select c1.Name).FirstOrDefault(),
                             //    Currency_id = c.Currency_id,
                             //    Currency_Name = (from c2 in _GalDBContext.Currency
                             //                     where c2.Currency_id == c.Currency_id
                             //                     select c2.ShortName).FirstOrDefault(),
                             //    Content = c.Content,
                             //    Device_id = c.Device_id,
                             //    Device_Name = (from c3 in _GalDBContext.Device
                             //                   where c3.Device_id == c.Device_id
                             //                   select c3.FullName).FirstOrDefault(),
                             //    Rating_id = c.Rating_id,
                             //    Rating_Name = (from c4 in _GalDBContext.Rating_type
                             //                   where c4.Rating_type1 == c.Rating_id
                             //                   select c4.Name).FirstOrDefault(),
                             //}).ToList(),
                             //網址 多筆
                             //Website_data = Website.Select(d => new ProductWebsitesDto
                             //{
                             //    Id = d.Id,
                             //    Type_id = d.Type_id,
                             //    Type_Name = (from d1 in _GalDBContext.Website_Type
                             //                 where d1.Type_id == d.Type_id
                             //                 select d1.Name).FirstOrDefault(),
                             //    Name = d.Name,
                             //    Url = d.Url,
                             //    Remark = d.Remark,
                             //}).ToList(),
                         };

            if (searchword != null)
            {
                result = result.Where(
                    a => a.P_id.Contains(searchword) ||
                         a.P_Name.Contains(searchword) ||
                         a.P_CName.Contains(searchword)
                );
            }

            if (searchword2 != null)
            {
                result = result.Where(
                    a => a.C_id.Contains(searchword2) ||
                         a.C_Name.Contains(searchword2)
                );
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.Take(300));
        }

        // GET: api/product/getnewpid
        [HttpGet("getnewpid")]
        public ActionResult<string> GetNewpid(string searchword)
        {
            var result = (from a in _GalDBContext.Product
                         where a.P_id.Contains(searchword)
                         orderby a.P_id descending
                         select a.P_id).FirstOrDefault();

            if (result == null)
            {
                return "NAN";
            }

            return result;
        }

        // POST api/product
        /*上傳json格式
        {
            "P_id": "A000000000",
            "C_id": "C000000001",
            "Name": "test",
            "C_Name": "test2",
            "Content": "test3",
            "Content_style": "test4",
            "Play_time": "",
            "Remark": ""
        }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ProductsDto value)
        {
            var isExists = _GalDBContext.Product.Any(a => a.P_id == value.P_id);

            if (isExists)
            {
                return Ok(new { message = "N#資料上傳失敗, 已有相同代碼" });
            }

            try
            {
                Product insert = new Product
                {
                    P_id = value.P_id,
                    C_id = value.C_id,
                    Name = value.Name,
                    C_Name = value.C_Name,
                    Content = value.Content,
                    Content_style = value.Content_style,
                    Play_time = value.Play_time,
                    Remark = value.Remark,
                    Upd_user = value.Upd_user,
                    Upd_date = DateTime.Now,
                    Create_dt = DateTime.Now,
                };
                _GalDBContext.Product.Add(insert);
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

        // PUT api/product/{id}
        /*上傳json格式
        {
            "P_id": "A000000000",
            "C_id": "C000000001",
            "Name": "test",
            "C_Name": "test2",
            "Content": "test3",
            "Content_style": "test4",
            "Play_time": "",
            "Remark": ""
        }
        */
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ProductsDto value)
        {
            var result = (from a in _GalDBContext.Product
                          where a.P_id == id
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
                    result.C_id = value.C_id;
                    result.Name = value.Name;
                    result.C_Name = value.C_Name;
                    result.Content = value.Content;
                    result.Content_style = value.Content_style;
                    result.Play_time = value.Play_time;
                    result.Remark = value.Remark;
                    result.Upd_user = value.Upd_user;
                    result.Upd_date = DateTime.Now;
                    result.Create_dt = DateTime.Now;

                    _GalDBContext.Product.Update(result);
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

        // DELETE api/product/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = (from a in _GalDBContext.Product
                          where a.P_id == id
                          select a).SingleOrDefault();

            if (result == null)
            {
                return NotFound(new { message = "N#資料刪除失敗，未搜尋到該id" });
            }
            else
            {
                try
                {
                    _GalDBContext.Product.Remove(result);
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
