using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Exercise05
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

        private static Mypermutation[] FindBestPermutation(Mypermutation input)
        {
            //   Debug.WriteLine($"Input = {input.ToString()}");
            int bestutility = 0;
            List<Mypermutation> bestList = new List<Mypermutation>();
            List<Mypermutation> toRemove = new List<Mypermutation>();

            for (int i = 0; i < input.permutationLength; i++)
            {
                int moveUp = input.permutation[i];
                int moveDown = input.permutationLength - input.permutation[i] - 1;
                for (int j = 1; j <= moveUp; j++)
                {
                    input.permutation[i] = input.permutation[i] - j;

                    input.CalculateUtility();
                    //  Debug.WriteLine($"Column nr {i} muve u nr {j} = {input.ToString()} Utility: {input.utility}");
                    if (input.utility >= bestutility)
                    {
                        bestutility = input.utility;
                        Mypermutation better = new Mypermutation(input);
                        bestList.Add(better);
                    }
                    input.permutation[i] = input.permutation[i] + j;
                }

                for (int k = 1; k <= moveDown; k++)
                {
                    input.permutation[i] = input.permutation[i] + k;
                    input.CalculateUtility();
                    // Debug.WriteLine($"Column nr {i} muve d nr {k} = {input.ToString()} Utility: {input.utility}");
                    if (input.utility >= bestutility)
                    {
                        bestutility = input.utility;
                        Mypermutation better = new Mypermutation(input);
                        bestList.Add(better);
                    }
                    input.permutation[i] = input.permutation[i] - k;
                }
            }
            foreach (Mypermutation item in bestList)
            {
                if (item.utility < bestutility)
                {
                    toRemove.Add(item);
                }
            }

            foreach (Mypermutation item in toRemove)
            {
                bestList.Remove(item);
            }
            // Debug.WriteLine($"Num of tables {bestList.Count} Utility: {bestutility}");

            return bestList.ToArray();
        }

        private static void Main(string[] args)
        {
            int permutationLength = 8;
            // Console.Write("Select number of gueens to proceed Genetic algorithm solution : ");
            // permutationLength = Convert.ToInt32(Console.ReadLine());
            // Console.WriteLine();
            Mypermutation startPermutation = new Mypermutation(permutationLength);
            Console.WriteLine($"Starting permutation is {startPermutation.ToString()} with utility {startPermutation.utility}");

            Mypermutation[] listOfBest;
            int bestUtility = 0;
            int numofboards = 1;
            bool found = false;
            int numberOfTries = 10;
            int bestPosibleUtility = startPermutation.bestutility;

            Random rnd = new Random();
            var watch = Stopwatch.StartNew();

            listOfBest = FindBestPermutation(startPermutation);

            if (bestUtility == bestPosibleUtility)
            {
                Console.WriteLine($"Found Soution {listOfBest[0].ToString()} Searched in {numofboards} tables");
                found = true;
            }
            numberOfTries--;
            bestUtility = listOfBest[0].utility;

            while (bestUtility < bestPosibleUtility && numberOfTries >= 0)
            {
                bestUtility = listOfBest[0].utility;
                int choice = rnd.Next(0, listOfBest.Length);
                if (bestUtility == bestPosibleUtility)
                {
                    Console.WriteLine($"Found Soution {listOfBest[0].ToString()} Searched in {numofboards} tables");
                    found = true;
                    break;
                }
                listOfBest = FindBestPermutation(listOfBest[choice]);
                numofboards++;
                numberOfTries--;
            }

            watch.Stop();
            float elapsedMs = watch.ElapsedMilliseconds;

            if (found)
            {
                Console.WriteLine($"Found solution in {elapsedMs} ms.");

                Console.Write($"Solution is: {listOfBest[0].ToString()}");
                Console.WriteLine("\n");
                Console.WriteLine($"Drawing chessboard...");
                DrawChessBoard(listOfBest[0].permutation);
            }
            else
            {
                Console.WriteLine($"Didn't found solution, local minimum, or excided number of tries Utility to get {bestPosibleUtility}");
                Console.WriteLine($"Best found permutation {listOfBest[0].ToString()} with utility of {listOfBest[0].utility}");
            }
            Console.ReadKey();
        }
    }
}