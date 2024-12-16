public class Part2 : Part1
{
    Dictionary<char, int> frequencies = new Dictionary<char, int>();

    new public int GetSolution()
    {
        for (int y = 0; y < Grid.Count(); y++)
        {
            for (int x = 0; x < Grid[0].Count(); x++)
            {
                if (HasAntenna((x, y)))
                {
                    if (Grid[y][x] != '.')
                    {
                        if (!frequencies.ContainsKey(Grid[y][x]))
                        {
                            frequencies.Add(Grid[y][x], 0);
                        }

                        frequencies[Grid[y][x]]++;
                    }

                    HandleCoord((x, y));
                }
            }
        }

        return CountAntinodes();
    }

    private void HandleCoord(Coord coord)
    {
        List<Coord> otherAntennaCoords = FindOtherMatchingAntennas(coord);

        foreach (Coord otherAntennaCoord in otherAntennaCoords)
        {
            Coord diff = (otherAntennaCoord.x - coord.x, otherAntennaCoord.y - coord.y);
            Coord antinodeCoord = (otherAntennaCoord.x + diff.x, otherAntennaCoord.y + diff.y);

            while (IsCoordWithinGrid(antinodeCoord))
            {
                if (Grid[antinodeCoord.y][antinodeCoord.x] == '.')
                {
                    Grid[antinodeCoord.y][antinodeCoord.x] = '#';
                }

                antinodeCoord = (antinodeCoord.x + diff.x, antinodeCoord.y + diff.y);
            }
        }
    }

    private int CountAntinodes()
    {
        int count = 0;

        for (int y = 0; y < Grid.Count(); y++)
        {
            for (int x = 0; x < Grid[0].Count(); x++)
            {
                if (Grid[y][x] == '#')
                {
                    count++;
                }
            }
        }

        foreach (var frequency in frequencies)
        {
            if (frequency.Value > 1)
            {
                count += frequency.Value;
            }
        }

        return count;
    }
}