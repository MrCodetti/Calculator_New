using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void BtnNumbers(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text == "0")
            {
                txtDisplay.Text = "";
            }
            txtDisplay.Text += ((Button)sender).Content;
        }



        private void btnOperation_Click(object sender, RoutedEventArgs e)
        {
            if (previousOperation == Operation.None)
            {
                currentOperation = BtnOperation((string)(sender as Button).Content);
                previousOperation = currentOperation;
                txtDisplay.Text += (sender as Button).Content;
            }
            else if (previousOperation != Operation.None)
            {
                if (txtDisplay.Text.Substring(txtDisplay.Text.Length - 1, 1) != "+" ||
                    txtDisplay.Text.Substring(txtDisplay.Text.Length - 1, 1) != "-" ||
                    txtDisplay.Text.Substring(txtDisplay.Text.Length - 1, 1) != "x" ||
                    txtDisplay.Text.Substring(txtDisplay.Text.Length - 1, 1) != "/")
                {
                    txtDisplay.Text += (sender as Button).Content;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }



        private Operation BtnOperation(string op)
        {
            if (op == "+")
            {
                return Operation.Plus;
            }
            else if (op == "-")
            {
                return Operation.Minus;
            }
            else if (op == "x")
            {
                return Operation.Multi;
            }
            else if (op == "/")
            {
                return Operation.Durch;
            }
            else
            {
                return Operation.None;
            }
        }



        private void Berechnung(Operation previesOperation)
        {
            List<double> lstNums = null;
            double sum = 0;

            switch (previesOperation)
            {
                case Operation.Plus:
                    lstNums = txtDisplay.Text.Split('+').Select(double.Parse).ToList();
                    foreach (var item in lstNums)
                    {
                        sum += item;
                    }
                    txtDisplay.Text = sum.ToString();
                    break;
                case Operation.Minus:
                    lstNums = txtDisplay.Text.Split('-').Select(double.Parse).ToList();
                    sum = lstNums[0];
                    for (int i = 1; i < lstNums.Count; i++)
                    {
                        sum = sum - lstNums[i];
                    }
                    txtDisplay.Text = sum.ToString();
                    break;
                case Operation.Multi:
                    lstNums = txtDisplay.Text.Split('x').Select(double.Parse).ToList();
                    sum = lstNums[0];
                    for (int i = 1; i < lstNums.Count; i++)
                    {
                        sum = sum * lstNums[i];
                    }
                    txtDisplay.Text = sum.ToString();
                    break;
                case Operation.Durch:
                    lstNums = txtDisplay.Text.Split('/').Select(double.Parse).ToList();
                    sum = lstNums[0];
                    for (int i = 1; i < lstNums.Count; i++)
                    {
                        sum = sum / lstNums[i];
                    }
                    txtDisplay.Text = sum.ToString();
                    break;


            }
        }

        enum Operation
        {
            Plus,
            Minus,
            Multi,
            Durch,
            None
        }

        Operation previousOperation = Operation.None;
        Operation currentOperation = Operation.None;

        private void btnResult_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text == "0") return;
            if (previousOperation != Operation.None)
            {
                Berechnung(previousOperation);
            }
            previousOperation = Operation.None;
        }

        private void btnCE_Click(object sender, RoutedEventArgs e)
        {

            txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            previousOperation = Operation.None;
            txtDisplay.Text = "0";
        }
    }
}
