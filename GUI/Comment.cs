using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class Comment
    {
        public int Id { get; private set; }
        public string Text { get; private set; }
        public string Date { get; private set; }
        public int ItemId { get; private set; }
        public Comment(int id, string text, string date, int itemId)
        {
            Id = id;
            Text = text;
            Date = date;
            ItemId = itemId;
        }
    }
}
