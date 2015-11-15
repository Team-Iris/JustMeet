namespace JustMeet.Services.Models.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First name should be between 3 and 25 characters long!")]
        public string FirstName { get; set; }

        [StringLength(25, MinimumLength = 3, ErrorMessage = "Last name should be between 3 and 25 characters long!")]
        public string LastName { get; set; }

        [Required]
        public bool IsMale { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}