using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.Data;

namespace GUI
{
    public partial class LoginWindow : Form
    {
        private UsersRepository repository = new UsersRepository();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            try
            {

                GUI.Data.UsersRepository.LoggedInUser = repository.Login(textBox1.Text, textBox2.Text); 
                UserWindow uw = new UserWindow();
                uw.ShowDialog();
                Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
