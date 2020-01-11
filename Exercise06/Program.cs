using System;
using System.Collections.Generic;

namespace Exercise06
{
    internal class Program
    {
        private static Mypermutation[] Populate(int permutationLength, int populationsize)
        {
            Mypermutation[] population = new Mypermutation[populationsize];

            for (int i = 0; i < populationsize; i++)
            {
                population[i] = new Mypermutation(permutationLength);
            }

            return population;
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

        private static double[] CalculateProbabilityOfSelection(Mypermutation[] population)
        {
            double sumOfAllNonAttackingPairs = 0;
            double[] probabilytyOfSelection = new double[population.Length];

            for (int i = 0; i < population.Length; i++)
            {
                population[i].CalculateUtility();
                sumOfAllNonAttackingPairs += population[i].utility;
            }
            for (int i = 0; i < population.Length; i++)
            {
                probabilytyOfSelection[i] = Math.Round(population[i].utility / sumOfAllNonAttackingPairs, 2);
            }
            return probabilytyOfSelection;
        }

        private static void CrossOver(int[] first, int[] second)
        {
            Random rnd = new Random();
            int crosspoint = rnd.Next(0, first.Length);
            int[] temp = new int[first.Length];

            Array.Copy(second, 0, temp, 0, first.Length);

            Array.Copy(first, 0, second, 0, crosspoint);

            Array.Copy(temp, 0, first, 0, crosspoint);
        }

        private static Mypermutation[] Crossbreed(Mypermutation[] population)
        {
            Array.Sort(population, delegate (Mypermutation x, Mypermutation y) { return x.utility.CompareTo(y.utility); });
            Array.Reverse(population);
            for (int i = 0; i < population.Length / 2; i += 2)
            {
                CrossOver(population[i].permutation, population[i + 1].permutation);
            }

            return population;
        }

        private static void Mutate(Mypermutation[] population)
        {
            Random rnd = new Random();

            for (int i = 0; i < population.Length; i++)
            {
                int newGene = rnd.Next(0, population.Length);
                int placeToSwap = rnd.Next(0, population.Length);
                population[i].permutation[placeToSwap] = newGene;
            }
        }

        private static bool CheckSolution(Mypermutation[] population)
        {
            foreach (Mypermutation item in population)
            {
                for (int i = 0; i < item.permutationLength; i++)
                {
                    for (int m = i + 1; m < item.permutationLength; m++)
                    {
                        //check for same row and diagonal
                        if (item.permutation[i] == item.permutation[m] || Math.Abs(item.permutation[i] - item.permutation[m]) == Math.Abs(i - m)) break;
                    }
                }
            }
            //return true if permutation is a solution
            return true;
        }

        private static void Main(string[] args)
        {
            int permutationLength = 8;
            int populationsize = 0;

            Console.Write("Select number of gueens to proceed Genetic algorithm solution : ");
            permutationLength = Convert.ToInt32(Console.ReadLine());
            Console.Write("Select population size : ");
            populationsize = Convert.ToInt32(Console.ReadLine());

            Mypermutation[] population = Populate(permutationLength, populationsize);

            double[] probabiltyofselection = CalculateProbabilityOfSelection(population);

            for (int i = 0; i < populationsize; i++)
            {
                Console.WriteLine(population[i].ToString() + "  " + population[i].utility + "  " + probabiltyofselection[i]);

                //DrawChessBoard(population[i].permutation);
            }
            Crossbreed(population);
            Mutate(population);
            CalculateProbabilityOfSelection(population);

            Console.WriteLine();
            for (int i = 0; i < populationsize; i++)
            {
                Console.WriteLine(population[i].ToString() + "  " + population[i].utility + "  " + probabiltyofselection[i]);

                //DrawChessBoard(population[i].permutation);
            }
            Console.ReadKey();
        }
    }
}