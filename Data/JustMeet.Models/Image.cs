namespace JustMeet.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string ImageUrl { get; set; }

        [Required]
        [MinLength(2)]
        public string FileExtension { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}