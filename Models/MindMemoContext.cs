using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MindMemo.Models;

public partial class MindMemoContext : DbContext
{
    public MindMemoContext()
    {
    }

    public MindMemoContext(DbContextOptions<MindMemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NumberMemoryScore> NumberMemoryScores { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=10.237.170.41; Port=3306; uid=williamzhang; pwd=password; database=MindMemo");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NumberMemoryScore>(entity =>
        {
            entity.HasKey(e => new { e.Username, e.Time }).HasName("PRIMARY");

            entity.ToTable("NumberMemoryScores", "MindMemo");

            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");
            entity.Property(e => e.Score).HasColumnName("score");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PRIMARY");

            entity.ToTable("User", "MindMemo");

            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
