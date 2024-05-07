using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PicturePuzzle
{
    /// <summary>
    /// A <see cref="GameEngine"/> osztály felelős a játékmechanikák szolgáltatásáért, és a pillanatnyi pozíciók kezeléséért.
    /// Paraméterként kapott delegáltakon keresztül (<see cref="gameSolved"/>, <see cref="syncPositions"/>) aktívan kommunikál a <see cref="MainWindow"/>
    /// osztállyal, mely pedig az adatok megjelenítéséért felelős.
    /// <see cref="currentPos"/>: Pozíció-mátrix: tárolja a gombok pillanatnyi pozícióját.
    /// <see cref="solution"/>: tárolja a helyes megoldás mátrixát
    /// </summary>
    public class GameEngine
    {
        public string[,] solution;
        public string[,] currentPos;
        private Action gameSolved;
        private Action<string[,]> syncPositions;

        public GameEngine(Action solved, Action<string[,]> syncPos)
        {
            solution = new string[,]
            {
                { "button1", "button2", "button3" },
                { "button4", "button5", "button6" },
                { "button7", "button8", "empty" }
            };

            currentPos = new string[,]
            {
                { "button1", "button2", "button3" },
                { "button4", "button5", "button6" },
                { "button7", "button8", "empty" }
            };

            gameSolved = solved;
            syncPositions = syncPos;
        }

        /// <summary>
        /// A <see cref="TryMove(Button, Button, Action{Button, Button})"/> metódus megkeresi a lenyomott gomb pozícióját a <see cref="FindButtonPosition(string)"/>
        /// metódus segítségével, és leellenőrzi, hogy az üres hely mellett található-e. Ha igen, akkor kicseréli a pozíciójukat a tárolt
        /// tömbben, és a swapButtons paraméter segítségével a felhasználói felületen is.
        /// </summary>
        /// <param name="clickedButton">A lenyomott gomb</param>
        /// <param name="emptyButton">Az üres hely</param>
        /// <param name="swapButtons">Az a delegált, amely meghatározza a gombok kicserélésének módját. Ezt a <see cref="MainWindow"/> definiálja</param>
        public void TryMove(Button clickedButton, Button emptyButton, Action<Button, Button> swapButtons)
        {
            string buttonId = clickedButton.Name;
            (int buttonPos_x, int buttonPos_y) = FindButtonPosition(buttonId);

            if (IsNextToEmpty(buttonPos_x, buttonPos_y))
            {
                swapButtons(clickedButton, emptyButton);
                UpdateArrayPosition(buttonPos_x, buttonPos_y);

                if(IsSolved())
                { gameSolved(); }
            }
        }

        /// <summary>
        /// A <see cref="RandomizeCurrentPositions"/> metódus a gombokat véletlenszerűen rendezi el a játéktérben.
        /// Ezt úgy teszi meg, hogy a mátrixban tárolt pozíciók közül generál egy véletlenszerű mátrix-beli pozíciót,
        /// és annak értékét rendeli hozzá a jelenleg soron lévő gombhoz. A véletlenszerűen generált pozíciókról egy listát
        /// tárol, és duplikáció-elkerülést végez a segítségével.
        /// </summary>
        public void RandomizeCurrentPositions()
        {
            Random random = new Random();
            List<Tuple<int, int>> takenPositions = new  List<Tuple<int, int>>();
            string[,] tempPos = new string[3, 3];

            for (int i = 0; i < currentPos.GetLength(0); i++)
            {
                for (int j = 0; j < currentPos.GetLength(1); j++)
                {
                    Tuple<int, int> new_x_y;
                    do
                    {
                        new_x_y = new Tuple<int, int>(random.Next(0, 3), random.Next(0, 3));
                        if (!takenPositions.Contains(new_x_y))
                        {
                            takenPositions.Add(new_x_y);
                            break;
                        }
                    } while (takenPositions.Contains(new_x_y));
                    tempPos[new_x_y.Item1, new_x_y.Item2] = currentPos[i, j];
                }
            }

            currentPos = tempPos;
            syncPositions(currentPos);
        }

        /// <summary>
        /// A <see cref="FindButtonPosition(string)"/> metódus megkeresi a bemenő paraméterként kapott gomb pozícióját
        /// a pozíció-mátrixban egy egyszerű kiválasztással.
        /// </summary>
        /// <param name="buttonId">A keresendő gomb neve</param>
        /// <returns></returns>
        private (int, int) FindButtonPosition(string buttonId)
        {
            (int, int) buttonPos = (0, 0);

            for (int i = 0; i < currentPos.GetLength(0); i++)
            {
                for (int j = 0; j < currentPos.GetLength(1); j++)
                {
                    if (currentPos[i, j] == buttonId)
                    { buttonPos = (i, j); }
                }
            }

            return buttonPos;
        }

        /// <summary>
        /// Az <see cref="IsNextToEmpty(int, int)"/> metódus igaz értékkel tér vissza, ha a megadott pozíciójú gomb határos az üres térrel,
        /// és hamis értékkel tér vissza, ha a megadott pozíciójú gomb nem határos az üres térrel.
        /// </summary>
        /// <param name="buttonPos_x">A keresett gomb x koordinátája</param>
        /// <param name="buttonPos_y">A keresett gomb y koordinátája</param>
        private bool IsNextToEmpty(int buttonPos_x, int buttonPos_y)
        {
            if ((buttonPos_x != 0) && (currentPos[buttonPos_x - 1, buttonPos_y] == "empty"))
            { return true; }
            else if ((buttonPos_x != 2) && (currentPos[buttonPos_x + 1, buttonPos_y] == "empty"))
            { return true; }
            else if ((buttonPos_y != 0) && (currentPos[buttonPos_x, buttonPos_y - 1] == "empty"))
            { return true; }
            else if ((buttonPos_y != 2) && (currentPos[buttonPos_x, buttonPos_y + 1] == "empty"))
            { return true; }

            return false;
        }

        /// <summary>
        /// Az <see cref="UpdateArrayPosition(int, int)"/> metódus felcseréli a paraméterként kapott koordináták mögött található gombot
        /// az üres térrel a <see cref="GameEngine"/> által tárolt pozíció-mátrixban.
        /// </summary>
        /// <param name="buttonPos_x">A keresett gomb x koordinátája</param>
        /// <param name="buttonPos_y">A keresett gomb y koordinátája</param>
        private void UpdateArrayPosition(int buttonPos_x, int buttonPos_y)
        {
            (int emptyPos_x, int emptyPos_y) = FindButtonPosition("empty");
            currentPos[emptyPos_x, emptyPos_y] = currentPos[buttonPos_x, buttonPos_y];
            currentPos[buttonPos_x, buttonPos_y] = "empty";
        }

        /// <summary>
        /// Az <see cref="IsSolved"/> metódus ellenőrzi, hogy a jelenlegi pozíció megegyezik-e a tárolt megoldás pozícióival.
        /// </summary>
        /// <returns>Igaz, ha a megoldás jó, hamis, ha a megoldás hibás</returns>
        private bool IsSolved()
        {
            bool solved = true;

            for (int i = 0; i < currentPos.GetLength(0); i++)
            {
                for (int j = 0; j < currentPos.GetLength(1); j++)
                {
                    if (currentPos[i, j] != solution[i, j])
                    { solved = false; }
                }
            }

            return solved;
        }
    }
}
