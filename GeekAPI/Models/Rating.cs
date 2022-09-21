namespace GeekAPI.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public DateTime RatingDate { get; set; }

        public int RatingValue { get; set; }
        
        public int RatingUser { get; set; }
        
        public int BookID { get; set; } 

    }
}
