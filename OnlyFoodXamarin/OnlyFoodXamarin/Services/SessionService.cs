using System;
using System.Collections.Generic;
using System.Text;
using OnlyFoodXamarin.Models;

namespace OnlyFoodXamarin.Services
{
    public class SessionService
    {
        public String Token { get; set; }
        public Usuario Usuario { get; set; }
        public String Password { get; set; }
        public SessionService()
        {
            
        }
    }
}
