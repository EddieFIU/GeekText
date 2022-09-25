using Microsoft.AspNetCore.Mvc;

namespace GeekAPI.Controllers.Alex
{
    [Route("api/[controller]")]
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

        // GET: api/<BookBrowsingController>
        [HttpGet]
        public JsonResult Get()
        {
            int topNum = 100;
            string sqlQuery = $@"
USE [GeekStore]

SELECT TOP ({topNum}) [ISBN]
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

            return BBC_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }
    }
}
