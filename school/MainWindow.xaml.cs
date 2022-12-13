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

namespace school
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            ClassPage.Base.BD=new BDBase();
            PanelDann.Navigate(new Page.ListOfServices());
            ClassPage.FrameNavigate.perehod = PanelDann;
           
        }

        private void Yslygi_Click(object sender, RoutedEventArgs e)
        {
            
            PanelDann.Navigate(new Page.ListOfServices());
            
        }

        private void admin_Click(object sender, RoutedEventArgs e)
        {
            Page.Admin windowPerson = new Page.Admin();  // создание объекта окна
            windowPerson.ShowDialog();
            ClassPage.FrameNavigate.perehod.Navigate(new Page.ListOfServices());
        }

      
    }
}
