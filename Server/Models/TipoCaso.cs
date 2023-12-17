using System;
using System.Collections.Generic;

namespace PROYECTOFINALPW.Server.Models;

public partial class TipoCaso
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Caso> Casos { get; } = new List<Caso>();
}
