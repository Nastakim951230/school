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
    /// Логика взаимодействия для AddEslegi.xaml
    /// </summary>
    public partial class AddEslegi
    {
        Service ser;
        public AddEslegi(Service ser)
        {
            InitializeComponent();
            this.ser = ser;
            Title.Text ="Название услуги: "+ ser.Title +" | "+ "Длительность услуги: " + ser.time;
            List<Client> clients=ClassPage.Base.BD.Client.ToList();
            for (int i = 0; i < clients.Count; i++)  // цикл для записи в выпадающий список всех пород котов из БД
            {
                FIOClient.Items.Add(clients[i].FIO);
            }

        }
    }
}
