using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameMenu
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

        private void TicTacToe_button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("..\\..\\..\\..\\TicTacToe\\bin\\Release\\MateBalazsSzokereso.exe");
        }

        private void Hangman_button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("..\\..\\..\\..\\HangmanGame\\bin\\Release\\net8.0-windows\\WPFTestApp.exe");
        }

        private void PicturePuzzle_button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("..\\..\\..\\..\\PicturePuzzle\\PicturePuzzle\\bin\\Release\\PicturePuzzle.exe");
        }
    }
}
