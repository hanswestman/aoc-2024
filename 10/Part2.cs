public class Part2 : Part1
{
    new public long GetSolution()
    {
        int trailheadScore = 0;

        List<Coord> trailheadCoords = Map.SearchCoordinates(0);

        foreach (Coord trailhead in trailheadCoords)
        {
            trailheadScore += WalkTheTrailAndGetScore(trailhead, 0);
        }

        return trailheadScore;
    }

    private int WalkTheTrailAndGetScore(Coord coord, int level)
    {
        List<Coord> neighbors = GetNeighborsOnTheNextLevel(coord, level + 1);
        int score = 0;

        if (level + 1 == 9)
        {
            return neighbors.Count();
        }

        foreach (Coord neighbor in neighbors)
        {
            score += WalkTheTrailAndGetScore(neighbor, level + 1);
        }

        return score;
    }
}