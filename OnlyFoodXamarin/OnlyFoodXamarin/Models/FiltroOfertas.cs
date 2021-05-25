using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFoodXamarin.Models
{
    public class FiltroOfertas
    {
        public FiltroOfertas()
        {
            this.IdCadenas = new List<int>();
        }
        public List<int> IdCadenas { get; set; }
        public int Preciomin { get; set; }
        public int Preciomax { get; set; }

    }
}
