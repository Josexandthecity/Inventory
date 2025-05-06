using SQLite;
using System;

namespace Inventory.Models
{
    public class Item
    {
        [PrimaryKey, ]
        public long Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public int Quantity { get; set; }
    }
}