using GUI.Data;
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
    public partial class ItemListWindow : Form
    {
        private ItemsRepository repository;
        public ItemListWindow()
        {
            InitializeComponent();

           
            
            repository = new ItemsRepository();
            List<Category> categoriesList = repository.GetCategories();
            int width = SideMenuPanel.Width - 5;
            foreach ( Category category in categoriesList)
            {
                Button categoryButton = new Button();
                categoryButton.Width = width;
                categoryButton.Text = category.Title;
                categoryButton.Tag = category;
                categoryButton.Click += CategoryButton_Click;
                SideMenuPanel.Controls.Add(categoryButton);
            }
        }


        private void CategoryButton_Click(object sender, EventArgs e)
        {
            flowLayoutPanel2.Controls.Clear();

            Button button = (Button)sender;
            Category category = (Category)button.Tag;

            if (UsersRepository.LoggedInUser == null)
            {
                foreach (Item item in category.Items)
                {
                    ItemsPublicControl ipc = new ItemsPublicControl(item);
                    flowLayoutPanel2.Controls.Add(ipc);
                }
                return;
            }
            if (GUI.Data.UsersRepository.LoggedInUser.GetAdmin() == "false")
            {
                foreach (Item item in category.Items)
                {
                    ItemsUserControl iuc = new ItemsUserControl(item);
                    flowLayoutPanel2.Controls.Add(iuc);
                }
                return;
            }
            else
            {
                foreach (Item item in category.Items)
                {
                    ItemsAdminControl iac = new ItemsAdminControl(item);
                    flowLayoutPanel2.Controls.Add(iac);
                }
            }
        }

        private void SideMenuPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ItemListWindow_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
