using System.ComponentModel.DataAnnotations;

namespace InstrumentStore.Models.DTO
{
    public class UserCredentials
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
