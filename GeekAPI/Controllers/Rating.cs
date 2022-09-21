using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeekAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Rating : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public Rating(IConfiguration configuration)
        {
            _configuration = configuration;    
        }

        // GET: api/<Rating>
        [HttpGet]
        public JsonResult Get()
        {
            string qry = @"Select RatingID,RatingDate,RatingValue,RatingUser,BookID from Rating;";
            DataTable ratingTable = new DataTable();
            string geekDbConnectionString = _configuration.GetConnectionString("GeekDBConnectionString");
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(geekDbConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    reader = cmd.ExecuteReader();
                    ratingTable.Load(reader);
                    reader.Close();
                    conn.Close();   
                }
            }
            return new JsonResult(ratingTable);
            // JsonConvert.SerializeObject(ratingTable);
        }


        // GET api/<Rating>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Rating>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Rating>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Rating>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
