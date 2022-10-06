using GeekAPI.DataAccessLayer;
using GeekAPI.Models;
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
        }

        [HttpPost]
        public JsonResult Post(Models.Rating newRating)
        {
            RatingAndComment ratingCommentInfo = new RatingAndComment(_configuration);
            if (newRating.RatingValue>5 || newRating.RatingValue<1)
            {
                return new JsonResult("Rating needs to be with-in 1 and 5.");
            }
            int newRatingID = ratingCommentInfo.CreateRating(newRating);
            if (newRatingID > 0)
            {
                return new JsonResult("Created rating successfullly with ID: " + newRatingID.ToString());
            }
            else
            {
                return new JsonResult("Issue creating rating");
            }

        }

        // GET api/<Rating>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
