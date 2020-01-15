using System;
using System.Collections.Generic;
using System.Linq;

//Krzysztof Mroziewicz
namespace Exercise03
{
    internal class Program
    {
        private static void PrintSol(int[] sol)
        {
            foreach (var t in sol)
            {
                Console.Write(t);
            }
            Console.WriteLine();
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

        private static int[] CreatePermutation(int numOfDigits, ref int numOfStates)
        {
            List<int> sol = new List<int>();
            while (sol.Count < numOfDigits)
            {
                sol.Add(0);

                PrintSol(sol.ToArray());

                numOfStates++;
                while (!CheckSolution(sol.ToArray()))
                {
                    while (sol.ElementAt(sol.Count - 1) >= numOfDigits - 1)
                    {
                        sol.RemoveAt(sol.Count - 1);
                        PrintSol(sol.ToArray());
                    }
                    if (sol.Count == 0)
                        return null;
                    else
                    {
                        sol[^1]++;

                        // PrintSol(sol);
                    }
                    numOfStates++;
                }
            }

            return CheckSolution(sol.ToArray()) ? sol.ToArray() : null;
        }

        private static bool CheckSolution(int[] permutation)
        {
            for (int i = 1; i < permutation.Length; i++)
            {
                for (int m = i - 1; m >= 0; m--)
                {
                    //check for same row and diagonal
                    if (permutation[i] == permutation[m] || Math.Abs(permutation[i] - permutation[m]) == Math.Abs(i - m)) return false;
                }
            }

            //return true if permutation is a solution
            return true;
        }

        private static int[] MRV(int size)
        {
            int[] sol = new int[size];
            sol[0] = returnMRV(sol, 0);
            sol[1] = returnMRV(sol, 1);
            sol[2] = returnMRV(sol, 2);
            sol[3] = returnMRV(sol, 3);
            sol[4] = returnMRV(sol, 4);
            sol[5] = returnMRV(sol, 5);
            sol[6] = returnMRV(sol, 6);
            sol[7] = returnMRV(sol, 7);
            PrintSol(sol);
            return null;
        }

        private static int returnMRV(int[] perm, int nextindex)
        {
            List<int> remaining = new List<int>();
            for (int i = 0; i < perm.Length; i++)
            {
                if (!perm.Contains(i))
                    remaining.Add(i);
            }

            for (int i = 0; i < nextindex; i++)
            {
                for (int m = 0; m < perm.Length; m++)
                {
                    if (Math.Abs(perm[i] - m) == Math.Abs(i - nextindex)) remaining.Remove(m);
                }
            }
            if (remaining.Count > 0)
                return remaining.ElementAt(0);
            return 0;
        }

        private static void Main(string[] args)
        {
            MRV(8);

            #region backtrack

            //try
            //{
            //    Console.Write("Please type number of digits to proceed permutation: ");

            //    //string inputString = Console.ReadLine();
            //    int numberOfDigits = 8;//int.Parse(inputString ?? throw new InvalidOperationException());
            //    Console.Write($"You select: {numberOfDigits} numbers\n");

            //    int[] solution = new int[numberOfDigits];
            //    int numOfStates = 0;
            //    var watch = System.Diagnostics.Stopwatch.StartNew();
            //    solution = CreatePermutation(numberOfDigits, ref numOfStates);
            //    watch.Stop();
            //    float elapsedMs = watch.ElapsedMilliseconds;

            //    Console.WriteLine($"Found solution in {elapsedMs / 1000} sec.\n");
            //    Console.Write($"Solution is: ");

            //    foreach (var t in solution)
            //    {
            //        Console.Write($"{t}");
            //    }

            //    Console.WriteLine();

            //    Console.WriteLine($"Searched through {numOfStates} number of states");

            //    Console.WriteLine();
            //    DrawChessBoard(solution);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            #endregion backtrack

            Console.ReadKey();
        }
    }
}