using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private char[,] board = new char[3, 3];
        private char currentPlayer = 'X';
        private bool gameOver = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                    Button button = (Button)this.FindName($"Button{i}{j}");
                    button.IsEnabled = false;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameOver) return;

            Button button = (Button)sender;
            int row = int.Parse((string)(button.Tag as string).Split(',')[0]);
            int col = int.Parse((string)(button.Tag as string).Split(',')[1]);

            if (board[row, col] == ' ')
            {
                board[row, col] = currentPlayer;
                button.Content = currentPlayer.ToString();
                button.IsEnabled = false;

                if (CheckWin(currentPlayer))
                {
                    StatusTextBlock.Text = $"{currentPlayer} выиграл!";
                    UpdateGameResult($"{currentPlayer} выиграл!");
                    gameOver = true;
                }
                else if (CheckDraw())
                {
                    StatusTextBlock.Text = "Ничья!";
                    UpdateGameResult("Ничья!");
                    gameOver = true;
                }
                else
                {
                    MakeRobotMove();
                }
            }
        }

        private void MakeRobotMove()
        {
    
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = 'O';
                        Button button = (Button)this.FindName($"Button{i}{j}");
                        button.Content = "O";
                        button.IsEnabled = false;
                        return;
                    }
                }
            }
        }

        private bool CheckWin(char player)
        {
         
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) return true;
                if (board[0, i] == player && board[1, i] == player && board[2, i] == player) return true;
            }
            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) return true;
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player) return true;
            return false;
        }

        private bool CheckDraw()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ') return false;
                }
            }
            return true;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            gameOver = false;
            currentPlayer = 'X';
            StatusTextBlock.Text = $"Ходит {currentPlayer}";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                    Button button = (Button)this.FindName($"Button{i}{j}");
                    button.Content = " ";
                    button.IsEnabled = true;
                }
            }
        }
        private void UpdateGameResult(string result)
        {
         
            TextBlock resultTextBlock = new TextBlock
            {
                Text = result,
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

          
            MainGrid.Children.Add(resultTextBlock);

            Grid.SetRow(resultTextBlock, 1);
            Grid.SetRowSpan(resultTextBlock, 3);
            Grid.SetColumn(resultTextBlock, 0);
            Grid.SetColumnSpan(resultTextBlock, 3);
        }
    }
}