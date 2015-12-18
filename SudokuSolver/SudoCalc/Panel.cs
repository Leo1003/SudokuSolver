﻿using System;

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
		
	}
}