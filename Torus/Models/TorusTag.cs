using System.ComponentModel.DataAnnotations;

namespace Torus.Models
{
    public class TorusTag
    {
        [Key]
        public uint TagID;
        [Required]
        public string FlairTitle = "";
    }
}
