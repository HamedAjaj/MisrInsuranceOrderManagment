namespace MisrInsuranceOrderManagment.DTOs
{
    public class CreateOrderDto
    {
        public int CustomerID { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
