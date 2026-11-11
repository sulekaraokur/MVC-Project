using CORE.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    public class Tag : Record
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        // Tags-blogs many to many relationship.
        public List<BlogTag> BlogTags { get; set; } = new();
    }
}
