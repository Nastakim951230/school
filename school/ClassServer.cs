using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace school
{
    public partial class Service
    {
        public static System.Windows.Visibility Btn_Admin = System.Windows.Visibility.Collapsed;
        public static System.Windows.Visibility btn_admin
        {
            get
            {
                return Btn_Admin;
            }
            set
            {
                Btn_Admin = value;
            }
        }
        public string skidka
        {
            get
            {
                if(Discount==0)
                {
                    return "";
                }
                else
                {
                    double a = Discount.Value * 100;
                    return "*скидка " + a+"%";
                }
            }
        }


        public string price_and_time
        {
            get
            {
                if (Discount == 0)
                {
                    double a = Convert.ToDouble(Cost);
                    int time = DurationInSeconds / 60;
                    return a+" рублей на "+time +" минут";
                }
                else
                { 
                    double b =(Convert.ToDouble(Cost)/100)*(100-(Discount.Value*100));
                    int time = DurationInSeconds / 60;
                    return b+ " рублей на " + time+ " минут";
                }
            }
        }
        public string pric

        {
            get
            {
                if (Discount == 0)
                {
                    return "";
                }
                else
                {
                    double a = Convert.ToDouble(Cost);
                    return a + " ";
                }
               
            }
        }

    }
}
