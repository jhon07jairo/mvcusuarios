using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace USUARIOSWEB.Models;

public partial class DbUsuariosContext : DbContext
{
    public DbUsuariosContext()
    {
    }

    public DbUsuariosContext(DbContextOptions<DbUsuariosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS; database=DB_USUARIOS; Trusted_Connection=SSPI; Encrypt=false; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__4E3E04AD6A48F644");

            entity.ToTable("USUARIO");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("sexo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
