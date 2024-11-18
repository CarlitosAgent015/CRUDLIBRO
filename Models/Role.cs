using System;
using System.Collections.Generic;

namespace CRUDROLES.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }
}
