using System.Globalization;

namespace CalculatorCet
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("tr-TR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("tr-TR");
        }

        double FirstNumber = 0;
        double memory = 0;
        bool isFirstNumberAfterOperator = true;
        Operator PreviousOperator = Operator.None;

        private void SubtractButton_Clicked(object sender, EventArgs e)
        {
            DoCalculation();
            PreviousOperator = Operator.Subtract;
        }

        private void MultiplyButton_Clicked(object sender, EventArgs e)
        {
            DoCalculation();
            PreviousOperator = Operator.Multiply;
        }

        private void DivideButton_Clicked(object sender, EventArgs e)
        {
            DoCalculation();
            PreviousOperator = Operator.Divide;
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            DoCalculation();
            PreviousOperator = Operator.Add;
        }

        private void DoCalculation()
        {
            if (double.TryParse(Display.Text, out double currentNumber))
            {
                switch (PreviousOperator)
                {
                    case Operator.None:
                        FirstNumber = currentNumber;
                        break;
                    case Operator.Add:
                        FirstNumber += currentNumber;
                        break;
                    case Operator.Subtract:
                        FirstNumber -= currentNumber;
                        break;
                    case Operator.Multiply:
                        FirstNumber *= currentNumber;
                        break;
                    case Operator.Divide:
                        if (currentNumber != 0)
                            FirstNumber /= currentNumber;
                        else
                            Display.Text = "Error";
                        break;
                }
                isFirstNumberAfterOperator = true;
                Display.Text = FirstNumber.ToString();
            }
        }

        private void Digit_Clicked(object sender, EventArgs e)
        {
            Button digitButton = sender as Button;
            if (isFirstNumberAfterOperator)
            {
                Display.Text = digitButton.Text;
                isFirstNumberAfterOperator = false;
            }
            else
            {
                Display.Text += digitButton.Text;
            }
        }

        private void DecimalButton_Clicked(object sender, EventArgs e)
        {
            if (!Display.Text.Contains(","))
            {
                Display.Text += ",";
            }
        }

        private void Display_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;


            string validCharacters = "0123456789+-*/.,";
            string newText = string.Concat(e.NewTextValue.Where(c => validCharacters.Contains(c)));


            if (e.OldTextValue == "0" && validCharacters.Contains(newText))
            {
                newText = newText.TrimStart('0');
            }


            if (newText.Contains(".") && newText.Contains(","))
            {
                newText = newText.Replace(",", "");
            }


            if (newText != e.NewTextValue)
            {
                entry.Text = newText;
            }
        }



        private void EqualButton_Clicked(object sender, EventArgs e)
        {
            DoCalculation();
            PreviousOperator = Operator.None;
        }

        private void CEButton_Clicked(object sender, EventArgs e)
        {
            Display.Text = "0";
            isFirstNumberAfterOperator = true;
        }

        private void CButton_Clicked(object sender, EventArgs e)
        {
            Display.Text = "0";
            FirstNumber = 0;
            PreviousOperator = Operator.None;
            isFirstNumberAfterOperator = true;
        }

        private void BackspaceButton_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Display.Text))
            {
                Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
            }

            if (string.IsNullOrEmpty(Display.Text))
            {
                Display.Text = "0";
            }
        }

        private void PercentButton_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(Display.Text, out double value))
            {
                Display.Text = (value / 100).ToString();
            }
            else
            {
                Display.Text = "Error";
            }
        }

        private void SqrtButton_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(Display.Text, out double value))
            {
                if (value < 0)
                {
                    Display.Text = "Error";
                }
                else
                {
                    Display.Text = Math.Sqrt(value).ToString();
                }
            }
            else
            {
                Display.Text = "Error";
            }
        }

        private void SquareButton_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(Display.Text, out double value))
            {
                Display.Text = Math.Pow(value, 2).ToString();
            }
            else
            {
                Display.Text = "Error";
            }
        }

        private void OneOverXButton_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(Display.Text, out double value))
            {
                if (value == 0)
                {
                    Display.Text = "Error";
                }
                else
                {
                    Display.Text = (1 / value).ToString();
                }
            }
            else
            {
                Display.Text = "Error";
            }
        }

        private void PlusMinusButton_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(Display.Text, out double value))
            {
                Display.Text = (-value).ToString();
            }
        }

        private void MCButton_Clicked(object sender, EventArgs e)
        {
            memory = 0;
        }

        private void MRButton_Clicked(object sender, EventArgs e)
        {
            Display.Text = memory.ToString();
        }

        private void MPlusButton_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(Display.Text, out double value))
            {
                memory += value;
            }
        }

        private void MMinusButton_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(Display.Text, out double value))
            {
                memory -= value;
            }
        }

        private void MSButton_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(Display.Text, out double value))
            {
                memory = value;
            }
        }
    }


}