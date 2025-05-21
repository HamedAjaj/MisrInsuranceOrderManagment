namespace MisrInsuranceOrderManagment.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {

        }
        public Product(string name, string description, decimal price, int stockQuantity)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
