using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

//Krzysztof Mroziewicz
namespace Exercise04
{
    internal class Program
    {
        private static void DrawSolutionPath(GameOf8 leaf)
        {
            while (leaf.parent != null)
            {
                leaf.DrawState();
                Console.WriteLine();
                leaf = leaf.parent;
            }
            leaf.DrawState();
        }

        private static GameOf8 DFS(GameOf8 root, int maxdepth)
        {
            List<GameOf8> open = new List<GameOf8> { root };
            List<GameOf8> closed = new List<GameOf8>();
            while (open.Count > 0)
            {
                if (open.ElementAt(0).Depth == maxdepth)
                {
                    closed.Add(open.ElementAt(0));
                    open.RemoveAt(0);
                    continue;
                }
                GameOf8 current = open.ElementAt(0);
                open.RemoveAt(0);
                closed.Add(current);

                if (current.CheckWin())
                {
                    Console.Write($" Searched in {closed.Count} states");
                    return current;
                }

                current.ExpandNode();
                for (int i = 0; i < current.children.Count; i++)
                {
                    GameOf8 currentchild = current.children[i];

                    if (open.Find(x => x.Compare(currentchild)) == null && closed.Find(x => x.Compare(currentchild)) == null)
                    {
                        open.Insert(0, currentchild);
                        continue;
                    }
                }
            }

            #region old

            //List<GameOf8> tree = new List<GameOf8> { root };
            //Stack<GameOf8> stack = new Stack<GameOf8>();

            //stack.Push(root);

            //while (stack.Count > 0)
            //{
            //    if (stack.Peek().Depth > maxdepth) { stack.Pop(); continue; }
            //    if (stack.Peek().CheckWin())
            //    {
            //        Console.Write($" Searched in {tree.Count} states");
            //        return stack.ElementAt(0);
            //    }
            //    GameOf8 moveup = new GameOf8(stack.Peek());
            //    GameOf8 movedown = new GameOf8(stack.Peek());
            //    GameOf8 moveleft = new GameOf8(stack.Peek());
            //    GameOf8 moveright = new GameOf8(stack.Peek());
            //    stack.Pop();

            //    if (moveleft.MoveLeft())
            //    {
            //        if (tree.Find(x => x.Compare(moveleft)) == null)
            //        {
            //            stack.Push(moveleft);
            //            tree.Add(moveleft);
            //        }
            //    }
            //    if (moveup.MoveUp())
            //    {
            //        if (tree.Find(x => x.Compare(moveup)) == null)
            //        {
            //            stack.Push(moveup);
            //            tree.Add(moveup);
            //        }
            //    }

            //    if (movedown.MoveDown())
            //    {
            //        if (tree.Find(x => x.Compare(movedown)) == null)
            //        {
            //            stack.Push(movedown);
            //            tree.Add(movedown);
            //        }
            //    }

            //    if (moveright.MoveRight())
            //    {
            //        if (tree.Find(x => x.Compare(moveright)) == null)
            //        {
            //            stack.Push(moveright);
            //            tree.Add(moveright);
            //        }
            //    }

            //}

            #endregion old

            Console.Write($" Searched in {closed.Count} states");
            return null;
        }

