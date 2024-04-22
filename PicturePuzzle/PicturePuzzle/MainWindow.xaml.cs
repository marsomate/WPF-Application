using System;
using System.Collections.Generic;
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

namespace PicturePuzzle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameEngine gameEngine;

        public MainWindow()
        {
            InitializeComponent();

            gameEngine = new GameEngine();
        }


        public void SwapButtons(Button button)
        {
            var empty_pos = empty.Margin;
            empty.Margin = button.Margin;
            button.Margin = empty_pos;
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button1, SwapButtons);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button2, SwapButtons);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button3, SwapButtons);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button4, SwapButtons);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button5, SwapButtons);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button6, SwapButtons);
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button7, SwapButtons);
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button8, SwapButtons);
        }
    }
}
