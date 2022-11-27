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

        [Route("api/newUser")]
        [HttpPost]
        public JsonResult newUser( String Username, String Password,String Email, String Name, String HomeAddress)
        {


            string sqlQuery = $@"
            USE [GeekStore]

            INSERT INTO USERS ( [Username],[Password],[Name],[Email], [HomeAddress])
            VALUES ('{Username}',  '{Password}',  '{Name}',  '{Email}',  '{HomeAddress}')";

            //return new JsonResult(new { message = "A new user has been saved in the data base. Success!" });


            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);

        }



        [Route("api/newCC")]
        [HttpPost]
        public JsonResult newCC(String CardNumber, String ExpDate, String PIN, String Name, String ZipCode, int UserId)
        {


            string sqlQuery = $@"
            USE [GeekStore]

            INSERT INTO CREDITCARDS ( [CardNumber],[ExpDate],[PIN],[Name], [ZipCode], [UserId])
            VALUES ('{CardNumber}',  '{ExpDate}',  '{PIN}',  '{Name}',  '{ZipCode}', '{UserId}' )";

            //return new JsonResult(new { message = "A new card has been saved in the data base. Success!" });


            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);

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
           //Console.WriteLine(Username);
           //Console.WriteLine(Password);
            //Console.WriteLine(Name);
            //Console.WriteLine(HomeAddress);

            string sqlQuery = $@"
            USE [GeekStore]

            UPDATE USERS
            Set [Username]= '{Username}', [Password]= '{Password}', [Name]= '{Name}', [HomeAddress]= '{HomeAddress}'
            Where [UserId] = '{UserId}'";

            

            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);

        }

        [Route("api/GetByCC/{UserId}")]
        [HttpGet]
        public JsonResult GetByCC(int UserId)
        {
            string sqlQuery = $@"
            USE [GeekStore]

            SELECT [CCID], [CardNumber],[Name], [ExpDate], [PIN]
            FROM [GeekStore].[dbo].[CREDITCARDS]
            WHERE [UserId]= {UserId}";

            return SQL_Helper.GetDbData(_geekDbConnectionString, sqlQuery);

        }



    }
}
