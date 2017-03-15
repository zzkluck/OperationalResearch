using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zzkluck.CAUC.OperationalResearch
{
	public class SimplexAlorgithm
	{
		private static LaxForm Pivot(LaxForm LF,int l,int e)
		{
			int m = LF.A.GetLength(0);
			int n = LF.A.GetLength(1);
			double[,] a = LF.A;
			double[,] A2 = new double[m,n];
			double[] b2 = new double[m];
			double[] c2 = new double[n];
			
			b2[e] = LF.b[l] /a[l, e];
			foreach(int j in LF.Nonbasics)
			{
				if (j == e)
					continue;
				A2[e, j] =a[l, j] /a[l, e];
			}

			foreach(int i in LF.Basics)
			{
				if (i == l)
					continue;
				b2[i] = LF.b[i] -a[i, e] * b2[e];
				foreach(int j in LF.Nonbasics)
				{
					if (j == e)
						continue;
					A2[i, j] =a[i, j] -a[i, e] * A2[e, j];
				}
				A2[i, l] = -LF.A[i, e] * A2[e, l];
			}

			double v2 = LF.v + LF.c[e] * b2[e];
			foreach(int j in LF.Nonbasics)
			{
				if (j == e)
					continue;
				c2[j] = LF.c[j] - LF.c[e] * A2[e, j];
			}
			c2[l] = -LF.c[e] * A2[e, l];

			List<int> temp = LF.Nonbasics.Where((x) => x != e).ToList();
			temp.Add(l);
			int[] N2 = temp.ToArray();

			List<int> temp2 = LF.Basics.Where((x) => x != l).ToList();
			temp.Add(e);
			int[] B2 = temp.ToArray();
			return new LaxForm(N2, B2, A2, b2, c2, v2);
		}
	}

	public class LaxForm : Tuple<int[], int[], double[,], double[], double[], double>
	{
		public LaxForm(int[] Nonbasics, int[] Basics, double[,] A, double[] b, double[] c, double v)
			: base(Nonbasics,Basics,A,b,c,v)
		{}
		public int[] Nonbasics { get { return Item1; } }
		public int[] Basics { get { return Item2; } }
		public double[,] A { get { return Item3; } }
		public double[] b { get { return Item4; } }
		public double[] c { get { return Item5; } }
		public double v { get { return Item6; } }
	}
	public class StandardForm : Tuple<double[,], double[], double[]>
	{
		public StandardForm(double[,] A, double[] b, double[] c) 
			: base(A,b,c)
		{}
		public double[,] A { get { return Item1; } }
		public double[] b { get { return Item2; } }
		public double[] c { get { return Item3; } }
	}

}
