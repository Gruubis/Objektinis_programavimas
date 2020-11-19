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
    public partial class AddItemWindow : Form
    {
        UsersRepository repository = new UsersRepository();
        public AddItemWindow()
        {
            InitializeComponent();
   
            List<Category> categories = repository.GetCategories();
            foreach (Category c in categories)
            {
                comboBox1.Items.Add($"{c.Id.ToString()} - {c.Title} ");
            }
        }
        private void ImgButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {

                textBox2.Text = ofd.FileName;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = comboBox1.SelectedIndex +1 ;
            repository.addNewItem(double.Parse(textBox3.Text), textBox1.Text, id, textBox4.Text, textBox2.Text);
        }

        private void AddItemWindow_Load(object sender, EventArgs e)
        {
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void AddItemWindow_Load_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
