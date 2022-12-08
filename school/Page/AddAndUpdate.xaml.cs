using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddAndUpdate.xaml
    /// </summary>
    public partial class AddAndUpdate 
    {
        string path;
        bool flagUpdate;
        Service ser;
       
        public AddAndUpdate()
        {
            InitializeComponent();
        }
        public AddAndUpdate(Service ser)
        {
            InitializeComponent();
            flagUpdate = true;
            this.ser = ser;
            NameYsl.Text = ser.Title;
            double skid = ser.Discount.Value * 100;
            Skidka.Text = Convert.ToString(skid);
            double pric = Convert.ToDouble(ser.Cost);
            PriceYsl.Text=Convert.ToString(pric);
            int tim = ser.DurationInSeconds / 60;
            TimeYsl.Text=Convert.ToString(tim);
            Opisanie.Text = ser.Description;
            IdServ.Text = Convert.ToString(ser.ID);

            // вывод картинки
            if (ser.MainImagePath != null)
            {
                BitmapImage img = new BitmapImage(new Uri(ser.MainImagePath, UriKind.RelativeOrAbsolute));
                ImageServis.Source = img;
            }
            UpdatePhoto.Visibility = Visibility.Visible;
            IdServ.Visibility=Visibility.Visible;

        }

        public bool NameServ(string a)
        {
           List<Service> servers=ClassPage.Base.BD.Service.Where(x=>x.Title==a).ToList();
           if(servers.Count>0)
            {
                MessageBox.Show("Данная услуга уже существует", "Ошибка", MessageBoxButton.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool SkidkaSer(string a)
        {
            try
            {
                int price = Convert.ToInt32(a);
                return true;

            }
            catch
            {
                MessageBox.Show("Напишите количество скидки цифрами", "Ошибка", MessageBoxButton.OK);
                return false;
            }
        }

        public bool PriceSer(string a)
        {
            try
            {
                int price = Convert.ToInt32(a);           
                    return true;
                
            }
            catch
            {
                MessageBox.Show("Напишите количество цены цифрами", "Ошибка", MessageBoxButton.OK);
                return false;
            }
        }
        public bool TimeSer(string a)
        {
            try
            {
                int time=Convert.ToInt32(a);
                if((time>0)&&(time<=240))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Время не должно быть отрицательное, а так же не должно быть больше 4 часов", "Ошибка", MessageBoxButton.OK);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Напишите количество времени цифрами", "Ошибка", MessageBoxButton.OK);
                return false;
            }
        }
        private void addPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                 OpenFileDialog OFD = new OpenFileDialog();  // создаем объект диалогового окна
            OFD.ShowDialog();  // открываем диалоговое окно
            path = OFD.FileName;  // извлекаем полный путь к картинке
            string[] arrayPath = path.Split('\\');  // разделяем путь к картинке в массив
            path = "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1];  // записываем в бд путь, начиная с имени папки
            BitmapImage image = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            ImageServis.Source = image;
            }
           catch
            {
                MessageBox.Show("Что то пошло не так", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void UpdatePhoto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add_Click(object sender, RoutedEventArgs e)
        {

            if (flagUpdate == false)
            {
                if (NameYsl.Text == "" || PriceYsl.Text == "" || TimeYsl.Text == "" || Skidka.Text == "" || path == null)
                {
                    MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK);
                }
                else
                {
                    if (NameServ(NameYsl.Text))
                    {
                        if (TimeSer(TimeYsl.Text))
                        {
                            if (PriceSer(PriceYsl.Text))
                            {
                                if (SkidkaSer(Skidka.Text))
                                {
                                    ser = new Service();
                                    ser.Title = NameYsl.Text;
                                    ser.Cost = Convert.ToInt32(PriceYsl.Text);
                                    double skidk = Convert.ToDouble(Skidka.Text) / 100;
                                    ser.Discount = skidk;
                                    int timeSecond = Convert.ToInt32(TimeYsl.Text) * 60;
                                    ser.DurationInSeconds = timeSecond;
                                    ser.MainImagePath = path;
                                    if (Opisanie.Text == "")
                                    {
                                        ser.Description = null;
                                    }
                                    else
                                    {
                                        ser.Description = Opisanie.Text;
                                    }

                                    ClassPage.Base.BD.Service.Add(ser);

                                    ClassPage.Base.BD.SaveChanges();
                                    MessageBox.Show("Информация добавлена");

                                    ClassPage.FrameNavigate.perehod.Navigate(new Page.ListOfServices());
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                if (NameYsl.Text == "" || PriceYsl.Text == "" || TimeYsl.Text == "" || Skidka.Text == "")
                {
                    MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK);
                }
                else
                {
                    if (TimeSer(TimeYsl.Text))
                    {
                        if (PriceSer(PriceYsl.Text))
                        {
                            if (SkidkaSer(Skidka.Text))
                            {
                                ser.Title = NameYsl.Text;
                                ser.Cost = Convert.ToInt32(PriceYsl.Text);
                                double skidk = Convert.ToDouble(Skidka.Text) / 100;
                                ser.Discount = skidk;
                                int timeSecond = Convert.ToInt32(TimeYsl.Text) * 60;
                                ser.DurationInSeconds = timeSecond;
                                if (path == null)
                                {
                                    path = ser.MainImagePath;
                                }
                                ser.MainImagePath = path;
                                if (Opisanie.Text == "")
                                {
                                    ser.Description = null;
                                }
                                else
                                {
                                    ser.Description = Opisanie.Text;
                                }
                                ClassPage.Base.BD.SaveChanges();
                                MessageBox.Show("Информация добавлена");

                                ClassPage.FrameNavigate.perehod.Navigate(new Page.ListOfServices());
                            }
                        }

                    }
                }
            }
            
        }
    }
}
