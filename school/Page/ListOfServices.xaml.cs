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
    /// Логика взаимодействия для ListOfServices.xaml
    /// </summary>
    public partial class ListOfServices 
    {
        public ListOfServices()
        {
            InitializeComponent();
            ListYslovie.ItemsSource = ClassPage.Base.BD.Service.ToList();
            Sortirovka.SelectedIndex = 0;
            Filtratsia.SelectedIndex = 0;
        }

        private void SearchName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SearchOpisanie_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Filtratsia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Sortirovka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
