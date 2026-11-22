using CORE.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class TagRequest : Record
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}
