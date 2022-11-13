using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GeekAPI.Controllers.Carlos
{

    [ApiController]
    public class SearchFunctionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _geekDbConnectionString;

        public SearchFunctionController(IConfiguration configuration)
        {
            _configuration = configuration;
            _geekDbConnectionString = _configuration.GetConnectionString("GeekDBConnectionString");
        }
        //GET book name
        [Route("api/GetDetails/{isbn}")]
        [HttpGet]
        public JsonResult GetDetails(string isbn)
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
            WHERE [ISBN]='{isbn}'";

            return Alex.SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }
    }
}
