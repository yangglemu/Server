using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Ghs
    {
        public string bh;
        public string name;
        public override string ToString()
        {
            return name;
        }
        public Ghs(string bh, string name)
        {
            this.bh = bh;
            this.name = name;
        }
    }
}
