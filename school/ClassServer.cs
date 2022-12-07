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
        public string skidka
        {
            get
            {
                if(Discount==null)
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
                if (Discount == null)
                {
                    double a = Convert.ToDouble(Cost);
                    int time = DurationInSeconds / 60;
                    return a+" рублей на "+time;
                }
                else
                {




                    double a = Convert.ToDouble(Cost);
                    double b =(Convert.ToDouble(Cost)/100)*(100-(Discount.Value*100));
                    int time = DurationInSeconds / 60;
                    return  a +" " +b+ " рублей на " + time;
                }
            }
        }
    }
}
