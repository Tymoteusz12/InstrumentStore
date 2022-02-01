﻿using System.Collections.Generic;

namespace InstrumentStore.Models.DTO
{
    public class ApplicationUser
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<Order> Orders { get; set; }
    }
}
