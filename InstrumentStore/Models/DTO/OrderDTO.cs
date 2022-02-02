using System;

namespace InstrumentStore.Models.DTO
{
    public class OrderDTO
    {
        public DateTime OrderDate { get; set; }
        public string OrderDetails { get; set; }
        public float Price { get; set; }
    }
}
