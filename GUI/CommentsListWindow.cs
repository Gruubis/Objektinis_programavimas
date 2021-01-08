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
    public partial class CommentsListWindow : Form
    {
        private ItemsRepository repository = new ItemsRepository();
        public CommentsListWindow()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CommentsListWindow_Load(object sender, EventArgs e)
        {
            var comments = repository.GetCommentsList();
            listView1.Items.Clear();
            foreach(var c in comments)
            {
                var row = new string[] { c.Id.ToString(), c.Text, c.Date, c.ItemId.ToString() };
                var lvi = new ListViewItem(row);

                lvi.Tag = c;
                listView1.Items.Add(lvi);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.FocusedItem.Selected == true)
                {
                    repository.DeleteComment(int.Parse(listView1.SelectedItems[0].SubItems[0].Text), int.Parse(listView1.SelectedItems[0].SubItems[3].Text), listView1.SelectedItems[0].SubItems[1].Text);
                 MessageBox.Show("User deleted");
                    Close();
                }
                else
                    MessageBox.Show("Need select user");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
