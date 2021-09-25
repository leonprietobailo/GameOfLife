using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;


namespace GameOfLife
{
    class Grid
    {
        //ATRIBUTOS
        int i, j;
        Cell[,] array;
        int boundaries;
        Rules rules;

        //CONSTRUCTOR
        public Grid(int iIn, int jIn)
        {
            this.i = iIn + 2;
            this.j = jIn + 2;
            this.array = new Cell[this.i, this.j];
            for (int n = 0; n < this.i; n++)
            {
                for (int s = 0; s < this.j; s++)
                {
                    array[n, s] = new Cell();
                }
            }
            rules = new Rules();
        }

        public Grid(Grid mesh)
        {
            array = new Cell[mesh.getSize()[0] + 2, mesh.getSize()[1] + 2];

            for (int n = 0; n < mesh.i; n++)
            {
                for (int s = 0; s < mesh.j; s++)
                {
                    array[n, s] = new Cell();
                    if (mesh.getCellStatus(n, s))
                    {
                        array[n, s].changeStatus();
                    }
                }
            }
            i = array.GetLength(0);
            j = array.GetLength(1);
            rules = mesh.rules;
        }

        public void setRules(int r)
        {
            rules.setRules(r);
        }

        public int getRules()
        {
            return rules.getRules();
        }

        public void setBoundaries(int b)
        {
            boundaries = b;
            setBoundaryLayer();
        }

        public int[] getSize()
        {
            int[] size = new int[2];
            size[0] = this.i - 2;
            size[1] = this.j - 2;
            return size;
        }

        public void reset()
        {
            for (int n = 1; n < this.i - 1; n++)
            {
                for (int s = 1; s < this.j - 1; s++)
                {
                    array[n, s].healCell();
                }
            }
        }

        public void setBoundaryLayer()
        {
            if (boundaries == 1)
            {
                array[0, 0] = array[2, 2];
                array[0, j - 1] = array[2, j - 3];
                array[i - 1, 0] = array[i - 3, 2];
                array[i - 1, j - 1] = array[i - 3, j - 3];

                for (int n = 1; n < i - 1; n++)
                {
                    array[n, 0] = array[n, 2];
                    array[n, j - 1] = array[n, j - 3];
                }

                for (int s = 1; s < i - 1; s++)
                {
                    array[0, s] = array[2, s];
                    array[i - 1, s] = array[i - 3, s];
                }
            }
            else
            {
                for (int n = 0; n < i; n++)
                {
                    array[n, 0] = new Cell();
                    array[n, j - 1] = new Cell();
                }

                for (int s = 0; s < i; s++)
                {
                    array[0, s] = new Cell();
                    array[i - 1, s] = new Cell();
                }
            }
        }

        //public int reflectiveBoundaries(int iIn, int jIn)
        //{
        //    int healthy = 0;
        //    int nT, sT;
        //    for (int n = iIn - 1; n <= iIn + 1; n++)
        //    {
        //        for (int s = jIn - 1; s <= jIn + 1; s++)
        //        {
        //            nT = n;
        //            sT = s;

        //            if (n < 0)
        //            {
        //                nT = n + 2;
        //            }
        //            if (s < 0)
        //            {
        //                sT = s + 2;
        //            }
        //            if (n >= i)
        //            {
        //                nT = n - 2;
        //            }
        //            if (s >= j)
        //            {
        //                sT = s - 2;
        //            }

        //            if (nT == iIn && sT == jIn)
        //            {
        //            }
        //            else if (!array[nT, sT].getStatus())
        //            {
        //                healthy++;
        //            }
        //        }
        //    }
        //    return healthy;

        //}

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

        //public void conwayAlgorithm()
        //{
        //    Cell[,] newIt = new Cell[this.i, this.j];
        //    for (int n = 0; n < this.i; n++)
        //    {
        //        for (int s = 0; s < this.j; s++)
        //        {
        //            int infected = 8 - getBoundaries(n, s);
        //            newIt[n,s] = new Cell();
        //            if (array[n, s].getStatus()) // C = 1
        //            {
        //                if (infected == 2 || infected == 3)
        //                {
        //                    newIt[n, s].changeStatus();
        //                }
        //            }
        //            else // C = 0
        //            {
        //                if (infected == 3)
        //                {
        //                    newIt[n, s].changeStatus();
        //                }
        //            }
        //        }
        //    }
        //    array = newIt;
        //}

