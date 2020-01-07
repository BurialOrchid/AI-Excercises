using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System;

namespace Exercise07
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            PointsAI = 0;
            PointsHuman = 0;
            Board = new char[3, 3] { { 'a', 'a', 'a' }, { 'a', 'a', 'a' }, { 'a', 'a', 'a' } };
            Turn = true;
            InitializeComponent();
        }

        public int PointsAI { get; set; }
        public int PointsHuman { get; set; }
        public bool Turn { get; set; }
        public char[,] Board { get; set; }

        private void PlayersTurn(Button button)
        {
            button.Content = 'X';
            switch (button.Name)
            {
                case "Button1": Board[0, 0] = 'X'; break;
                case "Button2": Board[0, 1] = 'X'; break;
                case "Button3": Board[0, 2] = 'X'; break;
                case "Button4": Board[1, 0] = 'X'; break;
                case "Button5": Board[1, 1] = 'X'; break;
                case "Button6": Board[1, 2] = 'X'; break;
                case "Button7": Board[2, 0] = 'X'; break;
                case "Button8": Board[2, 1] = 'X'; break;
                case "Button9": Board[2, 2] = 'X'; break;
                default: break;
            }
            DisableButtons();
            DrawBoard();
        }

        private void DrawBoard()
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

        private void AIsTurn()
        {
            DoBestMove();
        }

        private void DoBestMove()
        {
        }

        private int CheckIfWinning(char[,] board)
        {
            //check rows
            if (board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2])
            {
                if (board[0, 0] == 'X') return 1;
                if (board[0, 0] == 'O') return -1;
            }

            if (board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2])
            {
                if (board[1, 0] == 'X') return 1;
                if (board[1, 0] == 'O') return -1;
            }
            if (board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2])
            {
                if (board[2, 0] == 'X') return 1;
                if (board[2, 0] == 'O') return -1;
            }

            // check columns
            if (board[0, 0] == board[1, 0] && board[1, 0] == board[2, 0])
            {
                if (board[0, 0] == 'X') return 1;
                if (board[0, 0] == 'O') return -1;
            }
            if (board[0, 1] == board[1, 1] && board[1, 1] == board[2, 1])
            {
                if (board[0, 1] == 'X') return 1;
                if (board[0, 1] == 'O') return -1;
            }
            if (board[0, 2] == Board[1, 2] && board[1, 2] == board[2, 2])
            {
                if (board[0, 2] == 'X') return 1;
                if (board[0, 2] == 'O') return -1;
            }

            // check diags
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                if (board[0, 0] == 'X') return 1;
                if (board[0, 0] == 'O') return -1;
            }
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                if (board[0, 2] == 'X') return 1;
                if (board[0, 2] == 'O') return -1;
            }

            return 0;
        }

        private bool IsMovesLeft(char[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 'a')
                        return true;
                }
            }
            return false;
        }

        private void DisableButtons()
        {
            foreach (Button item in PlayGrid.Children)
            {
                if (item.Content != null)
                {
                    item.IsEnabled = false;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            PlayersTurn(button);
            AIsTurn();

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
                    }
                    break;

                case 1:
                    foreach (Button item in PlayGrid.Children)
                    {
                        item.IsEnabled = false;
                    }
                    PointsHuman++;
                    MessageBox.Show($"PLAYER WON", "GAME OVER");
                    this.HumanScoreValue.Content = PointsHuman;
                    break;

                case -1:
                    foreach (Button item in PlayGrid.Children)
                    {
                        item.IsEnabled = false;
                    }
                    PointsAI++;
                    MessageBox.Show($"AI WON", "GAME OVER");
                    this.AIScoreValue.Content = PointsAI;
                    break;

                default:
                    break;
            }
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button button in PlayGrid.Children)
            {
                button.IsEnabled = true;
                button.Content = null;
            }
            Board = new char[3, 3] { { 'a', 'a', 'a' }, { 'a', 'a', 'a' }, { 'a', 'a', 'a' } };
            DrawBoard();
        }
    }
}