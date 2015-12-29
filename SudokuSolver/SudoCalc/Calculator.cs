using System;
using System.Collections.Generic;

namespace SudokuSolver.SudoCalc
{
    /// <summary>
    /// Description of Calculator.
    /// </summary>
    public static class Calculator
    {
        public static void ExpelCandidate(ref Panel pl)
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (pl[x, y].Number == SudoNum.Unknown) continue;
                    RowExpel(ref pl, y, x, pl[x, y].Number);
                    ColumnExpel(ref pl, x, y, pl[x, y].Number);
                    RegionExpel(ref pl, Panel.GetRegion(x, y), Panel.GetRegionNumber(x, y), pl[x, y].Number);
                }
            }
        }
        public static void FullExpel(ref Panel pl)
        {
        	foreach (Block item in pl) 
        	{
        		if(item.Number == SudoNum.Unknown)
        		{
        			item.CandidateNumber=new Candidate();
        		}
        	}
        	ExpelCandidate(ref pl);
        }
        public static void RowExpel(ref Panel pl, int row, int except, SudoNum value)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i == except) continue;
                pl[i, row].CandidateNumber.Remove(value);
            }
        }
        public static void ColumnExpel(ref Panel pl, int column, int except, SudoNum value)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i == except) continue;
                pl[column, i].CandidateNumber.Remove(value);
            }
        }
        public static void RegionExpel(ref Panel pl, int region, int except, SudoNum value)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i == except) continue;
                pl[Panel.GetX(region, i), Panel.GetY(region, i)].CandidateNumber.Remove(value);
            }
        }
        public static bool Filler(ref Panel pl)
        {
            bool changed = false;
            //check blocks
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (pl[x, y].Number == SudoNum.Unknown && pl[x, y].CandidateNumber.Count == 1)
                    {
                        SudoNum tmp = pl[x, y].CandidateNumber.GetNumbers()[0];
                        pl[x, y].Number = tmp;
                        RowExpel(ref pl, y, x, tmp);
                        ColumnExpel(ref pl, x, y, tmp);
                        RegionExpel(ref pl, Panel.GetRegion(x, y), Panel.GetRegionNumber(x, y), tmp);
                        changed = true;
                    }
                }
            }
            //check regions
            for (int r = 0; r < 9; r++)
            {
                int[] counter = new int[10];
                for (int n = 0; n < 9; n++)
                {
                    if (pl[Panel.GetX(r, n), Panel.GetY(r, n)].Number != SudoNum.Unknown)
                    {
                        continue;
                    }
                    foreach (SudoNum item in pl[Panel.GetX(r, n), Panel.GetY(r, n)].CandidateNumber.GetNumbers())
                    {
                        counter[(int)item]++;
                    }
                }
                for (int i = 1; i < 10; i++)
                {
                    if (counter[i] == 1)
                    {
                        for (int n = 0; n < 9; n++)
                        {
                            if (pl[Panel.GetX(r, n), Panel.GetY(r, n)].CandidateNumber.HasNumber((SudoNum)i))
                            {
                                pl[Panel.GetX(r, n), Panel.GetY(r, n)].Number = (SudoNum)i;
                                RowExpel(ref pl, Panel.GetY(r, n), Panel.GetX(r, n), (SudoNum)i);
                                ColumnExpel(ref pl, Panel.GetX(r, n), Panel.GetY(r, n), (SudoNum)i);
                                changed = true;
                            }
                        }
                    }
                }
            }
            return changed;
        }
        public static Panel[] FindAnswer(Panel pl)//TODO:HasSomeProb
        {
            Panel p = (Panel)pl.Clone();
            List<Panel> ls = new List<Panel>();
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (pl[x, y].Number == SudoNum.Unknown)
                    {
                        foreach (SudoNum item in pl[x, y].CandidateNumber.GetNumbers())
                        {
                            p[x, y].Number = item;
                            FullExpel(ref p);
                            if (p.IsFull())
                            {
                            	ls.Add(p);
                            }
                            //TODO:AddSomeThing
                            FindAnswer(p);
                        }
                    }
                }
            }
            return ls.ToArray();
        }
    }
}
