using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentsShop.Models.DTO
{
    public class StoreItemDTO
    {
        public int StoreItemId { get; set; }
        public int InstrumentId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
    }
}
