using MisrInsuranceOrderManagment.Domain.Entities;
using MisrInsuranceOrderManagment.Domain.Specification;
using MisrInsuranceOrderManagment.Infrastructure.UnitOfwork;
 
namespace MisrInsuranceOrderManagment.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfwork _unitOfWork; 

        public OrderService(IUnitOfwork unitOfWork )
        {
            _unitOfWork = unitOfWork; 
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");

            if (order.CustomerID == 0)
                throw new ArgumentException("CustomerID is missing or invalid.");

            // Fetch the customer
            var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(order.CustomerID);
            if (customer == null)
                throw new InvalidOperationException($"Customer with ID {order.CustomerID} does not exist.");

            if (order.OrderItems == null || !order.OrderItems.Any())
                return null; 

            var productIds = order.OrderItems.Select(item => item.ProductID).ToList();
            var products = new List<Product>();
            foreach (var id in productIds)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
                if (product != null)
                {
                    products.Add(product);
                }
            }

            var validOrderItems = new List<OrderItem>();

            foreach (var item in order.OrderItems)
            {
                var product = products.FirstOrDefault(p => p.ID == item.ProductID);
                if (product == null)
                { 
                    return null;
                }
                product.StockQuantity -= item.Quantity;
                item.Subtotal = item.Quantity * product.Price;
                validOrderItems.Add(item);


                if (product.StockQuantity < item.Quantity)
                { 
                    return null;
                }

               
                _unitOfWork.Repository<Product>().Update(product);
            }

            if (!validOrderItems.Any())
                return null;

            order.OrderItems = validOrderItems;

            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.Save();

            return order;
        }





        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var spec = new OrderSpecifications(id);
            return await _unitOfWork.Repository<Order>().GetByIdWithSpecificationAsync(spec);
        }

        public async Task<Order> UpdateOrderStatusAsync(int id, string status)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(id);
            if (order == null)
                throw new KeyNotFoundException("Order not found");

            order.Status = status;
            await _unitOfWork.Save();

            return order;
        }

        public async Task<Order> GetOrderWithDetailsAsync(int orderId)
        {
            var spec = new OrderSpecifications(orderId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecificationAsync(spec);
            return order;
        }

       
    }
}
