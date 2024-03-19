using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Calc = CalculatriceLibrary.Calculatrice;

namespace Calculatrice.ViewModel
{
    internal class VMCalculatrice : INotifyPropertyChanged
    {

        private Calc calculatrice = new Calc();

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Proprietés connectées aux deux Labels de l’interface graphique
        public double Resultat { get => calculatrice.Resultat;}
        public string Operations { get => calculatrice.Operations;}

        // Propriétes connectées aux boutons de la calculatrice
        public ICommand AddDigitCommand { get; set; }
        public ICommand AddOperatorCommand { get; set; }  

        public ICommand ResetCommand { get; set; }

        public ICommand EqualCommand { get; set; }

        public VMCalculatrice()
        {
            AddDigitCommand = new Command<int>(AddDigit);
            AddOperatorCommand = new Command<string>(AddOperator);
            ResetCommand = new Command(Reset);
            EqualCommand = new Command(Equal);
        }




        private void AddDigit(int digit) {

            bool operationChanged = calculatrice.AddDigit(digit);

            if (operationChanged)
            {
                NotifyPropertyChanged(nameof(Resultat));
                NotifyPropertyChanged(nameof(Operations));
            }
        }

        // Le symbole ‘@’ devant le nom du paramètre operator permet d’utiliser des mots clés comme nom de variable
        private void AddOperator(string @operator)
        {
            Calc.Operation operationEnum;
            switch (@operator)
            {
                case "+":
                    operationEnum = Calc.Operation.ADDITIONNER;
                    break;
                case "-":
                    operationEnum = Calc.Operation.SOUSTRAIRE;
                    break;
                case "x":
                    operationEnum = Calc.Operation.MULTIPLIER;
                    break;
                case "/":
                    operationEnum = Calc.Operation.DIVISER;
                    break;

                default:
                    throw new ArgumentException($"L'opérateur {@operator} n'est pas reconnu.");
            }

            calculatrice.AddOperateur(operationEnum);
            NotifyPropertyChanged(nameof(Operations));
        }

        private void Reset()
        {
            calculatrice.Reset();
            NotifyPropertyChanged(nameof(Resultat));
            NotifyPropertyChanged(nameof(Operations));

        }

        private void Equal()
        {
            calculatrice.Equal();
            NotifyPropertyChanged(nameof(Operations));

        }


    }


            public class ConvertStringToInt : IValueConverter
        {
            public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
            {
                if (value is string str && int.TryParse(str, culture, out int res))
                {
                    return res;
                }
                return null;
            }
            public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            {
                if (value is int res)
                {
                    return res.ToString(culture);
                }
                return null;
            }
        }
}
