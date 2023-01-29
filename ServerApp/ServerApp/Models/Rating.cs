namespace ServerApp.Models
{
    public class Rating
    {
        public long RatingId { get; set; }
        public int Stars { get; set; } = 0;
        public Product Product { get; set; }

    }

}
