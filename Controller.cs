using System;
using System.Collections.Generic;
using System.Linq;

public class Controller
{
    private readonly LinkedList<Cell> missingCells;
    private readonly int[][] initialField;
    private readonly HashSet<int>[] rowValues;
    private readonly HashSet<int>[] colValues;
    private readonly HashSet<int>[] quadrantValues;

    public Controller()
    {
        this.missingCells = new LinkedList<Cell>();
        this.initialField = new int[9][];
        this.rowValues = new HashSet<int>[9];
        this.colValues = new HashSet<int>[9];
        this.quadrantValues = new HashSet<int>[9];

        this.ReadInput();
        this.Solve(this.missingCells.First);
    }

    private void ReadInput()
    {
        for (int i = 0; i < 9; i++)
        {
            this.rowValues[i] = new HashSet<int>();
            this.colValues[i] = new HashSet<int>();
            this.quadrantValues[i] = new HashSet<int>();
        }

        for (int row = 0; row < 9; row++)
        {
            this.initialField[row] = Console
                .ReadLine()
                .Trim()
                .ToCharArray()
                .Select(n => char.IsNumber(n) ? int.Parse(n.ToString()) : 0)
                .ToArray();

            if (this.initialField[row].Length != 9)
            {
                throw new Exception("There must be 9 cells per row!");
            }

            for (int col = 0; col < this.initialField[row].Length; col++)
            {
                var value = this.initialField[row][col];
                var cell = new Cell(row, col, value);

                if (cell.Value == 0)
                {
                    this.missingCells.AddLast(cell);
                }
                else
                {
                    if (this.rowValues[row].Contains(cell.Value) ||
                        this.colValues[col].Contains(cell.Value) ||
                        this.quadrantValues[cell.Quadrant].Contains(cell.Value))
                    {
                        throw new Exception("Each value must be unique for row col and quadrant!");
                    }

                    this.rowValues[row].Add(cell.Value);
                    this.colValues[col].Add(cell.Value);
                    this.quadrantValues[cell.Quadrant].Add(cell.Value);
                }
            }
        }
    }

    private void Solve(LinkedListNode<Cell> currentCellNode)
    {
        if (currentCellNode == null)
        {
            this.PrintResult();
            Environment.Exit(0);
        }

        var currentCell = currentCellNode.Value;

        for (int i = 1; i <= 9; i++)
        {
            int row = currentCell.Row;
            int col = currentCell.Col;
            int quadrant = currentCell.Quadrant;

            if (!this.rowValues[row].Contains(i) &&
                !this.colValues[col].Contains(i) &&
                !this.quadrantValues[quadrant].Contains(i))
            {
                currentCell.Value = i;
                this.rowValues[row].Add(i);
                this.colValues[col].Add(i);
                this.quadrantValues[quadrant].Add(i);

                this.Solve(currentCellNode.Next);

                currentCell.Value = 0;
                this.rowValues[row].Remove(i);
                this.colValues[col].Remove(i);
                this.quadrantValues[quadrant].Remove(i);
            }
        }
    }

    private void PrintResult()
    {
        var currentNode = this.missingCells.First;

        while (currentNode != null)
        {
            var cell = currentNode.Value;

            this.initialField[cell.Row][cell.Col] = cell.Value;
            currentNode = currentNode.Next;
        }

        foreach (var row in this.initialField)
        {
            Console.WriteLine(string.Join(" ", row));
        }
    }
}