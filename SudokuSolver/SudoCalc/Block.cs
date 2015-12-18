﻿using System;

namespace SudokuSolver.SudoCalc
{
	/// <summary>
	/// Description of Block.
	/// </summary>
	public class Block
	{
		private Candidate cand= new Candidate();
		private SudoNum num = SudoNum.Unknown;
		public Block()
		{
			Stable=false;
		}
		public Block(SudoNum number):this()
		{
			Number=number;
		}
		public Block(SudoNum number,bool stable):this(number)
		{
			Stable=stable;
		}
		public SudoNum Number
		{
			get
			{
				return num;
			}
			set
			{
				if((int)value<=9&&(int)value>=0)num=value;
				else throw new InvalidOperationException();
				if((int)value!=0)cand= new Candidate(value);
			}
		}
		public bool Stable
		{
			get;
			set;
		}
		public Candidate CandidateNumber
		{
			get
			{
				return cand;
			}
			set
			{
				if(Stable)return;
				cand=value;
				if(cand.Count>1)num= SudoNum.Unknown;
			}
		}
	}
	
}