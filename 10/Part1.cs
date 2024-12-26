global using Coord = (int x, int y);

public class Grid<T>
{
    private T[][] _grid;

    public Grid(T[][] grid)
    {
        _grid = grid;
    }

    public int Width
    {
        get { return _grid[0].Length; }
    }

    public int Height
    {
        get { return _grid.Length; }
    }

    public void Debug()
    {
        foreach (var line in _grid)
        {
            Console.WriteLine(string.Join("", line));
        }
    }

    public void ForEach(Action<T, Coord> callback)
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                callback(_grid[y][x], (x, y));
            }
        }
    }

    public T? Get(Coord coord)
    {
        return IsCoordWithinGrid(coord) ? _grid[coord.y][coord.x] : default;
    }

    public bool IsCoordWithinGrid(Coord coord)
    {
        return coord.x >= 0
            && coord.x < Width
            && coord.y >= 0
            && coord.y < Height;
    }

    public List<Coord> SearchCoordinates(T query)
    {
        List<Coord> coords = new List<Coord>();

        ForEach((item, coord) =>
        {
            if (EqualityComparer<T>.Default.Equals(item, query))
            {
                coords.Add(coord);
            }
        });

        return coords;
    }
}

public class Part1
{
    public Grid<int> Map;

    public Part1()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
        List<int[]> lines = new List<int[]>();

        foreach (string line in File.ReadAllLines(path))
        {
            lines.Add(line.ToCharArray().Select(item => int.Parse(item.ToString())).ToArray());
        }

        Map = new Grid<int>(lines.ToArray());
    }

    public long GetSolution()
    {
        int trailheadScore = 0;

        List<Coord> trailheadCoords = Map.SearchCoordinates(0);

        foreach (Coord trailhead in trailheadCoords)
        {
            trailheadScore += GetTrailheadScore(trailhead);
        }

        return trailheadScore;
    }

    private int GetTrailheadScore(Coord trailhead)
    {
        List<Coord> tops = WalkTheTrail(trailhead, 0);

        return tops.Distinct().Count();
    }

    private List<Coord> WalkTheTrail(Coord coord, int level)
    {
        //Console.WriteLine($"Walking: {coord.x}, {coord.y}");
        List<Coord> neighbors = GetNeighborsOnTheNextLevel(coord, level + 1);

        if (level + 1 == 9)
        {
            return neighbors;
        }

        List<Coord> tops = new List<Coord>();

        foreach (Coord neighbor in neighbors)
        {
            foreach (Coord top in WalkTheTrail(neighbor, level + 1))
            {
                tops.Add(top);
            }
        }

        return tops;
    }

    protected List<Coord> GetNeighborsOnTheNextLevel(Coord coord, int nextLevel)
    {
        List<Coord> coords = new List<Coord>();

        List<Coord> neighbors = new List<Coord> {
            (coord.x, coord.y - 1),
            (coord.x + 1, coord.y),
            (coord.x, coord.y + 1),
            (coord.x - 1, coord.y)
        };

        foreach (Coord neighbor in neighbors)
        {
            if (Map.IsCoordWithinGrid(neighbor) && Map.Get(neighbor) == nextLevel)
            {
                coords.Add(neighbor);
            }
        }

        return coords;
    }
}