
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDROLES.Models
{
    public partial class Autor
    {
        // El Id del autor es obligatorio y debe tener una longitud máxima
        [Required(ErrorMessage = "El ID del autor es obligatorio.")]
        [StringLength(50, ErrorMessage = "El ID del autor no puede tener más de 50 caracteres.")]
        public string Idautor { get; set; } = null!;

        // Nombre del autor es obligatorio y debe tener una longitud máxima
        [Required(ErrorMessage = "El nombre del autor es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string? Nombre { get; set; }

        // Apellido del autor es opcional, pero si se proporciona, no debe exceder los 100 caracteres
        [StringLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres.")]
        public string? Apellido { get; set; }

        // Nacionalidad es opcional, pero si se proporciona, no debe exceder los 100 caracteres
        [StringLength(100, ErrorMessage = "La nacionalidad no puede tener más de 100 caracteres.")]
        public string? Nacionalidad { get; set; }

        // Relación con la colección de libros, no requiere validación explícita
        public virtual ICollection<LibrosAutor> LibrosAutors { get; set; } = new List<LibrosAutor>();
    }
}

