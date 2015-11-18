using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustMeet.Services.Models.Images
{
    public class PostImageRequestModel
    {
        public byte[] imageData { get; set; }

        public string imageNameOrPathWithExtension { get; set; }
    }
}