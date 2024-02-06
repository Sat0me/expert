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

namespace opcserver
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Object<T>
        {
            public static string Accessibility;
            public static T Value;


            public Object(string Acc, T Val)
            {
                Accessibility = Acc;
                Value = Val;
            }
            public void print()
            {
                MessageBox.Show(Value.ToString());
            }

        }
        public MainWindow()
        {
            InitializeComponent();
            var d = new Object<string>("RW", "4");
            d.print();
        }

    }
}
