using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise04
{
    internal class GameOf8
    {
        private readonly int[] gamestate;
        public int utilityByPath;
        public int utilityByPlace;
        private readonly int size = 9;
        public bool searched = false;
        public GameOf8 parent = null;
        public long Depth = 0;

        public GameOf8()
        {
            Random rnd = new Random();
            List<int> permutation = new List<int>();
            do
            {
                while (permutation.Count < size)
                {
                    int nextrnd = rnd.Next(0, size);
                    if (!permutation.Contains(nextrnd))
                        permutation.Add(nextrnd);
                }
                gamestate = permutation.ToArray();
            }
            while (!isSolvable());
            CalculateUtilityByPath();
            CalculateUtilityByPlace();
        }

        public GameOf8(GameOf8 input)
        {
            parent = input;
            this.gamestate = new int[size];
            this.Depth = input.Depth + 1;
            Array.Copy(input.gamestate, 0, gamestate, 0, size);
            CalculateUtilityByPath();
            CalculateUtilityByPlace();
        }

        public bool Compare(GameOf8 obj)
        {
            for (int i = 0; i < size; i++)
            {
                if (obj.gamestate[i] != this.gamestate[i]) return false;
            }
            return true;
        }

        public void CalculateUtilityByPath()
        {
            utilityByPath = 0;
            for (int i = 0; i < size; i++)
            {
                utilityByPath += Math.Abs((i / 3) - (gamestate[i] / 3)) + Math.Abs(i % 3 - gamestate[i] % 3);
            }
            //Console.WriteLine(utilityByPath);
        }

        public void CalculateUtilityByPlace()
        {
            utilityByPlace = 0;
            for (int i = 0; i < size; i++)
            {
                if (gamestate[i] != i) utilityByPlace++;
            }
            // Console.WriteLine(utilityByPlace);
        }

        public void DrawState()
        {
            for (int i = 0; i < size; i++)
            {
                if (i % (int)Math.Sqrt(size) == 0)
                {
                    Console.WriteLine();
                }
                Console.Write($"|{gamestate[i]}|");
            }
        }

        public bool MoveRight()
        {
            for (int i = 0; i < size; i++)
            {
                if (gamestate[i] == 0 && i % (int)Math.Sqrt(size) != (int)Math.Sqrt(size) - 1)
                {
                    gamestate[i] = gamestate[i + 1];
                    gamestate[i + 1] = 0;
                    return true;
                }
            }
            return false;
        }

        public bool MoveLeft()
        {
            for (int i = 0; i < size; i++)
            {
                if (gamestate[i] == 0 && i % (int)Math.Sqrt(size) != 0)
                {
                    gamestate[i] = gamestate[i - 1];
                    gamestate[i - 1] = 0;
                    return true;
                }
            }
            return false;
        }

        public bool MoveUp()
        {
            for (int i = (int)Math.Sqrt(size); i < size; i++)
            {
                if (gamestate[i] == 0)
                {
                    gamestate[i] = gamestate[i - (int)Math.Sqrt(size)];
                    gamestate[i - (int)Math.Sqrt(size)] = 0;
                    return true;
                }
            }
            return false;
        }

        public bool MoveDown()
        {
            for (int i = 0; i < size - (int)Math.Sqrt(size); i++)
            {
                if (gamestate[i] == 0)
                {
                    gamestate[i] = gamestate[i + (int)Math.Sqrt(size)];
                    gamestate[i + (int)Math.Sqrt(size)] = 0;
                    return true;
                }
            }
            return false;
        }

        public bool CheckWin()
        {
            for (int i = 0; i < size; i++)
            {
                if (gamestate[i] != i) return false;
            }
            return true;
        }

        public bool isSolvable()
        {
            int inv_count = 0;
            int[,] arr = new int[(int)Math.Sqrt(size), (int)Math.Sqrt(size)];

            for (int l = 0; l < (int)Math.Sqrt(size); l++)
                for (int k = 0; k < (int)Math.Sqrt(size); k++)
                    arr[l, k] = gamestate[l + k];

            for (int i = 0; i < 3 - 1; i++)
                for (int j = i + 1; j < 3; j++)
                    if (arr[j, i] > 0 && arr[j, i] > arr[i, j])
                        inv_count++;

            return (inv_count % 2 == 0);
        }
    }
}