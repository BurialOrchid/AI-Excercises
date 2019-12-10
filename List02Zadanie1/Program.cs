using System;
using System.Collections.Generic;
using System.Linq;

namespace List02Zadanie1
{
    internal class Program
    {
        /*
       *
       * First Additional function for this exercise
       *
       */

        private static void DrawChessBoard(int[] permutation)
        {
            for (int i = 1; i <= permutation.Length; i++)
            {
                for (int j = 1; j <= permutation.Length; j++)
                {
                    if (permutation[i - 1] == j) Console.Write(" X ");
                    else Console.Write(" 0 ");
                }
                Console.WriteLine();
            }
        }

        /*
         *
         * Second Additional function for this exercise
         *
         */

        private static bool CheckSolution(int[] permutation)
        {
            for (int i = 1; i < permutation.Length; i++)
            {
                for (int m = i + 1; m < permutation.Length; m++)
                {
                    /*check if they stay in the same line
                     *
                     * permutation[i] same as letter 'j' on wiki
                     * permutation[m] same as letter 'n' on wiki
                    */

                    //check for same column
                    if (i == m) return false;

                    //check for same row
                    if (permutation[i - 1] == permutation[m - 1]) return false;

                    //check for same diagonal
                    if (Math.Abs(permutation[i - 1] - permutation[m - 1]) == Math.Abs(i - m))
                        return false;
                }
            }

            //return true if permutation is a solution
            return true;
        }

        private static int[] CreateAllPermutations(List<int> listOfDigits)
        {
            List<string> previousSequence = new List<string>();
            List<string> nextSequence = new List<string>();

            previousSequence.Add(listOfDigits.ElementAt(0).ToString());

            for (int i = 1; i < listOfDigits.Count; i++)
            {
                nextSequence.Clear();
                foreach (string item in previousSequence)
                {
                    int positionCounter = 0;
                    while (positionCounter <= item.Length)
                    {
                        string newString = new string(item);
                        newString = newString.Insert(positionCounter, listOfDigits.ElementAt(i).ToString());
                        if (!nextSequence.Contains(newString))
                            nextSequence.Add(newString);
                        positionCounter++;
                    }
                    previousSequence = new List<string>(nextSequence);
                }
            }
            List<int> intlist = nextSequence.Select(s => int.Parse(s)).ToList();
            return intlist.ToArray();
        }

        private static List<int> RandomNumberGenerator(int numberOfDigits)
        {
            Random rnd = new Random();
            List<int> listOfDigits = new List<int>();
            for (int i = 0; i < numberOfDigits; i++)
            {
                int nextrand = rnd.Next(numberOfDigits) + 1;
                //Uncomment if you don't want your digits to be repeated
                while (listOfDigits.Contains(nextrand))
                {
                    nextrand = rnd.Next(numberOfDigits) + 1;
                }
                listOfDigits.Add(nextrand);
                Console.Write($"{listOfDigits.ElementAt(i)} ");
            }
            Console.WriteLine();
            return listOfDigits;
        }

        private static void Write(int[] list)
        {
            int c = 0;
            foreach (int item in list)
            {
                if (c % 11 == 0) Console.WriteLine();
                Console.Write($"{item}  ");
                c++;
            }
        }

        public static int[] IntegerToArray(int n)
        {
            if (n == 0) return new int[1] { 0 };

            var digits = new List<int>();

            for (; n != 0; n /= 10)
                digits.Add(n % 10);

            var arr = digits.ToArray();
            Array.Reverse(arr);
            return arr;
        }

        private static void Main(string[] args)
        {
            Console.Write("Please type number of digits to proceed permutation: ");
            try
            {
                string inputString = Console.ReadLine();
                int numberOfDigits = int.Parse(inputString ?? throw new InvalidOperationException());
                Console.Write($"You select: {numberOfDigits} numbers: ");

                var listOfDigits = RandomNumberGenerator(numberOfDigits);

                if (numberOfDigits == 0)
                {
                    Console.WriteLine($"There is no numbers to permute");
                }
                else if (numberOfDigits == 1)
                {
                    Console.WriteLine($"Permutation of only one number equals this number, in this case this number is: {listOfDigits.ElementAt(0)}");
                }
                else if (numberOfDigits > 1)
                {
                    int[] listOfAllPermutations = CreateAllPermutations(listOfDigits);

                    Write(listOfAllPermutations);

                    foreach (int item in listOfAllPermutations)
                    {
                        int[] test = IntegerToArray(item);
                        string ids = String.Join("", test.Select(p => p.ToString()).ToArray());
                        if (CheckSolution(test))
                        {
                            Console.WriteLine("\n------------------------");
                            Console.WriteLine($"{ids} is a solution to problem!\n");
                            DrawChessBoard(test);
                            Console.WriteLine("\n\n");
                        };
                    }
                }
                else
                {
                    throw new Exception("Something went terribly wrong!");
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            Console.ReadLine();
        }
    }
}