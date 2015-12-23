using System;

namespace SudokuSolver.SudoCalc
{
	/// <summary>
	/// Description of Panel.
	/// </summary>
	public class Panel
	{
		Block[,] table = new Block[9,9];
		public Panel()
		{
			for (int i = 0; i < 9; i++) {
				for (int j = 0; j < 9; j++) {
					table[j,i]=new Block();
				}
			}
		}
		public Panel(string initstring)
		{
			while(initstring.Length<81)
			{
				initstring+='0';
			}
			for (int i = 0; i < 9; i++) {
				for (int j = 0; j < 9; j++) {
					table[j,i]=new Block((SudoNum)((int)initstring[i*9+j])-(int)'0');
					if((SudoNum)((int)initstring[i*9+j])-(int)'0'!=SudoNum.Unknown)
					{
						table[j,i].Stable=true;
					}
				}
			}
		}
		public Block this[int x,int y]
		{
			get
			{
				return table[x,y];
			}
			set
			{
				table[x,y]=value;
			}
		}
		public static int GetRegion(int x,int y)
		{
			return (y/3)*3+(x/3);
		}
		public static int GetRegionNumber(int x,int y)
		{
			return (y%3)*3+(x%3);
		}
		public static int GetX(int r,int n)
		{
			return (r%3)*3+(n%3);
		}
		public static int GetY(int r,int n)
		{
			return (r/3)*3+(n/3);
		}
	}
}
