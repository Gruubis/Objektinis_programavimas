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
    public partial class addCategoryWindowcs : Form
    {
        UsersRepository repository = new UsersRepository();
        public addCategoryWindowcs()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            repository.AddNewCategory(textBox1.Text);
            MessageBox.Show("Prideta");
            Close();
        }
    }
}
