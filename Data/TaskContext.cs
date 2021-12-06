using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnnouncmentTask.Models;

public class TaskContext : DbContext
{
    public DbSet<Announcment> Announcment { get; set; }

    public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging(true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Announcment>()
                    .HasKey(k => k.Id);

        modelBuilder.Entity<Announcment>()
                    .Property(p => p.DateOfCreation)
                    .HasColumnType("date");

        modelBuilder.Entity<Announcment>()
                    .Property(p => p.Description)
                    .IsRequired();

        modelBuilder.Entity<Announcment>()
                    .Property(p => p.Name)
                    .IsRequired();

        
    }
}
