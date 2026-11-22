using CORE.Domain;
using System.ComponentModel;

namespace APP.Models
{
    public class BlogResponse : Record
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public decimal? Rating { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsArchived { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        public int? UserId { get; set; }

        // Properties ending with R are formatted or custom values for the views.
        [DisplayName("Rating")]
        public string RatingR { get; set; }

        [DisplayName("Publish Date")]
        public string PublishDateR { get; set; }

        [DisplayName("Archived")]
        public string IsArchivedR { get; set; }

        [DisplayName("User")]
        public string UserR { get; set; }

        [DisplayName("Tags")]
        public string TagNamesR { get; set; }

        [DisplayName("Tags")]
        public List<TagResponse> TagsR { get; set; }
    }
}
