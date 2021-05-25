using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFood.Helpers
{
    public class HelperToolkit
    {
        public static bool CompararArrayBytes(byte[] a, byte[] b)
        {
            bool iguales = true;
            if (a.Length != b.Length)
            {
                return false;
            }
            for(int i = 0; i < a.Length; i++)
            {
                if (a[i].Equals(b[i]) == false)
                {
                    iguales= false;
                    break;
                }
            }
            return iguales;
        }
        public static String NormalizeName(String name)
        {
            String newname = "";
            foreach(Char a in name)
            {
                if (Char.IsDigit(a) || Char.IsLetter(a) || a=='.')
                {
                    newname += a;
                }
            }
            return newname;
        }
    }
}
