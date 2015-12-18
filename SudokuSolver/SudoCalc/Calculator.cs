using System;

namespace SudokuSolver.SudoCalc
{
	/// <summary>
	/// Description of Calculator.
	/// </summary>
	public static class Calculator
	{
		public static void ExpelCandidate(ref Panel pl)
		{
			for(int y=0;y<9;y++)
			{
				for(int x=0;x<9;x++)
				{
					if(pl[x,y].Number== SudoNum.Unknown)continue;
					RowExpel(ref pl,x,y,pl[x,y].Number);
					ColumnExpel(ref pl,y,x,pl[x,y].Number);
					RegionExpel(ref pl,(y/3)*3+(x/3),(y%3)*3+(x%3),pl[x,y].Number);
				}
			}
		}
		public static void RowExpel(ref Panel pl,int row,int except,SudoNum value)
		{
			for (int i = 0; i < 9; i++) {
				if(i==except)continue;
				pl[row,i].CandidateNumber.Remove(value);
			}
		}
		public static void ColumnExpel(ref Panel pl,int column,int except,SudoNum value)
		{
			for (int i = 0; i < 9; i++) {
				if(i==except)continue;
				pl[i,column].CandidateNumber.Remove(value);
			}
		}
		public static void RegionExpel(ref Panel pl,int region,int except,SudoNum value)
		{
			for (int i = 0; i < 9; i++) {
				if(i==except)continue;
				pl[(region%3)*3+i%3,(region/3)*3+i/3].CandidateNumber.Remove(value);
			}
		}
	}
}
