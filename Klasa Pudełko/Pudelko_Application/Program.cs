using System;
using System.Linq;
using Pudelko_Lib;

namespace Pudelko_App
{
    class Program
    {
        public static void Main(string[] args)
        {
            
            //SEKCJA1 -- INICJALIZACJA-----------------------------------------------------
            #region Initialization
            //Lista pudełek do przedstawienia sortowania.
            List<Pudelko> collectionOfBoxes = new List<Pudelko>()
            { new Pudelko(),
              new Pudelko(5, 5, 5),
              new Pudelko(5, 6),
              new Pudelko(3.23),
              new Pudelko(7.30, 6.342, 4.1),
              new Pudelko(60, 100, unit: UnitOfMeasure.centimeter),
              new Pudelko(500, unit: UnitOfMeasure.milimeter)
            };

            //Konsolowe wypisanie wszystkich możliwych testów i umożliwienie wyboru.
        starter:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Wynik jakiego testu chciałbyś otrzymać ?\n");
            Console.WriteLine("1 = Lista z ToString() oraz Posortowana lista z ToString().");
            Console.WriteLine("2 = ToString() w różnych formatach.");
            Console.WriteLine("3 = Pole oraz Objętość.");
            Console.WriteLine("4 = Indexer.");
            Console.WriteLine("5 = Equals oraz operatory == i !=.");
            Console.WriteLine("6 = Operator +.");
            Console.WriteLine("7 = Konwersje.");
            Console.WriteLine("8 = Enumerator.");
            Console.WriteLine("9 = Parse.");

            var selection = int.Parse(Console.ReadLine());
            Console.Clear();
            #endregion

            //SEKCJA2 -- TESTY-------------------------------------------------------------
            #region Tests
            //Kod wszystkich możliwych do wyboru testów.
            switch (selection)
            {
                case 1:
                    //ToString()
                    Console.WriteLine("Wypisanie pudełek z ToString() :\n");
                    foreach (var box in collectionOfBoxes)
                    {
                        Console.WriteLine(box);
                    }
                    Console.WriteLine();
  
                    //Sortowanie za pomocą delegatu.
                    collectionOfBoxes.Sort(CompareBoxes);
                    Console.WriteLine("Wypisanie pudełek po posortowaniu z delegatem Comparison<Pudelko> :\n");
                    foreach (var box in collectionOfBoxes)
                    {
                        Console.WriteLine(box);
                    }

                    //Przywrócenie liście wartości domyślnych.
                    collectionOfBoxes[0] = new Pudelko();
                    collectionOfBoxes[1] = new Pudelko(5, 5, 5);
                    collectionOfBoxes[2] = new Pudelko(5, 6);
                    collectionOfBoxes[3] = new Pudelko(3.23);
                    collectionOfBoxes[4] = new Pudelko(7.30, 6.342, 4.1);
                    collectionOfBoxes[5] = new Pudelko(60, 100, unit: UnitOfMeasure.centimeter);
                    collectionOfBoxes[6] = new Pudelko(500, unit: UnitOfMeasure.milimeter);
                    break;
                case 2:
                    //ToString() w różnych formatach.
                    Console.WriteLine("Wypisanie pudełek z ToString() w różnych formatach :\n");
                    Console.WriteLine("Pudełko: new Pudelko(), Format: cm");
                    Console.WriteLine((collectionOfBoxes[0]).ToString("cm"));
                    Console.WriteLine();
                    Console.WriteLine("Pudełko: new Pudelko(5, 5, 5), Format: cm");
                    Console.WriteLine((collectionOfBoxes[1]).ToString("cm"));
                    Console.WriteLine();
                    Console.WriteLine("Pudełko: new Pudelko(5, 6), Format: mm");
                    Console.WriteLine((collectionOfBoxes[2]).ToString("mm"));
                    Console.WriteLine();
                    Console.WriteLine("Pudełko: new Pudelko(3.23), Format: mm");
                    Console.WriteLine((collectionOfBoxes[3]).ToString("mm"));
                    Console.WriteLine();
                    Console.WriteLine("Pudełko: new Pudelko(60, 100, unit: UnitOfMeasure.centimeter), Format: m");
                    Console.WriteLine((collectionOfBoxes[5]).ToString("m"));
                    break;
                case 3:
                    //Pole oraz Objętość pudełka.
                    Console.WriteLine("Wypisanie Objętości oraz Pola pudełka :\n");
                    Console.WriteLine("Pudełko: new Pudelko(5, 5, 5)\n");
                    Console.WriteLine("Wynik:");
                    Console.WriteLine(collectionOfBoxes[1].Objetosc + " - Objętość");
                    Console.WriteLine(collectionOfBoxes[1].Pole + " - Pole");
                    break;
                case 4:
                    //Indexer.
                    Pudelko p = new Pudelko(7.30, 6.342, 4.1);
                    Console.WriteLine("Wypisanie wymiarów pudełka z indeksera :\n");
                    Console.WriteLine("Pudełko: new Pudelko(7.30, 6.342, 4.1)\n");
                    Console.WriteLine("Wynik:");
                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine(p[i]);
                    }
                    break;
                case 5:
                    //Equals oraz operatory == i !=.
                    Pudelko p1 = new Pudelko(10, 10, 10, UnitOfMeasure.centimeter);
                    Pudelko p2 = new Pudelko(100, 100, 100, UnitOfMeasure.milimeter);
                    Pudelko p3 = new Pudelko(100, 100, 150, UnitOfMeasure.milimeter);
                    
                    Console.WriteLine("Wypisanie wartości boolowej od Equals oraz Od operatorów == i != :\n");
                    Console.WriteLine("Pudełko p1: new Pudelko(10, 10, 10, UnitOfMeasure.centimeter)");
                    Console.WriteLine("Pudełko p2: new Pudelko(100, 100, 100, UnitOfMeasure.milimeter)");
                    Console.WriteLine("Pudełko p3: new Pudelko(100, 100, 150, UnitOfMeasure.milimeter)\n");
                    Console.WriteLine("Wyniki:");
                    Console.WriteLine($"Pudełko p1: {p1} jest równe pudełku p2: {p2}. Odpowiedź: {p1.Equals(p2)}");
                    Console.WriteLine($"Pudełko p2: {p2} jest równe pudełku p3: {p3}. Odpowiedź: {p2.Equals(p3)}");
                    Console.WriteLine($"Pudełko p1: {p1} jest równe pudełku p2: {p2}. Odpowiedź: {p1 == p2}");
                    Console.WriteLine($"Pudełko p2: {p2} jest równe pudełku p3: {p3}. Odpowiedź: {p2 == p3}");
                    Console.WriteLine($"Pudełko p1: {p1} nie jest równe pudełku p2: {p2}. Odpowiedź: {p1 != p2}");
                    Console.WriteLine($"Pudełko p2: {p2} nie jest równe pudełku p3: {p3}. Odpowiedź: {p2 != p3}");
                    break;
                case 6:
                    //Operator +.
                    Pudelko p4 = new Pudelko(100, 100, 100, UnitOfMeasure.milimeter);
                    Pudelko p5 = new Pudelko(100, 100, 150, UnitOfMeasure.milimeter);
                    Console.WriteLine("Wypisanie wyniku operatora dodawania + (p4 + p5) :\n");
                    Console.WriteLine("Pudełko p4: new Pudelko(100, 100, 100, UnitOfMeasure.milimeter)");
                    Console.WriteLine("Pudełko p5: new Pudelko(100, 100, 150, UnitOfMeasure.milimeter)\n");
                    Console.WriteLine("Wynik:");
                    Console.WriteLine(p4 + p5);
                    break;
                case 7:
                    //Konwersje
                    Pudelko p6 = new Pudelko(111, 222, 333, UnitOfMeasure.centimeter);
                    double[] bokiP6 = (double[])p6;
                    Console.WriteLine("Przekonwertowanie pudełka na typ double[] i wypisanie wartości A, B, C :\n");
                    Console.WriteLine("Pudełko: new Pudelko(111, 222, 333, UnitOfMeasure.centimeter)\n");
                    Console.WriteLine("Wynik:");
                    foreach (double bok in bokiP6)
                    {
                        Console.WriteLine(bok);
                    }
                    Console.WriteLine();

                    Console.WriteLine("Przekonwertowanie tupli na pudełko i wypisanie go w postaci ToString() :\n");
                    Console.WriteLine("Tupla: (5000, 5000, 5000)\n");
                    Console.WriteLine("Wynik:");
                    Pudelko conversionBox = (5000, 5000, 5000);
                    Console.WriteLine(conversionBox);
                    break;
                case 8:
                    //Enumerator
                    Pudelko p7 = new Pudelko(111, 222, 333, UnitOfMeasure.centimeter);
                    Console.WriteLine("Wypisanie wymiarów za pomocą enumeratora :\n");
                    Console.WriteLine("Pudełko: new Pudelko(111, 222, 333, UnitOfMeasure.centimeter)\n");
                    Console.WriteLine("Wynik:");
                    foreach (double wymiar in p7)
                    {
                        Console.WriteLine(wymiar);
                    }
                    break;
                case 9:
                    //Parse
                    Console.WriteLine("Porównanie do siebie elementu klasy pudełka i 'stringa' przeparsowanego na tą klasę :\n");
                    Console.WriteLine("Porównanie1: new Pudelko(2.5, 9.321, 0.1) == Pudelko.Parse(\"2.500 m × 9.321 m × 0.100 m\")\nPorównanie2: new Pudelko(250, 930, 200, UnitOfMeasure.centimeter) == Pudelko.Parse(\"2.500 m × 9.300 m × 2.000 m\")\n");
                    Console.WriteLine("Wynik:");
                    Console.WriteLine(new Pudelko(2.5, 9.321, 0.1) == Pudelko.Parse("2.500 m × 9.321 m × 0.100 m"));
                    Console.WriteLine(new Pudelko(250, 930, 200, UnitOfMeasure.centimeter) == Pudelko.Parse("2.500 m × 9.300 m × 2.000 m"));
                    break;
                default:
                    throw new FormatException("Numer takiego testu nie istnieje !");
            }
            #endregion

            //SEKCJA3 -- DOMKNIĘCIE--------------------------------------------------------
            #region Backer
            //Kod odpowiadający za powrót z powrotem do wyboru testów.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nWpisz '0' aby powrócić do wyboru testów.");
            var backer = int.Parse(Console.ReadLine());
            if (backer == 0) goto starter;
            else throw new FormatException("Jedynym dozwolonym wpisem jest 0 !");
            #endregion


        }

        //SEKCJA4 - SORTOWANIE-------------------------------------------------------------
        private static int CompareBoxes(Pudelko p1, Pudelko p2)
        {
            if (p1 is null && p2 is null) return 0;
            if (p1 is null) return -1;
            if (p2 is null) return 1;

            int result1 = p1.Objetosc.CompareTo(p2.Objetosc);
            int result2 = p1.Pole.CompareTo(p2.Pole);
            int result3 = (p1.A + p1.B + p1.C).CompareTo(p2.A + p2.B + p2.C);

            if (result1 != 0) return result1 * -1;
            else if (result2 != 0) return result2 * -1;
            else return result3 * -1;
        }
  
    }
}