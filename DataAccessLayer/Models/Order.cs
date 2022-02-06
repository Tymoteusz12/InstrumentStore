using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class Order : EntityObject
    {
        public DateTime OrderDate { get; set; }
        public string OrderDetails { get; set; }
        public float Price { get; set; }
        public string Street { get; set; }
        public int ApartamentNumber { get; set; }
        public int BuildingNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string Comment { get; set; }
        public int ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public List<Instrument> OrderedItems { get; set; }
    }
}
