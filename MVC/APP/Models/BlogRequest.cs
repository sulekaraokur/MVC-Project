using CORE.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class BlogRequest : Record
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(150, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        public string Content { get; set; }

        [Range(0, 5, ErrorMessage = "{0} must be between {1} and {2}!")]
        public decimal? Rating { get; set; }

        // Nullable so that [Required] fails when no value is posted.
        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Publish Date")]
        public DateTime? PublishDate { get; set; }

        [DisplayName("Archived")]
        public bool IsArchived { get; set; }

        [StringLength(30, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        // A blog may have no user.
        [DisplayName("User")]
        public int? UserId { get; set; }

        [DisplayName("Tags")]
        public List<int> TagIds { get; set; }
    }
}
