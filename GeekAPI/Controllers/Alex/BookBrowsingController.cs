using Microsoft.AspNetCore.Mvc;

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

            return BBC_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }

        [Route("api/GetByGenre/{genre}")]
        [HttpGet]
        public JsonResult GetByGenre(string genre)
        {
            string sqlQuery = $@"
            USE [GeekStore]

            SELECT [Title]
            FROM [GeekStore].[dbo].[Books]
            WHERE [Genre]='{genre}'";

            return BBC_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }

        [Route("api/GetTop10Sold")]
        [HttpGet]
        public JsonResult GetTop10Sold()
        {
            string sqlQuery = @"
            USE [GeekStore]

            SELECT TOP (10) [Title], [CopiesSold]
            FROM [dbo].[Books]
            ORDER BY [CopiesSold] DESC";

            return BBC_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }
    }
}
