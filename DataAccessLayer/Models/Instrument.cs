﻿using DataAccessLayer.Enum;
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
        public Guid BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
    }
}
