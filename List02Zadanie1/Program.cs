using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise02
{
    internal static class Program
    {
        /*
       *
       * First Additional function for this exercise
       *
       */

        private static void DrawChessBoard(string permutation)
        {
            for (int i = 0; i < permutation.Length; i++)
            {
                foreach (int numericValue in permutation.Select(t => (int)char.GetNumericValue(t)))
                {
                    Console.Write(numericValue == i ? " X " : " 0 ");
                }

                Console.WriteLine();
            }
        }

        /*
         *
         * Second Additional function for this exercise
         *
         */

        private static bool CheckSolution(string permutation)
        {
            for (int i = 0; i < permutation.Length; i++)
            {
                for (int m = i + 1; m < permutation.Length; m++)
                {

                    //check for same diagonal
                    int x = (int)char.GetNumericValue(permutation[i]);
                    int y = (int)char.GetNumericValue(permutation[m]);
                    if (Math.Abs(x - y) == Math.Abs(i - m))
                        return false;
                }
            }

            //return true if permutation is a solution
            return true;
        }

        private static List<string> CreateAllPermutationsOnLastOne(IEnumerable<int> listOfDigits)
        {
            string permutation = listOfDigits.Aggregate("", (current, i) => current + i.ToString());
            List<string> allPermutationsList = new List<string>();

            Permute(permutation, 0, permutation.Length - 1, allPermutationsList);
            return allPermutationsList;
        }

        private static void Permute(string str, int l, int r, ICollection<string> a)
        {
            if (l == r)
                a.Add(str);
            else
            {
                for (int i = l; i <= r; i++)
                {
                    str = Swap(str, l, i);
                    Permute(str, l + 1, r, a);
                    str = Swap(str, l, i);
                }
            }
        }

        private static string Swap(string a, int i, int j)
        {
            char[] charArray = a.ToCharArray();
            var temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;
            string s = new string(charArray);
            return s;
        }

        private static List<int> GenerateWithDuplicates(int numberOfDigits)
        {
            Random rnd = new Random();
            List<int> listOfDigits = new List<int>();
            for (int i = 0; i < numberOfDigits; i++)
            {
                int nextRandom = rnd.Next(numberOfDigits);
                listOfDigits.Add(nextRandom);
            }

            Console.WriteLine();
            return listOfDigits;
        }

        private static List<int> Generate(int numberOfDigits)
        {
            List<int> listOfDigits = new List<int>();
            for (int i = 0; i < numberOfDigits; i++)
            {
                listOfDigits.Add(i);
            }
            Console.WriteLine();
            return listOfDigits;
        }

        private static void Write(IEnumerable<string> list)
        {
            int c = 0;
            foreach (string item in list)
            {
                if (c % 11 == 0) Console.WriteLine();
                Console.Write($"{item}  ");
                c++;
            }
        }

        private static void Write(IEnumerable<int> list)
        {
            int c = 0;
            foreach (int item in list)
            {
                if (c % 11 == 0) Console.WriteLine();
                Console.Write($"{item}  ");
                c++;
            }
        }

        private static void Main()
        {
            try
            {
                //Variables
                // Start point of the App

                Console.Write("Please type number of digits to proceed permutation: ");
                string inputString = Console.ReadLine();
                int numberOfDigits = int.Parse(inputString ?? throw new InvalidOperationException());
                Console.Write($"You select: {numberOfDigits} numbers\n");
                Console.Write($"Select generator method:\n 1. Generate sequential numbers\n 2. Generate random numbers\n");
                char selector = Console.ReadKey().KeyChar;

                List<int> listOfDigits;
                switch (selector)
                {
                    case '1':
                        listOfDigits = Generate(numberOfDigits);
                        Console.Write($"Your numbers are: ");
                        Write(listOfDigits);
                        Console.WriteLine();
                        break;

                    case '2':
                        listOfDigits = GenerateWithDuplicates(numberOfDigits);
                        Console.Write($"Your numbers are: ");
                        Write(listOfDigits);
                        Console.WriteLine();
                        break;

                    default:
                        listOfDigits = null;
                        throw new Exception(" Next time press 1 or 2 on keyboard");
                }

                switch (numberOfDigits)
                {
                    case 0:
                        Console.WriteLine($"There is no numbers to permute");
                        break;

                    case 1:
                        Console.WriteLine($"Permutation of only one number equals this number, in this case this number is: {listOfDigits.ElementAt(0)}");
                        break;

                    default:
                        {
                            var watch = System.Diagnostics.Stopwatch.StartNew();
                            List<string> listOfAllPermutations = CreateAllPermutationsOnLastOne(listOfDigits);
                            watch.Stop();
                            float elapsedMs = watch.ElapsedMilliseconds;

                            Console.WriteLine($"Created {listOfAllPermutations.Count} permutations in {elapsedMs / 1000} sec.\n");
                            Console.Write($"Do You want to see them? Y/N");
                           
                            selector = Console.ReadKey().KeyChar;

                            if (selector == 'y')
                            {
                                Write(listOfAllPermutations);
                                Console.WriteLine();
                            }
                            else Console.WriteLine();

                            List<string> solutions = listOfAllPermutations.Where(CheckSolution).ToList();

                            Console.WriteLine($"Found {solutions.Count} solutions to {numberOfDigits} queens problem.\n");
                            if (solutions.Count > 0)
                            {
                                Console.Write($"Dou You want to see them? Y/N");

                                selector = Console.ReadKey().KeyChar;
                                if (selector == 'y')
                                {
                                    Write(solutions);
                                }
                                Console.WriteLine();

                                Console.Write($"\nDo You want to see chessboards? Y/N");

                                selector = Console.ReadKey().KeyChar;
                                Console.WriteLine();
                                if (selector == 'y')
                                {
                                    foreach (string sol in solutions)
                                    {
                                        Console.WriteLine(sol);
                                        DrawChessBoard(sol);
                                        Console.WriteLine('\n');
                                    }
                                }
                            }
                            Console.WriteLine();

                            break;
                        }
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