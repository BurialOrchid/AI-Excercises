using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise03
{
    internal class Program
    {
        private static void PrintSol(List<int> sol)
        {
            foreach (var t in sol)
                {
                    Console.Write(t);
                }
                Console.WriteLine();
        }

        private static int[] CreatePermutation(int numOfDigits, ref int numOfStates)
        {
            List<int> sol = new List<int>();
            while (sol.Count < numOfDigits)
            {
                sol.Add(0);

                PrintSol(sol);

                numOfStates++;
                while (!CheckSolution(sol.ToArray()))
                {
                    while (sol.ElementAt(sol.Count - 1) >= numOfDigits - 1)
                    {
                        sol.RemoveAt(sol.Count - 1);
                        PrintSol(sol);

                    }
                    if (sol.Count == 0)
                        return null;
                    else
                    {
                        sol[^1]++;

                        PrintSol(sol);

                    }
                    numOfStates++;
                }




            }

            return CheckSolution(sol.ToArray()) ? sol.ToArray() : null;
        }

        private static bool CheckSolution(int[] permutation)
        {
            for (int i = 0; i < permutation.Length; i++)
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
                    else if (permutation[i] == permutation[m]) return false;

                    //check for same diagonal
                    if (Math.Abs(permutation[i] - permutation[m]) == Math.Abs(i - m))
                        return false;
                }
            }

            //return true if permutation is a solution
            return true;
        }

        private static void DrawChessBoard(int[] permutation)
        {
            for (int i = 0; i < permutation.Length; i++)
            {
                foreach (var numericValue in permutation)
                {
                    Console.Write(numericValue == i ? " X " : " 0 ");
                }
                Console.WriteLine();
            }
        }

        private static void Main(string[] args)
        {
            try
            {
                Console.Write("Please type number of digits to proceed permutation: ");

                string inputString = Console.ReadLine();
                int numberOfDigits = int.Parse(inputString ?? throw new InvalidOperationException());
                Console.Write($"You select: {numberOfDigits} numbers\n");

                int[] solution = new int[numberOfDigits];
                int numOfStates = 0;
                var watch = System.Diagnostics.Stopwatch.StartNew();
                solution = CreatePermutation(numberOfDigits, ref numOfStates);
                watch.Stop();
                float elapsedMs = watch.ElapsedMilliseconds;

                Console.WriteLine($"Found solution in {elapsedMs / 1000} sec.\n");
                Console.Write($"Solution is: ");

                foreach (var t in solution)
                {
                    Console.Write($"{t}");
                }

                Console.WriteLine();

                Console.WriteLine($"Searched through {numOfStates} number of states");

                Console.WriteLine();
                DrawChessBoard(solution);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}