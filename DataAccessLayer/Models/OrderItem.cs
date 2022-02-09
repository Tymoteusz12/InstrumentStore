using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class OrderItem : EntityObject
    {
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int InstrumentId { get; set; }
    }
}
