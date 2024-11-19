using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CRUDROLES.Models
{
    public partial class Autor
    {
        // El Id del autor es obligatorio y debe tener una longitud máxima
        [Required(ErrorMessage = "El ID del autor es obligatorio.")]
        [StringLength(50, ErrorMessage = "El ID del autor no puede tener más de 50 caracteres.")]
        [UnicidadIdautorValidation] // Se agrega la validación personalizada aquí
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

        // Clase de validación personalizada para verificar la unicidad de Idautor
        public class UnicidadIdautorValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var context = (MiDbContext)validationContext.GetService(typeof(MiDbContext));

                // Obtener el valor de Idautor
                var idautor = value as string;

                // Verificar si el Idautor ya existe en la base de datos
                if (context.Autors.Any(a => a.Idautor == idautor))
                {
                    return new ValidationResult("El ID del autor ya existe.");
                }

                // Si el ID no está duplicado, la validación es exitosa
                return ValidationResult.Success;
            }
        }
    }
}
