using System.ComponentModel.DataAnnotations;

namespace MisrInsuranceOrderManagment.Domain.Entities
{
    public class Customer :BaseEntity
    {    
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
    }
}
