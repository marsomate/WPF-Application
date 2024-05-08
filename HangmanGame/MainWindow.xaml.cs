using System;
using System.Collections.Generic;
using System.Windows;

namespace HangmanGame
{
    /// <summary>
    /// A Windows alkalmazás főablaka, amely tartalmazza a Akasztófa játék logikáját és felhasználói felületét.
    /// </summary>
    public partial class MainWindow : Window
    {
        private string wordToGuess;
        private string displayedWord;
        private int attemptsLeft = 6;
        private List<char> wrongGuesses = new List<char>();

        /// <summary>
        /// A MainWindow osztály konstruktora, amely inicializálja az ablakot és feliratkozik a Loaded eseményre.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded; // Feliratkozás a Loaded eseményre
        }

        /// <summary>
        /// A MainWindow osztály Loaded eseménykezelője, amely meghívja a játék inicializálását.
        /// </summary>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }

        /// <summary>
        /// Inicializálja a játékot, kiválaszt egy véletlen szót a szókincsből.
        /// </summary>
        private void InitializeGame()
        {
            // Szavak tömbje a kitalálandó szavakhoz
            string[] words = { "akasztófa", "programozás", "számítógép", "billentyűzet", "algoritmus", "szoftver" };

            // Véletlen szó kiválasztása a tömbből
            Random random = new Random();
            wordToGuess = words[random.Next(words.Length)];

            // Inicializálás a megjelenített szóval
            displayedWord = new string('_', wordToGuess.Length);
            wordToGuessTextBlock.Text = displayedWord;
        }

        /// <summary>
        /// A "Tippelés" gomb eseménykezelője, amely ellenőrzi a felhasználó által megadott tippet és feldolgozza azt.
        /// </summary>
        private void GuessButton_Click(object sender, RoutedEventArgs e)
        {
            string guess = guessTextBox.Text.ToLower();

            if (guess.Length != 1 || !char.IsLetter(guess[0]))
            {
                resultTextBlock.Text = "Érvénytelen tipp. Kérlek adj meg egyetlen betűt.";
                return;
            }

            // Ellenőrizze, hogy a tippelt betű benne van-e a szóban
            bool letterFound = false;
            char guessedChar = guess[0];
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == guessedChar)
                {
                    displayedWord = displayedWord.Substring(0, i) + guessedChar + displayedWord.Substring(i + 1);
                    letterFound = true;
                }
            }

            if (!letterFound)
            {
                attemptsLeft--;
                wrongGuesses.Add(guessedChar);
                resultTextBlock.Text = $"Rossz tipp! Még {attemptsLeft} kísérlet van hátra.";
            }
            else
            {
                resultTextBlock.Text = "Helyes tipp!";
            }

            wordToGuessTextBlock.Text = displayedWord;

            // Ellenőrizzük, hogy a szót teljesen kitalálták-e
            if (displayedWord == wordToGuess)
            {
                resultTextBlock.Text = "Gratulálok! Kitaláltad a szót!";
                guessTextBox.IsEnabled = false;
            }

            // Ellenőrizzük, hogy a játékos felhasználta-e az összes lehetőséget
            if (attemptsLeft == 0)
            {
                resultTextBlock.Text = $"Játék vége! A szó: {wordToGuess}.";
                guessTextBox.IsEnabled = false;
            }

            guessTextBox.Text = ""; // Törlés a szövegdobozból minden tipp után

            // Helytelen tippek megjelenítése
            wrongGuessesTextBlock.Text = "Helytelen tippek: " + string.Join(", ", wrongGuesses);
        }
    }
}
