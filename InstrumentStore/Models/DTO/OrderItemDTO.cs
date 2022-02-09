using InstrumentStore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentsShop.Models.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int InstrumentId { get; set; }
    }
}
