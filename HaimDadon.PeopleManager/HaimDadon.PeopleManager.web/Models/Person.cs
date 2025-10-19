using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using HaimDadon.PeopleManager.web.Helpers;


namespace HaimDadon.PeopleManager.web.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = MessageConstants.Validate_ERROR_FULLNAME_REQUIRED)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = MessageConstants.Validate_ERROR_FULLNAME_LENGTH)]
        public string FullName { get; set; }

        [Required(ErrorMessage = MessageConstants.Validate_ERROR_PHONE_REQUIRED)]
        [Phone(ErrorMessage = MessageConstants.Validate_ERROR_PHONE_FORMAT)]
        [RegularExpression(@"^05\d{8}$", ErrorMessage = MessageConstants.Validate_ERROR_PHONE_REGULAR)]
        [Remote("IsPhoneAvailable", "People", ErrorMessage = MessageConstants.Validate_ERROR_PHONE_REMOTE)]
        public string Phone { get; set; }

        [Required(ErrorMessage = MessageConstants.Validate_ERROR_EMAIL_REQUIRED)]
        [EmailAddress(ErrorMessage = MessageConstants.Validate_ERROR_EMAIL_FORMAT)]
        [Remote("IsEmailAvailable", "People", ErrorMessage = MessageConstants.Validate_ERROR_EMAIL_REMOTE)]
        public string Email { get; set; }

        [RegularExpression(@"(?i)^.+\.(png|jpg)$", ErrorMessage = MessageConstants.Validate_ERROR_IMAGEPATH_REGULAR)]
        public string ImagePath { get; set; }
    }
}