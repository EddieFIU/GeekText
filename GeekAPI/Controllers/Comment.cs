using GeekAPI.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeekAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Comment : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public Comment(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       
        // GET api/<Comment>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Comment>
        [HttpPost]
        public JsonResult Post(Models.Comment newComment)
        {
            RatingAndComment ratingCommentInfo = new RatingAndComment(_configuration);
            
            //setting default for the created date
            newComment.CreatedDateTime= DateTime.Now;   

            int newCommentID = ratingCommentInfo.CreateComment(newComment);
            if (newCommentID > 0)
            {
                return new JsonResult("Created comment successfullly with ID: " + newCommentID.ToString());
            }
            else
            {
                return new JsonResult("Issue creating comment");
            }

        }

        // PUT api/<Comment>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Comment>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
