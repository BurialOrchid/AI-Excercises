using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Diagnostics;

namespace Exercise07
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool _turn;

        public bool Turn
        {
            get { return _turn; }
            set { _turn = value; }
        }

        private bool CheckIfWinning(char player)
        {
            char[,] board = new char[3, 3] { { 'a', 'a', 'a' }, { 'a', 'a', 'a' }, { 'a', 'a', 'a' } };
            if (Button1.Content != null)
                board[0, 0] = (char)Button1.Content;
            if (Button2.Content != null)
                board[0, 1] = (char)Button2.Content;
            if (Button3.Content != null)
                board[0, 2] = (char)Button3.Content;
            if (Button4.Content != null)
                board[1, 0] = (char)Button4.Content;
            if (Button5.Content != null)
                board[1, 1] = (char)Button5.Content;
            if (Button6.Content != null)
                board[1, 2] = (char)Button6.Content;
            if (Button7.Content != null)
                board[2, 0] = (char)Button7.Content;
            if (Button8.Content != null)
                board[2, 1] = (char)Button8.Content;
            if (Button9.Content != null)
                board[2, 2] = (char)Button9.Content;

            //check rows
            if (board[0, 0] == player && board[0, 1] == player && board[0, 2] == player) { return true; }
            if (board[1, 0] == player && board[1, 1] == player && board[1, 2] == player) { return true; }
            if (board[2, 0] == player && board[2, 1] == player && board[2, 2] == player) { return true; }

            // check columns
            if (board[0, 0] == player && board[1, 0] == player && board[2, 0] == player) { return true; }
            if (board[0, 1] == player && board[1, 1] == player && board[2, 1] == player) { return true; }
            if (board[0, 2] == player && board[1, 2] == player && board[2, 2] == player) { return true; }

            // check diags
            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) { return true; }
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player) { return true; }

            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Turn = !Turn;
            Button button = sender as Button;
            switch (Turn)
            {
                case true:
                    button.Content = 'X';
                    button.IsEnabled = false;
                    if (CheckIfWinning('X')) MessageBox.Show($"Won player X", "GAME OVER");
                    break;

                case false:
                    button.Content = 'O';
                    button.IsEnabled = false;
                    if (CheckIfWinning('O')) MessageBox.Show($"Won player O", "GAME OVER"); ;
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
        }
    }
}