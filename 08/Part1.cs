global using Coord = (int x, int y);

public class Part1
{
    protected char[][] Grid;
    protected char[][] AntinodeGrid;

    public Part1()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

        Grid = File.ReadAllLines(path).Select(line => line.ToCharArray()).ToArray();
        AntinodeGrid = File.ReadAllLines(path).Select(line => line.ToCharArray()).ToArray();
    }

    public int GetSolution()
    {
        for (int y = 0; y < Grid.Count(); y++)
        {
            for (int x = 0; x < Grid[0].Count(); x++)
            {
                if (HasAntenna((x, y)))
                {
                    HandleCoord((x, y));
                }
            }
        }

        return CountAntinodes();
    }

    protected bool HasAntenna(Coord coord)
    {
        char symbol = Grid[coord.y][coord.x];

        return symbol != '.' && symbol != '#';
    }

    private void HandleCoord(Coord coord)
    {
        List<Coord> otherAntennaCoords = FindOtherMatchingAntennas(coord);

        foreach (Coord otherAntennaCoord in otherAntennaCoords)
        {
            Coord diff = (otherAntennaCoord.x - coord.x, otherAntennaCoord.y - coord.y);
            Coord antinodeCoord = (otherAntennaCoord.x + diff.x, otherAntennaCoord.y + diff.y);

            if (!IsCoordWithinGrid(antinodeCoord))
            {
                continue;
            }

            AntinodeGrid[antinodeCoord.y][antinodeCoord.x] = '#';
        }
    }

    protected List<Coord> FindOtherMatchingAntennas(Coord baseCoord)
    {
        char antenna = Grid[baseCoord.y][baseCoord.x];
        List<Coord> otherAntennaCoords = new List<Coord>();

        for (int y = 0; y < Grid.Count(); y++)
        {
            for (int x = 0; x < Grid[0].Count(); x++)
            {
                if (Grid[y][x] == antenna && (baseCoord.x != x || baseCoord.y != y))
                {
                    otherAntennaCoords.Add((x, y));
                }
            }
        }

        return otherAntennaCoords;
    }

    private int CountAntinodes()
    {
        int count = 0;

        for (int y = 0; y < AntinodeGrid.Count(); y++)
        {
            for (int x = 0; x < AntinodeGrid[0].Count(); x++)
            {
                if (AntinodeGrid[y][x] == '#')
                {
                    count++;
                }
            }
        }

        return count;
    }

    protected bool IsCoordWithinGrid(Coord coord)
    {
        return coord.x >= 0
            && coord.x < Grid[0].Count()
            && coord.y >= 0
            && coord.y < Grid.Count();
    }
}