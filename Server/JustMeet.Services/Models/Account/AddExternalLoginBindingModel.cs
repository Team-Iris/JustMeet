﻿namespace JustMeet.Services.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}