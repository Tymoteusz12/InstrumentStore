using DataAccessLayer.Enum;

namespace InstrumentStore.Models
{
    public class Instrument
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public InstrumentTypeEnum InstrumentTypeValue { get; set; }
        public string BrandName { get; set; }
    }
}
