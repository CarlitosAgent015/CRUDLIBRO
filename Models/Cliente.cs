using System;
using System.Collections.Generic;

namespace CRUDROLES.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Email { get; set; }

    public DateOnly? FechaRegistro { get; set; }
}
