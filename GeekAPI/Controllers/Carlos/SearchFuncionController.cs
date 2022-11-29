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
        //GET book details
        [Route("api/GetBookDetails/{isbn}")]
        [HttpGet]
        public JsonResult GetBookDetails(long isbn)
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
            WHERE [ISBN]={isbn}";

            return Alex.SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }

        //GET list of books from authors
        [Route("api/GetBookList/{books}")]
        [HttpGet]
        public JsonResult GetBookList(string books)
        {
            string sqlQuery = $@"
            USE [GeekStore]

            SELECT [Books]
            FROM [GeekStore].[dbo].[Authors]
            WHERE [Books]='{books}'";

            return Alex.SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }
    }
}
