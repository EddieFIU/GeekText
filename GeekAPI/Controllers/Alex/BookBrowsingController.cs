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

        [Route("api/GetByGenre/{genre}")]
        [HttpGet]
        public JsonResult GetByGenre(string genre)
        {
            string sqlQuery = $@"
            USE [GeekStore]

            SELECT [BookID]
                  ,[ISBN]
                  ,[Title]
                  ,[FirstName] + ' ' + [LastName] AS 'Author'
                  ,[Description]
                  ,[Price]
                  ,[Genre]
                  ,b.[Publisher]
                  ,[YearPublished]
                  ,[CopiesSold]
            FROM [GeekStore].[dbo].[Books] AS b
            INNER JOIN [dbo].[Authors] AS a ON b.[AuthorID] = a.[AuthorID]
            WHERE[Genre] = '{genre}'";
;

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
                  ,[FirstName] + ' ' + [LastName] AS 'Author'
                  ,[Description]
                  ,[Price]
                  ,[Genre]
                  ,b.[Publisher]
                  ,[YearPublished]
                  ,[CopiesSold]
            FROM [dbo].[Books] AS b
            INNER JOIN [dbo].[Authors] AS a ON b.[AuthorID] = a.[AuthorID]
            ORDER BY [CopiesSold] DESC";

            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }

        [Route("api/GetByRating/{rating}")]
        [HttpGet]
        public JsonResult GetByRating(int rating)
        {
            string sqlQuery = $@"
            USE [GeekStore]

            SELECT b.[BookID]
                  ,[ISBN]
                  ,[Title]
                  ,[FirstName] + ' ' + [LastName] AS 'Author'
                  ,[Description]
                  ,[Price]
                  ,[Genre]
                  ,b.[Publisher]
                  ,[YearPublished]
                  ,[CopiesSold]
                  ,[RatingValue] AS 'Rating'
            FROM [dbo].[Books] AS b
            INNER JOIN [dbo].[Authors] AS a ON b.[AuthorID] = a.[AuthorID]
            INNER JOIN [dbo].[Rating] AS r ON b.[BookID] = r.[BookID]
            WHERE [RatingValue] >= '{rating}'
            ORDER BY [RatingValue]";

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
                  ,[FirstName] + ' ' + [LastName] AS 'Author'
                  ,[Description]
                  ,[Price]
                  ,[Genre]
                  ,b.[Publisher]
                  ,[YearPublished]
                  ,[CopiesSold]
            FROM [dbo].[Books] AS b
            INNER JOIN [dbo].[Authors] AS a ON b.[AuthorID] = a.[AuthorID]
            WHERE b.[BookID] BETWEEN {pos} AND {pos2};";

            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }
    }
}
