using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class Admin : User
    {
        public Admin(string name, string lastName, DateTime birthDate, string username, string password, string IsAdmin) : base(name, lastName, birthDate, username, password, IsAdmin)
        {

        }
    }
}
