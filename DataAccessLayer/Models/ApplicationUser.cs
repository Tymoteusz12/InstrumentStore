using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FullName { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}
