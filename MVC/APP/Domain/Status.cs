using CORE.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    public class Status : Record
    {
        [Required, StringLength(5)]
        public string Title { get; set; }

        // Status-users one to many relationship.
        public List<User> Users { get; set; } = new();
    }
}
