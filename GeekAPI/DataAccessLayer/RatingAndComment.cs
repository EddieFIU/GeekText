using System.Data.SqlClient;
using System.Data;

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
    }
}
