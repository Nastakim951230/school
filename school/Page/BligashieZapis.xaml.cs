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
            DateTime date = DateTime.Today;
            DateTime data = date.AddDays(2);
            List<ClientService> ser = ClassPage.Base.BD.ClientService.Where(x => x.StartTime >= DateTime.Today && x.StartTime < data).ToList();
            Zapisi.ItemsSource = ser.OrderBy(x => x.StartTime).ToList();
        }
    }
}
