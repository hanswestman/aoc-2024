global using Coord = (int x, int y);

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}

public class Part1
{
    protected char[][] grid;

    public Part1()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

        grid = File.ReadAllLines(path).Select(line => line.ToCharArray()).ToArray();
    }

    public int GetSolution()
    {
        Direction direction = Direction.Up;
        Coord position = FindStartCoord();

        MarkPosition(position);

        Coord nextPosition = GetNextPosition(position, direction);

        while (!IsCoordOutsideGrid(nextPosition))
        {
            while (GetCell(nextPosition) == '#')
            {
                direction = TurnRight(direction);
                nextPosition = GetNextPosition(position, direction);
            }

            position = nextPosition;

            MarkPosition(position);

            nextPosition = GetNextPosition(position, direction);
        }

        return GetUniqueSteps();
    }

    protected Coord FindStartCoord()
    {
        for (int y = 0; y < grid.Count(); y++)
        {
            for (int x = 0; x < grid[0].Count(); x++)
            {
                if (grid[y][x] == '^')
                {
                    return (x, y);
                }
            }
        }

        return (0, 0);
    }

    protected Coord GetNextPosition(Coord coord, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return (coord.x, coord.y - 1);
            case Direction.Right:
                return (coord.x + 1, coord.y);
            case Direction.Down:
                return (coord.x, coord.y + 1);
            case Direction.Left:
            default:
                return (coord.x - 1, coord.y);
        }
    }

    protected bool IsCoordOutsideGrid(Coord coord)
    {
        return coord.x < 0
            || coord.x >= grid[0].Count()
            || coord.y < 0
            || coord.y >= grid.Count();
    }

    protected char GetCell(Coord coord)
    {
        return grid[coord.y][coord.x];
    }

    protected Direction TurnRight(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Direction.Right;
            case Direction.Right:
                return Direction.Down;
            case Direction.Down:
                return Direction.Left;
            case Direction.Left:
            default:
                return Direction.Up;
        }
    }

    protected void MarkPosition(Coord coord)
    {
        grid[coord.y][coord.x] = 'X';
    }

    private int GetUniqueSteps()
    {
        int steps = 0;

        for (int y = 0; y < grid.Count(); y++)
        {
            for (int x = 0; x < grid[0].Count(); x++)
            {
                if (grid[y][x] == 'X')
                {
                    steps++;
                }
            }
        }

        return steps;
    }

}