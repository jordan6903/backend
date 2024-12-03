using Microsoft.AspNetCore.Mvc;
using MyApi2.Dtos;
using MyApi2.Models;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyApi2.Controllers
{
    [Route("api/view")]
    [ApiController]
    public class ViewsController : ControllerBase
    {
        private readonly GalDBContext _GalDBContext;

        public ViewsController(GalDBContext GalDBContext)
        {
            _GalDBContext = GalDBContext;
        }

        // GET: api/view
        [HttpGet]
        public ActionResult<IEnumerable<ViewDto1>> Get(string? searchword, int? C_type)
        {
            var result = from a in _GalDBContext.Company
                         join a1 in _GalDBContext.Company_type on a.C_type equals a1.C_type
                         join b in _GalDBContext.Product on a.C_id equals b.C_id
                         orderby a.C_id, b.P_id
                         select new
                         {
                             C_id = a.C_id,
                             C_Name = a.Name,
                             Name_origin = a.Name_origin,
                             Name_short = a.Name_short,
                             C_type = a.C_type,
                             C_type_name = a1.C_type_name,
                             Intro = a.Intro,
                             Remark = a.Remark,
                             Sdate = a.Sdate,
                             Edate = a.Edate
                         };

            // Distinct 過濾重複的結果
            var distinctResult = result.Distinct().ToList();

            // 重新組合子集合 Products
            var finalResult = from a in distinctResult
                              join b in _GalDBContext.Product on a.C_id equals b.C_id into products
                              select new
                              {
                                  C_id = a.C_id,
                                  C_Name = a.C_Name,
                                  Name_origin = a.Name_origin,
                                  Name_short = a.Name_short,
                                  C_type = a.C_type,
                                  C_type_name = a.C_type_name,
                                  Intro = a.Intro,
                                  Remark = a.Remark,
                                  Sdate = a.Sdate,
                                  Edate = a.Edate,
                                  Products = products.Select(b => new ViewDto1_2
                                  {
                                      P_id = b.P_id,
                                      P_Name = b.Name,
                                      P_CName = b.C_Name,
                                  }).ToList(),
                              };

            if (searchword != null)
            {
                finalResult = finalResult.Where(
                    a => a.C_Name.Contains(searchword) ||
                         a.C_id.Contains(searchword) ||
                         a.Name_origin.Contains(searchword) ||
                         a.Name_short.Contains(searchword) ||
                         a.Products.Any(b => b.P_id.Contains(searchword) ||
                                             b.P_Name.Contains(searchword) ||
                                             b.P_CName.Contains(searchword))
                );
            }

            if (C_type != null)
            {
                finalResult = finalResult.Where(a => a.C_type == C_type);
            }

            if (finalResult == null)
            {
                return NotFound();
            }

            return Ok(finalResult.Take(300));
        }

        // GET api/view/
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/view/
        [HttpGet("get1to4")]
        public ActionResult<IEnumerable<View1to4Dto>> Get1to4()
        {
            //var result = from a in _GalDBContext.Company
            //             join b in _GalDBContext.Product on a.C_id equals b.C_id
            //             join c1 in
            //                 (from prd in _GalDBContext.Product_Release_day
            //                  group prd by prd.P_id into g
            //                  select new
            //                  {
            //                      P_id = g.Key,
            //                      Sale_Date = g.Min(x => x.Sale_Date)
            //                  }) on b.P_id equals c1.P_id
            //             join c in _GalDBContext.Product_Release_day
            //                 on new { c1.P_id, c1.Sale_Date } equals new { c.P_id, c.Sale_Date }
            //             join d in _GalDBContext.Translation_team on b.P_id equals d.P_id
            //             orderby a.C_id, b.P_id
            //             select new
            //             {
            //                 Id = a.Id,
            //                 C_id = a.C_id,
            //                 C_Name = a.Name,
            //                 C_Name_origin = a.Name_origin,
            //                 P_id = b.P_id,
            //                 P_Name = b.Name,
            //                 P_CName = b.C_Name,
            //                 Sale_Date = c.Sale_Date,
            //                 T_id = "",
            //                 T_team = "",
            //                 T_type = "",
            //                 Remark = "",
            //                 url1 = "",
            //                 url2 = "",
            //                 url3 = "",
            //                 url4 = "",
            //                 pic = ""
            //             };

            var result = from a in _GalDBContext.Company
                         join b in _GalDBContext.Product on a.C_id equals b.C_id
                         join c in _GalDBContext.Product_Release_day on b.P_id equals c.P_id
                         join d in _GalDBContext.Translation_team on b.P_id equals d.P_id
                         where c.Official_First == true
                         orderby a.C_id, b.P_id
                         select new
                         {
                             Id = a.Id,
                             C_id = a.C_id,
                             C_Name = a.Name,
                             C_Name_origin = a.Name_origin,
                             P_id = b.P_id,
                             P_Name = b.Name,
                             P_CName = b.C_Name,
                             Sale_Date = c.Sale_Date,
                             T_id = "",
                             T_team = "",
                             T_type = "",
                             Remark = "",
                             url1 = "",
                             url2 = "",
                             url3 = "",
                             url4 = "",
                             pic = ""
                         };

            // Distinct 過濾重複的結果
            var distinctResult = result.Distinct().ToList();

            if (distinctResult == null)
            {
                return NotFound();
            }

            return Ok(distinctResult);

            //SELECT DISTINCT
            //        [A].[c_id] AS[C_id],
            //  CASE WHEN([A].[Name_origin] IS NOT NULL AND [A].[Name_origin]<> '') THEN[A].[Name] + ' (' + [A].[Name_origin] + ')'

            //            ELSE[A].[Name]

            //            END AS[C_Name],

            //        [B].[Name] AS[P_Name],
            //  [B].[C_Name] AS[P_CName],
            //  [C].[Sale_Date] AS[Sale_Date]
            //  FROM[GalDB].[dbo].[Company] as [A]
            //  inner join[Product] as [B] ON[A].[C_id] = [B].[C_id]
            //  inner JOIN(
            //    SELECT P_id, MIN(Sale_Date) AS Sale_Date
            //    FROM Product_Release_day
            //    GROUP BY P_id
            //  ) AS[C1] ON[B].[P_id] = [C1].[P_id]
            //  inner JOIN Product_Release_day AS[C] ON[C].[P_id] = [C1].[P_id] AND[C].[Sale_Date] = [C1].[Sale_Date]
            //  inner join[Translation_team] AS[D] ON[B].[P_id] = [D].[P_id]
        }

        // GET api/view/
        [HttpGet("get5to7")]
        public ActionResult<IEnumerable<View5to7Dto>> Get5to7()
        {
            // 定義排序順序
            //int[] customOrder = new int[] { 3, 4, 1, 6, 2, 7, 8, 5, 9, 10, 11 };

            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Translation_team on a.P_id equals b.P_id into TT
                         join c in _GalDBContext.Company on a.C_id equals c.C_id
                         orderby a.P_id
                         select new
                         {
                             P_id = a.P_id,
                             P_Name = a.Name,
                             P_CName = a.C_Name,
                             C_id = a.C_id,
                             C_Name = c.Name,
                             T_batch_data = TT.Select(b => new TTviewsDto1
                             {
                                 Id = b.Id,
                                 T_batch = b.T_batch,
                                 Type_id = b.Type_id,
                                 Remark = b.Remark,
                                 Type_Name = (from c in _GalDBContext.Translation_team_type
                                              where c.Type_id == b.Type_id
                                              select c.Name).FirstOrDefault(),
                                 TT_info = (from d in _GalDBContext.Translation_team_batch
                                            where b.Id == d.TT_id
                                            select new TTviewsDto2
                                            {
                                                T_id = d.T_id,
                                                T_Name = (from e in _GalDBContext.Translation_team_info
                                                          where e.T_id == d.T_id
                                                          select e.Name).FirstOrDefault(),
                                            }).ToList()
                             })
                             //.OrderBy(b => Array.IndexOf(customOrder, b.Type_id))
                             .ToList(),
                         };

            //過濾掉空集合
            var resultList = result.AsEnumerable()
                .Where(a => a.T_batch_data != null && a.T_batch_data.Any()); 

            if (resultList == null)
            {
                return NotFound();
            }

            return Ok(resultList);
        }

        // GET api/view/
        [HttpGet("get8to11")]
        public ActionResult<IEnumerable<View8to11Dto>> Get8to11()
        {
            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Product_Website on a.P_id equals b.P_id into Website
                         orderby a.P_id
                         select new
                         {
                             P_id = a.P_id,
                             Url_data = Website.Select( b => new View8to11Dto2 
                             {
                                 Type_id = b.Type_id,
                                 Name = b.Name,
                                 Url = b.Url,
                                 Remark = b.Remark,
                             }).ToList(),
                         };

            //過濾掉空集合
            var resultList = result.AsEnumerable().Where(a => a.Url_data != null && a.Url_data.Any());

            if (resultList == null)
            {
                return NotFound();
            }

            return Ok(resultList);
        }

        // GET api/view/
        [HttpGet("get12to")]
        public ActionResult<IEnumerable<View12toDto>> Get12to()
        {
            var result = from a in _GalDBContext.Product
                         join b in _GalDBContext.Product_Pic on a.P_id equals b.P_id into Website
                         orderby a.P_id
                         select new
                         {
                             P_id = a.P_id,
                             Pic_data = Website.Select(b => new View12toDto2
                             {
                                 Type_id = b.Type_id,
                                 Name = b.Name,
                                 Url = b.Url,
                                 width = b.width,
                                 height = b.height,
                                 Remark = b.Remark,
                             }).ToList(),
                         };

            //過濾掉空集合
            var resultList = result.AsEnumerable().Where(a => a.Pic_data != null && a.Pic_data.Any());

            if (resultList == null)
            {
                return NotFound();
            }

            return Ok(resultList);
        }
    }
}
