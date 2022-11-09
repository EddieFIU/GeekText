using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace GeekAPI.Controllers.Alex
{
    [ApiController]
    public class BookBrowsingController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _geekDbConnectionString;

        public BookBrowsingController(IConfiguration configuration)
        {
            _configuration = configuration;
            _geekDbConnectionString = _configuration.GetConnectionString("GeekDBConnectionString");
        }

        [Route("api/ExampleGet")]
        [HttpGet]
        public JsonResult ExampleGet()
        {
            int topNum = 100;
            string sqlQuery = $@"
            USE [GeekStore]

            SELECT TOP ({topNum}) [BookID]
                  ,[ISBN]
                  ,[Title]
                  ,[Description]
                  ,[Price]
                  ,[Genre]
                  ,[Publisher]
                  ,[YearPublished]
                  ,[CopiesSold]
                  ,[AuthorID]
            FROM [GeekStore].[dbo].[Books]";

            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }

        [Route("api/GetByGenre/{genre}")]
        [HttpGet]
        public JsonResult GetByGenre(string genre)
        {
            string sqlQuery = $@"
            USE [GeekStore]

            SELECT [BookID]
                  ,[ISBN]
                  ,[Title]
                  ,[Description]
                  ,[Price]
                  ,[Genre]
                  ,[Publisher]
                  ,[YearPublished]
                  ,[CopiesSold]
                  ,[AuthorID]
            FROM [GeekStore].[dbo].[Books]
            WHERE [Genre]='{genre}'";

            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }

        [Route("api/GetTop10Sold")]
        [HttpGet]
        public JsonResult GetTop10Sold()
        {
            string sqlQuery = @"
            USE [GeekStore]

            SELECT TOP (10) [BookID]
                  ,[ISBN]
                  ,[Title]
                  ,[Description]
                  ,[Price]
                  ,[Genre]
                  ,[Publisher]
                  ,[YearPublished]
                  ,[CopiesSold]
                  ,[AuthorID]
            FROM [dbo].[Books]
            ORDER BY [CopiesSold] DESC";

            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }

        [Route("api/GetByPosAndQty/{pos}/{qty}")]
        [HttpGet]
        public JsonResult GetByPosAndQty(int pos, int qty)
        {
            int pos2 = (pos + qty) - 1;
            string sqlQuery = $@"
            USE [GeekStore]

            SELECT [BookID]
                  ,[ISBN]
                  ,[Title]
                  ,[Description]
                  ,[Price]
                  ,[Genre]
                  ,[Publisher]
                  ,[YearPublished]
                  ,[CopiesSold]
                  ,[AuthorID]
            FROM [dbo].[Books]
            WHERE [BookID] BETWEEN {pos} AND {pos2};";

            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }

        [Route("api/GetByRating/{rating}")]
        [HttpGet]
        public JsonResult GetByRating(int rating)
        {
            string sqlQuery = $@"
            USE [GeekStore]

            SELECT [dbo].[Books].[BookID]
                  ,[ISBN]
                  ,[Title]
                  ,[Description]
                  ,[Price]
                  ,[Genre]
                  ,[Publisher]
                  ,[YearPublished]
                  ,[CopiesSold]
                  ,[AuthorID]
            FROM [dbo].[Books]
            INNER JOIN [dbo].[Rating] ON [dbo].[Books].[BookID] = [dbo].[Rating].[BookID]
            WHERE [RatingValue] >= '{rating}';";

            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }

        [Route("api/Example")]
        [HttpGet]
        public JsonResult Example()
        {
            string sqlQuery = @"
            SELECT TOP 5
                   [BookID],
                   [ISBN],
                   [Title] As 'Test.Title',
                   [Description],
                   [Price],
                   [Genre],
                   b.[Publisher],
                   [YearPublished],
                   [CopiesSold],
                   [FirstName] + ' ' + [LastName] As 'Author Name'
            FROM [GeekStore].[dbo].[Books] AS b
            INNER JOIN [dbo].[Authors] AS a ON b.[AuthorID] = a.[AuthorID]";

            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }
    }
}
