using System.ComponentModel.DataAnnotations;

namespace MelbourneData.Areas.MoulaCodeChalleng.Models
{
    public class Customer
    {
        [Key]
        public int PersonId { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public string LastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DateOfBirth { get; set; }

        public string CustCode { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
    }
}