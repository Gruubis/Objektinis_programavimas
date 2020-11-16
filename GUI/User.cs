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
        protected bool IsAdmin;
        protected Image picture;

        public User(string name, string lastName, DateTime birthDate, string username, string password, bool IsAdmin) : base(name, lastName, birthDate)
        {
            /*if (GetAge() < 14)
                throw new Exception("must be atleast 14 years haha old to register");*/
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
        public void ChangePassword(string password, string newPassword)
        {
            if(password != GUI.Data.UsersRepository.LoggedInUser.password)
            {
                throw new Exception("Password didnt match");
            }
            GUI.Data.UsersRepository.LoggedInUser.password = newPassword;
        }
       
        public Image setImage(Image profilePic)
        {
            picture = profilePic;
            return picture;
        }
        public Image GetImage()
        {
            return picture;
        }
        public bool GetAdmin()
        {
            return IsAdmin;
        }
    }
}