        //public void covid19Algorithm()
        //{
        //    Cell[,] newIt = new Cell[this.i, this.j];
        //    for (int n = 0; n < this.i; n++)
        //    {
        //        for (int s = 0; s < this.j; s++)
        //        {
        //            int infected = 8 - getBoundaries(n, s);
        //            newIt[n, s] = new Cell();
        //            if (array[n, s].getStatus()) // C = 1
        //            {
        //                if (infected == 2 || infected == 3 || infected == 4)
        //                {
        //                    newIt[n, s].changeStatus();
        //                }
        //            }
        //            else // C = 0
        //            {
        //                if (infected == 3 || infected == 4)
        //                {
        //                    newIt[n, s].changeStatus();
        //                }
        //            }
        //        }
        //    }
        //    array = newIt;
        //}

        //public int getBoundaries(int iIn, int jIn)
        //{
        //    if (boundaries == 0)
        //    {
        //        return aliveBoundaries(iIn, jIn);
        //    }
        //    else
        //    {
        //        return reflectiveBoundaries(iIn, jIn);
        //    }
        //}

        public int countHealedNeighbors(int iIn, int jIn)
        {

            int healed = 0;
            if (iIn == 2 & jIn == 2)
            {
                int a = 1;
            }

            for (int n = iIn - 1; n <= iIn + 1; n++)
            {
                for (int s = jIn - 1; s <= jIn + 1; s++)
                {
                    if (!(n == iIn && s == jIn))
                    {
                        if (!array[n, s].getStatus())
                        {
                            healed++;
                        }
                    }
                }
            }
            return healed;
        }

        //public int aliveBoundaries(int iIn, int jIn)
        //{
        //    int healthy = 0;
        //    for (int n = iIn - 1; n <= iIn + 1; n++)
        //    {
        //        for (int s = jIn - 1; s <= jIn + 1; s++)
        //        {
        //            if (n < 0 || n >= i || s < 0 || s >= j)
        //            {
        //                healthy++;
        //            }
        //            else if (n == iIn && s == jIn)
        //            {
        //            }
        //            else
        //            {
        //                if (!array[n, s].getStatus())
        //                {
        //                    healthy++;
        //                }
        //            }
        //        }
        //    }
        //    return healthy;
        //}



        public void iterate()
        {
            int neighborsHealed;
            Grid temporary = this.deepCopy();
            for (int n = 1; n < i - 1; n++)
            {
                for (int s = 1; s < j - 1; s++)
                {
                    {
                        if (n == 2 & s == 2)
                        {
                            int a = 1;
                        }
                        neighborsHealed = countHealedNeighbors(n, s);
                        temporary.array[n, s].setNextStatus(rules, neighborsHealed);
                    }
                }
            }
            this.array = temporary.array; // cuidado
        }

        public int countInfected()
        {
            int counter = 0;

            for (int n = 1; n < this.i - 1; n++)
            {
                for (int s = 1; s < this.j - 1; s++)
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
            SaveFileDialog dig = new SaveFileDialog();
            dig.Filter = "(*.txt)|*.*";
            dig.DefaultExt = "txt";
            

            if (dig.ShowDialog() == true)
            {
                FileStream emptyFile = File.Create(dig.FileName);
                emptyFile.Close();
                for (int n = 0; n < this.i; n++)
                {
                    for (int s = 0; s < this.j; s++)
                    {
                        if (array[n, s].getStatus())
                        {
                            File.AppendAllText(dig.FileName, "1");
                        }
                        else
                        {
                            File.AppendAllText(dig.FileName, "0");
                        }
                        if (s != j - 1)
                        {
                            File.AppendAllText(dig.FileName, " ");
                        }
                    }
                    if (n != i - 1)
                    {
                        File.AppendAllText(dig.FileName, "\n");
                    }
                }

            }
        }

        public void loadGrid()
        {
            var n = 0;
            var s = 0;
            Boolean readColumns = false;

            OpenFileDialog dig = new OpenFileDialog();
            dig.Multiselect = false;
            dig.Filter = "(*.txt*)|*.*";
            dig.DefaultExt = ".txt";
            if (dig.ShowDialog() == true)
            {
                StreamReader countFile = new StreamReader(dig.FileName);
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

                StreamReader readFile = new StreamReader(dig.FileName);
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
                        else if (subs[n] == "0")
                        {
                        }
                        else
                        {
                            throw new FileFormatException();
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

        public Boolean isLastIteration()
        {
            Grid copy;
            copy = this.deepCopy();
            copy.iterate();
            for (int n = 0; n < this.i; n++)
            {
                for (int s = 0; s < this.j; s++)
                {
                    if (!copy.getCellStatus(n, s) == this.getCellStatus(n, s))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Boolean isClean()
        {
            for (int n = 0; n < this.i; n++)
            {
                for (int s = 0; s < this.j; s++)
                {
                    if (this.getCellStatus(n, s))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
