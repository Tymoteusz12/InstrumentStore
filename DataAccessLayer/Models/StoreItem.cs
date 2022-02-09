using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class StoreItem : EntityObject
    {
        public int InstrumentId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
    }
}
