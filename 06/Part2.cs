public class Part2 : Part1
{
    new public int GetSolution()
    {
        int loopedPositions = 0;

        List<Coord> availablePositions = new List<Coord>();
        for (int y = 0; y < grid.Count(); y++)
        {
            for (int x = 0; x < grid[0].Count(); x++)
            {
                if (grid[y][x] == '.')
                {
                    availablePositions.Add((x, y));
                }
            }
        }

        foreach (Coord obstaclePosition in availablePositions)
        {
            if (!HasSolution(obstaclePosition))
            {
                loopedPositions++;
            }
        }

        return loopedPositions;
    }

    bool HasSolution(Coord obstaclePosition)
    {
        grid[obstaclePosition.y][obstaclePosition.x] = '#';

        List<Coord> turns = new List<Coord>();

        Direction direction = Direction.Up;
        Coord position = FindStartCoord();

        Coord nextPosition = GetNextPosition(position, direction);

        while (!IsCoordOutsideGrid(nextPosition))
        {
            bool hasTurned = false;

            while (GetCell(nextPosition) == '#')
            {
                direction = TurnRight(direction);
                nextPosition = GetNextPosition(position, direction);
                hasTurned = true;
            }

            if (hasTurned)
            {
                if (turns.Exists(turn => turn.x == position.x && turn.y == position.y))
                {
                    grid[obstaclePosition.y][obstaclePosition.x] = '.';

                    return false;
                }

                turns.Add(position);
            }

            position = nextPosition;

            nextPosition = GetNextPosition(position, direction);
        }

        grid[obstaclePosition.y][obstaclePosition.x] = '.';
        return true;
    }
}