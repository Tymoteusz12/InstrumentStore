using DataAccessLayer.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class Instrument : EntityObject
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public InstrumentTypeEnum InstrumentTypeValue { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
