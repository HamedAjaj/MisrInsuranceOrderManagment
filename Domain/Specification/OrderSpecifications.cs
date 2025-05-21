using Microsoft.EntityFrameworkCore;
using MisrInsuranceOrderManagment.Domain.Entities;
 
namespace MisrInsuranceOrderManagment.Domain.Specification
{
    public class OrderSpecifications : BaseSpecification<Order>
    {
        public OrderSpecifications(int id) : base(ord => ord.ID == id)
        {
            AddInclude(query => query.Include(ord => ord.Customer)); 
            AddInclude(query => query.Include(ord => ord.OrderItems).ThenInclude(or=>or.Product));

            AddOrderByDesc(ord => ord.OrderDate);
        }
    }

}
