using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public class User : Person
    {
        protected string username;
        protected string password;
        protected string IsAdmin;
        protected int id;
        protected string picture;

        public User(string name, string lastName, DateTime birthDate, string username, string password, string IsAdmin) : base(name, lastName, birthDate)
        {
          /* if (GetAge() < 14)
                throw new Exception("must be atleast 14 years old to register");*/
            if (username == "")
                throw new Exception("Username cannot be empty");
            if (password == "")
                throw new Exception("Password cannot be empty");

            this.username = username;
            this.password = password;
            this.IsAdmin = IsAdmin;
        }

        public string GetUserName()
        {
            return username;
        }
        public string GetName()
        {
            return Name;
        }
        public string GetLastName()
        {
            return LastName;
        }
       

        public string GetPassword()
        {
            return password;
        }
        
       
        public void setImage(string profilePic)
        {
            picture = profilePic;
            
        }
        public string GetImage()
        {
            return picture;
        }
        public string GetAdmin()
        {
            return IsAdmin;
        }
        public void SetUserId(int usrId)
        {
           id = usrId;
        }
        public int GetUserId()
        {
            return id;
        }
        
    }
}
