﻿using System;
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
    
    public partial class ItemsPublicControl : UserControl 
    {
        public ItemsPublicControl(Item item)
        {
            InitializeComponent();
            ItemsRepository repository = new ItemsRepository();
            TitleLabel.Text = item.Title;
            PriceLabel.Text = item.Price.ToString();
            DescriptionLabel.Text = item.Description;
            pictureBox1.Image = Image.FromFile(item.Image);
        }
        public ItemsPublicControl()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ItemsPublicControl_Load(object sender, EventArgs e)
        {

        }
    }
}
