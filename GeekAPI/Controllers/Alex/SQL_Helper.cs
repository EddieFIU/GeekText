using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace GeekAPI.Controllers.Alex
{
    public static class SQL_Helper
    {
        public static JsonResult GetDbData(string connectionString, string queryScript)
        {
            DataTable booksTable = new();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(queryScript, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    booksTable.Load(reader);
                    reader.Close();
                    conn.Close();
                }
            }

            return new JsonResult(booksTable);
        }

    }
}
