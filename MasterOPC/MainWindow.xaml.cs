using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MasterOPC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string NameSender = "text";

        public class Lub
        {
            public string Id;
            public int val;
        }


        public class Node
        {
            public string Name { get; set; }
            public ObservableCollection<Node> Nodes { get; set; }
            public int Id { get; set; } 

         }

        


        public MainWindow()
        {
            InitializeComponent();
            

            ObservableCollection<Node> nodes = new ObservableCollection<Node>()
            {
                new Node
                {
                    Name="Server",
                    Id= 1,
                    Nodes=new ObservableCollection<Node>()
                    {
                        new Node {Name="Group1", Id=2},
                        new Node {Name="Group2", Id=2},
                        new Node
                        {
                            Name="Group3", Id=2,
                            Nodes= new ObservableCollection<Node>{
                                new Node {Name="Tag1", Id=3}
                            }
                        }
                    }
                }
            };
            this.DataContext = this;
           
            TreeView1.ItemsSource = nodes;
            
         
        }

        


        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TC.Visibility= Visibility.Visible;
            var TagSender = (sender as TextBlock).Tag;
            NameSender = (sender as TextBlock).Text;
            
            TB_Name1.Text = "Сервер: " + NameSender;
            TB_Name2.Text = "Группа - " + NameSender;
            TB_Name3.Text = "Тег: " + NameSender; 
            



            switch (TagSender)
            {
                case 1:
                    TC.SelectedIndex = 0; 
                    break;
                case 2:
                    TC.SelectedIndex = 1;
                    break;
                case 3:
                    TC.SelectedIndex= 2;
                    break;
            }
        }
    }
}

