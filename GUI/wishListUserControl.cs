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
    public partial class wishListUserControl : UserControl
    {
        private ItemsRepository repository = new ItemsRepository();
        private Item item;
        public wishListUserControl(Item item)
        {
            InitializeComponent();
            this.item = item;
            pictureBox1.Image = Image.FromFile(item.Image);
            titleLabel.Text = item.Title;
            descriptionLabel.Text = item.Description;
            priceLabel.Text = item.Price.ToString();
        }

        private void nebemegti_Click(object sender, EventArgs e)
        {
            
            repository.DeleteLiked(item.Id);
            MessageBox.Show("Done");
            

        }
    }
}
