using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Character
{
    public class GameCharacterInfo
    {
        public enum CharacterClass { Warrior, Mage, Archer, Rogue};

        public Inventory inventory;
        public Slots stots;
        public Weapon weapon;
        public Position position;
        public float maxHealthPoints;
        public float healthPoints;
        public float manaPoints;
        public float agilityPoints;
        public float staminaPoints;
        public Damage baseAttackDamage;
        public Defence defence;
        public MainInfo info;
        public CharacterClass characterClass;


        public GameCharacterInfo()
        {
            healthPoints = 100;
            maxHealthPoints = 100;
            baseAttackDamage = new Damage(10, 10);
            defence = new Defence(10, 10);
        }
    }
}
