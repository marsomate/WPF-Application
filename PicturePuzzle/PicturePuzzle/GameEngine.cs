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

        public void TryMove(Button clickedButton, Button emptyButton, Action<Button, Button> swapButtons)
        {
            string buttonId = clickedButton.Name;
            (int buttonPos_x, int buttonPos_y) = FindButtonPosition(buttonId);
            Debug.WriteLine(currentPos[0, 0]);
            Debug.WriteLine(currentPos[1, 2]);
            Debug.WriteLine(currentPos[2, 1]);
            if (IsNextToEmpty(buttonPos_x, buttonPos_y))
            {
                swapButtons(clickedButton, emptyButton);
                UpdateArrayPosition(buttonPos_x, buttonPos_y);

                if(IsSolved())
                { gameSolved(); }
            }
        }

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

        private void UpdateArrayPosition(int buttonPos_x, int buttonPos_y)
        {
            (int emptyPos_x, int emptyPos_y) = FindButtonPosition("empty");
            Debug.Write($"Empty {emptyPos_x}, {emptyPos_y}");
            Debug.Write($"\nButton {buttonPos_x}, {buttonPos_y}");
            currentPos[emptyPos_x, emptyPos_y] = currentPos[buttonPos_x, buttonPos_y];
            currentPos[buttonPos_x, buttonPos_y] = "empty";

            Debug.Write('\n');
            for (int i = 0; i < currentPos.GetLength(0); i++)
            {
                for (int j = 0; j < currentPos.GetLength(1); j++)
                {
                    Debug.Write(currentPos[i, j]);
                }
                Debug.Write('\n');
            }
        }

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
