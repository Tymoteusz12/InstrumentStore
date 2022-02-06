using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Store : EntityObject
    {
        public int UserId { get; set; }
        public ApplicationUser User {get;set;}
        public float FinalPrice { get; set; }
        public List<Instrument> StoreItems { get; set; }

    }
}
