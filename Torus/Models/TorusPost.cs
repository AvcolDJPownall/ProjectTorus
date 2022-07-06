using System.ComponentModel.DataAnnotations;

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
        public uint PostID;

        [Required]
        public string Title { get; set; } = "";
        public string Description { get; set; } = "A Torus Asset";

        [Required]
        public AssetType PostType { get; set; }

        [Required]
        public long Cost { get; set; }

        public uint Likes { get; set; } = 0;
        public uint Dislikes { get; set; } = 0;


        ICollection<TorusTag>? Tags;

    }
}
