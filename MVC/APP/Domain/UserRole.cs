using CORE.Domain;

namespace APP.Domain
{
    // Relational entity for the users-roles many to many relationship.
    public class UserRole : Record
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
