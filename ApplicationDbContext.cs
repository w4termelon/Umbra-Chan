using Microsoft.EntityFrameworkCore;
using Umbra_C.Models;

namespace Umbra_C
{
public class ApplicationDbContext : DbContext
{
    public string DbPath { get; }

    public ApplicationDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "umbra-chan.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
        

    public DbSet<Mount> Mounts {get;set;}
    public DbSet<User> Users {get;set;}
}


}