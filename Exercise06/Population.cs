using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise06
{
    class Population
    {
        public Mypermutation[] populationarray;
        private readonly Random rnd;
        private readonly double[] probabilityOfSelection;
        public int populationSize;

        public Population(int permutationLength, int populationSize)
        {
            populationarray = Populate(permutationLength, populationSize);
            this.populationSize = populationSize;
            rnd = new Random();
            probabilityOfSelection = new double[populationSize];
            SortPopulation();
        }

        private Mypermutation[] Populate(int permutationLength, int populationsize)
        {
            Mypermutation[] population = new Mypermutation[populationsize];

            for (int i = 0; i < populationsize; i++)
            {
                population[i] = new Mypermutation(permutationLength);
            }
            return population;
        }

        public void Writeperm()
        {
            Console.WriteLine("----------");
            for (int i = 0; i < populationSize; i++)
            {
                Console.WriteLine($"{populationarray[i].ToString()}  {populationarray[i].utility}  {probabilityOfSelection[i]}");
            }
            Console.WriteLine();
        }

        public void SortPopulation()
        {
            CalculateProbabilityOfSelection();
            Array.Sort(populationarray, delegate (Mypermutation x, Mypermutation y) { return x.utility.CompareTo(y.utility); });
            Array.Reverse(populationarray);
        }

        public void CalculateProbabilityOfSelection()
        {
            double sumOfAllNonAttackingPairs = 0;

            for (int i = 0; i < populationSize; i++)
            {
                populationarray[i].CalculateUtility();
                sumOfAllNonAttackingPairs += populationarray[i].utility;
            }
            for (int i = 0; i < populationSize; i++)
            {
                probabilityOfSelection[i] = Math.Round(populationarray[i].utility / sumOfAllNonAttackingPairs * 100, 2);
            }
        }

        private void CrossOver(Mypermutation first, Mypermutation second)
        {
            int crosspoint = rnd.Next(0, first.permutationLength);
            int[] temp = new int[first.permutationLength];

            Array.Copy(second.permutation, 0, temp, 0, first.permutationLength);

            Array.Copy(first.permutation, 0, second.permutation, 0, crosspoint);

            Array.Copy(temp, 0, first.permutation, 0, crosspoint);
        }

        public void Crossbreed()
        {
            CalculateProbabilityOfSelection();
            int sum = 0;
            int choice;
            int index1 = -1;
            int index2 = -1;
            foreach (Mypermutation item in populationarray)
            {
                sum += item.utility;
            }
            for (int j = 0; j < populationSize; j ++)
            {
                choice = rnd.Next(0, sum);

                for (int i = 0; i < populationSize; i++)
                {
                    if (choice < populationarray[i].utility)
                    {
                        index1 = i;
                        break;
                    }
                    choice -= populationarray[i].utility;
                }
                do
                {
                    choice = rnd.Next(0, sum);
                    for (int i = 0; i < populationSize; i++)
                    {
                        if (choice < populationarray[i].utility)
                        {
                            index2 = i;
                            break;
                        }
                        choice -= populationarray[i].utility;
                    }
                }
                while (index1 == index2);

                CrossOver(populationarray[index1], populationarray[index2]);
            }

            Mutate();
        }

        public void Mutate()
        {
            int newGene, placeToSwap;

            for (int i = 0; i < populationSize; i++)
            {
                newGene = rnd.Next(0, populationarray[i].permutationLength);
                placeToSwap = rnd.Next(0, populationarray[i].permutationLength);
                populationarray[i].permutation[placeToSwap] = newGene;
            }
        }
    }
}
