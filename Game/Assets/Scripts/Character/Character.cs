using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Character;

namespace UnityObjects
{
    public class Character : MonoBehaviour
    {
        public GameCharacterInfo info;

        

        // Start is called before the first frame update
        void Start()
        {
            info = new GameCharacterInfo();
        }

        // Update is called once per frame
        void Update()
        {
        }

        
        public void GetDamage(Damage damage)
        {
            float prevHealth = info.healthPoints;
            if (damage.physicalDamage > 0)
                info.healthPoints -= damage.physicalDamage * Mathf.Pow(2,-info.defence.physicalProtection / damage.physicalDamage);
            if(damage.magicalDamage > 0)
                info.healthPoints -= damage.magicalDamage  * Mathf.Pow(2, -info.defence.magicalProtection / damage.magicalDamage);

            Debug.Log(string.Format("{0} damage taken", prevHealth - info.healthPoints));
        }

    }
}


