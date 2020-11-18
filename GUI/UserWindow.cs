using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class UserWindow : Form
    {
        public UserWindow()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserWindow_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = GUI.Data.UsersRepository.LoggedInUser.GetImage();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            label4.Text = Data.UsersRepository.LoggedInUser.GetFullName();
            label5.Text = GUI.Data.UsersRepository.LoggedInUser.GetAge().ToString();
            label6.Text = GUI.Data.UsersRepository.LoggedInUser.GetUserName();
            label8.Text = GUI.Data.UsersRepository.LoggedInUser.GetBirthDate().ToString();
            button3.Visible = bool.Parse(GUI.Data.UsersRepository.LoggedInUser.GetAdmin());

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangePasswordWindow cpw = new ChangePasswordWindow();
            cpw.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image newImage = Image.FromFile(ofd.FileName);
                pictureBox1.Image = GUI.Data.UsersRepository.LoggedInUser.setImage(newImage);

            }
        }

       
    private void ChangeImageWindow_Closed(object sender, EventArgs e)
        {
            pictureBox1.Image = GUI.Data.UsersRepository.LoggedInUser.GetImage();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            UsersListWindow ulw = new UsersListWindow();
            ulw.ShowDialog();
        }
    }
}
