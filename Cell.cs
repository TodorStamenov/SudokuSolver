using System;

public class Cell
{
    private int value;

    public Cell(int row, int col, int value)
    {
        this.Row = row;
        this.Col = col;
        this.Value = value;
    }

    public int Row { get; }

    public int Col { get; }

    public int Value
    {
        get { return this.value; }
        set
        {
            if (value < 0 || 9 < value)
            {
                throw new Exception("Cell value must be in range [1, 9]!");
            }

            this.value = value;
        }
    }

    public int Quadrant
    {
        get { return this.GetQuadrant(); }
    }

    private int GetQuadrant()
    {
        if (0 <= this.Row && this.Row < 3 && 0 <= this.Col && this.Col < 3)
        {
            return 0;
        }

        if (0 <= this.Row && this.Row < 3 && 3 <= this.Col && this.Col < 6)
        {
            return 1;
        }

        if (0 <= this.Row && this.Row < 3 && 6 <= this.Col && this.Col < 9)
        {
            return 2;
        }

        if (3 <= this.Row && this.Row < 6 && 0 <= this.Col && this.Col < 3)
        {
            return 3;
        }

        if (3 <= this.Row && this.Row < 6 && 3 <= this.Col && this.Col < 6)
        {
            return 4;
        }

        if (3 <= this.Row && this.Row < 6 && 6 <= this.Col && this.Col < 9)
        {
            return 5;
        }

        if (6 <= this.Row && this.Row < 9 && 0 <= this.Col && this.Col < 3)
        {
            return 6;
        }

        if (6 <= this.Row && this.Row < 9 && 3 <= this.Col && this.Col < 6)
        {
            return 7;
        }

        if (6 <= this.Row && this.Row < 9 && 6 <= this.Col && this.Col < 9)
        {
            return 8;
        }

        return -1;
    }
}