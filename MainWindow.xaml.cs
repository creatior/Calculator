using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] operations_signs = new string[] {"+","-","*","/"};
        public MainWindow()
        {
            InitializeComponent();

            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Button)
                {
                    ((Button)element).Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = (string)((Button)e.OriginalSource).Content;
            if (MainDisplay.Text.Contains("="))
                ContinueCalculating(str);
            if (str == "AC")
            {
                MainDisplay.Text = "";
            }
            if (str != "=" && str != "AC")
            {
                if (operations_signs.Any(sign => str == sign) && operations_signs.Any(sign => MainDisplay.Text.EndsWith(sign)))
                {
                    MainDisplay.Text = MainDisplay.Text.Remove(MainDisplay.Text.Length - 1);
                }
                MainDisplay.Text += str;
            }
            else if (str == "=")
            {
                Calculate();
            }
        }

        private void ContinueCalculating(string symbol)
        {
            if (!operations_signs.Any(sign => symbol == sign))
            {
                MainDisplay.Text = "";
            }
            else
            {
                string[] temp = MainDisplay.Text.Split('=');
                MainDisplay.Text = "";
                MainDisplay.Text = temp[1];
            }
        }

        private void Calculate()
        {
            Calculation result = new Calculation();
            if (operations_signs.Any(sign => MainDisplay.Text.Substring(0, 1) == sign))
            {
                MainDisplay.Text = MainDisplay.Text.Insert(0, "0");
            }
            MainDisplay.Text += $"={result.CalculateRPN(MainDisplay.Text)}";
        }
    }
}
