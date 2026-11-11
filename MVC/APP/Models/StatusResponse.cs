using CORE.Domain;
using System.ComponentModel;

namespace APP.Models
{
    public class StatusResponse : Record
    {
        public string Title { get; set; }

        [DisplayName("Users")]
        public string UserNamesR { get; set; }

        [DisplayName("User Count")]
        public int UserCountR { get; set; }

        [DisplayName("Users")]
        public List<UserResponse> UsersR { get; set; }
    }
}
