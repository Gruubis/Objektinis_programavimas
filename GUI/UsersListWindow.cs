using GUI.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class UsersListWindow : Form
    {
        UsersRepository repository = new UsersRepository();
        public UsersListWindow()
        {
            InitializeComponent();
           
        }


        private void UsersListWindow_Load(object sender, EventArgs e)
        {
            var people = GUI.Data.UsersRepository.GetUsers();
            listView1.Items.Clear();
            foreach( var User in people)
            {
                var row = new string[] { User.GetFullName(), User.GetBirthDate().ToString(), User.GetUserName(), User.GetPassword() };
                var lvi = new ListViewItem(row);

                lvi.Tag = User;
                listView1.Items.Add(lvi); 
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.FocusedItem.Selected == true)
                {
                    repository.RemUser(listView1.SelectedItems[0].SubItems[2].Text);
                    MessageBox.Show("User deleted");
                    Close();
                }
                else
                    MessageBox.Show("Need select user");
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
