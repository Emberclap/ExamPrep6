using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Beaver_at_Work
{
    public class Program
    {
        static void Main(string[] args)
        {
            int dimension = int.Parse(Console.ReadLine());

            string[,] matrix = new string[dimension, dimension];

            int currentRowIndex = 0;
            int currentColIndex = 0;
            int branchCounter = 0;

            for (int rows = 0; rows < matrix.GetLength(0); rows++)
            {
                string[] col = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                for (int cols = 0; cols < matrix.GetLength(1); cols++)
                {
                    char ch = char.Parse(col[cols]);
                    matrix[rows, cols] = col[cols];
                    if (col[cols] == "B")
                    {
                        currentRowIndex = rows;
                        currentColIndex = cols;
                    }

                    else if (char.IsLower(ch))
                    {
                        branchCounter++;
                    }
                }
            }
            Stack<string> branches = new Stack<string>();
            string command = string.Empty;
            while ((command = Console.ReadLine()) != "end")
            {
               
                switch (command)
                    {
                        case "up":
                            if (BoundsCheck(currentRowIndex - 1, currentColIndex, matrix))
                            {
                            matrix[currentRowIndex, currentColIndex] = "-";
                            currentRowIndex--;
                                (currentRowIndex, currentColIndex, branchCounter, branches, matrix) 
                                    = PositionAction(currentRowIndex, currentColIndex, matrix, branches, branchCounter, command);
                            }
                            else
                            { 
                                if (branches.Any())
                                {
                                    branches.Pop();
                                }
                            }

                            break;
                        case "down":
                            if (BoundsCheck(currentRowIndex + 1, currentColIndex, matrix))
                            {
                                matrix[currentRowIndex, currentColIndex] = "-";
                                currentRowIndex++;
                                (currentRowIndex, currentColIndex, branchCounter, branches, matrix)
                                                                    = PositionAction(currentRowIndex, currentColIndex, matrix, branches, branchCounter, command);
                            }
                            else
                            {
                                if (branches.Any())
                                {
                                    branches.Pop();
                                }
                            }
                            break;
                        case "right":
                            if (BoundsCheck(currentRowIndex, currentColIndex + 1, matrix))
                            {
                                 matrix[currentRowIndex, currentColIndex] = "-";
                                 currentColIndex++;
                                 (currentRowIndex, currentColIndex, branchCounter, branches, matrix)
                                                                    = PositionAction(currentRowIndex, currentColIndex, matrix, branches, branchCounter, command);
                            }
                            else
                            {
                                if (branches.Any())
                                {
                                    branches.Pop();
                                }
                            }
                            break;
                        case "left":
                            if (BoundsCheck(currentRowIndex, currentColIndex - 1, matrix))
                            {
                                matrix[currentRowIndex, currentColIndex] = "-";
                                currentColIndex--;
                                (currentRowIndex, currentColIndex, branchCounter, branches, matrix)
                                                                    = PositionAction(currentRowIndex, currentColIndex, matrix, branches, branchCounter, command);
                            }
                            else
                            {
                                if (branches.Any())
                                {
                                    branches.Pop();
                                }
                            }
                            break;

                    }
                if (branchCounter == 0)
                {
                    break;
                }

            }
            
            if (branchCounter > 0)
            {
                Console.WriteLine($"The Beaver failed to collect every wood branch. There are {branchCounter} branches left.");
            }
            else
            {
                Console.WriteLine($"The Beaver successfully collect {branches.Count} wood branches: {string.Join(", ", branches.Reverse())}.");
            }
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write($"{matrix[row, col]} ");
                }
                Console.WriteLine();
            }

        }
        public static bool BoundsCheck(int rowIndex, int colIndex, string[,] matrix)
        {
            if (rowIndex >= 0 && colIndex >= 0 && rowIndex < matrix.GetLength(0) && colIndex < matrix.GetLength(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static Tuple<int, int, int, Stack<string>, string[,]> 
            PositionAction(int row, int col, string[,] matrix, Stack<string> branches, int branchCounter, string direction)
        {

            if (matrix[row, col] == "F" && direction == "up")
            {
                matrix[row, col] = "-";
                if (row == 0)
                {
                    row = matrix.GetUpperBound(0);
                }
                else
                {
                    row = 0;
                }
                
            }
            else if (matrix[row,col] == "F" && direction == "down")
            {
                matrix[row, col] = "-";
                if (row == matrix.GetUpperBound(0))
                {
                    row = 0;
                }
                else
                {
                    row = matrix.GetUpperBound(0);
                }
            }
            else if (matrix[row, col] == "F" && direction == "left")
            {
                matrix[row, col] = "-";
                if (col == 0)
                {
                    col = matrix.GetUpperBound(0);
                }
                else
                {
                    col = 0;
                }
            }
            else if (matrix[row, col] == "F" && direction == "right")
            {
                matrix[row, col] = "-";
                if (col == matrix.GetUpperBound(1))
                {
                    col = 0;
                }
                else
                {
                    col = matrix.GetUpperBound(1);
                }
            }
            if (IsBranch(row, col, matrix))
            {
                branches.Push(matrix[row, col]);
                branchCounter--;
                matrix[row, col] = "-";
            }
            matrix[row, col] = "B";
            return Tuple.Create(row, col, branchCounter, branches, matrix);
        }
        public static bool IsBranch(int rowIndex, int colIndex, string[,] matrix)
        {
            char ch = char.Parse(matrix[rowIndex, colIndex]);
            if (char.IsLower(ch))
            {
                return true;
            }
            return false;
        }
    }

}
