namespace MisrInsuranceOrderManagment.Domain.Entities
{
    public class OrderItem
    {
        public OrderItem()
        {

        }
        public OrderItem (int orderID, int quantity, decimal subtotal, Product product)
        {
            OrderID = orderID;
            Quantity = quantity;
            Subtotal = subtotal; 
            Product = product; 
        }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }

        public    Product Product { get; set; } 
    }
}
