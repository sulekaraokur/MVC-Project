using CORE.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    public class Role : Record
    {
        [Required, StringLength(25)]
        public string Name { get; set; }

        // Roles-users many to many relationship.
        public List<UserRole> UserRoles { get; set; } = new();
    }
}
