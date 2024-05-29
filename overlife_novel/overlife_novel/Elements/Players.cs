using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overlife_novel.Elements
{
    public class Players
    {
        public string Name { get; set; }
        public string State { get; set; }
        public string StateName { get; set; }

        public Players(string name, string state, string stateName)
        {
            Name = name;
            State = state;
            StateName = stateName;
        }
        public Players() { }
    }
}