        private static GameOf8 BFS(GameOf8 root, int maxdepth)
        {
            List<GameOf8> open = new List<GameOf8> { root };
            List<GameOf8> closed = new List<GameOf8>();
            while (open.Count > 0)
            {
                if (open.ElementAt(0).Depth == maxdepth)
                {
                    closed.Add(open.ElementAt(0));
                    open.RemoveAt(0);
                    continue;
                }
                GameOf8 current = open.ElementAt(0);

                closed.Add(current);
                open.RemoveAt(0);
                if (current.CheckWin())
                {
                    Console.Write($" Searched in {closed.Count} states");
                    return current;
                }

                current.ExpandNode();

                for (int i = 0; i < current.children.Count; i++)
                {
                    GameOf8 currentchild = current.children[i];
                    if (open.Find(x => x.Compare(currentchild)) == null && closed.Find(x => x.Compare(currentchild)) == null)
                    {
                        open.Add(currentchild);

                    }
                }

                #region old

                //if (queue.ElementAt(0).CheckWin())
                //{
                //}
                //GameOf8 moveup = new GameOf8(queue.ElementAt(0));
                //GameOf8 movedown = new GameOf8(queue.ElementAt(0));
                //GameOf8 moveleft = new GameOf8(queue.ElementAt(0));
                //GameOf8 moveright = new GameOf8(queue.ElementAt(0));
                //if (moveup.MoveUp())
                //    if (tree.Find(x => x.Compare(moveup)) == null)
                //    {
                //        queue.Add(moveup);
                //        tree.Add(moveup);
                //    }

                //if (movedown.MoveDown())
                //    if (tree.Find(x => x.Compare(movedown)) == null)
                //    {
                //        queue.Add(movedown);
                //        tree.Add(movedown);
                //    }

                //if (moveleft.MoveLeft())
                //    if (tree.Find(x => x.Compare(moveleft)) == null)
                //    {
                //        queue.Add(moveleft);
                //        tree.Add(moveleft);
                //    }

                //if (moveright.MoveRight())
                //    if (tree.Find(x => x.Compare(moveright)) == null)
                //    {
                //        queue.Add(moveright);
                //        tree.Add(moveright);
                //    }
                //queue.RemoveAt(0);

                #endregion old
            }
            Console.Write($" Searched in {closed.Count} states");
            return null;
        }

        private static GameOf8 GBestFSPath(GameOf8 root, int maxdepth)
        {
            List<GameOf8> open = new List<GameOf8> { root };
            List<GameOf8> closed = new List<GameOf8>();
            while (open.Count > 0)
            {
                open = open.OrderBy(x => x.utilityByPath).ToList();
                if (open.ElementAt(0).Depth == maxdepth)
                {
                    closed.Add(open.ElementAt(0));
                    open.RemoveAt(0);
                    continue;
                }
                GameOf8 current = open.ElementAt(0);
                closed.Add(current);
                open.RemoveAt(0);
                if (current.CheckWin())
                {
                    Console.Write($" Searched in {closed.Count} states");
                    return current;
                }

                current.ExpandNode();

                for (int i = 0; i < current.children.Count; i++)
                {
                    GameOf8 currentchild = current.children[i];
                    if (open.Find(x => x.Compare(currentchild)) == null && closed.Find(x => x.Compare(currentchild)) == null)
                    {
                        open.Add(currentchild);

                    }
                }
            }

            #region old

            //List<GameOf8> tree = new List<GameOf8> { root };
            //List<GameOf8> PriorityQueue = new List<GameOf8> { root };
            //while (PriorityQueue.Count > 0)
            //{
            //    PriorityQueue = PriorityQueue.OrderBy(x => x.utilityByPath).ToList();
            //    if (PriorityQueue[0].CheckWin())
            //    {
            //        Console.Write($" Searched in {tree.Count} states");
            //        return PriorityQueue.ElementAt(0);
            //    }
            //    else
            //    {
            //        GameOf8 moveup = new GameOf8(PriorityQueue[0]);
            //        GameOf8 movedown = new GameOf8(PriorityQueue[0]);
            //        GameOf8 moveleft = new GameOf8(PriorityQueue[0]);
            //        GameOf8 moveright = new GameOf8(PriorityQueue[0]);
            //        if (moveup.MoveUp())
            //            if (tree.Find(x => x.Compare(moveup)) == null)
            //            {
            //                PriorityQueue.Add(moveup);
            //                tree.Add(moveup);
            //            }

            //        if (movedown.MoveDown())
            //            if (tree.Find(x => x.Compare(movedown)) == null)
            //            {
            //                PriorityQueue.Add(movedown);
            //                tree.Add(movedown);
            //            }

            //        if (moveleft.MoveLeft())
            //            if (tree.Find(x => x.Compare(moveleft)) == null)
            //            {
            //                PriorityQueue.Add(moveleft);
            //                tree.Add(moveleft);
            //            }

            //        if (moveright.MoveRight())
            //            if (tree.Find(x => x.Compare(moveright)) == null)
            //            {
            //                PriorityQueue.Add(moveright);
            //                tree.Add(moveright);
            //            }
            //    }
            //    PriorityQueue.RemoveAt(0);
            //}

            #endregion old

            Console.Write($" Searched in {closed.Count} states");
            return null;
        }

