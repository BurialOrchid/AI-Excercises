using System;
using System.Collections.Generic;
using System.Linq;

//Mroziewicz Krzysztof
namespace Exercise01
{
    internal static class Program
    {
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

        private static void Write(List<string> list)
        {
            int c = 0;
            foreach (string item in list)
            {
                if (c % 11 == 0) Console.WriteLine();
                Console.Write($"{item}  ");
                c++;
            }
        }

        private static void Write(List<int> list)
        {
            int c = 0;
            foreach (int item in list)
            {
                if (c % 11 == 0) Console.WriteLine();
                Console.Write($"{item}  ");
                c++;
            }
        }

        private static List<string> CreatePermutationsSwap(List<int> listOfDigits)
        {
            string permutation = listOfDigits.Aggregate("", (current, i) => current + i.ToString());
            List<string> allPermutationsList = new List<string>();

            Permute(permutation, 0, permutation.Length - 1, allPermutationsList);
            return allPermutationsList;
            ;
        }

        private static void Permute(string str, int l, int r, List<string> a)
        {
            if (l == r && !a.Contains(str))
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

        private static List<string> CreatePermutationsInsert(List<int> listOfDigits)
        {
            //
            // This is main part of the program - Algorithm
            //

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

            return nextSequence;
        }

        private static void Main(string[] args)
        {
            //Variables
            // Start point of the App

            Console.Write("Please type number of digits to proceed permutation: ");

            string inputString = Console.ReadLine();
            int numberOfDigits = int.Parse(inputString ?? throw new InvalidOperationException());

            Console.Write($"You select: {numberOfDigits} numbers\n");
            Console.Write($"Select generator method:\n 1. Generate sequential numbers\n 2. Generate random numbers\n");
            char selector = Console.ReadKey().KeyChar;
            try
            {
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
                            Console.Write(
                                $"Select permutation method:\n 1. Switch two digits \n 2. Insert next digit to array\n");
                            char selector2 = Console.ReadKey().KeyChar;
                            Console.WriteLine($"\n Im working... It may take a while...\n");
                            var watch = System.Diagnostics.Stopwatch.StartNew();
                            List<string> permutations = null;
                            switch (selector2)
                            {
                                case '1':

                                    permutations = CreatePermutationsSwap(listOfDigits);
                                    break;

                                case '2':

                                    permutations = CreatePermutationsInsert(listOfDigits);
                                    break;

                                default:
                                    throw new Exception(" Next time press 1 or 2 on keyboard");
                            }

                            watch.Stop();
                            float elapsedMs = watch.ElapsedMilliseconds;
                            if (permutations != null)
                            {
                                Console.WriteLine($"Created permutations in {elapsedMs / 1000} sec.\n");

                                Console.Write($"Dou You want to see them? Y/N \n");
                                selector = Console.ReadKey().KeyChar;
                                if (selector == 'y')
                                    Write(permutations);
                            }
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