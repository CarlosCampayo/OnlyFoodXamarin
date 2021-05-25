using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFoodXamarin.Models
{
    public class Oferta
    {
        public int Id { get; set; }
        public int IdCadena { get; set; }
        public String Titulo { get; set; }
        public String Descripcion { get; set; }
        public String Imagen { get; set; }
        public String Web { get; set; }
        public double Precio { get; set; }
        public String Codigo { get; set; }
        public int IdUsuario { get; set; }
    }
}
