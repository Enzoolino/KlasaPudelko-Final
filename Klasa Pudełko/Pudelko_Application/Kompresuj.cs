using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pudelko_Lib;

namespace Pudelko_App
{
    public static class Kompresuj
    {
        public static Pudelko Compress (this Pudelko p)
        {
            double V = p.Objetosc;
            var bokSześcianowy = Math.Cbrt(V);

            return new Pudelko(bokSześcianowy, bokSześcianowy, bokSześcianowy);

        }
    }
}
