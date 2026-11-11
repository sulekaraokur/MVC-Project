using CORE.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class StatusRequest : Record
    {
        [Required, StringLength(5)]
        public string Title { get; set; }
    }
}
