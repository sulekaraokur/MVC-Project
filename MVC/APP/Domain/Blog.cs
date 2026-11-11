using CORE.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Domain
{
    public class Blog : Record
    {
        [Required, StringLength(150)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public decimal? Rating { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsArchived { get; set; }

        [StringLength(30)]
        public string UserName { get; set; }

        // Blogs-user one to many relationship (a blog may have no user).
        public int? UserId { get; set; }

        public User User { get; set; }

        // Blogs-tags many to many relationship.
        public List<BlogTag> BlogTags { get; set; } = new();

        // Manages the BlogTags relational entities through Tag Id values.
        [NotMapped]
        public List<int> TagIds
        {
            get => BlogTags.Select(blogTagEntity => blogTagEntity.TagId).ToList();
            set => BlogTags = value.Select(tagIdValue => new BlogTag
            {
                TagId = tagIdValue
            }).ToList();
        }
    }
}
