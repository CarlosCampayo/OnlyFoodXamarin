using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFoodXamarin.Models
{
    public class Votacion
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdOferta { get; set; }
        public String Voto { get; set; }
    }
}
