using CalculatriceLibrary;
using static CalculatriceLibrary.Calculatrice;

namespace TestCalculatrice
{
    [TestClass]
    public class UnitTest1
    {
        public static Random RND = new();
        [TestMethod]
        public void TestOperandesThrowArgumentOutOfRange()
        {
            Calculatrice calc = new();
            calc.AddDigit(1);
            string res = "1";
            for (int _ = 0; _ < 100; ++_)
            {
                int v = RND.Next(-100, 100);
                if (v >= 0 && v <= 9)
                {
                    calc.AddDigit(v);
                    res += v.ToString();
                    Assert.AreEqual(res, calc.Operations);
                }
                else
                {
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() => { calc.AddDigit(v); });
                }
            }
        }
        [TestMethod]
        public void TestOperandes()
        {
            // tests aléatoires
            Calculatrice calc = new();
            for (int _ = 0; _ < 100; ++_)
            {
                calc.Reset();
                calc.AddDigit(0);
                Assert.IsTrue(Math.Abs(calc.Resultat) < 1e-5, $"Le résultat ({calc.Resultat}) devrait être nul !");
                Assert.AreEqual("0", calc.Operations);
                calc.AddDigit(0);
                Assert.IsTrue(Math.Abs(calc.Resultat) < 1e-5, $"Le résultat ({calc.Resultat}) devrait être nul !");
                Assert.AreEqual("0", calc.Operations);
                double res = 0.0;
                for (int __ = 0; __ < 5; __++)
                {
                    int digit = RND.Next(0, 10);
                    res = res * 10.0 + digit;
                    calc.AddDigit(digit);
                    Assert.IsTrue(Math.Abs(calc.Resultat - res) < 1e-5,
                     $"Le résultat {calc.Resultat} devrait être égal à {res}");
                    Assert.AreEqual(((int)res).ToString(), calc.Operations);
                }
            }
        }


        [TestMethod]
        public void TestOperateurs()
        {
            // Premier test : 0 0 + 0 2 * 0 0 - 0 4
            // Test de la gestion des 0
            Calculatrice calc = new Calculatrice();
            Assert.IsFalse(calc.AddDigit(0));
            Assert.AreEqual("0", calc.Operations);
            Assert.IsFalse(calc.AddDigit(0));
            Assert.AreEqual("0", calc.Operations);
            calc.AddOperateur(Operation.ADDITIONNER);
            Assert.AreEqual("0+", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 0");
            Assert.IsTrue(calc.AddDigit(0));
            Assert.AreEqual("0+0", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 0");
            Assert.IsTrue(calc.AddDigit(2));
            Assert.AreEqual("0+2", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat - 2) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 2");
            calc.AddOperateur(Operation.MULTIPLIER);
            Assert.AreEqual("0+2*", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat - 2) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 2");
            Assert.IsTrue(calc.AddDigit(0));
            Assert.AreEqual("0+2*0", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 0");
            Assert.IsFalse(calc.AddDigit(0));
            Assert.AreEqual("0+2*0", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 0");
            calc.AddOperateur(Operation.SOUSTRAIRE);
            Assert.AreEqual("0+2*0-", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 0");
            Assert.IsTrue(calc.AddDigit(0));
            Assert.AreEqual("0+2*0-0", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 0");
            Assert.IsTrue(calc.AddDigit(4));
            Assert.AreEqual("0+2*0-4", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat + 4) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à -4");
            calc.Reset();
            // Test de la gestion des operateurs
            calc.AddDigit(5);
            calc.AddDigit(4);
            Assert.AreEqual("54", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat - 54) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 54");
            calc.AddOperateur(Operation.ADDITIONNER);
            Assert.AreEqual("54+", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat - 54) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 54");
            calc.AddOperateur(Operation.SOUSTRAIRE);
            Assert.AreEqual("54-", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat - 54) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 54");
            calc.AddOperateur(Operation.DIVISER);
            Assert.AreEqual("54/", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat - 54) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 54");
            calc.AddOperateur(Operation.MULTIPLIER);
            Assert.AreEqual("54*", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat - 54) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 54");

            calc.AddOperateur(Operation.MULTIPLIER);
            Assert.AreEqual("54*", calc.Operations);
            Assert.IsTrue(Math.Abs(calc.Resultat - 54) < 1e-5, $"Le resultat {calc.Resultat} devrait être egal à 54");
        }


    }
}