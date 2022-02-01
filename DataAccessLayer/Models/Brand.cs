using DataAccessLayer.Enum;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Brand : EntityObject
    {
        public string BrandName { get; set; }
        public string BrandDetails { get; set; }
        public string Comment { get; set; }
        public string LogoURL { get; set; }
        public List<Instrument> Instruments { get; set; }
    }
}
