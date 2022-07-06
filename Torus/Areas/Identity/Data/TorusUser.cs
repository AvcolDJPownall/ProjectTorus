using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Torus.Areas.Identity.Data;


// Bitmask representing a user's prefered area(s) of 3D art.
// Can represent any combination of artist types using bitwise OR operations.
public enum ArtistType
{
    None = 0,
    Modelling = 1,
    Shader = 2,
    Particles = 4,
    NormalMap = 8,
    TextureDesigner = 16,
    Animation = 32,
    Rigging = 64,
};

public class TorusUser : IdentityUser
{
    public ArtistType ArtistType { get; set; } = 0;
    public long Balance { get; set; }
    public string Bio = "";
}

