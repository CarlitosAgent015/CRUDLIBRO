using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CRUDROLES.Models
{
    public partial class Categoria
    {
        // El código de la categoría es obligatorio y debe ser un número positivo
        [Required(ErrorMessage = "El código de la categoría es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El código de la categoría debe ser un número positivo.")]
        [UnicidadCodigoCategoriaValidation] // Agregamos la validación personalizada aquí
        public int CodigoCategoria { get; set; }

        // El nombre es obligatorio y debe tener una longitud entre 3 y 100 caracteres
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre de la categoría debe tener entre 3 y 100 caracteres.")]
        public string? Nombre { get; set; }

        // Relación con la colección de libros, no requiere validación explícita
        public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();

        // Clase de validación personalizada para verificar la unicidad del CódigoCategoria
        public class UnicidadCodigoCategoriaValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var context = (MiDbContext)validationContext.GetService(typeof(MiDbContext));

                // Obtener el valor de CodigoCategoria
                var codigoCategoria = (int)value;

                // Verificar si el CodigoCategoria ya existe en la base de datos
                if (context.Categorias.Any(c => c.CodigoCategoria == codigoCategoria))
                {
                    return new ValidationResult("El código de la categoría ya existe.");
                }

                // Si el código no está duplicado, la validación es exitosa
                return ValidationResult.Success;
            }
        }
    }
}
