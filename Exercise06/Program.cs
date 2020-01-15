using System;
using System.Collections.Generic;

//Krzysztof Mroziewicz
namespace Exercise06
{
    internal class Program
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

        private static bool CheckSolution(int[] permutation)
        {
            for (int i = 0; i < permutation.Length; i++)
            {
                for (int m = i + 1; m < permutation.Length; m++)
                {
                    //check for same row and diagonal
                    if (permutation[i] == permutation[m] || Math.Abs(permutation[i] - permutation[m]) == Math.Abs(i - m)) return false;
                }
            }

            //return true if permutation is a solution
            return true;
        }

        private static void Main(string[] args)
        {
            int permutationLength = 8;
            int populationsize = 30;
            bool foundSolution = false;
            int generation = 1;
            int solutionindex = -1;

            Console.Write("Select number of gueens to proceed Genetic algorithm solution : ");
            permutationLength = Convert.ToInt32(Console.ReadLine());
            Console.Write("Select population size : ");
            populationsize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Population population = new Population(permutationLength, populationsize);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            while (!foundSolution)
            {
                for (int i = 0; i < population.populationSize; i++)
                {
                    if (CheckSolution(population.populationarray[i].permutation))
                    {
                        foundSolution = true;
                        solutionindex = i;
                        break;
                    }
                }
                if (foundSolution) break;
                else
                {
                    population.Crossbreed();
                    generation++;
                }
            }
            watch.Stop();
            float elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Found solution in {elapsedMs} ms.");
            Console.WriteLine($"Found solution in {generation} generation.");
            Console.Write($"Solution is: ");
            for (int i = 0; i < permutationLength; i++)
            {
                Console.Write(population.populationarray[solutionindex].permutation[i]);
            }
            Console.WriteLine("\n");
            Console.WriteLine($"Drawing chessboard...");
            DrawChessBoard(population.populationarray[solutionindex].permutation);
            Console.ReadKey();
        }
    }
}