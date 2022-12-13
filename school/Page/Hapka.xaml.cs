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

namespace school.Page
{
    /// <summary>
    /// Логика взаимодействия для Hapka.xaml
    /// </summary>
    public partial class Hapka 
    {
        public static string kod;
        public Hapka()
        {
            InitializeComponent();
            if(kod=="0000")
            {
                Zap.Visibility = Visibility.Visible;
            }
            else
            {
                Zap.Visibility = Visibility.Collapsed;
            }
        }
        private void Yslygi_Click(object sender, RoutedEventArgs e)
        {

            ClassPage.FrameNavigate.perehod.Navigate(new Page.ListOfServices());

        }

        private void admin_Click(object sender, RoutedEventArgs e)
        {
            Page.Admin windowPerson = new Page.Admin();  // создание объекта окна
            windowPerson.ShowDialog();

            ClassPage.FrameNavigate.hapka.Navigate(new Page.Hapka());
            ClassPage.FrameNavigate.perehod.Navigate(new Page.ListOfServices());
        }

        private void Zapisi_Click(object sender, RoutedEventArgs e)
        {
            ClassPage.FrameNavigate.perehod.Navigate(new Page.BligashieZapis());
        }
    }
}
