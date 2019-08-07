using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Character
{
    public class Inventory
    {
        public int maxSize;
        public Item[] items;

        public Inventory(int size)
        {
            maxSize = size;
            items = new Item[10];
        }

        public Item this[int key]
        {
            get => items[key];
            set => items[key] = value;
        }
    }
}
