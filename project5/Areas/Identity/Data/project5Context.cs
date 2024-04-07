using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project5.Areas.Identity.Data;
using project5.Models;

namespace project5.Data;

public class project5Context : IdentityDbContext<project5User>
{
    internal IEnumerable<object> catalogcourse;

    public project5Context(DbContextOptions<project5Context> options)
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


public DbSet<project5.Models.Plan> Plan { get; set; } = default!;

public DbSet<project5.Models.Major> Major { get; set; } = default!;

public DbSet<project5.Models.courses> courses { get; set; } = default!;


public DbSet<project5.Models.Requirements> Requirements { get; set; } = default!;

public DbSet<project5.Models.Catalogcourse> Catalogcourse { get; set; } = default!;

public DbSet<project5.Models.plancourses> plancourses { get; set; } = default!;

public DbSet<project5.Models.majorplan> majorplan { get; set; } = default!;
}
