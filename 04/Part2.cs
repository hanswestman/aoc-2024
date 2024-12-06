public class Part2 : Part1
{
    new public int GetSolution()
    {
        int xmasCount = 0;

        for (int y = 0; y < grid.Count(); y++)
        {
            for (int x = 0; x < grid[0].Count(); x++)
            {
                if (grid[y][x] == 'A' && isXmasAtCoord((x, y)))
                {
                    xmasCount++;
                }
            }
        }

        return xmasCount;
    }

    bool isXmasAtCoord((int x, int y) coordA)
    {
        List<List<(int x, int y)>> paths =
        [
            [(-1, 1), (1, -1)], // Bottom Left to Top Right
            [(-1, -1), (1, 1)], // Top Left to Bottom Right
        ];

        return paths.All(path =>
        {
            (int x, int y) coordLeft = (coordA.x + path[0].x, coordA.y + path[0].y);
            (int x, int y) coordRight = (coordA.x + path[1].x, coordA.y + path[1].y);

            if (!isCoordWithinGrid(coordLeft) || !isCoordWithinGrid(coordRight))
            {
                return false;
            }

            string word = $"{grid[coordLeft.y][coordLeft.x]}A{grid[coordRight.y][coordRight.x]}";

            if (word == "MAS" || word == "SAM")
            {
                return true;
            }

            return false;
        });
    }


}