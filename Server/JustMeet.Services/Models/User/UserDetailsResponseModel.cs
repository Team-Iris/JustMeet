namespace JustMeet.Services.Models.User
{
    using System;
    using Infrastructure.Mappings;
    using JustMeet.Models;

    public class UserDetailsResponseModel : IMapFrom<User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsMale { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }
    }
}