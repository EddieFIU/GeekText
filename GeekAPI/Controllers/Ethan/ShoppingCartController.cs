using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace GeekAPI.Controllers.Ethan
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _geekDbConnectionString;

        public ShoppingCartController(IConfiguration configuration)
        {
            _configuration = configuration;
            _geekDbConnectionString = _configuration.GetConnectionString("GeekDBConnectionString");
        }
        [HttpGet]
        public JsonResult ExampleGet()
        {
            string sqlQuery = $@"
            USE [GeekStore]
    
            SELECT *
            from [dbo].[ShoppingCart] 
            join [dbo].[Books] on [dbo].[Books].[BookID] = [dbo].[ShoppingCart].[BookID]";
            return Alex.SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }
        //GET .../api/ShoppingCart/{id}
        [HttpGet("{id}")]
        public JsonResult GetID(int id)
        {
            string sqlQuery = $@"
            USE [GeekStore]
    
            SELECT *
            from [dbo].[ShoppingCart] 
            where ShoppingCartId = " + id;
            return Alex.SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }//
        [HttpPut]
        public JsonResult put(Models.ShoppingCart sc)
        {
            DataTable booksTable = new();
            SqlDataReader reader;
            string sqlQuery = $@"
            USE [GeekStore]
            
            update ShoppingCart 
            set BookID = @BookID 
            where ShoppingCartID = @ShoppingCartID";

            using (SqlConnection conn = new SqlConnection(_geekDbConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@BookID", sc.BookID);
                    cmd.Parameters.AddWithValue("@ShoppingCartID", sc.ShoppingCartID);
                    reader = cmd.ExecuteReader();
                    booksTable.Load(reader);
                    reader.Close();
                    conn.Close();
                }
            }
            return new JsonResult(booksTable);
        }
        [HttpDelete("{id}")]
        public JsonResult deleteBook(Models.ShoppingCart sc)
        {
            DataTable booksTable = new DataTable();
            SqlDataReader sqlDataReader;
            string sqlQuery = $@"
            USE [GeekStore]
            
            update ShoppingCart 
            set [ShoppingCart].BookID = [Books].BookID
            from [Books]
            where [books].isbn = 0 and ShoppingCartID = @ShoppingCartID;";

            using (SqlConnection conn = new SqlConnection(_geekDbConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                        cmd.Parameters.AddWithValue("@ShoppingCartID", sc.ShoppingCartID);
                        sqlDataReader = cmd.ExecuteReader();
                        booksTable.Load(sqlDataReader);
                        sqlDataReader.Close();
                        conn.Close();
                }
            }
            return new JsonResult("deleted");
        }
        [HttpPost]
        public JsonResult Create(Models.ShoppingCart sc)
        {
            DataTable booksTable = new DataTable();
            SqlDataReader sqlDataReader;
            string sqlQuery = @"insert into ShoppingCart(UserID,BookID) values(@UserID,@BookID);";

            using (SqlConnection conn = new SqlConnection(_geekDbConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", sc.UserID);
                    cmd.Parameters.AddWithValue("@BookID", sc.BookID);
                    sqlDataReader = cmd.ExecuteReader();
                    booksTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    conn.Close();
                }
            }
            return new JsonResult("Created Shopping Cart");
        }
    }

}
