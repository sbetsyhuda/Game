using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityObjects
{
    public class ignoreCharacters : MonoBehaviour
    {
        public GameObject character1;
        public GameObject character2;

        // Start is called before the first frame update
        void Start()
        {
            Physics2D.IgnoreLayerCollision(8, 8);
            Physics2D.IgnoreLayerCollision(8, 9);
            Physics2D.IgnoreLayerCollision(9, 9);
            //Physics2D.IgnoreCollision(character1.GetComponent<Collider2D>(), character2.GetComponent<Collider2D>());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
