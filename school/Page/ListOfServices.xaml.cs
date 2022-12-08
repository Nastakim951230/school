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
        public static string admin;
        public ListOfServices()
        {
            InitializeComponent();
            ListYslovie.ItemsSource = ClassPage.Base.BD.Service.ToList();
            Sortirovka.SelectedIndex = 0;
            Filtratsia.SelectedIndex = 0;
            kolvo.Text = ClassPage.Base.BD.Service.ToList().Count +"/"+ ClassPage.Base.BD.Service.ToList().Count;
            if(admin=="0000")
            {
                Service.btn_admin=Visibility.Visible;
                add_service.Visibility = Visibility.Visible;
            }
            else
            {
                Service.btn_admin=Visibility.Collapsed;
                add_service.Visibility = Visibility.Collapsed;
            }
        }
        void Filter()
        {
            List<Service> services = new List<Service>();
            services=ClassPage.Base.BD.Service.ToList();
            //поиск название

            if (!string.IsNullOrWhiteSpace(SearchName.Text))  // если строка не пустая и если она не состоит из пробелов
            {
                services = services.Where(x => x.Title.ToLower().Contains(SearchName.Text.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(SearchOpisanie.Text))  // если строка не пустая и если она не состоит из пробелов
            {

                List<Service> trl = services.Where(x => x.Description != null).ToList();
                if (trl.Count > 0)
                {
                    foreach (Service tr in trl)
                    {
                        services = services.Where(x => x.Description.ToLower().Contains(SearchOpisanie.Text.ToLower())).ToList();
                    }
                }
                else
                {
                    MessageBox.Show("нет описания");
                    SearchOpisanie.Text = "";
                }

            }
            //Фильтрация

            switch (Filtratsia.SelectedIndex)
            {
                case 0:
                    {
                        services = services.ToList();
                    }
                    break;
                    case 1:
                    {
                        List<Service> ser = services.Where(x => x.pric == "").ToList();
                        if (ser.Count > 0)
                        {
                            foreach (Service s in ser)
                            {
                                services = services.Where(x =>((x.Discount>=0 )&&(x.Discount*100<5))).ToList();

                            }
                        }
                        
                    }
                    break ;
                    case 2:
                    {
                        List<Service> ser = services.Where(x => x.pric == "").ToList();
                        if (ser.Count > 0)
                        {
                            foreach (Service s in ser)
                            {
                                services = services.Where(x => ((x.Discount*100 >= 5) && (x.Discount * 100 < 15))).ToList();

                            }
                        }
                    }
                    break ;
                case 3:
                    {
                        List<Service> ser = services.Where(x => x.pric == "").ToList();
                        if (ser.Count > 0)
                        {
                            foreach (Service s in ser)
                            {
                                services = services.Where(x => ((x.Discount * 100 >= 15) && (x.Discount * 100 < 30))).ToList();

                            }
                        }
                    }
                    break;
                case 4:
                    {
                        List<Service> ser = services.Where(x => x.pric == "").ToList();
                        if (ser.Count > 0)
                        {
                            foreach (Service s in ser)
                            {
                                services = services.Where(x => ((x.Discount * 100 >= 30) && (x.Discount * 100 < 70))).ToList();

                            }
                        }
                    }
                    break;
                case 5:
                    {
                        List<Service> ser = services.Where(x => x.pric == "").ToList();
                        if (ser.Count > 0)
                        {
                            foreach (Service s in ser)
                            {
                                services = services.Where(x => ((x.Discount * 100 >= 70) && (x.Discount * 100 < 100))).ToList();

                            }
                        }
                    }
                    break;
            }

            //сортировка

            switch (Sortirovka.SelectedIndex)
            {
                case 0:
                    {
                        services.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                    }
                    break;
                case 1:
                    {
                        services.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                        services.Reverse();
                    }
                    break;
            }

            ListYslovie.ItemsSource = services;
            if (services.Count == 0)
            {
                MessageBox.Show("нет записей");
                kolvo.Text = ClassPage.Base.BD.Service.ToList().Count + "/" + ClassPage.Base.BD.Service.ToList().Count;
                SearchName.Text = "";
                SearchOpisanie.Text = "";
                Sortirovka.SelectedIndex = 0;
                Filtratsia.SelectedIndex = 0;
               
            }
            kolvo.Text = services.Count+"/" + ClassPage.Base.BD.Service.ToList().Count;

        }
        private void SearchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void SearchOpisanie_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void Filtratsia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Sortirovka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Delet_Click(object sender, RoutedEventArgs e)
        {
            Button btn= (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            Service serv = ClassPage.Base.BD.Service.FirstOrDefault(x=>x.ID==id);
            List<ClientService> clientservices = ClassPage.Base.BD.ClientService.Where(x => x.ServiceID == serv.ID).ToList();
            if(clientservices.Count>0)
            {
                MessageBox.Show("Данную услугу нельзя удалить");
            }
            else
            {
                ClassPage.Base.BD.Service.Remove(serv);
                ClassPage.Base.BD.SaveChanges();
                ClassPage.FrameNavigate.perehod.Navigate(new Page.ListOfServices());
            }

        }

        private void add_service_Click(object sender, RoutedEventArgs e)
        {
            ClassPage.FrameNavigate.perehod.Navigate(new Page.AddAndUpdate());
        }
    }
}
