namespace _08.DistanceInLabyrinth
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        private static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            string [,] labyrinth = new string[n,n];

            int[] startPosition = new int [2];

            FillLabyrinth(n, startPosition, labyrinth);

            Travel(labyrinth, startPosition,new List<int[]>(),  0);

            FillUnreachedCells(n, labyrinth);

            PrintLabyrinth(n, labyrinth);
        }

        private static void PrintLabyrinth(int n, string[,] labyrinth)
        {
            Console.WriteLine();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(labyrinth[i, j] + " ");
                }

                Console.WriteLine();
            }
        }

        private static void FillLabyrinth(int n, int[] startPosition, string[,] labyrinth)
        {
            for (int row = 0; row < n; row++)
            {
                string[] inputRow = Console.ReadLine().Split();
                for (int column = 0; column < n; column++)
                {
                    if (inputRow[column] == "*")
                    {
                        startPosition[0] = row;
                        startPosition[1] = column;
                    }

                    labyrinth[row, column] = inputRow[column];
                }
            }
        }

        private static void FillUnreachedCells(int n, string[,] labyrinth)
        {
            for (int row = 0; row < n; row++)
            {
                for (int column = 0; column < n; column++)
                {
                    if (labyrinth[row, column] == "0")
                    {
                        labyrinth[row, column] = "u";
                    }
                }
            }
        }

        private static void Travel(string[,] labyrinth, int[] currentPosition,List<int []> passedCells, int steps)
        {
           bool isPossibleToGoLeft = GoLeft(labyrinth, currentPosition, passedCells);
            if (isPossibleToGoLeft)
            {
                    steps++;
                int cellValue = int.Parse(labyrinth[currentPosition[0], currentPosition[1]]);
                if (cellValue == 0 || cellValue > steps)
                {
                    labyrinth[currentPosition[0], currentPosition[1]] = steps.ToString();
                }

                var toAdd = new int[] {currentPosition[0], currentPosition[1]};
                    passedCells.Add(toAdd);
                    Travel(labyrinth, currentPosition, passedCells, steps);
                currentPosition = new int[] { toAdd[0], toAdd[1] + 1 };
                steps--;
            }

            bool isPossibleToGoUp = GoUp(labyrinth, currentPosition, passedCells);
            if (isPossibleToGoUp)
            {
                    steps++;
                int cellValue = int.Parse(labyrinth[currentPosition[0], currentPosition[1]]);
                if (cellValue == 0 || cellValue > steps)
                {
                    labyrinth[currentPosition[0], currentPosition[1]] = steps.ToString();
                }

                var toAdd = new int[] {currentPosition[0], currentPosition[1]};
                    passedCells.Add(toAdd);
                    Travel(labyrinth, currentPosition,passedCells, steps);
                currentPosition = new int[] { toAdd[0] + 1, toAdd[1] };
                steps--;
            }

            bool isPossibleToGoRight = GoRight(labyrinth, currentPosition, passedCells);
            if (isPossibleToGoRight)
            {
                    steps++;
                int cellValue = int.Parse(labyrinth[currentPosition[0], currentPosition[1]]);
                if (cellValue == 0 || cellValue > steps)
                {
                    labyrinth[currentPosition[0], currentPosition[1]] = steps.ToString();
                }

                var toAdd = new int[] {currentPosition[0], currentPosition[1]};
                    passedCells.Add(toAdd);
                    Travel(labyrinth, currentPosition, passedCells, steps);
                currentPosition = new int[] { toAdd[0], toAdd[1] - 1 };
                steps--;
            }

            bool isPossibleToGoDown = GoDown(labyrinth, currentPosition, passedCells);
            if (isPossibleToGoDown)
            {
                    steps++;
                int cellValue = int.Parse(labyrinth[currentPosition[0], currentPosition[1]]);
                if (cellValue == 0 || cellValue > steps)
                {
                    labyrinth[currentPosition[0], currentPosition[1]] = steps.ToString();
                }

                var toAdd = new int[] {currentPosition[0], currentPosition[1]};
                    passedCells.Add(toAdd);
                    Travel(labyrinth, currentPosition, passedCells, steps);
                currentPosition = new int[] { toAdd[0] - 1, toAdd[1]};
                     steps--;
            }

            int count = passedCells.Count - 1;
            if (count >= 0)
            {
                passedCells.RemoveAt(count);
            }
        }

        private static bool GoRight(string[,] labyrinth, int[] currentPosition, List<int []> passedCells )
        {
            int changedColumn = currentPosition[1] + 1;
            if (changedColumn >= labyrinth.GetLength(1))
            {
                return false;
            }

            if (labyrinth[currentPosition[0], changedColumn] == "x" ||
                labyrinth[currentPosition[0], changedColumn] == "*")
            {
                return false;
            }

            currentPosition[1]++;
            var isPassedCell = CheckIfItIsPassedCell(currentPosition, passedCells);

            if (isPassedCell)
            {
                currentPosition[1] --;
                return false;
            }

            return true;
        }

        private static bool CheckIfItIsPassedCell(int[] currentPosition, List<int[]> passedCells)
        {
            bool isPassedCell = false;
            foreach (var passedCell in passedCells)
            {
                if (passedCell[0] == currentPosition[0] &&
                    passedCell[1] == currentPosition[1])
                {
                    isPassedCell = true;
                }
            }

            return isPassedCell;
        }

        private static bool GoLeft(string[,] labyrinth, int[] currentPosition, List<int[]> passedCells)
        {
            int changedColumn = currentPosition[1] - 1;
            if (changedColumn < 0)
            {
                return false;
            }

            if (labyrinth[currentPosition[0], changedColumn] == "x" ||
                labyrinth[currentPosition[0], changedColumn] == "*")
            {
                return false;
            }

            currentPosition[1]--;
            var isPassedCell = CheckIfItIsPassedCell(currentPosition, passedCells);
            if (isPassedCell)
            {
                currentPosition[1]++;
                return false;
            }

            return true;
        }

        private static bool GoUp(string[,] labyrinth, int[] currentPosition, List<int[]> passedCells)
        {
            int changedRow = currentPosition[0] - 1;
            if (changedRow < 0)
            {
                return false;
            }

            if (labyrinth[changedRow, currentPosition[1]] == "x" ||
                labyrinth[changedRow, currentPosition[1]] == "*")
            {
                return false;
            }

            currentPosition[0]--;
            var isPassedCell = CheckIfItIsPassedCell(currentPosition, passedCells);
            if (isPassedCell)
            {
                currentPosition[0]++;
                return false;
            }

            return true;
        }

        private static bool GoDown(string[,] labyrinth, int[] currentPosition, List<int[]> passedCells)
        {
            int changedRow = currentPosition[0] + 1;
            if (changedRow >= labyrinth.GetLength(0))
            {
                return false;
            }

            if (labyrinth[changedRow, currentPosition[1]] == "x" ||
                labyrinth[changedRow, currentPosition[1]] == "*")
            {
                return false;
            }

            currentPosition[0]++;
            var isPassedCell = CheckIfItIsPassedCell(currentPosition, passedCells);
            if (isPassedCell)
            {
                currentPosition[0] --;
                return false;
            }

            return true;
        }
    }
}