using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class EntityObject
    {
        [Key]
        public int Id { get; set; }
    }
}
