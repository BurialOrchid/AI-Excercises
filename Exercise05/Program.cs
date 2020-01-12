using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercise05
{
    class Program
    {
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

        static Mypermutation FindBestPermutation(Mypermutation input)
        {
            int bestutility = 0;
            Mypermutation best = null;
            for (int i = 0; i < input.permutationLength; i++)
            {
                int moveUp = input.permutation[i];
                int moveDown = input.permutationLength - input.permutation[i];
                for (int j = 0; j < moveUp; j++)
                {
                    input.permutation[i] = input.permutation[i] - j;
                    input.CalculateUtility();
                    if (input.utility >= bestutility)
                    {
                        bestutility = input.utility;
                        best = new Mypermutation(input);
                    }
                    input.permutation[i] = input.permutation[i] +j;
                }
                for (int k = 0; k < moveDown; k++)
                {
                    input.permutation[i] = input.permutation[i] + k;
                    input.CalculateUtility();
                    if (input.utility >= bestutility)
                    {
                        bestutility = input.utility;
                        best = new Mypermutation(input);
                    }
                    input.permutation[i] = input.permutation[i] - k;
                }
            }
            return best;
        }
        static void Main(string[] args)
        {
            int permutationLength = 8;
            bool foundSolution = false;

            // Console.Write("Select number of gueens to proceed Genetic algorithm solution : ");
            // permutationLength = Convert.ToInt32(Console.ReadLine());
            // Console.WriteLine();
            Mypermutation mypermutation = new Mypermutation(permutationLength);
            int[] lasttenbest = new int[10];
            int index = 0;
            bool mybreak = false;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"For {i} permutation, utility is {mypermutation.utility}");
                mypermutation = FindBestPermutation(mypermutation);
                if (mypermutation.utility == 28) break;
                lasttenbest[index] = mypermutation.utility;
                for (int j = 0; j < 10; j++)
                {
                    if (lasttenbest[j] != lasttenbest[0]) mybreak = true;
                    break;
                }
                if (mybreak) break;
                index++;
                if (index == 10) index = 0;
            }

            watch.Stop();
            float elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Found solution in {elapsedMs} ms.");
           
            Console.Write($"Solution is: ");
            for (int i = 0; i < mypermutation.permutationLength; i++)
            {
                Console.Write(mypermutation.permutation[i]);
            }
            Console.WriteLine("\n");
            Console.WriteLine($"Drawing chessboard...");
            DrawChessBoard(mypermutation.permutation);
        }
    }
}
