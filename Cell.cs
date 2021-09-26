using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Cell
    {
        // ATRIBUTOS
        private Boolean infected;

        //CONSTRUCTORES
        public Cell()
        {
            this.infected = false;
        }

        //METODOS
        public void healCell()
        {
            this.infected = false;
        }

        public void sickCell()
        {
            this.infected = true;
        }

        public void changeStatus()
        {
            if (this.infected)
            {
                this.infected = false;
            }
            else
            {
                this.infected = true;
            }
        }

        public Boolean getStatus()
        {
            return this.infected;
        }

        //public void setNextStatus(Rules r, int neighborsHealed)
        //{
        //    int neighborsIinfected = 8 - neighborsHealed;
        //    if (r.getRules() == 0) // CONWAY
        //    {
                
        //        if (infected) // C = 1
        //        {
        //            if (neighborsIinfected < 2 || neighborsIinfected > 3)
        //            {
        //                infected = false;
        //            }
        //        }
        //        else // C = 0
        //        {
        //            if (neighborsIinfected == 3)
        //            {
        //                infected = true;
        //            }
        //        }
        //    }

        //    else //COVID19
        //    {
        //        if (infected) // C = 1
        //        {
        //            if (neighborsIinfected < 2 || neighborsIinfected > 4)
        //            {
        //                infected = false;
        //            }
        //        }
        //        else // C = 0
        //        {
        //            if (neighborsIinfected == 3 || neighborsIinfected == 4)
        //            {
        //                infected = true;
        //            }
        //        }
        //    }

        //}

        public void setNextStatus(Rules r, int neighborsHealed)
        {
            int neighborsInfected = 8 - neighborsHealed;
            infected = r.getNextStatus(neighborsInfected, infected);
        }
    }
}