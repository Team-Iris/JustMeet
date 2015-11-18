namespace JustMeet.Services.Models.Images
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class PostImageRequestModel
    {
        public byte[] ImageData { get; set; }

        public string ImageNameOrPathWithExtension { get; set; }
    }
}