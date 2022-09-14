using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Torus.Models;

namespace Torus.Areas.Identity.Data;


public class TorusUser : IdentityUser
{
    public AssetType ArtistType { get; set; } = 0;
    public long Balance { get; set; }
    public string? Bio { get; set; }
}

