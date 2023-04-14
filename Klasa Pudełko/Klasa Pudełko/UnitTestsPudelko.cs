using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pudelko_Lib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace PudelkoUnitTests
{

    [TestClass]
    public static class InitializeCulture
    {
        [AssemblyInitialize]
        public static void SetEnglishCultureOnAllUnitTest(TestContext context)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
    }

    // ========================================

    [TestClass]
    public class UnitTestsPudelkoConstructors
    {
        private static double defaultSize = 0.1; // w metrach
        private static double accuracy = 0.001; //dokładność 3 miejsca po przecinku

        private void AssertPudelko(Pudelko p, double expectedA, double expectedB, double expectedC)
        {
            Assert.AreEqual(expectedA, p.A, delta: accuracy);
            Assert.AreEqual(expectedB, p.B, delta: accuracy);
            Assert.AreEqual(expectedC, p.C, delta: accuracy);
        }

        #region Constructor tests ================================

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            Pudelko p = new Pudelko();

            Assert.AreEqual(defaultSize, p.A, delta: accuracy);
            Assert.AreEqual(defaultSize, p.B, delta: accuracy);
            Assert.AreEqual(defaultSize, p.C, delta: accuracy);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
        public void Constructor_3params_DefaultMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
        public void Constructor_3params_InMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100.0, 25.5, 3.1,
                 1.0, 0.255, 0.031)]
        [DataRow(100.0, 25.58, 3.13,
                 1.0, 0.255, 0.031)] // dla centymertów liczy się tylko 1 miejsce po przecinku
        public void Constructor_3params_InCentimeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a: a, b: b, c: c, unit: UnitOfMeasure.centimeter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100, 255, 3,
                 0.1, 0.255, 0.003)]
        [DataRow(100.0, 25.58, 3.13,
                 0.1, 0.025, 0.003)] // dla milimetrów nie liczą się miejsca po przecinku
        public void Constructor_3params_InMilimeters(double a, double b, double c,
                                                     double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a, b: b, c: c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }


        // ----

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_DefaultMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a, b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_InMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a: a, b: b, unit: UnitOfMeasure.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 2.5, 0.11, 0.025)]
        [DataRow(100.1, 2.599, 1.001, 0.025)]
        [DataRow(2.0019, 0.25999, 0.02, 0.002)]
        public void Constructor_2params_InCentimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.centimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 2.0, 0.011, 0.002)]
        [DataRow(100.1, 2599, 0.1, 2.599)]
        [DataRow(200.19, 2.5999, 0.2, 0.002)]
        public void Constructor_2params_InMilimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        // -------

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_DefaultMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_InMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 0.11)]
        [DataRow(100.1, 1.001)]
        [DataRow(2.0019, 0.02)]
        public void Constructor_1param_InCentimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.centimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 0.011)]
        [DataRow(100.1, 0.1)]
        [DataRow(200.19, 0.2)]
        public void Constructor_1param_InMilimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        // ---

        public static IEnumerable<object[]> DataSet1Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5, 3.1},
            new object[] {1.0, -2.5, 3.1},
            new object[] {1.0, 2.5, -3.1},
            new object[] {-1.0, -2.5, 3.1},
            new object[] {-1.0, 2.5, -3.1},
            new object[] {1.0, -2.5, -3.1},
            new object[] {-1.0, -2.5, -3.1},
            new object[] {0, 2.5, 3.1},
            new object[] {1.0, 0, 3.1},
            new object[] {1.0, 2.5, 0},
            new object[] {1.0, 0, 0},
            new object[] {0, 2.5, 0},
            new object[] {0, 0, 3.1},
            new object[] {0, 0, 0},
            new object[] {10.1, 2.5, 3.1},
            new object[] {10, 10.1, 3.1},
            new object[] {10, 10, 10.1},
            new object[] {10.1, 10.1, 3.1},
            new object[] {10.1, 10, 10.1},
            new object[] {10, 10.1, 10.1},
            new object[] {10.1, 10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_DefaultMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.01, 0.1, 1)]
        [DataRow(0.1, 0.01, 1)]
        [DataRow(0.1, 0.1, 0.01)]
        [DataRow(1001, 1, 1)]
        [DataRow(1, 1001, 1)]
        [DataRow(1, 1, 1001)]
        [DataRow(1001, 1, 1001)]
        [DataRow(1, 1001, 1001)]
        [DataRow(1001, 1001, 1)]
        [DataRow(1001, 1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InCentimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.centimeter);
        }


        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.1, 1, 1)]
        [DataRow(1, 0.1, 1)]
        [DataRow(1, 1, 0.1)]
        [DataRow(10001, 1, 1)]
        [DataRow(1, 10001, 1)]
        [DataRow(1, 1, 10001)]
        [DataRow(10001, 10001, 1)]
        [DataRow(10001, 1, 10001)]
        [DataRow(1, 10001, 10001)]
        [DataRow(10001, 10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMiliimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.milimeter);
        }


        public static IEnumerable<object[]> DataSet2Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5},
            new object[] {1.0, -2.5},
            new object[] {-1.0, -2.5},
            new object[] {0, 2.5},
            new object[] {1.0, 0},
            new object[] {0, 0},
            new object[] {10.1, 10},
            new object[] {10, 10.1},
            new object[] {10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_DefaultMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.01, 1)]
        [DataRow(1, 0.01)]
        [DataRow(0.01, 0.01)]
        [DataRow(1001, 1)]
        [DataRow(1, 1001)]
        [DataRow(1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InCentimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.1, 1)]
        [DataRow(1, 0.1)]
        [DataRow(0.1, 0.1)]
        [DataRow(10001, 1)]
        [DataRow(1, 10001)]
        [DataRow(10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMilimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.milimeter);
        }




        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_DefaultMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(0.01)]
        [DataRow(1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InCentimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(0.1)]
        [DataRow(10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMilimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.milimeter);
        }

        #endregion


        #region ToString tests ===================================

        [TestMethod, TestCategory("String representation")]
        public void ToString_Default_Culture_EN()
        {
            var p = new Pudelko(2.5, 9.321);
            string expectedStringEN = "2.500 m × 9.321 m × 0.100 m";

            Assert.AreEqual(expectedStringEN, p.ToString());
        }

        [DataTestMethod, TestCategory("String representation")]
        [DataRow(null, 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("m", 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("cm", 2.5, 9.321, 0.1, "250.0 cm × 932.1 cm × 10.0 cm")]
        [DataRow("mm", 2.5, 9.321, 0.1, "2500 mm × 9321 mm × 100 mm")]
        public void ToString_Formattable_Culture_EN(string format, double a, double b, double c, string expectedStringRepresentation)
        {
            var p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Assert.AreEqual(expectedStringRepresentation, p.ToString(format));
        }

        [TestMethod, TestCategory("String representation")]
        [ExpectedException(typeof(FormatException))]
        public void ToString_Formattable_WrongFormat_FormatException()
        {
            var p = new Pudelko(1);
            var stringformatedrepreentation = p.ToString("wrong code");
        }

        #endregion


        #region Pole, Objętość ===================================

        [TestMethod, TestCategory("Volume")]
        public void Volume_Default()
        {
            Pudelko p = new Pudelko();
            double expectedVolume = 0.001;

            Assert.AreEqual(expectedVolume, p.Objetosc);
        }

        [DataTestMethod, TestCategory("Volume")]
        [DataRow(5.15, 5, 5.15, 132.6125)]
        [DataRow(7.524, 8.555, 3.22, 207.2643804)]
        [DataRow(2.227, 7.898, 1.89, 33.24291894)]
        [DataRow(1, 1, 1, 1)]
        [DataRow(7, 7, 7, 343)]
        public void Volume_Values_InMeters(double a, double b, double c, double expectedV)
        {
            Pudelko p = new Pudelko(a, b, c);
            Assert.AreEqual(expectedV, p.Objetosc);

        }

        [DataTestMethod, TestCategory("Volume")]
        [DataRow(700, 700, 700, 343)]
        [DataRow(10, 10, 10, 0.001)]
        [DataRow(300, 325.1, 450.8, 43.966524)]
        public void Volume_Values_InCentimeters(double a, double b, double c, double expectedV)
        {
            Pudelko p = new Pudelko(a, b, c, UnitOfMeasure.centimeter);
            Assert.AreEqual(expectedV, p.Objetosc);
        }

        [DataTestMethod, TestCategory("Volume")]
        [DataRow(7000, 7000, 7000, 343)]
        [DataRow(100, 100, 100, 0.001)]
        [DataRow(500, 255, 700, 0.08925)]
        public void Volume_Values_InMilimeters(double a, double b, double c, double expectedV)
        {
            Pudelko p = new Pudelko(a, b, c, UnitOfMeasure.milimeter);
            Assert.AreEqual(expectedV, p.Objetosc);
        }

        [TestMethod, TestCategory("Area")]
        public void Area_Default()
        {
            Pudelko p = new Pudelko();
            double expectedArea = 0.06;

            Assert.AreEqual(expectedArea, p.Pole);
        }

        [DataTestMethod, TestCategory("Area")]
        [DataRow(1, 1, 1, 6)]
        [DataRow(1, 9.8, 6, 149.2)]
        [DataRow(5, 6, 7, 214)]
        [DataRow(7.15, 4.445, 7.2, 230.5315)]
        [DataRow(2, 9.99, 2, 87.92)]
        public void Area_Values_InMeters(double a, double b, double c, double expectedP)
        {
            Pudelko p = new Pudelko(a, b, c);
            Assert.AreEqual(expectedP, p.Pole);
        }

        [DataTestMethod, TestCategory("Area")]
        [DataRow(100, 100, 100, 6)]
        [DataRow(10, 10, 10, 0.06)]
        [DataRow(715, 444.5, 720, 230.5315)]
        public void Area_Values_InCentimeters(double a, double b, double c, double expectedP)
        {
            Pudelko p = new Pudelko(a, b, c, UnitOfMeasure.centimeter);
            Assert.AreEqual(expectedP, p.Pole);
        }

        [DataTestMethod, TestCategory("Area")]
        [DataRow(1000, 1000, 1000, 6)]
        [DataRow(100, 100, 100, 0.06)]
        [DataRow(7150, 4445, 7200, 230.5315)]
        public void Area_Values_InMilimeters(double a, double b, double c, double expectedP)
        {
            Pudelko p = new Pudelko(a, b, c, UnitOfMeasure.milimeter);
            Assert.AreEqual(expectedP, p.Pole);
        }

        #endregion

        #region Equals ===========================================
        [TestMethod, TestCategory("Equals")]
        public void Equals_Defaults()
        {
            Pudelko p1 = new Pudelko();
            Pudelko p2 = new Pudelko();

            Assert.AreEqual(p1, p2);
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1, 1, 1, 1, 1, 1, true)]
        [DataRow(7, 8, 9, 9, 8, 7, true)]
        [DataRow(7.33, 8.22, 5.11, 5.11, 7.33, 8.22, true)]
        [DataRow(7.33, 8.22, 5.11, 5.11, 7.22, 8.33, false)]
        [DataRow(5, 7, 9.99, 9.99, 7.001, 5, false)]
        [DataRow(5, 5, 5, 6, 6, 6, false)]
        [DataRow(5.354, 7.8888, 6.343, 6.343, 5.354, 7.8888, true)]
        public void Equals_Values_InMeters(double a1, double b1, double c1, double a2, double b2, double c2, bool expectedAnswer)
        {
            Pudelko p1 = new Pudelko(a1, b1, c1);
            Pudelko p2 = new Pudelko(a2, b2, c2);

            Assert.AreEqual(expectedAnswer, p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(100, 100, 100, 100, 100, 100, true)]
        [DataRow(733, 822, 511, 511, 733, 822, true)]
        [DataRow(500, 700, 999, 999, 700.1, 500, false)]
        public void Equals_Values_InCentimeters(double a1, double b1, double c1, double a2, double b2, double c2, bool expectedAnswer)
        {
            Pudelko p1 = new Pudelko(a1, b1, c1, UnitOfMeasure.centimeter);
            Pudelko p2 = new Pudelko(a2, b2, c2, UnitOfMeasure.centimeter);

            Assert.AreEqual(expectedAnswer, p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1000, 1000, 1000, 1000, 1000, 1000, true)]
        [DataRow(7330, 8220, 5110, 5110, 7330, 8220, true)]
        [DataRow(5000, 7000, 9999, 9999, 7001.555, 5000, false)]
        public void Equals_Values_InMilimeters(double a1, double b1, double c1, double a2, double b2, double c2, bool expectedAnswer)
        {
            Pudelko p1 = new Pudelko(a1, b1, c1, UnitOfMeasure.milimeter);
            Pudelko p2 = new Pudelko(a2, b2, c2, UnitOfMeasure.milimeter);

            Assert.AreEqual(expectedAnswer, p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1, 1, 1, 100, 100, 100, true)]
        [DataRow(7.33, 8.22, 5.11, 511, 733, 822, true)]
        [DataRow(5, 7, 9.99, 999, 700, 500, true)]
        [DataRow(5, 7, 9.99, 999, 700.1, 500, false)]
        public void Equals_Values_InMeters_InCentimeters(double a1, double b1, double c1, double a2, double b2, double c2, bool expectedAnswer)
        {
            Pudelko p1 = new Pudelko(a1, b1, c1);
            Pudelko p2 = new Pudelko(a2, b2, c2, UnitOfMeasure.centimeter);

            Assert.AreEqual(expectedAnswer, p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1, 1, 1, 1000, 1000, 1000, true)]
        [DataRow(7.33, 8.22, 5.11, 5110, 7330, 8220, true)]
        [DataRow(5, 7, 9.999, 9999, 7000, 5000, true)]
        [DataRow(5, 7, 9.999, 9999, 7001, 5000, false)]
        public void Equals_Values_InMeters_InMilimeters(double a1, double b1, double c1, double a2, double b2, double c2, bool expectedAnswer)
        {
            Pudelko p1 = new Pudelko(a1, b1, c1);
            Pudelko p2 = new Pudelko(a2, b2, c2, UnitOfMeasure.milimeter);

            Assert.AreEqual(expectedAnswer, p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(100, 100, 100, 1000, 1000, 1000, true)]
        [DataRow(733, 822, 511, 5110, 7330, 8220, true)]
        [DataRow(500, 700, 999.9, 9999, 7000, 5000, true)]
        [DataRow(500, 700, 999.9, 9999, 7001, 5000, false)]
        public void Equals_Values_InCentimeters_InMilimeters(double a1, double b1, double c1, double a2, double b2, double c2, bool expectedAnswer)
        {
            Pudelko p1 = new Pudelko(a1, b1, c1, UnitOfMeasure.centimeter);
            Pudelko p2 = new Pudelko(a2, b2, c2, UnitOfMeasure.milimeter);

            Assert.AreEqual(expectedAnswer, p1.Equals(p2));
        }


        #endregion

        #region Operators overloading ===========================
 
        [TestMethod, TestCategory("+ Operator")]
        public void SumOperator_Default()
        {
            Pudelko expectedBox = new Pudelko(0.1, 0.1, 0.2);
            Pudelko p1 = new Pudelko();
            Pudelko p2 = new Pudelko();

            Assert.AreEqual(expectedBox, p1 + p2);
        }

        [DataTestMethod, TestCategory("+ Operator")]
        [DataRow(5, 5, 5, 5, 5, 5, 5, 5, 10)]
        [DataRow(3, 4, 5, 5, 6, 7, 7, 6, 8)]
        [DataRow(2.256, 6.33, 1.70, 2, 4, 5.60, 6.33, 4, 3.70)]
        [DataRow(2.23, 5.63, 4.99, 2.22, 5, 5.64, 5.64, 5, 4.45)]
        [DataRow(9.99, 9.99, 1, 9.99, 9.99, 0.99, 9.99, 9.99, 1.99)]
        public void SumOperator_InMeters(double a1, double b1, double c1, double a2, double b2, double c2, double expectedA, double expectedB, double expectedC)
        {
            Pudelko p1 = new Pudelko(a1, b1, c1);
            Pudelko p2 = new Pudelko(a2, b2, c2);

            Pudelko expectedBox = new Pudelko(expectedA, expectedB, expectedC);

            Assert.AreEqual(expectedBox, p1 + p2);

        }

        [DataTestMethod, TestCategory("+ Operator")]
        [DataRow(500, 500, 500, 500, 500, 500, 500, 500, 1000)]
        [DataRow(300, 400, 500, 500, 600, 700, 700, 600, 800)]
        [DataRow(225.6, 633, 170, 200, 400, 560, 633, 400, 370)]
        public void SumOperator_InCentimeters(double a1, double b1, double c1, double a2, double b2, double c2, double expectedA, double expectedB, double expectedC)
        {
            Pudelko p1 = new Pudelko(a1, b1, c1, UnitOfMeasure.centimeter);
            Pudelko p2 = new Pudelko(a2, b2, c2, UnitOfMeasure.centimeter);

            Pudelko expectedBox = new Pudelko(expectedA, expectedB, expectedC, UnitOfMeasure.centimeter);

            Assert.AreEqual(expectedBox, p1 + p2);

        }

        [DataTestMethod, TestCategory("+ Operator")]
        [DataRow(5000, 5000, 5000, 5000, 5000, 5000, 5000, 5000, 10000)]
        [DataRow(3000, 4000, 5000, 5000, 6000, 7000, 7000, 6000, 8000)]
        [DataRow(2256, 6330, 1700, 2000, 4000, 5600, 6330, 4000, 3700)]
        public void SumOperator_InMilimeters(double a1, double b1, double c1, double a2, double b2, double c2, double expectedA, double expectedB, double expectedC)
        {
            Pudelko p1 = new Pudelko(a1, b1, c1, UnitOfMeasure.milimeter);
            Pudelko p2 = new Pudelko(a2, b2, c2, UnitOfMeasure.milimeter);

            Pudelko expectedBox = new Pudelko(expectedA, expectedB, expectedC, UnitOfMeasure.milimeter);

            Assert.AreEqual(expectedBox, p1 + p2);

        }

        [DataTestMethod, TestCategory("+ Operator")]
        [DataRow(5, 5, 5, 500, 500, 500, 5, 5, 10)]
        [DataRow(3, 4, 5, 500, 600, 700, 7, 6, 8)]
        [DataRow(2.256, 6.33, 1.70, 200, 400, 560, 6.33, 4, 3.70)]
        public void SumOperator_InMeters_InCentimeters(double a1, double b1, double c1, double a2, double b2, double c2, double expectedA, double expectedB, double expectedC)
        {
            Pudelko p1 = new Pudelko(a1, b1, c1);
            Pudelko p2 = new Pudelko(a2, b2, c2, UnitOfMeasure.centimeter);

            Pudelko expectedBox = new Pudelko(expectedA, expectedB, expectedC);

            Assert.AreEqual(expectedBox, p1 + p2);

        }

        [DataTestMethod, TestCategory("+ Operator")]
        [DataRow(5, 5, 5, 5000, 5000, 5000, 5, 5, 10)]
        [DataRow(3, 4, 5, 5000, 6000, 7000, 7, 6, 8)]
        [DataRow(2.256, 6.33, 1.70, 2000, 4000, 5600, 6.33, 4, 3.70)]
        public void SumOperator_InMeters_InMilimeters(double a1, double b1, double c1, double a2, double b2, double c2, double expectedA, double expectedB, double expectedC)
        {
            Pudelko p1 = new Pudelko(a1, b1, c1);
            Pudelko p2 = new Pudelko(a2, b2, c2, UnitOfMeasure.milimeter);

            Pudelko expectedBox = new Pudelko(expectedA, expectedB, expectedC);

            Assert.AreEqual(expectedBox, p1 + p2);

        }

        [DataTestMethod, TestCategory("+ Operator")]
        [DataRow(500, 500, 500, 5000, 5000, 5000, 5, 5, 10)]
        [DataRow(300, 400, 500, 5000, 6000, 7000, 7, 6, 8)]
        [DataRow(225.6, 633, 170, 2000, 4000, 5600, 6.33, 4, 3.70)]
        public void SumOperator_InCentimeters_InMilimeters(double a1, double b1, double c1, double a2, double b2, double c2, double expectedA, double expectedB, double expectedC)
        {
            Pudelko p1 = new Pudelko(a1, b1, c1, UnitOfMeasure.centimeter);
            Pudelko p2 = new Pudelko(a2, b2, c2, UnitOfMeasure.milimeter);

            Pudelko expectedBox = new Pudelko(expectedA, expectedB, expectedC);

            Assert.AreEqual(expectedBox, p1 + p2);

        }

        #endregion

        #region Conversions =====================================
        [TestMethod]
        public void ExplicitConversion_ToDoubleArray_AsMeters()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            double[] tab = (double[])p;
            Assert.AreEqual(3, tab.Length);
            Assert.AreEqual(p.A, tab[0]);
            Assert.AreEqual(p.B, tab[1]);
            Assert.AreEqual(p.C, tab[2]);
        }

        [TestMethod]
        public void ImplicitConversion_FromAalueTuple_As_Pudelko_InMilimeters()
        {
            var (a, b, c) = (2500, 9321, 100); // in milimeters, ValueTuple
            Pudelko p = (a, b, c);
            Assert.AreEqual((int)(p.A * 1000), a);
            Assert.AreEqual((int)(p.B * 1000), b);
            Assert.AreEqual((int)(p.C * 1000), c);
        }

        #endregion

        #region Indexer, enumeration ============================
        [TestMethod]
        public void Indexer_ReadFrom()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            Assert.AreEqual(p.A, p[0]);
            Assert.AreEqual(p.B, p[1]);
            Assert.AreEqual(p.C, p[2]);
        }

        [TestMethod]
        public void ForEach_Test()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            var tab = new[] { p.A, p.B, p.C };
            int i = 0;
            foreach (double x in p)
            {
                Assert.AreEqual(x, tab[i]);
                i++;
            }
        }

        #endregion

        #region Parsing =========================================

        #endregion

    }
}
