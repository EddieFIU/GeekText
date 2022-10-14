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
        }
        [HttpPut("{id}")]
        public void put(int id, [FromBody] String sc)
        {

        }
        [HttpDelete("{id}")]
        public string deleteSC(int id, ConfigurationManager configurationManager)
        {
            DataTable booksTable = new();
            string sqlQuery = $@"
            USE [GeekStore]
            
            DELETE FROM ShoppingCart WHERE ShoppingCartID =" + id;

            using (SqlConnection conn = new SqlConnection(_geekDbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        adapter.Fill(booksTable);
                    }
                }
            }
            return "Deleted";
        }
    }

}
