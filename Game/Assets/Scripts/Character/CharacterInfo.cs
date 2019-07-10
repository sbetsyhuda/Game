using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Character
{
    class CharacterInfo
    {
        public enum CharacterClass { Warrior, Mage, Archer, Rogue};

        public Inventory inventory;
        public Slots stots;
        public Weapon weapon;
        public float healthPoints;
        public float manaPoints;
        public float agilityPoints;
        public float staminaPoints;
        public Defence defence;
        public MainInfo info;
        public CharacterClass characterClass;
    }
}
