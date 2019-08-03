using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityObjects
{
    public class CharactersPool : MonoBehaviour
    {
        public List<Character> characters;
        public static CharactersPool  instance;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
