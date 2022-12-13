﻿using System;
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
using System.Windows.Threading;

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
            Title.Text ="Название услуги: "+ ser.Title +" | "+ "Длительность услуги: " + ser.time +" минут";
            List<Client> clients=ClassPage.Base.BD.Client.ToList();
            for (int i = 0; i < clients.Count; i++)  // цикл для записи в выпадающий список всех пород котов из БД
            {
                FIOClient.Items.Add(clients[i].FIO);
            }

            hh.Text = DateTime.Now.ToString("HH");
            mm.Text = DateTime.Now.ToString("mm");
            int HH = Convert.ToInt32(DateTime.Now.ToString("HH"));
            int MM = Convert.ToInt32(DateTime.Now.ToString("mm"));
            DateTime date = new DateTime(2000,2,2 ,HH, MM, 0);
            DateTime data= date.AddMinutes(Convert.ToInt32(ser.time));
            TimeEnd.Text = data.ToShortTimeString();


        }

        void TIMER()
        {
           try
            {
                int h = Convert.ToInt32(hh.Text);
                int m = Convert.ToInt32(mm.Text);
                if((h<24)&&(m<60))
                {
                 
                    int HH = Convert.ToInt32(h);
                    int MM = Convert.ToInt32(m);
                    DateTime date = new DateTime(2000, 2, 2, HH, MM, 0);
                    DateTime data = date.AddMinutes(Convert.ToInt32(ser.time));
                    TimeEnd.Text = data.ToShortTimeString();
                }
                else
                {
                    MessageBox.Show("Введите время правильно", "Ошибка", MessageBoxButton.OK);

                }
            }
            catch
            {
             

            }
        }
        private void hh_TextChanged(object sender, TextChangedEventArgs e)
        {
            TIMER();
        }

        private void mm_TextChanged(object sender, TextChangedEventArgs e)
        {
            TIMER();
        }
    }
}
