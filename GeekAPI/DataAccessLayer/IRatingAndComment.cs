using System.Data.SqlClient;
using System.Data;
using GeekAPI.Models;

namespace GeekAPI.DataAccessLayer
{
    public interface IRatingAndComment
    {
        
       
        public DataTable GetAverageBookRating(int bookId);


        public DataTable GetAllHighestRatings();

        public int CreateRating(Rating newRating);


        public int CreateComment(Comment newComment);

        public DataTable GetCommentByID(int commentID);

        public DataTable GetCommentByRatingID(int ratingID);
    }
}

