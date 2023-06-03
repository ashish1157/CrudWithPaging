namespace WebApplication3105.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public String CategoryName { get; set; }

        public ICollection<Product> Product { get; set; }



    }
}