using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class DormitoryContext : DbContext
{
    public DbSet<Dormitory> Dormitories { get; set; }
    public DbSet<Block> Blocks { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Tool> Tools { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Supervisor> Supervisors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Dormitory.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dormitory>()
            .HasMany(d => d.Blocks)
            .WithOne()
            .HasForeignKey(b => b.DormitoryId);
        modelBuilder.Entity<Block>()
            .HasMany(b => b.Rooms)
            .WithOne()
            .HasForeignKey(r => r.BlockId);
        modelBuilder.Entity<Room>()
            .HasMany(r => r.Tools)
            .WithOne()
            .HasForeignKey(t => t.RoomId);
        modelBuilder.Entity<Room>()
            .HasMany(r => r.Students)
            .WithOne()
            .HasForeignKey(s => s.RoomId);
        modelBuilder.Entity<Dormitory>()
            .HasMany(d => d.Students)
            .WithOne()
            .HasForeignKey(s => s.DormitoryId);
        modelBuilder.Entity<Dormitory>()
            .HasMany(d => d.Supervisors)
            .WithOne()
            .HasForeignKey(s => s.DormitoryId);
    }
}