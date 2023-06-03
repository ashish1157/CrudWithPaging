namespace WebApplication3105.Models
{
    public class UpdateProductModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
