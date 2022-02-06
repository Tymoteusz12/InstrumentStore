using DataAccessLayer.Models;
using System.Collections.Generic;

namespace InstrumentStore.Models.DTO
{
    public class StoreDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public float FinalPrice { get; set; }
        public List<InstrumentDTO> StoreItems { get; set; } = new List<InstrumentDTO>();
    }
}
