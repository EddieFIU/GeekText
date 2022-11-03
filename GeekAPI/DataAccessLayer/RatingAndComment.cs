using System.Data.SqlClient;
using System.Data;
using GeekAPI.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace GeekAPI.DataAccessLayer
{

    public class RatingAndComment: IRatingAndComment
    {
        private readonly IConfiguration _configuration;

        public RatingAndComment(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// SELECT   avg([RatingValue])              FROM[GeekStore].[dbo].[Rating]        where BookID =
        /// </summary>
        /// <returns></returns>

        public DataTable GetAverageBookRating(int bookId)
        {
            string qry = @"SELECT  avg([RatingValue]) as AverageBookRating FROM[GeekStore].[dbo].[Rating] where BookID =@BookID;";
            DataTable bookRatingAvg = new DataTable();
            string geekDbConnectionString = _configuration.GetConnectionString("GeekDBConnectionString");
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(geekDbConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookId);

                    reader = cmd.ExecuteReader();

                    bookRatingAvg.Load(reader);

                    reader.Close();
                    conn.Close();

                }
            }
            return bookRatingAvg;

        }

        public DataTable GetAllHighestRatings()
        {
            string qry = @"SELECT *    
                        FROM [GeekStore].[dbo].[Rating] r
                        
                        order by RatingValue desc;";
            DataTable ratingTable = new();
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
            return ratingTable;
        }
        public int CreateRating(Rating newRating)
        {
            string qry = @"Insert into Rating
                        ([RatingDate]
                        ,[RatingValue]
                        ,[RatingUser]
                        ,[BookID])   
                          output INSERTED.RatingID  Values(@RatingDate,@RatingValue,@RatingUser,@BookID);";
            DataTable ratingTable = new DataTable();
            string geekDbConnectionString = _configuration.GetConnectionString("GeekDBConnectionString");
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(geekDbConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    cmd.Parameters.AddWithValue("@RatingDate", newRating.RatingDate);
                    cmd.Parameters.AddWithValue("@RatingValue", newRating.RatingValue);
                    cmd.Parameters.AddWithValue("@RatingUser", newRating.RatingUser);
                    cmd.Parameters.AddWithValue("@BookID", newRating.BookID);

                    reader = cmd.ExecuteReader();

                    ratingTable.Load(reader);

                    reader.Close();
                    conn.Close();

                }
            }
            return ratingTable is null ? -1 : int.Parse(ratingTable.Rows[0]["RatingID"].ToString());

        }

        public int CreateComment(Comment newComment)
        {
            string qry = @"Insert into Comment
                        ([UserID]
                        ,[RatingID]
                        ,[BookID]
                        ,[Comment]
                        ,[CreateDate])   
                          output INSERTED.CommentID  Values(@UserID,@RatingID,@BookID,@Comment,@CreatedDate);";
            DataTable commentTable = new DataTable();
            string geekDbConnectionString = _configuration.GetConnectionString("GeekDBConnectionString");
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(geekDbConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", newComment.UserID);
                    cmd.Parameters.AddWithValue("@RatingID", newComment.RatingID);                    
                    cmd.Parameters.AddWithValue("@BookID", newComment.BookID);
                    cmd.Parameters.AddWithValue("@Comment", newComment.CommentValue);
                    cmd.Parameters.AddWithValue("@CreatedDate", newComment.CreatedDateTime);

                    reader = cmd.ExecuteReader();

                    commentTable.Load(reader);

                    reader.Close();
                    conn.Close();

                }
            }
            return commentTable is null ? -1 : int.Parse(commentTable.Rows[0]["CommentID"].ToString());

        }

        public bool UpdateComment(Models.Comment updatedComment)
        {
            DataTable commentTable = new DataTable();
            string geekDbConnectionString = _configuration.GetConnectionString("GeekDBConnectionString");
            SqlDataReader reader;

            string sqlQuery = $@"
            USE [GeekStore]
            
            update Comment 
            set Comment = @Comment 
            where CommentID = @CommentId";
            try
            {
                using (SqlConnection conn = new SqlConnection(geekDbConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Comment", updatedComment.CommentValue);
                        cmd.Parameters.AddWithValue("@CommentId", updatedComment.CommentId);
                        reader = cmd.ExecuteReader();
                        commentTable.Load(reader);
                        reader.Close();
                        conn.Close();
                    }
                }
                return true;
            }
            catch  {
                throw;
            }
        }
            
            

        public DataTable GetCommentByID(int commentID)
        {
            string qry = @"SELECT [CommentID]
                            ,[UserID]
                            ,[RatingID]
                            ,[BookID]
                            ,[Comment]
                            ,[CreateDate]
                            FROM [dbo].[Comment] where [CommentID]=@CommentID;";
            DataTable commentTable = new DataTable();
            string geekDbConnectionString = _configuration.GetConnectionString("GeekDBConnectionString");
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(geekDbConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    cmd.Parameters.AddWithValue("@CommentID", commentID);
                    
                    reader = cmd.ExecuteReader();

                    commentTable.Load(reader);

                    reader.Close();
                    conn.Close();

                }
            }
            return commentTable;

        }



        public DataTable GetCommentByRatingID(int ratingID)
        {
            string qry = @"SELECT [CommentID]
                            ,[UserID]
                            ,[RatingID]
                            ,[BookID]
                            ,[Comment]
                            ,[CreateDate]
                            FROM [dbo].[Comment] where [RatingID]=@RatingID;";
            DataTable commentTable = new DataTable();
            string geekDbConnectionString = _configuration.GetConnectionString("GeekDBConnectionString");
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(geekDbConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    cmd.Parameters.AddWithValue("@RatingID", ratingID);

                    reader = cmd.ExecuteReader();

                    commentTable.Load(reader);

                    reader.Close();
                    conn.Close();

                }
            }
            return commentTable;

        }
    }
}
