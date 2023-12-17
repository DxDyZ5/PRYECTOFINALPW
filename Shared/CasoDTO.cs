using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //requerIED file

namespace PROYECTOFINALPW.Shared
{
    public class CasoDTO
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
        public int ClienteId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
        public int TipoId { get; set; }

        [Required(ErrorMessage = "El campo Latitud es requerido.")]
        [Range(-90.000000, 90.000000, ErrorMessage = "La Latitud debe estar entre -90 y 90.")]
        public decimal Latitud { get; set; }

        [Required(ErrorMessage = "El campo Longitud es requerido.")]
        [Range(-180.000000, 180.000000, ErrorMessage = "La Longitud debe estar entre -180 y 180.")]
        public decimal Longitud { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Descripcion { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
        public int AbogadoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
        public int EstadoId { get; set; }

        public ClienteDTO? Cliente { get; set; }
        public TipoCasoDTO? Tipo { get; set; }

        public AbogadoDTO? Abogado { get; set; }

        public EstadoCasoDTO? Estado { get; set; }

    }
}
