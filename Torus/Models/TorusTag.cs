using System.ComponentModel.DataAnnotations;

namespace Torus.Models
{
    public class TorusTag
    {
        public uint TagID;
        [Required]
        public string FlairTitle = "";
    }
}
