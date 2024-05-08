using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private bool isPlayerXTurn = true; // X kezdi a kört
        private string[,] board = new string[3, 3]; // játéktábla

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
        }
        /// <summary>
        /// Inicializálja a játéktáblát üres cellákkal.
        /// </summary>
        private void InitializeBoard()
        {
            // tábla radírozása
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ""; // üres cella
                }
            }
        }
        /// <summary>
        /// Eseménykezelő, ami lefut, amikor egy gombra kattintanak a játéktáblán.
        /// Frissíti a táblát és ellenőrzi a győzelmi vagy döntetlen feltételeket.
        /// </summary>
        /// <param name="sender">Az eseményt kiváltó gomb.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int row = Grid.GetRow(clickedButton);
            int col = Grid.GetColumn(clickedButton);

            // Ellenőrzi, hogy a cella üres-e
            if (board[row, col] == "")
            {
                // Tábla frissítése
                board[row, col] = isPlayerXTurn ? "X" : "O";

                // Tartalom frissítése
                clickedButton.Content = board[row, col];

                // Győzelem ellenőrzése
                if (CheckWin())
                {
                    MessageBox.Show($"Player {(isPlayerXTurn ? "X" : "O")} wins!");
                    RestartGame();
                    return;
                }

                // Döntetlen ellenőrzése
                if (CheckDraw())
                {
                    MessageBox.Show("It's a draw!");
                    RestartGame();
                    return;
                }

                // Alakzat cseréje
                isPlayerXTurn = !isPlayerXTurn;
            }
        }
        /// <summary>
        /// Ellenőrzi a játéktábla állapotát, hogy van-e győztes.
        /// </summary>
        /// <returns>True, ha van győztes, egyébként false.</returns>
        private bool CheckWin()
        {
            // Sorok ellenőrzése
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != "" && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    return true;
                }
            }

            // Oszlopok ellenőrzése
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] != "" && board[0, j] == board[1, j] && board[1, j] == board[2, j])
                {
                    return true;
                }
            }

            // Átlók ellenőrzése
            if (board[0, 0] != "" && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return true;
            }
            if (board[0, 2] != "" && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Ellenőrzi a játéktábla állapotát, hogy van-e döntetlen.
        /// </summary>
        /// <returns>True, ha a játék döntetlen, egyébként false.</returns>
        private bool CheckDraw()
        {
            // Ellenőrzi, hogy az összes cella ki van-e töltve
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == "")
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// Új játék kezdése: törli a játéktáblát és visszaállítja a játékot kezdő állapotra.
        /// </summary>        
        private void RestartGame()
        {
            // TÁbla törlése és játékkészre állítása
            isPlayerXTurn = true;
            InitializeBoard();

            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Button button)
                {
                    button.Content = "";
                }
            }
        }
    }
}
