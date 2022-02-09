using DataAccessLayer.Models;
using InstrumentsShop.Models.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace InstrumentStore.Models.DTO
{
    public class StoreDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }
        public float FinalPrice { get; set; }
        public List<StoreItemDTO> StoreItems { get; set; } = new List<StoreItemDTO>();
    }
}
