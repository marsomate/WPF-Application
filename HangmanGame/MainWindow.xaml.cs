using System;
using System.Collections.Generic;
using System.Windows;

namespace HangmanGame
{
    public partial class MainWindow : Window
    {
        private string wordToGuess;
        private string displayedWord;
        private int attemptsLeft = 6;
        private List<char> wrongGuesses = new List<char>();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded; // Subscribe to the Loaded event
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Array of words to guess
            string[] words = { "hangman", "programming", "computer", "keyboard", "algorithm", "software" };

            // Select a random word from the array
            Random random = new Random();
            wordToGuess = words[random.Next(words.Length)];

            // Initialize displayed word
            displayedWord = new string('_', wordToGuess.Length);
            wordToGuessTextBlock.Text = displayedWord;
        }

        private void GuessButton_Click(object sender, RoutedEventArgs e)
        {
            string guess = guessTextBox.Text.ToLower();

            if (guess.Length != 1 || !char.IsLetter(guess[0]))
            {
                resultTextBlock.Text = "Invalid guess. Please enter a single letter.";
                return;
            }

            // Check if the guessed letter is in the word
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
                resultTextBlock.Text = $"Incorrect guess! {attemptsLeft} attempts left.";
            }
            else
            {
                resultTextBlock.Text = "Correct guess!";
            }

            wordToGuessTextBlock.Text = displayedWord;

            // Check if the word has been guessed completely
            if (displayedWord == wordToGuess)
            {
                resultTextBlock.Text = "Congratulations! You've guessed the word!";
                guessTextBox.IsEnabled = false;
            }

            // Check if the player has used all attempts
            if (attemptsLeft == 0)
            {
                resultTextBlock.Text = $"Game over! The word was: {wordToGuess}.";
                guessTextBox.IsEnabled = false;
            }

            guessTextBox.Text = ""; // Clear the textbox after each guess

            // Display wrong guesses
            wrongGuessesTextBlock.Text = "Wrong guesses: " + string.Join(", ", wrongGuesses);
        }
    }
}
