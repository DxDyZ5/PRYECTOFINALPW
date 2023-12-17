using System;
using System.Collections.Generic;

namespace PROYECTOFINALPW.Server.Models;

public partial class EstadoCaso
{
    public int Id { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Caso> Casos { get; } = new List<Caso>();
}
