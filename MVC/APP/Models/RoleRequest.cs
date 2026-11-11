using CORE.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class RoleRequest : Record
    {
        [Required, StringLength(25)]
        public string Name { get; set; }
    }
}
