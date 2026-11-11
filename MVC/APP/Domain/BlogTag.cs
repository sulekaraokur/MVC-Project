using CORE.Domain;

namespace APP.Domain
{
    // Relational entity for the blogs-tags many to many relationship.
    public class BlogTag : Record
    {
        public int BlogId { get; set; }

        public Blog Blog { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