        private static GameOf8 GBestFSPlace(GameOf8 root, int maxdepth)
        {
            List<GameOf8> open = new List<GameOf8> { root };
            List<GameOf8> closed = new List<GameOf8>();
            while (open.Count > 0)
            {
                open = open.OrderBy(x => x.utilityByPlace).ToList();
                if (open.ElementAt(0).Depth == maxdepth)
                {
                    closed.Add(open.ElementAt(0));
                    open.RemoveAt(0);
                    continue;
                }
                GameOf8 current = open.ElementAt(0);
                closed.Add(current);
                open.RemoveAt(0);
                if (current.CheckWin())
                {
                    Console.Write($" Searched in {closed.Count} states");
                    return current;
                }
                current.ExpandNode();

                for (int i = 0; i < current.children.Count; i++)
                {
                    GameOf8 currentchild = current.children[i];

                    if (open.Find(x => x.Compare(currentchild)) == null && closed.Find(x => x.Compare(currentchild)) == null)
                    {
                        open.Add(currentchild);
                    }
                }
            }

            #region old

            //List<GameOf8> tree = new List<GameOf8> { root };
            //List<GameOf8> PriorityQueue = new List<GameOf8> { root };
            //while (PriorityQueue.Count > 0)
            //{
            //    PriorityQueue = PriorityQueue.OrderBy(x => x.utilityByPlace).ToList();
            //    if (PriorityQueue[0].CheckWin())
            //    {
            //        Console.Write($" Searched in {tree.Count} states");
            //        return PriorityQueue.ElementAt(0);
            //    }
            //    else
            //    {
            //        GameOf8 moveup = new GameOf8(PriorityQueue[0]);
            //        GameOf8 movedown = new GameOf8(PriorityQueue[0]);
            //        GameOf8 moveleft = new GameOf8(PriorityQueue[0]);
            //        GameOf8 moveright = new GameOf8(PriorityQueue[0]);
            //        if (moveup.MoveUp())
            //            if (tree.Find(x => x.Compare(moveup)) == null)
            //            {
            //                PriorityQueue.Add(moveup);
            //                tree.Add(moveup);
            //            }

            //        if (movedown.MoveDown())
            //            if (tree.Find(x => x.Compare(movedown)) == null)
            //            {
            //                PriorityQueue.Add(movedown);
            //                tree.Add(movedown);
            //            }

            //        if (moveleft.MoveLeft())
            //            if (tree.Find(x => x.Compare(moveleft)) == null)
            //            {
            //                PriorityQueue.Add(moveleft);
            //                tree.Add(moveleft);
            //            }

            //        if (moveright.MoveRight())
            //            if (tree.Find(x => x.Compare(moveright)) == null)
            //            {
            //                PriorityQueue.Add(moveright);
            //                tree.Add(moveright);
            //            }
            //    }
            //    PriorityQueue.RemoveAt(0);
            //}

            #endregion old

            Console.Write($" Searched in {closed.Count} states");
            return null;
        }

