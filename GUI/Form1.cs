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
    public partial class Form1 : Form
    {
        UsersRepository repository = new UsersRepository();
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            { 
                User user = new User(textBox1.Text, textBox2.Text, dateTimePicker1.Value, textBox3.Text, textBox4.Text, false);
                
                repository.Register(user, textBox3.Text);
                MessageBox.Show("Registered");
                Close();
               }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
