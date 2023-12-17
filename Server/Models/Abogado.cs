using System;
using System.Collections.Generic;

namespace PROYECTOFINALPW.Server.Models;

public partial class Abogado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Caso> Casos { get; } = new List<Caso>();
}
