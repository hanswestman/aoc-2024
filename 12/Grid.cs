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

    public T Get(Coord coord)
    {
        if (!IsCoordWithinGrid(coord))
        {
            throw new Exception("Coord is outside grid.");
        }

        return _grid[coord.y][coord.x];
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