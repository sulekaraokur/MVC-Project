using CORE.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Domain
{
    public class User : Record
    {
        [Required, StringLength(30)]
        public string UserName { get; set; }

        [Required, StringLength(15)]
        public string Password { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public Genders Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public double Score { get; set; }

        public bool IsOnline { get; set; }

        // Users-status one to many relationship.
        public int StatusId { get; set; }

        public Status Status { get; set; }

        // Users-roles many to many relationship.
        public List<UserRole> UserRoles { get; set; } = new();

        // User-blogs one to many relationship.
        public List<Blog> Blogs { get; set; } = new();

        // Manages the UserRoles relational entities through Role Id values.
        [NotMapped]
        public List<int> RoleIds
        {
            get => UserRoles.Select(userRoleEntity => userRoleEntity.RoleId).ToList();
            set => UserRoles = value.Select(roleIdValue => new UserRole
            {
                RoleId = roleIdValue
            }).ToList();
        }
    }
}
