using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFoodXamarin.Models
{
    public class UsuarioLogin
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Salt { get; set; }
        public String ResetSalt { get; set; }
        public String UserName { get; set; }
        public int Rol { get; set; }
        public String Nombre { get; set; }
        public String Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
