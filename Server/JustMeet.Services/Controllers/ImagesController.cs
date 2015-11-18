namespace JustMeet.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Data;
    using JustMeet.Data;
    using JustMeet.Models;
    using Microsoft.AspNet.Identity;
    using Models.Images;

    [EnableCors("*", "*", "*")]
    [Authorize]
    public class ImagesController : ApiController
    {
        private JustMeetDbContext data;
        private GoogleDriveService drive;

        public ImagesController(JustMeetDbContext data, GoogleDriveService drive)
        {
            this.data = data;
            this.drive = drive;
        }

        /// <summary>
        /// Gets user image(s) link(s) from database.
        /// </summary>
        /// <returns>List of link(s)</returns>
        /// GET api/Images/
        [HttpGet]
        public IHttpActionResult Get()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var imagesLinksList = data.Images.Where(i => i.UserId == currentUserId).Select(i => i.ImageUrl).ToList();

            return this.Ok(imagesLinksList);
        }

        /// <summary>
        /// Uploads given file data to Google Drive and saves the image link to database.
        /// </summary>
        /// <param name="image"></param>
        /// <returns>Image id</returns>
        // POST api/Images/
        [HttpPost]
        public IHttpActionResult Post([FromBody]PostImageRequestModel image)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var fileId = drive.UploadFile(image.ImageData,image.ImageNameOrPathWithExtension);
            string ext = System.IO.Path.GetExtension(image.ImageNameOrPathWithExtension).ToLower();
            var imageToAdd = new Image()
            {
                ImageUrl = drive.GetLinkById(fileId),
                FileExtension = ext,
                UserId = currentUserId,
            };
            data.Images.Add(imageToAdd);
            data.SaveChanges();
            return this.Ok(fileId);
        }

        /// <summary>
        /// Extracts id(from provided link) and deletes file from Google Drive.
        /// </summary>
        /// <param name="imageLink"></param>
        /// <returns>Id of removed image.</returns>
        // DELETE api/Images/
        [HttpGet]
        public IHttpActionResult Delete([FromBody]DeleteImageRequestModel imageLink)
        {
            // extract id(from provided link) and delete from drive
            // remove image from database by Link
            var id = drive.GetIdByLink(imageLink.ImageLink);
            drive.DeleteFileByID(id);
            var imageToRemove = data.Images.Where(i => i.ImageUrl == imageLink.ImageLink).FirstOrDefault();
            var removedId = data.Images.Remove(imageToRemove).Id;
            data.SaveChanges();
            return this.Ok(removedId);
        }
    }
}