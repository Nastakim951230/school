using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school
{
    public partial class Client
    {
        public string FIO
        {
            get
            {
                string fio = FirstName + " " + LastName + " " + Patronymic;
                return fio;
            }
        }
    }
}
