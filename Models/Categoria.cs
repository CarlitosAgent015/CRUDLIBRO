
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDROLES.Models
{
    public partial class Categoria
    {
        // El código de la categoría es obligatorio y debe ser un número positivo
        [Required(ErrorMessage = "El código de la categoría es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El código de la categoría debe ser un número positivo.")]
        public int CodigoCategoria { get; set; }

        // El nombre es obligatorio y debe tener una longitud entre 3 y 100 caracteres
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre de la categoría debe tener entre 3 y 100 caracteres.")]
        public string? Nombre { get; set; }

        // Relación con la colección de libros, no requiere validación explícita
        public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}

