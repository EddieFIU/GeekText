namespace GeekAPI.Models
{
    public class Comment
    {
        public int CommentId { get; set; } 
        public int UserID { get; set; }
        public int RatingID { get; set; }   
        public int BookID { get; set; } 
        public string CommentValue { get; set;}
        public DateTime CreatedDateTime { get; set; }   


    }
}
