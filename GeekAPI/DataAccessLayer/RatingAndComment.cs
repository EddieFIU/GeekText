using System.Data.SqlClient;
using System.Data;
using GeekAPI.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace GeekAPI.DataAccessLayer
{

    public class RatingAndComment
    {
        private readonly IConfiguration _configuration;

        public RatingAndComment(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public DataTable GetAllRatings()
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
    }
}
