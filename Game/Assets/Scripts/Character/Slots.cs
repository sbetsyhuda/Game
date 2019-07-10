using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Character
{
    class Slots
    {
        public int currentSlot;
        public int maxSize;
        public Item[] items;

        public Item this[int key]
        {
            get
            {
                return items[key];
            }
            set
            {
                items[key] = value;
            }
        }
    }
}
