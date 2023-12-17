using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PROYECTOFINALPW.Server.Models;

public partial class ProyectofinalpwContext : DbContext
{
    public ProyectofinalpwContext()
    {
    }

    public ProyectofinalpwContext(DbContextOptions<ProyectofinalpwContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abogado> Abogados { get; set; }

    public virtual DbSet<Caso> Casos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<EstadoCaso> EstadoCasos { get; set; }

    public virtual DbSet<TipoCaso> TipoCasos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=AppConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abogado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Abogado__3214EC07D2FD417A");

            entity.ToTable("Abogado");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Caso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Caso__3214EC0707D883B4");

            entity.ToTable("Caso");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Latitud).HasColumnType("decimal(10, 6)");
            entity.Property(e => e.Longitud).HasColumnType("decimal(10, 6)");

            entity.HasOne(d => d.Abogado).WithMany(p => p.Casos)
                .HasForeignKey(d => d.AbogadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Caso__AbogadoId__2E1BDC42");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Casos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Caso__ClienteId__2C3393D0");

            entity.HasOne(d => d.Estado).WithMany(p => p.Casos)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Caso__EstadoId__2F10007B");

            entity.HasOne(d => d.Tipo).WithMany(p => p.Casos)
                .HasForeignKey(d => d.TipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Caso__TipoId__2D27B809");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3214EC071B9B8161");

            entity.ToTable("Cliente");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cedula)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Estado_Civil");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(16)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoCaso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EstadoCa__3214EC0752B1549B");

            entity.ToTable("EstadoCaso");

            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoCaso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoCaso__3214EC076C1932CE");

            entity.ToTable("TipoCaso");

            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
