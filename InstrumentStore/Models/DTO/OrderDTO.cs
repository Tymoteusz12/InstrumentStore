using System;
using System.Collections.Generic;

namespace InstrumentStore.Models.DTO
{
    public class OrderDTO
    {
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDetails { get; set; }
        public float Price { get; set; }
        public string Street { get; set; }
        public int ApartamentNumber { get; set; }
        public int BuildingNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string Comment { get; set; }
        public List<InstrumentDTO> OrderedItems { get; set; } = new List<InstrumentDTO>();
    }
}
