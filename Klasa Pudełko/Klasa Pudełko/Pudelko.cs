using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace Pudelko_Lib
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>, IEnumerator<double>
    {
        //Wartości przechowywane są w metrach (transformacja na fazie konstruktora).
        private readonly double x = 0.1;
        private readonly double y = 0.1;
        private readonly double z = 0.1;

        //Długości krawędzi pudełka przedstawione w metrach i skrócone o ustaloną liczbę miejsc.
        public double A => Math.Truncate(x * 1000) / 1000;
        public double B => Math.Truncate(y * 1000) / 1000;
        public double C => Math.Truncate(z * 1000) / 1000;

        //Objętość pudełka.
        public double Objetosc
        {
            get 
            {
                double V = (A * B * C);
                return Math.Round(V, 9);
            }
        }

        //Pole całkowite pudełka.
        public double Pole
        {
            get
            {
                double P = (2 * (A * B + A * C + B * C));
                return Math.Round(P, 6);
            }
        }

        //Tablica wymiarów stworzona na potrzeby indexera.
        private double[] ArrOfDimensions => new double[] { A, B, C };

        public static double MeterNumberConverter(double num, UnitOfMeasure unit)
        {
            return unit switch
            {
                UnitOfMeasure.milimeter => num / 1000,
                UnitOfMeasure.centimeter => num / 100,
                _ => num,
            };
        }

        private static void CheckExceptions(double a, double b, double c)
        {
            //Reguła niedodatniości.
            if (a <= 0 || b <= 0 || c <= 0)
            {
                throw new ArgumentOutOfRangeException("Wymiary nie mogą być niedodatnie !");
            }

            //Ograniczenie wymiarów do max 10m.
            if (a > 10 || b > 10 || c > 10)
            {
                throw new ArgumentOutOfRangeException("Wymiary większe niż 10m są niedozwolone !");
            }
        }

        //Indexer
        public double this[int index]
        {
            get => ArrOfDimensions[index];
        }

        //Konstruktor
        public Pudelko(double a, double b, double c, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            x = MeterNumberConverter(a, unit);
            y = MeterNumberConverter(b, unit);
            z = MeterNumberConverter(c, unit);

            CheckExceptions(A, B, C);
        }

        public Pudelko(double a, double b, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            x = MeterNumberConverter(a, unit);
            y = MeterNumberConverter(b, unit);

            CheckExceptions(A, B, C);
        }

        public Pudelko(double a = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            x = MeterNumberConverter(a, unit);

            CheckExceptions(A, B, C);
        }

        #region Implementacja IFormattable

        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider = null)
        {
            if (String.IsNullOrEmpty(format)) format = "G";
            provider ??= CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "G":
                case "M":
                    return A.ToString("F3", provider) + " m" + " × " + B.ToString("F3", provider) + " m" + " × " + C.ToString("F3", provider) + " m";
                case "CM":
                    return (A * 100).ToString("F1", provider) + " cm" + " × " + (B * 100).ToString("F1", provider) + " cm" + " × " + (C * 100).ToString("F1", provider) + " cm";
                case "MM":
                    return (A * 1000).ToString("F0", provider) + " mm" + " × " + (B * 1000).ToString("F0", provider) + " mm" + " × " + (C * 1000).ToString("F0", provider) + " mm";
                default:
                    throw new FormatException(String.Format("Format {0} nie jest obsługiwany.", format));
            }
        }

        #endregion Implementacja IFormattable

        #region Implementacja IEquatable<Pudelko>

        public bool Equals(Pudelko other)
        {
            if (other is null) return false;
            if (Object.ReferenceEquals(this, other))
                return true;

            double[] box1 = { A, B, C };
            double[] box2 = { other.A, other.B, other.C };
            Array.Sort(box1);
            Array.Sort(box2);

            return
                 (box1[0] == box2[0] &&
                  box1[1] == box2[1] &&
                  box1[2] == box2[2]
                 );
        }

        public override bool Equals(object obj)
        {
            return obj is Pudelko ? Equals((Pudelko)obj) : false;
        }

        public override int GetHashCode() => (A, B, C).GetHashCode();

        #endregion Implementacja IEquatable<Pudelko>

        #region Operators

        public static bool operator ==(Pudelko p1, Pudelko p2)
        {
            if (object.ReferenceEquals(p1, null) || object.ReferenceEquals(p2, null))
                return object.ReferenceEquals(p1, p2);

            return p1.Equals(p2);
        }
        public static bool operator !=(Pudelko p1, Pudelko p2) => !(p1 == p2);

        public static Pudelko operator +(Pudelko p1, Pudelko p2)
        {
            
            double[] box1 = { p1.A, p1.B, p1.C };
            double[] box2 = { p2.A, p2.B, p2.C };
            Array.Sort(box1);
            Array.Sort(box2);
            Array.Reverse(box1);
            Array.Reverse(box2);

            double length = Math.Max(box1[0], box2[0]);
            double width = Math.Max(box1[1], box2[1]);
            double height = box1[2] + box2[2];

            return new Pudelko(length, width, height);
        }

        #endregion Operators

        #region Konwersje

        public static explicit operator double[](Pudelko p)
        {
            return new double[] { p.A, p.B, p.C };
        }

        public static implicit operator Pudelko((int x, int y, int z) dimensions) => new Pudelko(dimensions.x, dimensions.y, dimensions.z, UnitOfMeasure.milimeter);

        #endregion Konwersje

        #region Enumerator

        public IEnumerator<double> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        int index = -1;
        
        public double Current => this[index];
        object IEnumerator.Current => Current;

        public void Dispose()
        {
            //Brak elementów, których trzeba się pozbyć
        }

        public bool MoveNext()
        {
            index++;
            return (index < 3);
        }

        public void Reset()
        {
            index = -1;
        }
        #endregion Enumerator

        #region Parse
        public static Pudelko Parse(string pudelko)
        {
            if (pudelko == null) throw new ArgumentNullException(nameof(pudelko));

            var parts = pudelko.Split(new string[] { " x ", "×" }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 3)
            {
                throw new FormatException($"Nieprawidłowy format ciągu wejściowego: {pudelko}");
            }

            UnitOfMeasure unit = UnitOfMeasure.meter;

            if (pudelko.Contains("m"))
                unit = UnitOfMeasure.meter;
            else if (pudelko.Contains("cm"))
                unit = UnitOfMeasure.centimeter;
            else if (pudelko.Contains("mm"))
                unit = UnitOfMeasure.milimeter;
            

            var a = double.Parse(parts[0].Trim().Replace("m", "").Replace("cm", "").Replace("mm", ""), CultureInfo.InvariantCulture);
            var b = double.Parse(parts[1].Trim().Replace("m", "").Replace("cm", "").Replace("mm", ""), CultureInfo.InvariantCulture);
            var c = double.Parse(parts[2].Trim().Replace("m", "").Replace("cm", "").Replace("mm", ""), CultureInfo.InvariantCulture);

            return new Pudelko(a, b, c, unit);

        }
        #endregion Parse
    }
}
