using System;
using System.Collections;

namespace SudokuSolver.SudoCalc
{
    public class Panel : ICloneable, IEnumerable
    {
        Block[,] table = new Block[9, 9];
        public Panel()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    table[j, i] = new Block();
                }
            }
        }
        public Panel(string initstring)
        {
            while (initstring.Length < 81)
            {
                initstring += '0';
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    table[j, i] = new Block((SudoNum)((int)initstring[i * 9 + j]) - (int)'0');
                    if ((SudoNum)((int)initstring[i * 9 + j]) - (int)'0' != SudoNum.Unknown)
                    {
                        table[j, i].Stable = true;
                    }
                }
            }
        }
        protected Panel(Block[,] data)
        {
            table = data;
        }
        public Block this[int x, int y]
        {
            get
            {
                return table[x, y];
            }
            set
            {
                table[x, y] = value;
            }
        }
        public bool IsFull()
        {
            foreach (Block item in table)
            {
                if (item.Number == SudoNum.Unknown) return false;
            }
            return true;
        }
        public static int GetRegion(int x, int y)
        {
            return (y / 3) * 3 + (x / 3);
        }
        public static int GetRegionNumber(int x, int y)
        {
            return (y % 3) * 3 + (x % 3);
        }
        public static int GetX(int r, int n)
        {
            return (r % 3) * 3 + (n % 3);
        }
        public static int GetY(int r, int n)
        {
            return (r / 3) * 3 + (n / 3);
        }

        public object Clone()
        {
            Block[,] tmp = new Block[9, 9];
            //Array.Copy(table, tmp, table.Length);
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    tmp[x, y] = (Block)table[x, y].Clone();
                }
            }
            return new Panel(tmp);
        }

        public IEnumerator GetEnumerator()
        {
            return new SudoEnumerator(table);
        }
    }
    class SudoEnumerator : IEnumerator
    {
        int posx = -1;
        int posy = 0;
        Block[,] d;
        internal SudoEnumerator(Block[,] data)
        {
            d = data;
        }

        public object Current
        {
            get
            {
                return d[posx, posy];
            }
        }

        public bool MoveNext()
        {
            posx++;
            if (posx >= 9)
            {
                posx -= 9;
                posy++;
                if (posy >= 9)
                {
                    return false;
                }
            }
            return true;
        }

        public void Reset()
        {
            posx = -1;
            posy = 0;
        }
    }
}
