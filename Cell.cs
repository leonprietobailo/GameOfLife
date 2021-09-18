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


    }
}
