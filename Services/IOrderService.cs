using MisrInsuranceOrderManagment.Domain.Entities;

namespace MisrInsuranceOrderManagment.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderWithDetailsAsync(int orderId);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> UpdateOrderStatusAsync(int id, string status);
    }
}
