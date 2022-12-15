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
        bool flagUpdatePhoto;
        Service ser;
        ServicePhoto servicephoto;
       
        public AddAndUpdate()
        {
            InitializeComponent();
            addPhotos.Visibility = Visibility.Collapsed;
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
                List<ServicePhoto> photos = ClassPage.Base.BD.ServicePhoto.Where(x=>x.ServiceID == ser.ID).ToList();
                if(photos.Count==0)
                {
                    servicephoto = new ServicePhoto();
                    servicephoto.ServiceID = ser.ID;
                    servicephoto.PhotoPath = ser.MainImagePath;
                    ClassPage.Base.BD.ServicePhoto.Add(servicephoto);
                }

            BitmapImage image = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            ImageServis.Source = image;
            }
           catch
            {
                MessageBox.Show("Что то пошло не так", "Ошибка", MessageBoxButton.OK);
            }
        }
        int n = 0;
        private void UpdatePhoto_Click(object sender, RoutedEventArgs e)
        {
            List<ServicePhoto> servicePhoto = ClassPage.Base.BD.ServicePhoto.Where(x => x.ServiceID == ser.ID).ToList();
            if (servicePhoto.Count > 1)
            {
                
                BitmapImage img = new BitmapImage(new Uri(servicePhoto[n].PhotoPath, UriKind.RelativeOrAbsolute));
                ImageServis.Source = img;
                
                Next.Visibility = Visibility.Visible;
                Bakc.Visibility = Visibility.Visible;
                sohranitPhoto.Visibility = Visibility.Visible;
                addPhoto.Visibility = Visibility.Collapsed;
                UpdatePhoto.Visibility = Visibility.Collapsed;
                nazad.Visibility = Visibility.Visible;
                addPhotos.Visibility = Visibility.Collapsed;
                DeletPhoto.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Нет дополнительных фотографий", "Ошибка", MessageBoxButton.OK);
            }
           
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
                                    servicephoto = new ServicePhoto();
                                        servicephoto.ServiceID = ser.ID;
                                        servicephoto.PhotoPath = path;
                                        ClassPage.Base.BD.ServicePhoto.Add(servicephoto);

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
                                if((path != null)&&(flagUpdatePhoto==false))
                                {
                                    servicephoto = new ServicePhoto();
                                    servicephoto.ServiceID = ser.ID;
                                    servicephoto.PhotoPath = path;
                                    ClassPage.Base.BD.ServicePhoto.Add(servicephoto);

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
        
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            List<ServicePhoto> servicePhoto = ClassPage.Base.BD.ServicePhoto.Where(x => x.ServiceID == ser.ID).ToList();
            
            n++;
            if (Bakc.IsEnabled == false)
            {
                Bakc.IsEnabled = true;
            }
            if (servicePhoto != null)  // если объект не пустой, начинает переводить байтовый массив в изображение
            {
               
                BitmapImage img = new BitmapImage(new Uri(servicePhoto[n].PhotoPath, UriKind.RelativeOrAbsolute));
                ImageServis.Source = img;
            }
            if (n == servicePhoto.Count - 1)
            {
                Next.IsEnabled = false;
            }
        }
    

        private void Bakc_Click(object sender, RoutedEventArgs e)
        {
            List<ServicePhoto> u = ClassPage.Base.BD.ServicePhoto.Where(x => x.ServiceID == ser.ID).ToList();
          
            n--;
            if (Next.IsEnabled == false)
            {
                Next.IsEnabled = true;
            }
            if (u != null)  // если объект не пустой, начинает переводить байтовый массив в изображение
            {

                BitmapImage img = new BitmapImage(new Uri(u[n].PhotoPath, UriKind.RelativeOrAbsolute));
                ImageServis.Source = img;
            }
            if (n == 0)
            {
                Bakc.IsEnabled = false;
            }
        }

        private void sohranitPhoto_Click(object sender, RoutedEventArgs e)
        {
            flagUpdatePhoto=true;
            List<ServicePhoto> u = ClassPage.Base.BD.ServicePhoto.Where(x => x.ServiceID == ser.ID).ToList();
            ser.MainImagePath = u[n].PhotoPath;
            ClassPage.Base.BD.SaveChanges();
            MessageBox.Show("Фотография изменена");
            Next.Visibility = Visibility.Collapsed;
            Bakc.Visibility = Visibility.Collapsed;
            sohranitPhoto.Visibility = Visibility.Collapsed;
            addPhoto.Visibility = Visibility.Visible;
            nazad.Visibility = Visibility.Collapsed;
            UpdatePhoto.Visibility = Visibility.Visible;
            addPhotos.Visibility = Visibility.Visible;
            DeletPhoto.Visibility = Visibility.Collapsed;
        }

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            flagUpdatePhoto=false;
            Next.Visibility = Visibility.Collapsed;
            Bakc.Visibility = Visibility.Collapsed;
            sohranitPhoto.Visibility = Visibility.Collapsed;
            addPhoto.Visibility = Visibility.Visible;
            nazad.Visibility = Visibility.Collapsed;
            UpdatePhoto.Visibility = Visibility.Visible;
            addPhotos.Visibility = Visibility.Visible;
            DeletPhoto.Visibility = Visibility.Collapsed;
            if (ser.MainImagePath != null)
            {
                BitmapImage img = new BitmapImage(new Uri(ser.MainImagePath, UriKind.RelativeOrAbsolute));
                ImageServis.Source = img;
            }

        }
        private void addPhotos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно
                OFD.Multiselect = true;  // открытие диалогового окна с возможностью выбора нескольких элементов
                if (OFD.ShowDialog() == true)  // пока диалоговое окно открыто, будет в цикле записывать каждое выбранное изображение в БД
                {
                    foreach (string file in OFD.FileNames)  // цикл организован по именам выбранных файлов
                    {
                        ServicePhoto u =new ServicePhoto();
                        u.ServiceID = ser.ID;
                        path = file;  // извлекаем полный путь к картинке
                        string[] arrayPath = path.Split('\\');  // разделяем путь к картинке в массив
                        path = "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1];  // записываем в бд путь, начиная с имени папки
                        u.PhotoPath = path;
                        ClassPage.Base.BD.ServicePhoto.Add(u);
                       
                    }
                    ClassPage.Base.BD.SaveChanges();
                    MessageBox.Show("Фото добавлены");
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void DeletPhoto_Click(object sender, RoutedEventArgs e)
        {
            List<ServicePhoto> photos = ClassPage.Base.BD.ServicePhoto.Where(x=>x.ServiceID==ser.ID).ToList();
            if (photos[n].PhotoPath != ser.MainImagePath)
            {
                ServicePhoto photo = photos.FirstOrDefault(x => x.PhotoPath == photos[n].PhotoPath);
                ClassPage.Base.BD.ServicePhoto.Remove(photo);
                ClassPage.Base.BD.SaveChanges();
                ClassPage.FrameNavigate.perehod.Navigate(new Page.AddAndUpdate(ser));
            }
            else
            {
                MessageBox.Show("Данную фотографию нельзя удалить, так как она является обязательной", "Ошибка", MessageBoxButton.OK);
            }
        }
    }
}
