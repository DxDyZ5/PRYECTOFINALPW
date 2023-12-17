using System;
using System.Collections.Generic;

namespace PROYECTOFINALPW.Server.Models;

public partial class Caso
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public int ClienteId { get; set; }

    public int TipoId { get; set; }

    public decimal Latitud { get; set; }

    public decimal Longitud { get; set; }

    public string Descripcion { get; set; } = null!;

    public int AbogadoId { get; set; }

    public int EstadoId { get; set; }

    public virtual Abogado Abogado { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual EstadoCaso Estado { get; set; } = null!;

    public virtual TipoCaso Tipo { get; set; } = null!;
}
