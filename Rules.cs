using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Rules
    {
        private int virusType;

        public void setRules(int i)
        {
            virusType = i;
        }

        public int getRules()
        {
            return virusType;
        }

    }
}
