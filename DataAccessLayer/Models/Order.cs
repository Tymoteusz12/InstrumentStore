using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class Order : EntityObject
    {
        public DateTime OrderDate { get; set; }
        public string OrderDetails { get; set; }
        public float Price { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
