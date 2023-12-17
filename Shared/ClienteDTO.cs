using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTOFINALPW.Shared
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Apellido { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Cedula { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Correo { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Telefono { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Celular { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Direccion { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string EstadoCivil { get; set; } = null!;
    }
}
