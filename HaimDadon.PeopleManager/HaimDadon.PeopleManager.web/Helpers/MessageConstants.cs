
namespace HaimDadon.PeopleManager.web.Helpers
{
    public static class MessageConstants
    {

        public const string PEOPLE_CREATE_ERROR = "The person was not added. Please check the data and try again.";
        public const string PEOPLE_CREATE_SUCCESS = "The person was created successfully. You can continue adding or go to the people page.";
    
        public const string Validate_ERROR_FULLNAME_REQUIRED = "Full Name is required.";
        public const string Validate_ERROR_FULLNAME_LENGTH = "Full name must be between 2 and 30 characters.";

        public const string Validate_ERROR_PHONE_REQUIRED = "Phone is required.";
        public const string Validate_ERROR_PHONE_FORMAT = "Incorrect phone.";
        public const string Validate_ERROR_PHONE_REGULAR = "Phone must be in format 0500000000.";
        public const string Validate_ERROR_PHONE_REMOTE = "Phone already exists.";

        public const string Validate_ERROR_EMAIL_REQUIRED = "Email is required.";
        public const string Validate_ERROR_EMAIL_FORMAT = "Incorrect email.";
        public const string Validate_ERROR_EMAIL_REMOTE = "Email already exists.";

        public const string Validate_ERROR_IMAGEPATH_REGULAR = "The file must end in .png or .jpg.";
        
    }
}