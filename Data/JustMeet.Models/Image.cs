namespace JustMeet.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string ImageUrl { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public string FileExtension { get; set; }

        public string UserId { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}