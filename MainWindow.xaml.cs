using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;
        public MainWindow()
        {
            InitializeComponent();

            //create event handlers
            acButton.Click += AcButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse((string)resultLabel.Content, out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Subtract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                    default:

                        break;
                }
                resultLabel.Content = result.ToString();
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;

            // tryparse will evaluate whether its valid entry
            if (double.TryParse((string)resultLabel.Content, out tempNumber))
            {
                tempNumber = tempNumber / 100 ;
                if (lastNumber != 0)
                {
                    tempNumber *= lastNumber;
                    resultLabel.Content = tempNumber.ToString();

                }
            }
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            
            // needs to convert from object to string to double, if its true, value will be stored in lastNumber
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender == multiplicationButton)
                selectedOperator = SelectedOperator.Multiplication;
            if (sender == divisionButton)
                selectedOperator = SelectedOperator.Division;
            if (sender == subtractionButton)
                selectedOperator = SelectedOperator.Subtraction;
            if (sender == additionButton)
                selectedOperator = SelectedOperator.Addition;
            
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {

                resultLabel.Content = "0";
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            //sender carries the information that triggers the event handlers

            //refactoring if statements
            int selectedValue = int.Parse((sender as Button).Content.ToString());
            
            /*int selectedValue = 0;

            //sender carries the information that triggers the event handlers
            if (sender == zeroButton)
                selectedValue = 0;
            if (sender == oneButton)
                selectedValue = 1;
            if (sender == twoButton)
                selectedValue = 2;
            if (sender == threeButton)
                selectedValue = 3;
            if (sender == fourButton)
                selectedValue = 4;
            if (sender == fiveButton)
                selectedValue = 5;
            if (sender == sixButton)
                selectedValue = 6;
            if (sender == sevenButton)
                selectedValue = 7;
            if (sender == eightButton)
                selectedValue = 8;
            if (sender == nineButton)
                selectedValue = 9;*/


            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = $"{selectedValue}";

            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}"; 
            }
        }

        private void dotButton_Click(object sender, RoutedEventArgs e)
        {
            if (resultLabel.Content.ToString().Contains("."))
            {
                //do nothing
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}.";
            }

        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            
            resultLabel.Content = "0";
            result = 0;
            lastNumber = 0;
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
    }

    public class SimpleMath
    {
        public static double Add(double n1, double n2)
        {
            return n1 + n2;
        }

        public static double Subtract(double n1, double n2)
        {
            return n1 - n2;
        }

        public static double Multiply(double n1, double n2)
        {
            return n1 * n2;
        }

        public static double Divide(double n1, double n2)
        {
            if (n2 == 0)
            {
                MessageBox.Show("You cannot divide by zero", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return n1 / n2;
        }
    }

}
