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

        //GET list of books from authors table
        [Route("api/GetBookList/{authorsLastName}")]
        [HttpGet]
        public JsonResult GetBookList(string authorsLastName)
        {
            string sqlQuery = $@"
            USE [GeekStore]

            SELECT [Books]
            FROM [GeekStore].[dbo].[Authors]
            WHERE [LastName]='{authorsLastName}'";

            return Alex.SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }

        //Add a book into the the library
        [Route("api/CreateAuthor")]
        [HttpPost]
        public JsonResult CreateAuthor(String firstName, String lastName, String biography, String publisher)
        {
            string sqlQuery = $@"
            USE [GeekStore]

            INSERT INTO [Geekstore].[dbo].[Authors] ([FirstName], [LastName], [Biography], [Publisher])
            VALUES ('{firstName}', '{lastName}', '{biography}', '{publisher}')";

            return new JsonResult(new {message = "A new author has been created."});

            //return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }

        //Create a new author
        [Route("api/CreateBook")]
        [HttpPost]
        public JsonResult CreateBook(long isbn, String title, String description, int price, String genre, String publisher, int year, int copies)
        {
            string sqlQuery = $@"
            USE [GeekStore]

            INSERT INTO [Geekstore].[dbo].[Books] ([ISBN], [Title], [Description], [Price], [Genre], [Publisher], [YearPublished], [CopiesSold])
            VALUES ('{isbn}', '{title}', '{description}', '{price}', '{genre}', '{publisher}', '{year}', '{copies}')";

            return new JsonResult(new { message = "A new book has been added to the library." });

            //return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }
    }
}
