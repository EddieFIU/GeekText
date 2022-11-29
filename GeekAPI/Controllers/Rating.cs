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
        /// <summary>My super duper data</summary>
        [HttpGet]
        public JsonResult GetHighestRatingsWithComments()
        {
            IRatingAndComment ratingCommentInfo = new RatingAndComment(_configuration);
            DataTable dataTable = ratingCommentInfo.GetAllHighestRatings();
            List<Models.Rating> ratingList = new List<Models.Rating>();
            foreach (DataRow row in dataTable.Rows)
            {
                Models.Rating rating = new Models.Rating();
                rating.RatingId = int.Parse(row["RatingID"].ToString());
                rating.RatingDate = DateTime.Parse(row["RatingDate"].ToString());
                rating.RatingValue = int.Parse(row["RatingValue"].ToString());
                rating.RatingUser = int.Parse(row["RatingUser"].ToString());
                rating.BookID = int.Parse(row["BookID"].ToString());
                ratingList.Add(rating);
            }

            foreach(Models.Rating rating in ratingList)
            {
                DataTable comment = ratingCommentInfo.GetCommentByRatingID(rating.RatingId);
                if (comment.Rows.Count>0)
                {
                    Models.Comment foundComment = new Models.Comment();
                    foundComment.CommentId = int.Parse(comment.Rows[0]["CommentId"].ToString());
                    foundComment.UserID = int.Parse(comment.Rows[0]["UserId"].ToString());
                    foundComment.CommentValue = comment.Rows[0]["Comment"].ToString();
                    foundComment.BookID = int.Parse(comment.Rows[0]["BookId"].ToString());
                    foundComment.RatingID = int.Parse(comment.Rows[0]["RatingId"].ToString());
                    foundComment.CreatedDateTime = DateTime.Parse(comment.Rows[0]["CreateDate"].ToString());
                    rating.RatingComment = foundComment;
                }
            }

            return new JsonResult(ratingList);            
        }

        [HttpGet("{bookId}")]
        public JsonResult GetAverageBookRating(int bookId)
        {
            IRatingAndComment ratingCommentInfo = new RatingAndComment(_configuration);
            DataTable avgBookRating= ratingCommentInfo.GetAverageBookRating(bookId);
            if (avgBookRating.Rows[0][0].ToString() !="")
            {
               return new JsonResult(avgBookRating);
            }
            return  new JsonResult(NotFound("Book doesn't exist") );
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


    }
}
