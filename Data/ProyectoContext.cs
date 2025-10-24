using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;

namespace Proyecto.Data;

public partial class ProyectoContext : DbContext
{
    public ProyectoContext()
    {
    }

    public ProyectoContext(DbContextOptions<ProyectoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agronomo> Agronomos { get; set; }

    public virtual DbSet<Cultivo> Cultivos { get; set; }

    public virtual DbSet<Enfermedad> Enfermedads { get; set; }

    public virtual DbSet<Etapa> Etapas { get; set; }

    public virtual DbSet<Hoja> Hojas { get; set; }

    public virtual DbSet<Prediccion> Prediccions { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<Tratamiento> Tratamientos { get; set; }

    public virtual DbSet<TratamientosCultivo> TratamientosCultivos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuariosCultivo> UsuariosCultivos { get; set; }

    public virtual DbSet<VariedadTomate> VariedadTomates { get; set; }

  //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
  //      => optionsBuilder.UseMySQL("Server=localhost;Database=pg;Uid=root;Pwd=Judith0709;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agronomo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.SegundoApellido).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.SegundoNombre).HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Agronomos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Agronomos_ibfk_1");
        });

        modelBuilder.Entity<Cultivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaCommit).HasDefaultValueSql("'current_timestamp()'");

            entity.HasOne(d => d.IdAgronomoNavigation).WithMany(p => p.Cultivos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Cultivos_ibfk_1");

            entity.HasOne(d => d.IdVariedadTomateNavigation).WithMany(p => p.Cultivos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Cultivos_ibfk_2");
        });

        modelBuilder.Entity<Enfermedad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Classname).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Nombre).HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Etapa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Nombre).HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Hoja>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Url).HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.IdEnfermedadNavigation).WithMany(p => p.Hojas)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Hojas_ibfk_1");
        });

        modelBuilder.Entity<Prediccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.IdCultvioNavigation).WithMany(p => p.Prediccions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Prediccion_ibfk_1");

            entity.HasOne(d => d.IdEnfermedadNavigation).WithMany(p => p.Prediccions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Prediccion_ibfk_2");

            entity.HasOne(d => d.IdEtapaNavigation).WithMany(p => p.Prediccions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Prediccion_ibfk_3");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaCommit).HasDefaultValueSql("'current_timestamp()'");
            entity.Property(e => e.Observacion).HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.IdTratamientoCultivoNavigation).WithMany(p => p.Reportes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Reporte_ibfk_1");
        });

        modelBuilder.Entity<Tratamiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CantidadPorPlanta).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Descripcion).HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.IdEnfermedadNavigation).WithMany(p => p.Tratamientos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Tratamientos_ibfk_2");

            entity.HasOne(d => d.IdEtapaNavigation).WithMany(p => p.Tratamientos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Tratamientos_ibfk_1");
        });

        modelBuilder.Entity<TratamientosCultivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FechaCommit).HasDefaultValueSql("'current_timestamp()'");

            entity.HasOne(d => d.IdCultivoNavigation).WithMany(p => p.TratamientosCultivos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Tratamientos_cultivo_ibfk_2");

            entity.HasOne(d => d.IdTratamientoNavigation).WithMany(p => p.TratamientosCultivos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Tratamientos_cultivo_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Isadmin).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<UsuariosCultivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.IdCultivoNavigation).WithMany(p => p.UsuariosCultivos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usuarios_cultivos_ibfk_2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuariosCultivos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usuarios_cultivos_ibfk_1");
        });

        modelBuilder.Entity<VariedadTomate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
