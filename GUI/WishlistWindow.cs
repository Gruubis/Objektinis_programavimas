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
    public partial class WishlistWindow : Form
    {
        private ItemsRepository repository;
        public WishlistWindow()
        {
            repository = new ItemsRepository();
            InitializeComponent();
            List<Item> WishItems = repository.GetWishlist(GUI.Data.UsersRepository.LoggedInUser.GetUserId());
            foreach(Item item in WishItems)
            {
                wishListUserControl wluc = new wishListUserControl(item);
                flowLayoutPanel1.Controls.Add(wluc);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
