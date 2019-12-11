using System;

namespace List03Zadanie1
{
    class Program
    {
        private static int[] CreatePermutation(int[] state)
        {

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


        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                Console.Write("Please type number of digits to proceed permutation: ");

                string inputString = Console.ReadLine();
                int numberOfDigits = int.Parse(inputString ?? throw new InvalidOperationException());
                Console.Write($"You select: {numberOfDigits} numbers\n");

                int[] solution = new int[numberOfDigits];

                var watch = System.Diagnostics.Stopwatch.StartNew();               
                solution = CreatePermutation(solution);
                watch.Stop();
                float elapsedMs = watch.ElapsedMilliseconds;

                Console.WriteLine($"Found solution in {elapsedMs / 1000} sec.\n");
                Console.Write($"Solution is: ");

                for (int i = 0; i < solution.Length; i++)
                {
                    Console.Write($"{i}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
