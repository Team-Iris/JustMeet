namespace JustMeet.Services.Models.User
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserUpdateRequestModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool IsMale { get; set; }

        public string Description { get; set; }
    }
}