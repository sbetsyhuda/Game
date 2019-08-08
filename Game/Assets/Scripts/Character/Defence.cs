using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Character
{
    public class Defence
    {
        public float magicalProtection;
        public float physicalProtection;
        public Defence(float magProtection, float physProtection)
        {
            magicalProtection = magProtection;
            physicalProtection = physProtection;
        }
    }
}
