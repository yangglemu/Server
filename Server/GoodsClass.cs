using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class GoodsClass
    {
        public int bh;
        public string pm;
        public string dnm;

        public GoodsClass()
        {

        }
        public GoodsClass(int bh, string pm, string dnm)
        {
            this.bh = bh;
            this.pm = pm;
            this.dnm = dnm;
        }
        public override string ToString()
        {
            return dnm + "--" + pm;
        }
    }
}
