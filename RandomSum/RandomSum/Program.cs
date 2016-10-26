using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace RandomSum
{
	public static class RandomNumber
	{
		private static readonly RNGCryptoServiceProvider generator = new RNGCryptoServiceProvider();

		public static int Between(int minimumValue, int maximumValue)
		{
			byte[] randomNumber = new byte[1];

			generator.GetBytes(randomNumber);

			double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

			// We are using Math.Max, and substracting 0.00000000001, 
			// to ensure "multiplier" will always be between 0.0 and .99999999999
			// Otherwise, it's possible for it to be "1", which causes problems in our rounding.
			double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

			// We need to add one to the range, to allow for the rounding done with Math.Floor
			int range = maximumValue - minimumValue + 1;

			double randomValueInRange = Math.Floor(multiplier * range);

			return (int)(minimumValue + randomValueInRange);
		}
	}

	class MainClass
	{



		public static void Main(string[] args)
		{
			//SumRandomNumbers();

			//SumAllPossible();

			SumCryptRand();

		}

		public static void SumCryptRand()
		{
			int intLeft;
			int intRight;

			int sumWrong = 0;
			int sumCorrect = 0;
			int maxSum = 10;
			int randomMin = 1;

			Console.Write("Hoeveel sommen? ");
			int sumNr = Convert.ToInt32(Console.ReadLine());

			for (int i = 0; i < sumNr; i++)
			{
				intLeft = RandomNumber.Between(randomMin, maxSum);
				intRight = RandomNumber.Between(randomMin, maxSum - intLeft);

				Console.Write("{0} + {1} = ", intLeft, intRight);

				//int answer = Convert.ToInt32(Console.ReadLine());
				int answer = intLeft + intRight;

				if (intLeft + intRight == answer)
				{
					Console.WriteLine("GOED!");
					sumCorrect = sumCorrect + 1;
				}
				else
				{
					Console.WriteLine("FOUT!");
					sumWrong = sumWrong + 1;
				}
				Console.WriteLine("-------------");

			}

			Console.WriteLine("Alle {0} sommen voltooid!", sumNr);
			Console.WriteLine("Aantal goed: {0}", sumCorrect);
			Console.WriteLine("Aantal fout: {0}", sumWrong);

			Console.ReadKey();

		}


		public static void SumRandomNumbers()
		{
			//Random rand = new Random();
			Random rand = new Random(DateTime.Now.Millisecond);

			int intLeft;
			int intRight;

			int sumWrong = 0;
			int sumCorrect = 0;
			int maxSum = 10;
			int randomMin = 1;

			//int randomMultiplier = 100000;

			Console.Write("Hoeveel sommen? ");
			int sumNr = Convert.ToInt32(Console.ReadLine());

			for (int i = 0; i < sumNr; i++)
			{
				intLeft = rand.Next(randomMin, maxSum);
				//intLeft = rand.Next(randomMin, randomMax * randomMultiplier) / randomMultiplier;

				intRight = rand.Next(randomMin, maxSum - intLeft);

				Console.WriteLine("LEFT min {0} max {1} = ", randomMin, maxSum);
				Console.WriteLine("RIGHT min {0} max {1} = ", randomMin, maxSum - intLeft);
				Console.Write("{0} + {1} = ", intLeft, intRight);

				//int answer = Convert.ToInt32(Console.ReadLine());
				int answer = intLeft + intRight;

				if (intLeft + intRight == answer)
				{
					Console.WriteLine("GOED!");
					sumCorrect = sumCorrect + 1;
				}
				else
				{
					Console.WriteLine("FOUT!");
					sumWrong = sumWrong + 1;
				}
				Console.WriteLine("-------------");

			}

			Console.WriteLine("Alle {0} sommen voltooid!", sumNr);
			Console.WriteLine("Aantal goed: {0}", sumCorrect);
			Console.WriteLine("Aantal fout: {0}", sumWrong);

			Console.ReadKey();
			
		}

		public static void SumAllPossible()
		{
			int maxSum = 10;
			List<int> listLeft = new List<int>();
			List<int> listRight = new List<int>();
			int maxLeft = 10;
			int maxRight = 10;

			for (int i = 0; i < maxLeft; i++)
			{
				listLeft.Add(i);
			}
			for (int i = 0; i < maxRight; i++)
			{
				listRight.Add(i);
			}


			var listPoss = listLeft.SelectMany(l => listRight.Select(r => new { listLeft = l, listRight = r, sum = l + r }));

			var listMaxSum = listPoss.Where(s => s.sum <= maxSum && s.sum > 0);

			foreach (var entry in listMaxSum)
			{
				Console.WriteLine("{0}, {1}, {2}", entry.listLeft, entry.listRight, entry.sum);
			}
		}

	}
}
