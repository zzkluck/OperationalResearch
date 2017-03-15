using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			long[] result = SumDigPow(1, 100);
			foreach (long  i in result)
			{
				Console.WriteLine(i);
			}
			Console.Read();
		}


		public static string DuplicateEncode(string word)
		{
			//使用精心优化过（可能也没多精心）的算法，对于word="faggahghdafhdhhdgasfdgdfdgsah"，循环100000次，用时0.43秒
			//对于一个随机长字符串（length=1000000），该算法用时0.9秒
			string lowerWord = word.ToLower();
			Hashtable isDisplayed = new Hashtable();
			HashSet<char> isDuplicated = new HashSet<char>();
			StringBuilder sb = new StringBuilder(word.Length);

			for (int i = 0; i < lowerWord.Length; i++)
			{
				char c = lowerWord[i];
				if (isDuplicated.Contains(c))
				{
					sb.Append(')');
				}
				else
				{
					if (isDisplayed.ContainsKey(c))
					{
						isDuplicated.Add(c);
						sb[(int)isDisplayed[c]] = ')';
						sb.Append(')');
					}
					else
					{
						isDisplayed.Add(c, i);
						sb.Append('(');
					}
				}
			}
			return sb.ToString();

			//使用这个强大的语句，对于word="faggahghdafhdhhdgasfdgdfdgsah"，循环100000次，用时2.39秒
			//对于一个随机的较长字符串（length=10000），该算法用时2秒
			//return new string(word.ToLower().Select(ch => word.ToLower().Count(innerCh => ch == innerCh) == 1 ? '(' : ')').ToArray());
		}
		public static string Longest(string s1, string s2)
		{
			HashSet<char> letterDisplayed = new HashSet<char>();
			foreach (char c in s1 + s2)
			{
				letterDisplayed.Add(c);
			}
			StringBuilder sb = new StringBuilder(26);
			for (char c = 'a'; c <= 'z'; c++)
			{
				if (letterDisplayed.Contains(c))
				{
					sb.Append(c);
				}
			}
			return sb.ToString();
			//return new string((s1 + s2).Distinct().OrderBy(x => x).ToArray());
		}
		public static List<int> RemoveSmallest(List<int> n)
		{
			//List<int> result = new List<int>();
			//int min = int.MaxValue;
			//int minIndex = -1;

			//for(int i = 0; i < n.Count; i++)
			//{
			//	if (n[i] < min)
			//	{
			//		min = n[i];
			//		minIndex = i;
			//	}
			//}
			//if (minIndex != -1)
			//{
			//	result=n.ToList();
			//	result.RemoveAt(minIndex);
			//}
			//return n;

			return n.Where((x, i) => i != n.IndexOf(n.Min())).ToList();
		}
		public static int DigitalRoot(long n)
		{
			//Your awesome code here!
			//if (n / 10 == 0)
			//	return (int)n;

			//long sum = 0;
			//while (n / 10 != 0)
			//{
			//	sum += n % 10;
			//	n /= 10;
			//}
			//sum += n;
			//return DigitalRoot(sum);

			//↓↓↓↓↓↓	Amazing！！！	↓↓↓↓↓↓↓↓↓
			return (int)(1 + (n - 1) % 9);
		}
		public static int GetSum(int a, int b)
		{
			//Good Luck!
			//if (Math.Abs(a) < Math.Abs(b))
			//{
			//	int temp = a;
			//	a = b;
			//	b = temp;
			//}
			//if (a == 0) return 0;
			//int AbsLargerSign = Math.Abs(a) / a;
			//int absA = Math.Abs(a), absB = Math.Abs(b);
			//int sum = (absA * absA - absB * absB + absA - absB) / 2 * AbsLargerSign;
			//if (a * b > 0)
			//{
			//	sum += b;
			//}
			//return sum;
			return (a + b) * (Math.Abs(a - b) + 1) / 2;
		}
		public static int[] DeleteNth(int[] arr, int x)
		{
			// ...
			//Dictionary<int, int> occurTimes = new Dictionary<int, int>();
			//List<int> result = new List<int>();
			//for (int i = 0; i < arr.Length; i++)
			//{
			//	int nowNum = arr[i];
			//	if (occurTimes.ContainsKey(nowNum) && occurTimes[nowNum] < x)
			//	{
			//		result.Add(nowNum);
			//		occurTimes[nowNum] += 1;
			//	}
			//	else if (!occurTimes.ContainsKey(nowNum))
			//	{
			//		result.Add(nowNum);
			//		occurTimes.Add(nowNum, 1);
			//	}
			//}
			//return result.ToArray();

			var cache = new Dictionary<int, int>();
			return arr.Where(n => {
				int count = cache.ContainsKey(n) ? cache[n] : 0;
				cache[n] = count + 1;
				return cache[n] <= x;
			}).ToArray();

		}
		public static long[] SumDigPow(long a, long b)
		{
			// your code
			return Enumerable.Range((int)a, (int)(b - a + 1)).Where(
				n =>n.ToString().Select((c, i) => Math.Pow(char.GetNumericValue(c), i+1)).Sum() == n
			).Select(n => (long)n).ToArray();
		}
		public static string PigIt(string str)
		{
			//StringBuilder result = new StringBuilder(str.Length * 2);
			//var Words = str.Split(' ').Select(s =>
			//  {
			//	  StringBuilder sb = new StringBuilder(s, s.Length + 2);
			//	  string postfix = sb[0] + "ay";
			//	  sb.Remove(0, 1);
			//	  sb.Append(postfix);
			//	  return sb.ToString();
			//  });

			//foreach(string s in Words)
			//{
			//	result.Append(s);
			//	result.Append(' ');
			//}
			//return result.ToString().TrimEnd(' ');

			//solution 1:
			return string.Join(" ", str.Split().Select(x => x.Substring(1) + x[0] + "ay"));
			//solution 2:
			return Regex.Replace(str, "(?<=^| )(\\w)(\\w+)", m => m.Groups[2].Value + m.Groups[1].Value + "ay");

		}
	}
}
