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
            .WithOne(b => b.Dormitory)
            .HasForeignKey(b => b.DormitoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Block>()
            .HasMany(b => b.Rooms)
            .WithOne(r => r.Block)
            .HasForeignKey(r => r.BlockId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Room>()
            .HasMany(r => r.Tools)
            .WithOne(t => t.Room)
            .HasForeignKey(t => t.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Room>()
            .HasMany(r => r.Students)
            .WithOne(s => s.Room)
            .HasForeignKey(s => s.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Dormitory>()
            .HasMany(d => d.Students)
            .WithOne(s => s.Dormitory)
            .HasForeignKey(s => s.DormitoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Dormitory>()
            .HasMany(d => d.Supervisors)
            .WithOne(s => s.Dormitory)
            .HasForeignKey(s => s.DormitoryId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Block>()
            .HasMany(b => b.Supervisors)
            .WithOne(s => s.Block)
            .HasForeignKey(s => s.BlockId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Student>()
            .HasIndex(s => s.NationalCode)
            .IsUnique();

        modelBuilder.Entity<Student>()
            .HasIndex(s => s.PhoneNumber)
            .IsUnique();

        modelBuilder.Entity<Supervisor>()
            .HasIndex(s => s.NationalCode)
            .IsUnique();

        modelBuilder.Entity<Supervisor>()
            .HasIndex(s => s.PhoneNumber)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
