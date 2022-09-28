using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            join [dbo].[Books] on [dbo].[Books].[BookID] = [dbo].[ShoppingCart].[BooksID]";
            return SC_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
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
            return SC_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }
    }
}
