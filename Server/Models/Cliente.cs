using System;
using System.Collections.Generic;

namespace PROYECTOFINALPW.Server.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string EstadoCivil { get; set; } = null!;

    public virtual ICollection<Caso> Casos { get; } = new List<Caso>();
}
