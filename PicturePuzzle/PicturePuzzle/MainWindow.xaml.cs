using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private Thickness[,] margins;

        public MainWindow()
        {
            InitializeComponent();

            gameEngine = new GameEngine(GameSolved);

            //Alapállapot, a gombok fix helyzete az ablakban. Ehhez hivatkozva lehet majd a gombokat mozgatni.
            margins = new Thickness[,]
{
                { button1.Margin, button2.Margin, button3.Margin },
                { button4.Margin, button5.Margin, button6.Margin },
                { button7.Margin, button8.Margin, empty.Margin }
};
        }

        private void GameSolved()
        {
            MessageBox.Show("Sikeres megoldás!");
        }

        public void SwapButtons(Button button1, Button button2)
        {
            var temp_pos = button2.Margin;
            button2.Margin = button1.Margin;
            button1.Margin = temp_pos;
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button1, empty, SwapButtons);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button2, empty, SwapButtons);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button3, empty, SwapButtons);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button4, empty, SwapButtons);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button5, empty, SwapButtons);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button6, empty, SwapButtons);
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button7, empty, SwapButtons);
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.TryMove(button8, empty, SwapButtons);
        }
    }
}
