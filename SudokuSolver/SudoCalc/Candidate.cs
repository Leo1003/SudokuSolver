﻿using System;
using System.Collections.Generic;

namespace SudokuSolver.SudoCalc
{
	/// <summary>
	/// Description of Candidate.
	/// </summary>
	public class Candidate
	{
		private bool[] nums = new bool[10];
		public Candidate()
		{
			for (int i = 1; i < 10; i++) 
			{
				nums[i]=true;
			}
		}
		public Candidate(params SudoNum[] num)
		{
			foreach (SudoNum i in num) 
			{
				nums[(int)i]=true;
			}
		}
		public SudoNum[] GetNumbers()
		{
			SudoNum[] n = new SudoNum[Count];
			int s=0;
			for (int i = 1; i < 10; i++) 
			{
				if(nums[i])n[s]=(SudoNum)i;
			}
			return n;
		}
		public void Add(SudoNum n)
		{
			nums[(int)n]=true;
		}
		public void Remove(SudoNum n)
		{
			nums[(int)n]=false;
		}
		public int Count
		{
			get
			{
				int c=0;
				for (int i = 1; i < 10; i++) {
					if(nums[i])c++;
				}
				return c;
			}
		}
	}
}
