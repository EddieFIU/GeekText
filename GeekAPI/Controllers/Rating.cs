using GeekAPI.DataAccessLayer;
using GeekAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;


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
        public JsonResult GetHighestRatingsWithComments()
        {
            RatingAndComment ratingCommentInfo = new RatingAndComment(_configuration);
            
            return new JsonResult(ratingCommentInfo.GetAllHighestRatings());            
        }

        [HttpGet("{bookId}")]
        public JsonResult GetAverageBookRating(int bookId)
        {
            RatingAndComment ratingCommentInfo = new RatingAndComment(_configuration);
            DataTable avgBookRating= ratingCommentInfo.GetAverageBookRating(bookId);
            if (avgBookRating.Rows[0][0].ToString() !="")
            {
               return new JsonResult(avgBookRating);
            }
            return new JsonResult("Book doesn't exist");
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
                return new JsonResult("Created rating successfully with ID: " + newRatingID.ToString());
            }
            else
            {
                return new JsonResult("Issue creating rating");
            }

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
