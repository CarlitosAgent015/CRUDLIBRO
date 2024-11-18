
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDROLES.Models
{
    public partial class Libro
    {
        // ISBN único y obligatorio
        [Key]
        [Required(ErrorMessage = "El ISBN es obligatorio.")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "El ISBN debe tener entre 10 y 13 caracteres.")]
        public string Isbn { get; set; } = null!;

        // Título obligatorio
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(255, ErrorMessage = "El título no puede exceder los 255 caracteres.")]
        public string Titulo { get; set; } = null!;

        // Descripción opcional con longitud máxima
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres.")]
        public string? Descripcion { get; set; }

        // Nombre del autor opcional con longitud máxima
        [StringLength(255, ErrorMessage = "El nombre del autor no puede exceder los 255 caracteres.")]
        public string? NombreAutor { get; set; }

        // Fecha de publicación opcional
        [DataType(DataType.Date, ErrorMessage = "La fecha de publicación debe ser una fecha válida.")]
        public DateOnly? Publicacion { get; set; }

        // Fecha de registro opcional
        [DataType(DataType.Date, ErrorMessage = "La fecha de registro debe ser una fecha válida.")]
        public DateOnly? FechaRegistro { get; set; }

        // Relación con categoría (opcional)
        [Range(1, int.MaxValue, ErrorMessage = "El código de categoría debe ser un número positivo.")]
        public int? CodigoCategoria { get; set; }

        // Relación con editorial (opcional)
        [Range(1, int.MaxValue, ErrorMessage = "El NIT de la editorial debe ser un número positivo.")]
        public int? NitEditorial { get; set; }

        // Relación con Categoría
        public virtual Categoria? CodigoCategoriaNavigation { get; set; }

        // Relación con LibrosAutor
        public virtual ICollection<LibrosAutor> LibrosAutors { get; set; } = new List<LibrosAutor>();

        // Relación con Editorial
        public virtual Editoriale? NitEditorialNavigation { get; set; }
    }
}

