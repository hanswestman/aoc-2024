public class Part2 : Part1
{
    new public long GetSolution()
    {
        int sum = 0;

        for (int regionIndex = 0; regionIndex <= RegionIndex; regionIndex++)
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
            int corners = 0;

            foreach (Coord plotCoord in regionCoords)
            {
                foreach (int rowOffset in (int[])[1, -1])
                {
                    foreach (int columnOffset in (int[])[1, -1])
                    {
                        Coord rowNeighbor = (plotCoord.x + rowOffset, plotCoord.y);
                        Coord columnNeighbor = (plotCoord.x, plotCoord.y + columnOffset);
                        Coord diagonalNeighbor = (plotCoord.x + rowOffset, plotCoord.y + columnOffset);

                        // Exterion corner
                        if (
                            !IsInRegion(rowNeighbor, regionIndex)
                            && !IsInRegion(columnNeighbor, regionIndex)
                        )
                        {
                            //Console.WriteLine($"Ext Corner, X: {plotCoord.x} Y: {plotCoord.y} R: {rowOffset} C: {columnOffset}");
                            corners++;
                        }

                        // Interior corner
                        if (
                            IsInRegion(rowNeighbor, regionIndex)
                            && IsInRegion(columnNeighbor, regionIndex)
                            && !IsInRegion(diagonalNeighbor, regionIndex)
                        )
                        {
                            //Console.WriteLine($"Int Corner, X: {plotCoord.x} Y: {plotCoord.y} R: {rowOffset} C: {columnOffset}");
                            corners++;
                        }

                    }
                }
            }


            sum += area * corners;
        }

        return sum;
    }
}