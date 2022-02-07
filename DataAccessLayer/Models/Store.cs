using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Store : EntityObject
    {
        public virtual ApplicationUser User { get; set; }
        public float FinalPrice { get; set; }
        public List<OrderItem> StoreItems { get; set; }

    }
}
