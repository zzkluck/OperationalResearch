using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class SecondaryRadarCode
	{
		public static string GrayCode2Bin(string grayCode)
		{
			if (grayCode.Where((n) => n != '0' && n != '1').Count() != 0)
				throw new ArgumentException("Error Code");

			if(grayCode[0]!='0')
				throw new ArgumentException("Error Code Format : maybe too much");
			StringBuilder BinaryCode = new StringBuilder(grayCode.Length);

			BinaryCode.Append(grayCode[0]);
			for (int i = 1; i < grayCode.Length; i++)
			{
				BinaryCode.Append(BinaryCode[i-1] == grayCode[i] ? '0' : '1');
			}
			return BinaryCode.ToString();
		}
		private static void CheckCode(string Code)
		{
			if (Code.Length != 12)
				throw new ArgumentException("Error Code : error length");
			if (Code.Where((n) => n != '0' && n != '1').Count() != 0)
				throw new ArgumentException("Error Code");
		}
		private static void CheckFourWordCode(string FourWordCode)
		{
			if (FourWordCode.Length != 4)
				throw new ArgumentException("Error FourWordCode code : error length");
			if (FourWordCode.Where((n) => n < '0' || n > '7').Count() != 0)
				throw new ArgumentException("Error FourWordCode code");
		}
		public static string ExplainCodeA(string CodeA)
		{
			CheckCode(CodeA);

			Func<int, string> temp = delegate (int i)
			 {
				 return ((CodeA[i + 4] - '0') * 4 + (CodeA[i + 2] - '0') * 2 + (CodeA[i]) - '0').ToString();
			 };
			return temp(1) + temp(6) + temp(0) + temp(7);
		}
		public static string GetCodeA(string FourWordCode)
		{
			CheckFourWordCode(FourWordCode);

			int WordA = FourWordCode[0] - '0';
			int WordB = FourWordCode[1] - '0';
			int WordC = FourWordCode[2] - '0';
			int WordD = FourWordCode[3] - '0';

			Func<int, int, char> temp = delegate (int word, int offset)
				{
					return (word & offset) == 0 ? '0' : '1';
				};

			StringBuilder CodeA = new StringBuilder(12);
			CodeA.Append(temp(WordC, 1)).Append(temp(WordA, 1)).Append(temp(WordC, 2)).Append(temp(WordA, 2))
				.Append(temp(WordC, 4)).Append(temp(WordA, 4)).Append(temp(WordB, 1)).Append(temp(WordD, 1)).Append(temp(WordB, 2))
				.Append(temp(WordD, 2)).Append(temp(WordB, 4)).Append(temp(WordD, 4));
			return CodeA.ToString();

		}
		public static string ExplainCodeC(string CodeC)
		{
			CheckCode(CodeC);

			throw new NotImplementedException();
		}

	}
}
