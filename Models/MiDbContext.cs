using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CRUDROLES.Models;

public partial class MiDbContext : DbContext
{
    public MiDbContext()
    {
    }

    public MiDbContext(DbContextOptions<MiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Editoriale> Editoriales { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<LibrosAutor> LibrosAutors { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=libros;Username=postgres;Password=cosmicos");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.Idautor).HasName("autor_pkey");

            entity.ToTable("autor");

            entity.Property(e => e.Idautor)
                .HasMaxLength(50)
                .HasColumnName("idautor");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(50)
                .HasColumnName("nacionalidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CodigoCategoria).HasName("categorias_pkey");

            entity.ToTable("categorias");

            entity.Property(e => e.CodigoCategoria)
                .ValueGeneratedNever()
                .HasColumnName("codigo_categoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Editoriale>(entity =>
        {
            entity.HasKey(e => e.Nit).HasName("editoriales_pkey");

            entity.ToTable("editoriales");

            entity.Property(e => e.Nit)
                .ValueGeneratedNever()
                .HasColumnName("nit");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .HasColumnName("nombres");
            entity.Property(e => e.Sitioweb)
                .HasMaxLength(100)
                .HasColumnName("sitioweb");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("libros_pkey");

            entity.ToTable("libros");

            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("isbn");
            entity.Property(e => e.CodigoCategoria).HasColumnName("codigo_categoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro).HasColumnName("fecha_registro");
            entity.Property(e => e.NitEditorial).HasColumnName("nit_editorial");
            entity.Property(e => e.NombreAutor)
                .HasMaxLength(100)
                .HasColumnName("nombre_autor");
            entity.Property(e => e.Publicacion).HasColumnName("publicacion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .HasColumnName("titulo");

            entity.HasOne(d => d.CodigoCategoriaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.CodigoCategoria)
                .HasConstraintName("libros_codigo_categoria_fkey");

            entity.HasOne(d => d.NitEditorialNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.NitEditorial)
                .HasConstraintName("libros_nit_editorial_fkey");
        });

        modelBuilder.Entity<LibrosAutor>(entity =>
        {
            entity.HasKey(e => e.Idlibroautor).HasName("libros_autor_pkey");

            entity.ToTable("libros_autor");

            entity.Property(e => e.Idlibroautor)
                .ValueGeneratedNever()
                .HasColumnName("idlibroautor");
            entity.Property(e => e.Idautor)
                .HasMaxLength(50)
                .HasColumnName("idautor");
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("isbn");

            entity.HasOne(d => d.IdautorNavigation).WithMany(p => p.LibrosAutors)
                .HasForeignKey(d => d.Idautor)
                .HasConstraintName("libros_autor_idautor_fkey");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.LibrosAutors)
                .HasForeignKey(d => d.Isbn)
                .HasConstraintName("libros_autor_isbn_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
