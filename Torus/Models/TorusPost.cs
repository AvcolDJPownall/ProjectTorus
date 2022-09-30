using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Torus.Models
{

    // Bitmask representing a user's prefered area(s) of 3D art.
    // Can represent any combination of artist types using bitwise OR operations.
    public enum AssetType
    {
        None = 0,
        Mesh = 1,
        Shader = 2,
        Particles = 4,
        NormalMap = 8,
        TextureDesign = 16,
        Animation = 32,
        Rigging = 64,
    };

    public class TorusPost
    {
        [Key]
        public uint PostID { get; set; }

        [Required]
        public string Title { get; set; } = "";
        public string Description { get; set; } = "A Torus Asset";

        [DataType(DataType.Upload)]
        [NotMapped]
        public IFormFile? ImageThumbnail { get; set; }
        public string ImageFileGUID { get; set; } = "";

        [Required][DisplayName("Post Type")]
        public AssetType PostType { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:#,###.00}")]
        public double Cost { get; set; }

        public uint Likes { get; set; } = 0;
        public uint Dislikes { get; set; } = 0;
        [Required]
        [DisplayName("Page Views")]
        public uint PageViews { get; set; } = 0;


        ICollection<TorusTag>? Tags;

    }
}
