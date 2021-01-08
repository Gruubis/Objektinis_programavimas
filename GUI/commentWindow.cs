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
    public partial class commentWindow : Form
    {
        private ItemsRepository repository = new ItemsRepository();
        private Item item;
        public commentWindow(Item item)
        {
            InitializeComponent();
            this.item = item;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            repository.AddComment(item.Id, textBox1.Text, UsersRepository.LoggedInUser.GetUserId());
            MessageBox.Show("Done");
            Close();
        }
    }
}
