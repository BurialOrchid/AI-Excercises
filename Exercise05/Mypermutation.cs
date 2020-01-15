using System;
using System.Collections.Generic;
using System.Text;

//Krzysztof Mroziewicz
namespace Exercise05
{
    internal class Mypermutation
    {
        public int[] permutation;
        public int permutationLength;
        private readonly Random rnd;
        public int utility;
        public int bestutility;

        public Mypermutation(Mypermutation item)
        {
            int[] newperm = new int[item.permutationLength];
            Array.Copy(item.permutation, 0, newperm, 0, item.permutationLength);
            this.permutation = newperm;
            this.permutationLength = item.permutationLength;
            CalculateUtility();
            rnd = new Random();
        }

        public Mypermutation(int size)
        {
            permutationLength = size;
            List<int> permutation = new List<int>();
            rnd = new Random();
            do
            {
                int nextrnd = rnd.Next(0, permutationLength);
                permutation.Add(nextrnd);
                //if (!permutation.Contains(nextrnd)) permutation.Add(nextrnd);
            } while (permutation.Count < permutationLength);
            this.permutation = permutation.ToArray();

            CalculateUtility();
        }

        override public string ToString()
        {
            string output = "";

            for (int i = 0; i < permutationLength; i++)
            {
                output += permutation[i].ToString();
            }

            return output;
        }

        public void CalculateUtility()
        {
            int notattacked = 0; ;
            for (int i = 0; i < permutationLength; i++)
            {
                for (int m = i + 1; m < permutationLength; m++)
                {
                    //check for same row and diagonal
                    if (!(permutation[i] == permutation[m] || Math.Abs(permutation[i] - permutation[m]) == Math.Abs(i - m))) notattacked++;
                }
            }
            utility = notattacked;

            for (int i = 0; i < permutationLength; i++)
            {
                bestutility += i;
            }
        }
    }
}