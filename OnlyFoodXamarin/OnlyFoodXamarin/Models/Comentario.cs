using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFoodXamarin.Models
{
    public class Comentario
    {
        public int IdComentario { get; set; }
        public int IdOferta { get; set; }
        public int IdUsuario { get; set; }
        public String Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public int IdRespuesta { get; set; }
    }
}
