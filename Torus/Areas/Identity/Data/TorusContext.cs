﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Torus.Areas.Identity.Data;
using Torus.Models;

namespace Torus.Data;

public class TorusContext : IdentityDbContext<TorusUser>
{
    public TorusContext(DbContextOptions<TorusContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Torus.Models.TorusPost>? TorusPost { get; set; }
}
