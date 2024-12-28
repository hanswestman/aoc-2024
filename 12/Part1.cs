public class Plot
{
    public int Region = -1;
    public char Plant;

    public Plot(char plant)
    {
        Plant = plant;
    }

    public void Debug()
    {
        Console.WriteLine($"Plant: {Plant}, Region: {Region}");
    }
}

public class Part1
{
    public Grid<Plot> Map;

    public  int RegionIndex = -1;

    private Coord _lastGroupingCoord = (-1, 0);

    public Part1()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
        List<Plot[]> lines = new List<Plot[]>();

        foreach (string line in File.ReadAllLines(path))
        {
            lines.Add(
                line.ToCharArray()
                .Select(item => new Plot(item))
                .ToArray()
            );
        }

        Map = new Grid<Plot>(lines.ToArray());

        GroupRegions();
    }

    public long GetSolution()
    {
        int sum = 0;
        /*Map.ForEach((plot, coord) =>
        {
            Console.WriteLine($"{coord.x}, {coord.y}");
            plot.Debug();
        });*/

        for(int regionIndex = 0; regionIndex <= RegionIndex; regionIndex++)
        {
            List<Coord> regionCoords = new List<Coord>();

            Map.ForEach((plot, coord) =>
            {
                if (plot.Region == regionIndex)
                {
                    regionCoords.Add(coord);
                }
            });

            int area = regionCoords.Count();
            int perimiter = 0;

            foreach (Coord plotCoord in regionCoords)
            {
                List<Coord> neighbors = new List<Coord> {
                    (plotCoord.x, plotCoord.y - 1),
                    (plotCoord.x + 1, plotCoord.y),
                    (plotCoord.x, plotCoord.y + 1),
                    (plotCoord.x - 1, plotCoord.y),
                };

                foreach (Coord neighbor in neighbors)
                {
                    if (!IsInRegion(neighbor, regionIndex))
                    {
                        perimiter++;
                    }
                }
            }

            sum += area * perimiter;
        }

        return sum;
    }

    protected bool IsInRegion(Coord coord, int region)
    {
        return Map.IsCoordWithinGrid(coord) && Map.Get(coord).Region == region;
    }

    private void GroupRegions()
    {
        Coord? nextUngroupedPlot = GetNextUngroupedPlot();

        while (nextUngroupedPlot != null)
        {
            RegionIndex++;

            GroupRegion((Coord)nextUngroupedPlot, RegionIndex);

            nextUngroupedPlot = GetNextUngroupedPlot();
        }
    }

    private void GroupRegion(Coord coord, int regionIndex)
    {
        Plot plot = Map.Get(coord);
        plot.Region = regionIndex;

        List<Coord> neighbors = new List<Coord> {
            (coord.x, coord.y - 1),
            (coord.x + 1, coord.y),
            (coord.x, coord.y + 1),
            (coord.x - 1, coord.y),
        };

        foreach (Coord neighbor in neighbors)
        {
            if (!Map.IsCoordWithinGrid(neighbor))
            {
                continue;
            }

            Plot neighborPlot = Map.Get(neighbor);

            if (neighborPlot.Plant == plot.Plant && neighborPlot.Region == -1)
            {
                GroupRegion(neighbor, regionIndex);
            }
        }
    }

    private Coord? GetNextUngroupedPlot()
    {
        Coord coord = (_lastGroupingCoord.x + 1, _lastGroupingCoord.y);

        if (!Map.IsCoordWithinGrid(coord))
        {
            coord.x = 0;
            coord.y++;
        }

        while (Map.IsCoordWithinGrid(coord))
        {
            if (Map.Get(coord).Region == -1)
            {
                _lastGroupingCoord = coord;

                return coord;
            }

            coord.x++;

            if (!Map.IsCoordWithinGrid(coord))
            {
                coord.x = 0;
                coord.y++;
            }
        }

        return null;
    }
}