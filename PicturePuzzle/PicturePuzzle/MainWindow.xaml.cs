using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PicturePuzzle
{
    /// <summary>
    /// A <see cref="MainWindow"/> osztály felelős a <see cref="gameEngine"/> objektum által szolgáltatott adatok vizuális megjelenítéséért.
    /// Tartalma:
    /// <list type="bullet">
    /// <item>
    ///     <see cref="gameEngine"/>: egy <see cref="GameEngine"/> típusú változó, amely a program működéséért felelős</item>
    /// <item>
    ///     <see cref="margins"/>: egy <see cref="Thickness[,]"/> típusú változó, amely a gombok helyzetét tárolja, a gombok margóinak vastagsága segítségével</item>
    /// <item>
    ///     <see cref="buttons"/>: egy <see cref="List{Button}"/> típusú változó, mely a játékban szereplő gombok listája</item>
    /// </list>
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameEngine gameEngine;
        private Thickness[,] margins;
        private List<Button> buttons;
        
        public MainWindow()
        {
            InitializeComponent();

            gameEngine = new GameEngine(GameSolved, SynchronizePositions);

            // Alapállapot, a gombok fix helyzete az ablakban. Ehhez hivatkozva lehet majd a gombokat mozgatni.
            margins = new Thickness[,]
{
                { button1.Margin, button2.Margin, button3.Margin },
                { button4.Margin, button5.Margin, button6.Margin },
                { button7.Margin, button8.Margin, empty.Margin }
            };

            // A játékban szereplő képkockákat a kódban kattintható gombok reprezentálják, melyek egy listába vannak rendezve.
            buttons = new List<Button>()
            { button1, button2, button3, button4, button5, button6, button7, button8, empty};

            gameEngine.RandomizeCurrentPositions();
        }

        /// <summary>
        /// A játék sikeres megoldása esetén a <see cref="GameSolved"/> metódus kerül meghívásra.
        /// </summary>
        private void GameSolved()
        {
            MessageBox.Show("Sikeres megoldás!");
        }

        /// <summary>
        /// A <see cref="SwapButtons(Button, Button)"/> metódus a két paraméterként kapott gomb pozícióját kicseréli
        /// az őket határoló margók szélességének megváltoztatásával.
        /// </summary>
        /// <param name="button1"></param>
        /// <param name="button2"></param>
        public void SwapButtons(Button button1, Button button2)
        {
            var temp_pos = button2.Margin;
            button2.Margin = button1.Margin;
            button1.Margin = temp_pos;
        }

        /// <summary>
        /// A <see cref="SynchronizePositions(string[,])"/> metódus paraméterként kap egy pozíciókból álló táblázatot (kétdimenziós tömb)
        /// és a <see cref="MainWindow"/> által tárolt <see cref="buttons"/> listában található gombok pozícióját szinkronizálja a
        /// beérkező táblázat alapján.
        /// </summary>
        /// <param name="currentPos"></param>
        public void SynchronizePositions(string[,] currentPos)
        {
            for (int i = 0; i < currentPos.GetLength(0); i++)
            {
                for (int j = 0; j < currentPos.GetLength(1); j++)
                {
                    foreach (var button in buttons)
                    {
                        if (currentPos[i, j] == button.Name)
                        {
                            button.Margin = margins[i, j];
                        }
                    }
                }
            }
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

        /// <summary>
        /// A <see cref="RandomButton_Click(object, RoutedEventArgs)"/> metódus a <see cref="gameEngine"/> objektum RandomizeCurrentPositions
        /// metódusát hívja meg, mely véletlenszerűen helyezi el a gombokat a játéktéren.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.RandomizeCurrentPositions();
        }
    }
}
