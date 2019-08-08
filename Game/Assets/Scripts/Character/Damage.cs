using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Character
{
    public class Damage
    {


        public float magicalDamage;
        public float physicalDamage;

        public Damage(float magDamage, float physDamage)
        {
            magicalDamage = magDamage;
            physicalDamage = physDamage;
        }
    }
}
