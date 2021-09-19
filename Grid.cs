using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GameOfLife
{
    class Grid
    {
        //ATRIBUTOS
        int i, j;
        Cell[,] array;
        int strategy, boundaries;

        //CONSTRUCTOR
        public Grid(int iIn, int jIn)
        {
            this.i = iIn;
            this.j = jIn;
            this.array = new Cell[this.i, this.j];
            for (int n = 0; n < this.i; n++)
            {
                for (int s = 0; s < this.j; s++)
                {
                    array[n, s] = new Cell();
                }
            }
        }

        public Grid(Grid mesh)
        {
            array = mesh.array;
            i = array.GetLength(0);
            j = array.GetLength(1);
        }
        //METODOS

        public void setStrategy(int s)
        {
            strategy = s;
        }

        public void setBoundaries(int b)
        {
            boundaries = b;
        }

        public int[] getSize()
        {
            int[] size = new int[2];
            size[0] = this.i;
            size[1] = this.j;
            return size;
        }
        
        public void reset()
        {
            for (int n = 0; n < this.i; n++)
            {
                for (int s = 0; s < this.j; s++)
                {
                    array[n, s].healCell();
                }
            }
        }

        public void changeCellStatus(int iIn, int jIn)
        {
            array[iIn, jIn].changeStatus();
        }

        public Boolean getCellStatus(int iIn, int jIn)
        {
            return array[iIn, jIn].getStatus();
        }

        public Grid deepCopy()
        {
            Grid deepCopyGrid = new Grid(this);
            return deepCopyGrid;
        }

        public void conwayAlgorithm()
        {
            Cell[,] newIt = new Cell[this.i, this.j];
            for (int n = 0; n < this.i; n++)
            {
                for (int s = 0; s < this.j; s++)
                {
                    int infected = 8 - getBoundaries(n, s);
                    newIt[n,s] = new Cell();
                    if (array[n, s].getStatus()) // C = 1
                    {
                        if (infected == 2 || infected == 3)
                        {
                            newIt[n, s].changeStatus();
                        }
                    }
                    else // C = 0
                    {
                        if (infected == 3)
                        {
                            newIt[n, s].changeStatus();
                        }
                    }
                }
            }
            array = newIt;
        }

        public void covid19Algorithm()
        {
            Cell[,] newIt = new Cell[this.i, this.j];
            for (int n = 0; n < this.i; n++)
            {
                for (int s = 0; s < this.j; s++)
                {
                    int infected = 8 - getBoundaries(n, s);
                    newIt[n, s] = new Cell();
                    if (array[n, s].getStatus()) // C = 1
                    {
                        if (infected == 2 || infected == 3 || infected == 4)
                        {
                            newIt[n, s].changeStatus();
                        }
                    }
                    else // C = 0
                    {
                        if (infected == 3 || infected == 4)
                        {
                            newIt[n, s].changeStatus();
                        }
                    }
                }
            }
            array = newIt;
        }

        public int getBoundaries(int iIn, int jIn)
        {
            if (boundaries == 0)
            {
                return aliveBoundaries(iIn, jIn);
            }
            else
            {
                return reflectiveBoundaries(iIn, jIn);
            }
        }


        public int aliveBoundaries(int iIn, int jIn)
        {
            int healthy = 0;
            for (int n = iIn - 1; n <= iIn + 1; n++)
            {
                for (int s = jIn - 1; s <= jIn + 1; s++)
                {
                    if (n < 0 || n >= i || s < 0 || s >= j)
                    {
                        healthy++;
                    }
                    else if (n == iIn && s == jIn)
                    {
                    }
                    else
                    {
                        if (!array[n, s].getStatus())
                        {
                            healthy++;
                        }
                    }
                }
            }
            return healthy;
        }

        public int reflectiveBoundaries(int iIn, int jIn)
        {
            int healthy = 0;
            int nT, sT;
            for (int n = iIn - 1; n <= iIn + 1; n++)
            {
                for (int s = jIn - 1; s <= jIn + 1; s++)
                {
                    nT = n;
                    sT = s;

                    if (n < 0)
                    {
                        nT = n + 2;
                    }
                    if (s < 0)
                    {
                        sT = s + 2;
                    }
                    if (n >= i)
                    {
                        nT = n - 2;
                    }
                    if (s >= j)
                    {
                        sT = s - 2;
                    }

                    if (nT == iIn && sT == jIn)
                    {
                    }
                    else if (!array[nT, sT].getStatus())
                    {
                        healthy++;
                    }
                }
            }
            return healthy;

        }

        public void iterate()
        {
            if (strategy == 0)
            {
                conwayAlgorithm();
            }
            else
            {
                covid19Algorithm();
            }
        }

        public int countInfected()
        {
            int counter = 0;
            for (int n = 0; n < this.i; n++)
            {
                for (int s = 0; s < this.j; s++)
                {
                    if (array[n, s].getStatus())
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        public void saveGrid()
        {
            StreamWriter file = new StreamWriter("grid.txt");
            for (int n = 0; n < this.i; n++)
            {
                for (int s = 0; s < this.j; s++)
                {
                    if (array[n, s].getStatus())
                        file.Write("1");
                    else
                        file.Write("0");
                    if (s != j - 1)
                    {
                        file.Write(" ");
                    }
                }
                if (n != i - 1)
                {
                    file.Write("\n");
                }
            }
            file.Close();
        }

        public void loadGrid()
        {
            int n = 0;
            int s = 0;
            Boolean readColumns = false;
            StreamReader countFile = new StreamReader("grid.txt");
            string strReadline = countFile.ReadLine();
            while (strReadline != null)
            {
                if (!readColumns)
                {
                    s = strReadline.Length;
                    readColumns = true;
                }
                n++;
                strReadline = countFile.ReadLine();
            }
            countFile.Close();
            
            StreamReader readFile = new StreamReader("grid.txt");
            int rows = n;
            int columns = (s + 1) / 2;
            Grid loadedGrid = new Grid(rows, columns);
            strReadline = readFile.ReadLine();
            s = 0;
            while (strReadline != null)
            {
                string[] subs = strReadline.Split(' ');
                for (n = 0; n < subs.Length; n++)
                {
                    if (subs[n] == "1")
                    {
                        loadedGrid.changeCellStatus(s, n);
                    }
                }
                strReadline = readFile.ReadLine();
                s++;
            }
            readFile.Close();
            this.i = rows;
            this.j = columns;
            this.array = loadedGrid.array;
        }
    }
}
