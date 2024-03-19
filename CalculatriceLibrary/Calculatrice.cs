using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatriceLibrary
{
    public class Calculatrice
    {
        public string Operations { get; private set; }
        public double Resultat { get; private set; }

        public enum Operation { ADDITIONNER, SOUSTRAIRE, MULTIPLIER ,DIVISER }

        private Dictionary<Operation, string> operation;

        private bool _operateur = false;
        private bool _unique_0 = true;

        public Calculatrice()
        {
                operation = new Dictionary<Operation, string>
            {
                { Operation.ADDITIONNER, "+" },
                { Operation.SOUSTRAIRE, "-" },
                { Operation.MULTIPLIER, "*" },
                { Operation.DIVISER, "/" }
            }; 
            
             Operations = "0";
        }

        public bool AddDigit(int digit)
        {
            // Vérification si le digit est compris entre 0 et 9
            if (digit < 0 || digit > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(digit), $"Calculatrice.AddDigit : Le paramètre {digit} doit être compris entre 0 et 9.");
            }

            if (digit == 0)
            {
                if (_operateur)
                {
                    // Ajout de 0 au calcul en cours et mise à true de _unique_0
                    Operations += "0";
                    _unique_0 = true;
                }
                else if (_unique_0)
                {
                    return false;
                }
                else
                {
                    Operations += "0";
                }
            }
            else
            {
                if (_unique_0)
                {
                    Operations = Operations.Remove(Operations.Length - 1) + digit;
                    _unique_0 = false;
                }
                else
                {
                    Operations += digit;
                }

                _unique_0 = false;
            }

            _operateur = false;
            

            DataTable dt = new DataTable();
            string res = Operations.Replace(',' , '.');

            var result = dt.Compute(res, null);
            Resultat = double.Parse(result.ToString() ?? "0");
            return true;
        }

        public void Reset()
        {
            Operations = "0";
            _unique_0 = true;
            _operateur = false;
            Resultat = 0;
        }

        public void AddOperateur(Operation operateur)
        {
            _unique_0 = false;

            if (_operateur)
            {
                // On remplace l'ancien opérateur par le nouveau
                Operations = Operations.Substring(0, Operations.Length - 1) + operation[operateur];
            }
            else 
            {
                // Ajouter le nouvel opérateur
               Operations += operation[operateur];
            }

            _operateur = true;

        }

        public void Equal()
        {
            Operations = Resultat.ToString();
            _operateur = false;

            if (Resultat == 0)
            {
                _unique_0 = true;
            }
            else
            {
                _unique_0 = false;
            }


        }
    }
}
