using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overlife_novel.Elements
{
    public class Classes
    {
        public string Classname { get; set; }
        public string Cabinet { get; set; }
        public Classes(string classname, string cabinet)
        {
            Classname = classname;
            Cabinet = cabinet;
        }

        public Classes() { }
    }
}
