using GeekAPI.Controllers.Alex;
using Microsoft.AspNetCore.Mvc;

namespace GeekAPI.Controllers.Lili
{
    [ApiController]
    public class ProfileManagmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _geekDbConnectionString;

        public ProfileManagmentController(IConfiguration configuration)
        {
            _configuration = configuration;
            _geekDbConnectionString = _configuration.GetConnectionString("GeekDBConnectionString");
        }

        [Route("api/[controller]")]
        [HttpGet]
        public JsonResult ExampleGet()
        {
            int topNum = 100;
            string sqlQuery = $@"
USE [GeekStore]

SELECT * from [dbo].[Users]";

            return Alex.SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);
        }

        [Route("api/GetByUsername/{Username}")]
        [HttpGet]
        public JsonResult GetByUsername(string Username)
        {
            string sqlQuery = $@"
            USE [GeekStore]

            SELECT [Username], [UserID],[Name], [Email], [HomeAddress]
            FROM [GeekStore].[dbo].[USERS]
            WHERE [username]= '{Username}'";

            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);

        }

        [Route("api/UpdateInfo/{UserId}")]
        [HttpPost]
        public JsonResult UpdateInfo(int UserId, String Username, String Password, String Name, String HomeAddress )
        {
           Console.WriteLine(Username);
            Console.WriteLine(Password);
            Console.WriteLine(Name);
            Console.WriteLine(HomeAddress);

            string sqlQuery = $@"
            USE [GeekStore]

            UPDATE UserCreation
            Set [Username]= '{Username}', [Password]= '{Password}', [Name]= '{Name}', [HomeAddress]= '{HomeAddress}'
            Where [UserId] = {UserId}";

            

            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);

        }





    }
}