        private static GameOf8 AStarPath(GameOf8 root, int maxdepth)
        {
            List<GameOf8> open = new List<GameOf8> { root };
            List<GameOf8> closed = new List<GameOf8>();
            while (open.Count > 0)
            {
                open = open.OrderBy(x => x.utilityByPath + x.Depth).ToList();
                if (open.ElementAt(0).Depth == maxdepth)
                {
                    closed.Add(open.ElementAt(0));
                    open.RemoveAt(0);
                    continue;
                }
                GameOf8 current = open.ElementAt(0);
                closed.Add(current);
                open.RemoveAt(0);
                if (current.CheckWin())
                {
                    Console.Write($" Searched in {closed.Count} states");
                    return current;
                }
                current.ExpandNode();

                for (int i = 0; i < current.children.Count; i++)
                {
                    GameOf8 currentchild = current.children[i];


                    if (open.Find(x => x.Compare(currentchild)) == null && closed.Find(x => x.Compare(currentchild)) == null)
                    {
                        open.Add(currentchild);
                    }
                }
            }

            #region old

            //List<GameOf8> tree = new List<GameOf8> { root };
            //List<GameOf8> PriorityQueue = new List<GameOf8> { root };
            //while (PriorityQueue.Count > 0 && PriorityQueue[0].Depth < maxdepth)
            //{
            //    PriorityQueue = PriorityQueue.OrderBy(x => x.utilityByPath + x.Depth).ToList();
            //    if (PriorityQueue[0].CheckWin())
            //    {
            //        Console.Write($" Searched in {tree.Count} states");
            //        return PriorityQueue.ElementAt(0);
            //    }
            //    else
            //    {
            //        GameOf8 moveup = new GameOf8(PriorityQueue[0]);
            //        GameOf8 movedown = new GameOf8(PriorityQueue[0]);
            //        GameOf8 moveleft = new GameOf8(PriorityQueue[0]);
            //        GameOf8 moveright = new GameOf8(PriorityQueue[0]);
            //        if (moveup.MoveUp())
            //            if (tree.Find(x => x.Compare(moveup)) == null)
            //            {
            //                PriorityQueue.Add(moveup);
            //                tree.Add(moveup);
            //            }

            //        if (movedown.MoveDown())
            //            if (tree.Find(x => x.Compare(movedown)) == null)
            //            {
            //                PriorityQueue.Add(movedown);
            //                tree.Add(movedown);
            //            }

            //        if (moveleft.MoveLeft())
            //            if (tree.Find(x => x.Compare(moveleft)) == null)
            //            {
            //                PriorityQueue.Add(moveleft);
            //                tree.Add(moveleft);
            //            }

            //        if (moveright.MoveRight())
            //            if (tree.Find(x => x.Compare(moveright)) == null)
            //            {
            //                PriorityQueue.Add(moveright);
            //                tree.Add(moveright);
            //            }
            //    }
            //    PriorityQueue.RemoveAt(0);
            //}

            #endregion old

            Console.Write($" Searched in {closed.Count} states");
            return null;
        }

