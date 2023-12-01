
using System.ComponentModel.DataAnnotations;

namespace BlazingPizza
{
    public class Address
    {
        public int Id { get; set; }

        [Required, MinLength(3, ErrorMessage = "Please use a Name bigger than 3 letters."), MaxLength(100, ErrorMessage = "Please use a Name less than 100 letters")]
        public string Name { get; set; }

        [Required, MinLength(5, ErrorMessage = "Please use a Name bigger than 5 letters."), MaxLength(100, ErrorMessage = "Please use a Name less than 100 letters")]
        public string Line1 { get; set; }

        [MaxLength(100, ErrorMessage = "Please use a Name less than 100 letters")]
        public string Line2 { get; set; }

        public string City { get; set; }

        [Required, MinLength(3, ErrorMessage = "Please use a Name bigger than 3 letters."), MaxLength(20, ErrorMessage = "Please use a Name less than 20 letters")]
        public string Region { get; set; }

        [Required, RegularExpression(@"^([0-9]{5})$", ErrorMessage = "Please use a valid Postal Code with five numbers.")]
        public string PostalCode { get; set; }
    }
}
