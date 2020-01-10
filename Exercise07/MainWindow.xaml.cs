using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Exercise07
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class MainWindow : Window
    {
        private class Move
        {
            public int i, j;
        };

        public MainWindow()
        {
            PointsAI = 0;
            PointsHuman = 0;
            Board = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };

            InitializeComponent();

        }

        public char ai = 'X';
        public char player = 'O';
        public char[,] Board;
        public int PointsAI { get; set; }
        public int PointsHuman { get; set; }

        private void DebugBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Debug.Write(Board[i, j]);
                }
                Debug.WriteLine("");
            }
            Debug.WriteLine("");
        }

        private void PlayersTurn(Button button)
        {
            button.Content = player;
            switch (button.Name)
            {
                case "Button1": if (Board[0, 0] == ' ') Board[0, 0] = player; break;
                case "Button2": if (Board[0, 1] == ' ') Board[0, 1] = player; break;
                case "Button3": if (Board[0, 2] == ' ') Board[0, 2] = player; break;
                case "Button4": if (Board[1, 0] == ' ') Board[1, 0] = player; break;
                case "Button5": if (Board[1, 1] == ' ') Board[1, 1] = player; break;
                case "Button6": if (Board[1, 2] == ' ') Board[1, 2] = player; break;
                case "Button7": if (Board[2, 0] == ' ') Board[2, 0] = player; break;
                case "Button8": if (Board[2, 1] == ' ') Board[2, 1] = player; break;
                case "Button9": if (Board[2, 2] == ' ') Board[2, 2] = player; break;
                default: break;
            }
            DisableButtons();
            // DebugBoard();
        }

        private void AIsTurn()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            DoBestMove();
            watch.Stop();
            double elapsedMs = watch.ElapsedTicks;
            Debug.Write(elapsedMs.ToString()+" ");
            if (Board[0, 0] == ai) Button1.Content = ai;
            if (Board[0, 1] == ai) Button2.Content = ai;
            if (Board[0, 2] == ai) Button3.Content = ai;
            if (Board[1, 0] == ai) Button4.Content = ai;
            if (Board[1, 1] == ai) Button5.Content = ai;
            if (Board[1, 2] == ai) Button6.Content = ai;
            if (Board[2, 0] == ai) Button7.Content = ai;
            if (Board[2, 1] == ai) Button8.Content = ai;
            if (Board[2, 2] == ai) Button9.Content = ai;
            DisableButtons();
            // DebugBoard();
        }

        private void DoBestMove()
        {
            int bestScore = -int.MaxValue;
            int ii = -1;
            int jj = -1;


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] == ' ')
                    {
                        Board[i, j] = ai;
                        int score = MiniMax(Board, 0, -int.MaxValue, int.MaxValue, false);
                        Board[i, j] = ' ';
                        if (score > bestScore)
                        {
                            bestScore = score;
                            ii = i;
                            jj = j;
                        }
                    }
                }
            }
            Board[ii, jj] = ai;
        }

        private int MiniMax(char[,] board, int depth, int alpha, int beta, bool isMaximizing)
        {

            int value = CheckIfWinning(board);
            if (value != 0) return value;
            if (!IsMovesLeft(board))
            {
                return CheckIfWinning(board);
            }
            if (isMaximizing)
            {
                int bestScore = -int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == ' ')
                        {
                            board[i, j] = ai;
                            int score = MiniMax(board, depth + 1, alpha, beta, false);
                            board[i, j] = ' ';
                            bestScore = Math.Max(bestScore, score);
                            alpha = Math.Max(alpha, score);
                            if (beta <= alpha)
                                break;
                        }
                    }
                }

                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == ' ')
                        {
                            board[i, j] = player;
                            int score = MiniMax(board, depth + 1, alpha, beta, true);
                            board[i, j] = ' ';
                            bestScore = Math.Min(bestScore, score);
                            beta = Math.Min(beta, score);
                            if (beta <= alpha)
                                break;
                        }
                    }
                }

                return bestScore;
            }
        }

        private bool Equals3(char a, char b, char c)
        {
            return a == b && b == c && a != ' ';
        }

        private int CheckIfWinning(char[,] board)
        {
            char winner = ' ';

            // horizontal
            for (int i = 0; i < 3; i++)
            {
                if (Equals3(board[i, 0], board[i, 1], board[i, 2]))
                {
                    winner = board[i, 0];
                }
            }

            // Vertical
            for (int i = 0; i < 3; i++)
            {
                if (Equals3(board[0, i], board[1, i], board[2, i]))
                {
                    winner = board[0, i];
                }
            }

            // Diagonal
            if (Equals3(board[0, 0], board[1, 1], board[2, 2]))
            {
                winner = board[0, 0];
            }
            if (Equals3(board[2, 0], board[1, 1], board[0, 2]))
            {
                winner = board[2, 0];
            }

            if (!IsMovesLeft(board))
            {
                if (winner == ' ') { return 0; }
            }
            if (winner == 'X') return 1;
            if (winner == 'O') return -1;
            return 0;
        }

        private bool IsMovesLeft(char[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                        return true;
                }
            }
            return false;
        }

        private bool CheckIfEnd()
        {
            switch (CheckIfWinning(Board))
            {
                case 0:
                    if (!IsMovesLeft(Board))
                    {
                        MessageBox.Show($"TIE", "GAME OVER");
                        foreach (Button item in PlayGrid.Children)
                        {
                            item.IsEnabled = false;
                        }
                        return true;
                    }
                    break;

                case -1:
                    foreach (Button item in PlayGrid.Children)
                    {
                        item.IsEnabled = false;
                    }
                    PointsHuman++;
                    MessageBox.Show($"PLAYER WON", "GAME OVER");
                    this.HumanScoreValue.Content = PointsHuman;
                    return true;

                case 1:
                    foreach (Button item in PlayGrid.Children)
                    {
                        item.IsEnabled = false;
                    }
                    PointsAI++;
                    MessageBox.Show($"AI WON", "GAME OVER");
                    this.AIScoreValue.Content = PointsAI;
                    return true;

                default:

                    break;
            }
            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            PlayersTurn(button);

            if (!CheckIfEnd())
            {
                AIsTurn();
                CheckIfEnd();
            }
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button button in PlayGrid.Children)
            {
                button.IsEnabled = true;
                button.Content = null;
            }
            Board = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
        }

        private void DisableButtons()
        {
            if (Board[0, 0] != ' ') Button1.IsEnabled = false;
            if (Board[0, 1] != ' ') Button2.IsEnabled = false;
            if (Board[0, 2] != ' ') Button3.IsEnabled = false;
            if (Board[1, 0] != ' ') Button4.IsEnabled = false;
            if (Board[1, 1] != ' ') Button5.IsEnabled = false;
            if (Board[1, 2] != ' ') Button6.IsEnabled = false;
            if (Board[2, 0] != ' ') Button7.IsEnabled = false;
            if (Board[2, 1] != ' ') Button8.IsEnabled = false;
            if (Board[2, 2] != ' ') Button9.IsEnabled = false;
        }
    }
}