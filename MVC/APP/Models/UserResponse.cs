using APP.Domain;
using CORE.Domain;
using System.ComponentModel;

namespace APP.Models
{
    public class UserResponse : Record
    {
        [DisplayName("User Name")]
        public string UserName { get; set; }

        public string Password { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public Genders Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public double Score { get; set; }

        public bool IsOnline { get; set; }

        public int StatusId { get; set; }

        // Properties ending with R are formatted or custom values for the views.
        [DisplayName("Online")]
        public string IsOnlineR { get; set; }

        [DisplayName("Score")]
        public string ScoreR { get; set; }

        [DisplayName("Registration Date")]
        public string RegistrationDateR { get; set; }

        [DisplayName("Birth Date")]
        public string BirthDateR { get; set; }

        [DisplayName("Gender")]
        public string GenderR { get; set; }

        [DisplayName("Full Name")]
        public string FullNameR { get; set; }

        [DisplayName("Password")]
        public string PasswordR { get; set; }

        [DisplayName("Status")]
        public string StatusTitleR { get; set; }

        [DisplayName("Status")]
        public StatusResponse StatusR { get; set; }

        [DisplayName("Roles")]
        public string RoleNamesR { get; set; }

        [DisplayName("Roles")]
        public List<RoleResponse> RolesR { get; set; }
    }
}
