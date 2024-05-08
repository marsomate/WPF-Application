using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private bool isPlayerXTurn = true; // Player X starts the game
        private string[,] board = new string[3, 3]; // Represents the game board

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            // Clear the board
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ""; // Empty cell
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int row = Grid.GetRow(clickedButton);
            int col = Grid.GetColumn(clickedButton);

            // Check if the cell is already occupied
            if (board[row, col] == "")
            {
                // Update the board
                board[row, col] = isPlayerXTurn ? "X" : "O";

                // Update the button content
                clickedButton.Content = board[row, col];

                // Check for a win
                if (CheckWin())
                {
                    MessageBox.Show($"Player {(isPlayerXTurn ? "X" : "O")} wins!");
                    RestartGame();
                    return;
                }

                // Check for a draw
                if (CheckDraw())
                {
                    MessageBox.Show("It's a draw!");
                    RestartGame();
                    return;
                }

                // Switch player turn
                isPlayerXTurn = !isPlayerXTurn;
            }
        }

        private bool CheckWin()
        {
            // Check rows
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != "" && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    return true;
                }
            }

            // Check columns
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] != "" && board[0, j] == board[1, j] && board[1, j] == board[2, j])
                {
                    return true;
                }
            }

            // Check diagonals
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

        private bool CheckDraw()
        {
            // Check if all cells are occupied
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
        /// Megoldás terv
        /// 1. Felhasználói felület kialakítása: WPF alkalmazás segítségével elkészítettük a játékfelületet, amely tartalmazza a 3x3-as játéktáblát és a "Restart" gombot.
        /// 2. Játéklogika implementációja: A játéklogikát C#-ban írtuk meg, beleértve a játéktábla állapotának kezelését, a játékosok váltakozását és a győzelmi/döntetlen feltételek ellenőrzését.
        /// 3. Eseménykezelés: A játéktáblán történő kattintásokat és a "Restart" gomb megnyomását eseménykezelők kezelik, amelyek aktiválják a megfelelő játéklogikát.
        /// 
        /// Osztályok
        /// - MainWindow: A fő ablak osztálya, amely tartalmazza a játékfelületet és kezeli a játéklogikát.
        /// - GameLogic: A játéklogika osztálya, amely felelős a játéktábla állapotának kezeléséért és a győzelmi/döntetlen feltételek ellenőrzéséért.
        /// 
        /// Használati útmutató
        /// 1. Indítsd el az alkalmazást.
        /// 2. Kattints a játéktáblán egy üres mezőre a szimbólumod elhelyezéséhez.
        /// 3. Váltakozva helyezd el a szimbólumokat mindaddig, amíg valaki nyer vagy a játék döntetlen lesz.
        /// 4. Amennyiben valaki nyer vagy a játék döntetlen lesz, nyomj a "Restart" gombra az új játék indításához
        ///
        /// Ez a fejlesztői dokumentáció segít megismerni és megérteni a Tic Tac Toe játék alapvető működését és implementációját.
        /// </summary>
        private void RestartGame()
        {
            // Clear the board and reset the buttons
            isPlayerXTurn = true;
            InitializeBoard();

            // Reset button content
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
