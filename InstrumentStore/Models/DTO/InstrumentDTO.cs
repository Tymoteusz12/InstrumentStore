using DataAccessLayer.Enum;
using System;

namespace InstrumentStore.Models.DTO
{
    public class InstrumentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public InstrumentTypeEnum InstrumentTypeValue { get; set; }
        public string BrandName { get; set; }
    }
}
