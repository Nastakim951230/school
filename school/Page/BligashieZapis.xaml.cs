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
    /// Логика взаимодействия для BligashieZapis.xaml
    /// </summary>
    public partial class BligashieZapis 
    {
        public BligashieZapis()
        {
            InitializeComponent();
            Zapisi.ItemsSource = ClassPage.Base.BD.ClientService.ToList();
        }
    }
}
