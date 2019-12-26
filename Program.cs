using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class Program
    {
        private static LinkedList<Cell> missingCells = new LinkedList<Cell>();
        private static readonly int[][] initialField = new int[9][];
        private static readonly HashSet<int>[] rowValues = new HashSet<int>[9];
        private static readonly HashSet<int>[] colValues = new HashSet<int>[9];
        private static readonly HashSet<int>[] quadrantValues = new HashSet<int>[9];

        public static void Main()
        {
            ReadInput();
            Solve(missingCells.First);
        }

        private static void ReadInput()
        {
            for (int i = 0; i < 9; i++)
            {
                rowValues[i] = new HashSet<int>();
                colValues[i] = new HashSet<int>();
                quadrantValues[i] = new HashSet<int>();  
            }

            for (int row = 0; row < 9; row++)
            {
                initialField[row] = Console
                    .ReadLine()
                    .Trim()
                    .ToCharArray()
                    .Select(n => int.Parse(n.ToString()))
                    .ToArray();

                for (int col = 0; col < initialField[row].Length; col++)
                {
                    var value = initialField[row][col];
                    var cell = new Cell(row, col, value);

                    if (cell.Value == 0)
                    {
                        missingCells.AddLast(cell);
                    }
                    else if (cell.Value > 0)
                    {
                        rowValues[row].Add(cell.Value);
                        colValues[col].Add(cell.Value);
                        quadrantValues[cell.Quadrant].Add(cell.Value);                        
                    }
                }
            }
        }

        private static void Solve(LinkedListNode<Cell> currentCellNode)
        {
            if (currentCellNode == null)
            {
                PrintResult();
                Environment.Exit(0);
            }

            var currentCell = currentCellNode.Value;

            for (int i = 1; i <= 9; i++)
            {
                int row = currentCell.Row;
                int col = currentCell.Col;
                int quadrant = currentCell.Quadrant;

                if (!rowValues[row].Contains(i) &&
                    !colValues[col].Contains(i) &&
                    !quadrantValues[quadrant].Contains(i))
                {                    
                    currentCell.Value = i;
                    rowValues[row].Add(i);
                    colValues[col].Add(i);
                    quadrantValues[quadrant].Add(i);

                    Solve(currentCellNode.Next);

                    currentCell.Value = 0;
                    rowValues[row].Remove(i);
                    colValues[col].Remove(i);
                    quadrantValues[quadrant].Remove(i);
                }
            }
        }

        private static void PrintResult()
        {
            var currentNode = missingCells.First;

            while (currentNode != null)
            {
                var cell = currentNode.Value;

                initialField[cell.Row][cell.Col] = cell.Value;
                currentNode = currentNode.Next;
            }

            foreach (var item in initialField)
            {
                Console.WriteLine(string.Join(" ", item));
            }
        }
    }
}
