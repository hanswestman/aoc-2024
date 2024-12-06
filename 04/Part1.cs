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
        int xmasCount = 0;

        for (int y = 0; y < grid.Count(); y++)
        {
            for (int x = 0; x < grid[0].Count(); x++)
            {
                if (grid[y][x] == 'X')
                {
                    xmasCount += findXmasFromCoord((x, y));
                }
            }
        }

        return xmasCount;
    }

    int findXmasFromCoord((int x, int y) coordX)
    {
        int xmasCountAtCoord = 0;
        List<List<(int x, int y)>> paths =
        [
            [(0, -1), (0, -2), (0, -3)], // Top
            [(1, -1), (2, -2), (3, -3)], // Top Right
            [(1, 0), (2, 0), (3, 0)], // Right
            [(1, 1), (2, 2), (3, 3)], // Bottom Right
            [(0, 1), (0, 2), (0, 3)], // Bottom
            [(-1, 1), (-2, 2), (-3, 3)], // Bottom Left
            [(-1, 0), (-2, 0), (-3, 0)], // Left
            [(-1, -1), (-2, -2), (-3, -3)], // Top Left
        ];

        foreach (var path in paths)
        {
            (int x, int y) coordM = (coordX.x + path[0].x, coordX.y + path[0].y);
            (int x, int y) coordA = (coordX.x + path[1].x, coordX.y + path[1].y);
            (int x, int y) coordS = (coordX.x + path[2].x, coordX.y + path[2].y);

            if (!isCoordWithinGrid(coordM) || grid[coordM.y][coordM.x] != 'M')
            {
                continue;
            }

            if (!isCoordWithinGrid(coordA) || grid[coordA.y][coordA.x] != 'A')
            {
                continue;
            }

            if (!isCoordWithinGrid(coordS) || grid[coordS.y][coordS.x] != 'S')
            {
                continue;
            }

            xmasCountAtCoord++;
        }

        return xmasCountAtCoord;
    }

    protected bool isCoordWithinGrid((int x, int y) coord)
    {
        return coord.x >= 0
            && coord.x < grid[0].Count()
            && coord.y >= 0
            && coord.y < grid.Count();
    }


}