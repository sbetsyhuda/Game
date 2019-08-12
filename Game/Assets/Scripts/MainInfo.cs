using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class MainInfo
    {
        public string name;
        public string description;

		public MainInfo(string name) : this(name, "")
		{

		}

		public MainInfo(string name, string description)
		{
			this.name = name;
			this.description = description;
		}
    }
}
