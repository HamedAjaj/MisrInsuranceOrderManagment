using System.Net;

namespace MisrInsuranceOrderManagment.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Order() { }
        public Order( int customerId , DateTime orderDate, string status , Customer customer ,ICollection<OrderItem> orderItems )
        {
            CustomerID = customerId;
            OrderDate = orderDate;
            Status = status;
            Customer = customer;
            OrderItems = orderItems;
        }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";

        public   Customer? Customer { get; set; }
        public   ICollection<OrderItem> OrderItems { get; set; }
    }
}
