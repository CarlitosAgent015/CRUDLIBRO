
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDROLES.Models
{
    public partial class Editoriale
    {
        // El NIT debe ser obligatorio y positivo
        [Required(ErrorMessage = "El NIT es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El NIT debe ser un número positivo.")]
        public int Nit { get; set; }

        // El nombre de la editorial es obligatorio y con una longitud entre 3 y 100 caracteres
        [Required(ErrorMessage = "El nombre de la editorial es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string? Nombres { get; set; }

        // El teléfono debe seguir un formato específico
        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\+?\d{7,15}$", ErrorMessage = "El teléfono debe contener entre 7 y 15 dígitos, y puede incluir el prefijo internacional.")]
        public string? Telefono { get; set; }

        // La dirección es opcional pero con un límite de caracteres
        [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres.")]
        public string? Direccion { get; set; }

        // Validación para el correo electrónico
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        public string? Email { get; set; }

        // Validación para el sitio web
        [Url(ErrorMessage = "El sitio web no tiene un formato válido.")]
        [StringLength(200, ErrorMessage = "El sitio web no puede exceder los 200 caracteres.")]
        public string? Sitioweb { get; set; }

        // Relación con libros, sin validaciones explícitas
        public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}