        private static GameOf8 AStarPlace(GameOf8 root, int maxdepth)
        {
            List<GameOf8> open = new List<GameOf8> { root };
            List<GameOf8> closed = new List<GameOf8>();
            while (open.Count > 0)
            {
                open = open.OrderBy(x => x.utilityByPlace + x.Depth).ToList();
                if (open.ElementAt(0).Depth == maxdepth)
                {
                    closed.Add(open.ElementAt(0));
                    open.RemoveAt(0);
                    continue;
                }
                GameOf8 current = open.ElementAt(0);
                closed.Add(current);
                open.RemoveAt(0);
                if (current.CheckWin())
                {
                    Console.Write($" Searched in {closed.Count} states");
                    return current;
                }
                current.ExpandNode();

                for (int i = 0; i < current.children.Count; i++)
                {
                    GameOf8 currentchild = current.children[i];


                    if (open.Find(x => x.Compare(currentchild)) == null && closed.Find(x => x.Compare(currentchild)) == null)
                    {
                        open.Add(currentchild);
                    }
                }
            }

            #region old

            //List<GameOf8> tree = new List<GameOf8> { root };
            //List<GameOf8> PriorityQueue = new List<GameOf8> { root };
            //while (PriorityQueue.Count > 0 && PriorityQueue[0].Depth < maxdepth)
            //{
            //    PriorityQueue = PriorityQueue.OrderBy(x => x.utilityByPlace + x.Depth).ToList();
            //    if (PriorityQueue[0].CheckWin())
            //    {
            //        Console.Write($" Searched in {tree.Count} states");
            //        return PriorityQueue.ElementAt(0);
            //    }
            //    else
            //    {
            //        GameOf8 moveup = new GameOf8(PriorityQueue[0]);
            //        GameOf8 movedown = new GameOf8(PriorityQueue[0]);
            //        GameOf8 moveleft = new GameOf8(PriorityQueue[0]);
            //        GameOf8 moveright = new GameOf8(PriorityQueue[0]);
            //        if (moveup.MoveUp())
            //            if (tree.Find(x => x.Compare(moveup)) == null)
            //            {
            //                PriorityQueue.Add(moveup);
            //                tree.Add(moveup);
            //            }

            //        if (movedown.MoveDown())
            //            if (tree.Find(x => x.Compare(movedown)) == null)
            //            {
            //                PriorityQueue.Add(movedown);
            //                tree.Add(movedown);
            //            }

            //        if (moveleft.MoveLeft())
            //            if (tree.Find(x => x.Compare(moveleft)) == null)
            //            {
            //                PriorityQueue.Add(moveleft);
            //                tree.Add(moveleft);
            //            }

            //        if (moveright.MoveRight())
            //            if (tree.Find(x => x.Compare(moveright)) == null)
            //            {
            //                PriorityQueue.Add(moveright);
            //                tree.Add(moveright);
            //            }
            //    }
            //    PriorityQueue.RemoveAt(0);
            //}

            #endregion old

            Console.Write($" Searched in {closed.Count} states");
            return null;
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello\n");

            GameOf8 game = new GameOf8();
            int maxdepth = 17;

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine();
                switch (i)
                {
                    case 0: Console.WriteLine($"GREEDY BEST FIRST SEARCH BY PATH UTILITY"); break;
                    case 1: Console.WriteLine($"GREEDY BEST FIRST SEARCH BY PLACE UTILITY"); break;
                    case 2: Console.WriteLine($"A STAR BY PATH UTILITY"); break;
                    case 3: Console.WriteLine($"A STAR BY PLACE UTILITY"); break;
                    case 4: Console.WriteLine($"DEPTH FIRST SEARCH"); break;
                    case 5: Console.WriteLine($"BREATH FIRST SEARCH"); break;
                }
                var watch = Stopwatch.StartNew();
                GameOf8 solution = null;
                switch (i)
                {
                    case 0: solution = GBestFSPath(game, maxdepth); break;
                    case 1: solution = GBestFSPlace(game, maxdepth); break;
                    case 2: solution = AStarPath(game, maxdepth); break;
                    case 3: solution = AStarPlace(game, maxdepth); break;
                    case 4: solution = DFS(game, maxdepth); break;
                    case 5: solution = BFS(game, maxdepth); break;
                }

                watch.Stop();
                float elapsedMs = watch.ElapsedMilliseconds;
                if (solution != null) Console.Write($" Found on level {solution.Depth}");
                Console.Write($" Elapsed time:{elapsedMs} ms.\n");
                if (solution != null)
                {
                    Console.Write($" Trace Path To solution? Y/N");
                    char x = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    switch (x)
                    {
                        case 'y':
                            DrawSolutionPath(solution);
                            break;
                    }
                }
            }
            Console.WriteLine("\nEND");

            #region Game to Play

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

            #endregion Game to Play

            Console.ReadKey();
        }
    }
}