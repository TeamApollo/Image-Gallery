namespace ImageGallery.Api.Models.Account.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class RegisterBindingModel
    {
        private const string ErrorMessageFormat = "The {0} must be at least {2} characters long.";

        [Required]
        [StringLength(ValidationConstants.NameMaxLength, ErrorMessage = ErrorMessageFormat)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(ValidationConstants.NameMaxLength, ErrorMessage = ErrorMessageFormat)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = ErrorMessageFormat, MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}