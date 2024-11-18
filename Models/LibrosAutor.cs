
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDROLES.Models
{
    public partial class LibrosAutor
    {
        // ID único para cada relación entre libro y autor
        [Key]
        [Required(ErrorMessage = "El ID de LibroAutor es obligatorio.")]
        public int Idlibroautor { get; set; }

        // El ID del autor es obligatorio y debe tener un formato válido
        [Required(ErrorMessage = "El ID del autor es obligatorio.")]
        [StringLength(36, ErrorMessage = "El ID del autor no puede exceder los 36 caracteres.")]
        public string? Idautor { get; set; }

        // El ISBN es obligatorio y debe tener un formato válido
        [Required(ErrorMessage = "El ISBN del libro es obligatorio.")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "El ISBN debe tener entre 10 y 13 caracteres.")]
        public string? Isbn { get; set; }

        // Relación con la entidad `Autor`
        public virtual Autor? IdautorNavigation { get; set; }

        // Relación con la entidad `Libro`
        public virtual Libro? IsbnNavigation { get; set; }
    }
}

