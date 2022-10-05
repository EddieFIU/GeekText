using GeekAPI.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;


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
            RatingAndComment ratingCommentInfo = new RatingAndComment(_configuration);

            return new JsonResult(ratingCommentInfo.GetAllRatings());
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
