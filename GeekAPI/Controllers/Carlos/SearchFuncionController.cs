using Microsoft.AspNetCore.Mvc;

namespace GeekAPI.Controllers.Carlos
{
    [Route("api/[controller]")]
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
        [HttpGet("{name}")]
        public JsonResult GetName(string name)
        {
            string sqlQuery = $@"
            USE [GeekStore]
    
            SELECT *
            from [dbo].[Books] 
            where BookName = " + name;
            return Alex.SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }
    }
}
