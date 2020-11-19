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
    public partial class ItemsUserControl : ItemsPublicControl
    {
        private UsersRepository repository = new UsersRepository();
        private Item item;
        public ItemsUserControl(Item item) : base(item)
        {
            InitializeComponent();
            this.item = item;
        }
        public ItemsUserControl() 
        {
            InitializeComponent();

        }
        private void TitleLabel_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int UserId = UsersRepository.LoggedInUser.GetUserId();

            repository.AddToWhishlist(UserId, item.Id);
            MessageBox.Show("Preke isiminta!");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            commentWindow cw = new commentWindow(item);
            cw.ShowDialog();

        }
    }
}
