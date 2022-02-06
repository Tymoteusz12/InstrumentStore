using System.Collections.Generic;

namespace InstrumentStore.Models.DTO
{
    public class UserDTO
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
    }
}
