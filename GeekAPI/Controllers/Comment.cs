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

     
        // POST api/<Comment>
        [HttpPost]
        public JsonResult Post(Models.Comment newComment)
        {
            IRatingAndComment ratingCommentInfo = new RatingAndComment(_configuration);
            
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

    }
}
