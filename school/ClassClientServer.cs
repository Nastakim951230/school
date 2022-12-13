using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace school
{
    public partial class ClientService
    {
        public SolidColorBrush TextBrush
        {
            get
            {
                var brushConverter = new BrushConverter();

                if (ID%2 == 0)
                {
                    return (SolidColorBrush)(Brush)brushConverter.ConvertFrom("#FFFFFF");
                }
                else
                {
                    return (SolidColorBrush)(Brush)brushConverter.ConvertFrom("#e7fabf");

                }

            }
        }
    }
}
