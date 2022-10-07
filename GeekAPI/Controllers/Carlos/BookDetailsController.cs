using Microsoft.AspNetCore.Mvc;

namespace GeekAPI.Controllers.Alex
{
    [ApiController]
    public class BookDetailsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _geekDbConnectionString;

        public BookDetailsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _geekDbConnectionString = _configuration.GetConnectionString("GeekDBConnectionString");
        }

        [Route("api/[controller]")]
        [Route("api/test")]
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
      ,[RatingID]
      ,[AuthorID]
  FROM [GeekStore].[dbo].[Books]";

            return Alex.SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }

    }
}
