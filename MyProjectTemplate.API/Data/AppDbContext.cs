using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyProjectTemplate.API.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MyProjectTemplate.API.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SubAlarmsDatum> SubAlarmsData { get; set; }

    public virtual DbSet<SubControlDatum> SubControlData { get; set; }

    public virtual DbSet<SubDatum> SubData { get; set; }

    public virtual DbSet<SubLifeSupportDatum> SubLifeSupportData { get; set; }

    public virtual DbSet<SubLog> SubLogs { get; set; }

    public virtual DbSet<SubPositionDatum> SubPositionData { get; set; }

    public virtual DbSet<SubReactorDatum> SubReactorData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=submarinedb;user=root;password=DB_PASSWORD", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.44-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<SubAlarmsDatum>(entity =>
        {
            entity.HasKey(e => e.AlarmId).HasName("PRIMARY");

            entity.ToTable("sub_alarms_data");

            entity.HasIndex(e => e.SubId, "SubID");

            entity.Property(e => e.AlarmId)
                .ValueGeneratedNever()
                .HasColumnName("AlarmID");
            entity.Property(e => e.AlarmName).HasMaxLength(50);
            entity.Property(e => e.ClearedAt).HasMaxLength(40);
            entity.Property(e => e.RaisedAt).HasMaxLength(40);
            entity.Property(e => e.SubId).HasColumnName("SubID");

            entity.HasOne(d => d.Sub).WithMany(p => p.SubAlarmsData)
                .HasForeignKey(d => d.SubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sub_alarms_data_ibfk_1");
        });

        modelBuilder.Entity<SubControlDatum>(entity =>
        {
            entity.HasKey(e => new { e.SubId, e.TimeData })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("sub_control_data");

            entity.Property(e => e.SubId).HasColumnName("SubID");
            entity.Property(e => e.TimeData).HasMaxLength(40);

            entity.HasOne(d => d.Sub).WithMany(p => p.SubControlData)
                .HasForeignKey(d => d.SubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sub_control_data_ibfk_1");
        });

        modelBuilder.Entity<SubDatum>(entity =>
        {
            entity.HasKey(e => e.SubId).HasName("PRIMARY");

            entity.ToTable("sub_data");

            entity.Property(e => e.SubId).HasColumnName("SubID");
            entity.Property(e => e.SubName).HasMaxLength(50);
        });

        modelBuilder.Entity<SubLifeSupportDatum>(entity =>
        {
            entity.HasKey(e => new { e.SubId, e.TimeData })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("sub_life_support_data");

            entity.Property(e => e.SubId).HasColumnName("SubID");
            entity.Property(e => e.TimeData).HasMaxLength(40);
            entity.Property(e => e.Co2level).HasColumnName("CO2Level");
            entity.Property(e => e.O2level).HasColumnName("O2Level");

            entity.HasOne(d => d.Sub).WithMany(p => p.SubLifeSupportData)
                .HasForeignKey(d => d.SubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sub_life_support_data_ibfk_1");
        });

        modelBuilder.Entity<SubLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PRIMARY");

            entity.ToTable("sub_logs");

            entity.HasIndex(e => e.SubId, "SubID");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.ActionTaken).HasMaxLength(200);
            entity.Property(e => e.Command).HasColumnType("json");
            entity.Property(e => e.PerformedBy).HasMaxLength(100);
            entity.Property(e => e.SubId).HasColumnName("SubID");
            entity.Property(e => e.TimeData).HasMaxLength(40);

            entity.HasOne(d => d.Sub).WithMany(p => p.SubLogs)
                .HasForeignKey(d => d.SubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sub_logs_ibfk_1");
        });

        modelBuilder.Entity<SubPositionDatum>(entity =>
        {
            entity.HasKey(e => new { e.SubId, e.TimeData })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("sub_position_data");

            entity.Property(e => e.SubId).HasColumnName("SubID");
            entity.Property(e => e.TimeData).HasMaxLength(40);

            entity.HasOne(d => d.Sub).WithMany(p => p.SubPositionData)
                .HasForeignKey(d => d.SubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sub_position_data_ibfk_1");
        });

        modelBuilder.Entity<SubReactorDatum>(entity =>
        {
            entity.HasKey(e => e.ReactorId).HasName("PRIMARY");

            entity.ToTable("sub_reactor_data");

            entity.HasIndex(e => e.SubId, "SubID");

            entity.Property(e => e.ReactorId)
                .ValueGeneratedNever()
                .HasColumnName("ReactorID");
            entity.Property(e => e.SubId).HasColumnName("SubID");

            entity.HasOne(d => d.Sub).WithMany(p => p.SubReactorData)
                .HasForeignKey(d => d.SubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sub_reactor_data_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
