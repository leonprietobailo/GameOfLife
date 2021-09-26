using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Rules
    {
        private bool[] infected = new bool[9];
        private bool[] healed = new bool[9];
        
        // Añadir nuevas reglas.
        public Rules(bool[] inf, bool[] heal)
        {
            infected = inf;
            healed = heal;
        }

        public Rules()
        {
        }
        public void setConway()
        {
            infected[0] = false;
            infected[1] = false;
            infected[2] = true;
            infected[3] = true;
            infected[4] = false;
            infected[5] = false;
            infected[6] = false;
            infected[7] = false;
            infected[8] = false;

            healed[0] = false;
            healed[1] = false;
            healed[2] = false;
            healed[3] = true;
            healed[4] = false;
            healed[5] = false;
            healed[6] = false;
            healed[7] = false;
            healed[8] = false;
        }

        public void setCOVID19()
        {
            infected[0] = false;
            infected[1] = false;
            infected[2] = true;
            infected[3] = true;
            infected[4] = true;
            infected[5] = false;
            infected[6] = false;
            infected[7] = false;
            infected[8] = false;

            healed[0] = false;
            healed[1] = false;
            healed[2] = false;
            healed[3] = true;
            healed[4] = true;
            healed[5] = false;
            healed[6] = false;
            healed[7] = false;
            healed[8] = false;
        }

        public bool getNextStatus(int neighbors, bool isInfected)
        {
            if (isInfected)
            {
                return infected[neighbors];
            }
            else
            {
                return healed[neighbors];
            }
        }

        //public void setRules(int i)
        //{
        //    virusType = i;
        //}

        //public int getRules()
        //{
        //    return virusType;
        //}

    }
}
