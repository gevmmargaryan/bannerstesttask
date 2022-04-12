namespace Banners.Infrastructure.Structures
{
    public class Pagination
    {
        public int Page { get; set; }
        public int Count { get; set; }

        public Pagination() 
        {
            Page = 1;
            Count = 10;
        }
    }
}
