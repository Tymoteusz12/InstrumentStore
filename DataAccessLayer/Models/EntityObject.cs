using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class EntityObject
    {
        [Key]
        public Guid Id { get; set; }
    }
}
