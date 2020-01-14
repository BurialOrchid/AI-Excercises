using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Exercise04
{
    internal class Program
    {
        private static List<GameOf8> DFS(GameOf8 root, List<GameOf8> list)
        {
            GameOf8 moveup = new GameOf8(root);
            GameOf8 movedown = new GameOf8(root);
            GameOf8 moveleft = new GameOf8(root);
            GameOf8 moveright = new GameOf8(root);

            if (root.CheckWin())
            {
                Console.WriteLine($"Found solution at the depth of {root.Depth}");
                return list;
            }
            if (root.Depth > 12) return list;
            if (moveup.MoveUp())
            {
                if (list.Find(x => x.Compare(moveup)) == null)
                {
                    list.Add(moveup);
                    DFS(moveup, list);
                }
            }
            if (movedown.MoveDown())
            {
                if (list.Find(x => x.Compare(movedown)) == null)
                {
                    list.Add(movedown);
                    DFS(movedown, list);
                }
            }
            if (moveleft.MoveLeft())
            {
                if (list.Find(x => x.Compare(moveleft)) == null)
                {
                    list.Add(moveleft);
                    DFS(moveleft, list);
                }
            }
            if (moveright.MoveRight())
            {
                if (list.Find(x => x.Compare(moveright)) == null)
                {
                    list.Add(moveright);
                    DFS(moveright, list);
                }
            }

            return list;
        }

        private static List<GameOf8> BFS(List<GameOf8> tree, List<GameOf8> queue)
        {
            List<GameOf8> nextQueue = new List<GameOf8>();
            if (tree.Find(x => x.CheckWin()) != null)
            {
                GameOf8 winner = tree.Find(x => x.CheckWin());
                Console.WriteLine($"Found solution at the depth of {winner.Depth}");
                return tree;
            }
            if (tree[tree.Count - 1].Depth > 12) { return tree; }
            else
                foreach (GameOf8 item in queue)
                {
                    GameOf8 moveup = new GameOf8(item);
                    GameOf8 movedown = new GameOf8(item);
                    GameOf8 moveleft = new GameOf8(item);
                    GameOf8 moveright = new GameOf8(item);
                    if (moveup.MoveUp())
                        if (tree.Find(x => x.Compare(moveup)) == null)
                        {
                            nextQueue.Add(moveup);
                            tree.Add(moveup);
                        }

                    if (movedown.MoveDown())
                        if (tree.Find(x => x.Compare(movedown)) == null)
                        {
                            nextQueue.Add(movedown);
                            tree.Add(movedown);
                        }

                    if (moveleft.MoveLeft())
                        if (tree.Find(x => x.Compare(moveleft)) == null)
                        {
                            nextQueue.Add(moveleft);
                            tree.Add(moveleft);
                        }

                    if (moveright.MoveRight())
                        if (tree.Find(x => x.Compare(moveright)) == null)
                        {
                            nextQueue.Add(moveright);
                            tree.Add(moveright);
                        }
                }
            BFS(tree, nextQueue);
            return tree;
        }

        private static GameOf8 GBestFSPath(GameOf8 root)
        {
            List<GameOf8> tree = new List<GameOf8> { root };
            List<GameOf8> PriorityQueue = new List<GameOf8> { root };
            while (PriorityQueue.Count > 0)
            {
                // Debug.WriteLine(PriorityQueue[0].utilityByPath);
                PriorityQueue = PriorityQueue.OrderBy(x => x.utilityByPath).ToList();
                if (PriorityQueue[0].CheckWin()) break;
                else
                {
                    GameOf8 moveup = new GameOf8(PriorityQueue[0]);
                    GameOf8 movedown = new GameOf8(PriorityQueue[0]);
                    GameOf8 moveleft = new GameOf8(PriorityQueue[0]);
                    GameOf8 moveright = new GameOf8(PriorityQueue[0]);
                    if (moveup.MoveUp())
                        if (tree.Find(x => x.Compare(moveup)) == null)
                        {
                            PriorityQueue.Add(moveup);
                            tree.Add(moveup);
                        }

                    if (movedown.MoveDown())
                        if (tree.Find(x => x.Compare(movedown)) == null)
                        {
                            PriorityQueue.Add(movedown);
                            tree.Add(movedown);
                        }

                    if (moveleft.MoveLeft())
                        if (tree.Find(x => x.Compare(moveleft)) == null)
                        {
                            PriorityQueue.Add(moveleft);
                            tree.Add(moveleft);
                        }

                    if (moveright.MoveRight())
                        if (tree.Find(x => x.Compare(moveright)) == null)
                        {
                            PriorityQueue.Add(moveright);
                            tree.Add(moveright);
                        }
                }
                PriorityQueue.RemoveAt(0);
            }
            Console.WriteLine($"Searched solution in {tree.Count} number of states");
            Console.WriteLine($"Solution found on level {PriorityQueue[0].Depth}");
            return PriorityQueue[0];
        }

        private static void Main(string[] args)
        {
            //for (int i = 0; i < 50; i++)
            //{
            //    GameOf8 game = new GameOf8();
            //    Console.Write($"{game.isSolvable().ToString()} ");
            //    List<GameOf8> listOfNodes = new List<GameOf8> { game };
            //    List<GameOf8> queue = new List<GameOf8> { game };
            //    listOfNodes = BFS(listOfNodes, queue);
            //    Console.WriteLine($"Searched solution in {listOfNodes.Count} number of states");
            //}

            GameOf8 game = new GameOf8();
            GBestFSPath(game);

            //while (true)
            //{
            //    game.DrawState();
            //    Console.WriteLine();
            //    Console.WriteLine(game.utilityByPlace);
            //    Console.WriteLine(game.utilityByPath);
            //    char x = Console.ReadKey().KeyChar;
            //    switch (x)
            //    {
            //        case 'w':
            //            game.MoveUp();
            //            break;

            //        case 'a':
            //            game.MoveLeft();
            //            break;

            //        case 's':
            //            game.MoveDown();
            //            break;

            //        case 'd':
            //            game.MoveRight();
            //            break;

            //        default:
            //            break;
            //    }
            //    game.CalculateUtilityByPlace();
            //    game.CalculateUtilityByPath();

            //    Console.Clear();
            //}

            Console.ReadKey();
        }
    }
}