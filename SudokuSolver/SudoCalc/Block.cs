using System;

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
		}
		public SudoNum Number
		{
			get
			{
				return num;
			}
			set
			{
				if(value<=9&&value>=0)num=(SudoNum)value;
				else throw new InvalidOperationException();
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
			}
		}
	}
	
}
