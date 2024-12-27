public class Part2 : Part1
{
    Dictionary<long, long> StoneDict = new Dictionary<long, long>();

    new public long GetSolution()
    {
        foreach (long stone in Stones)
        {
            StoneDict.Add(stone, 1);
        }

        for (int i = 0; i < 75; i++)
        {
            Dictionary<long, long> newStoneDict = new Dictionary<long, long>();

            foreach (KeyValuePair<long, long> pair in StoneDict)
            {
                foreach (long newStone in GetNewStones(pair.Key))
                {
                    if (!newStoneDict.ContainsKey(newStone))
                    {
                        newStoneDict.Add(newStone, 0);
                    }

                    newStoneDict[newStone] += pair.Value;
                }
            }

            StoneDict = newStoneDict;
        }

        return StoneDict.Values.Sum();
    }

    private List<long> GetNewStones(long stone)
    {
        string stoneStr = stone.ToString();

        if (stone == 0)
        {
            return new List<long>([1]);
        }
        else if (stoneStr.Length % 2 == 0)
        {
            string stoneLeft = stoneStr.Substring(0, stoneStr.Length / 2);
            string stoneRight = stoneStr.Substring(stoneStr.Length / 2);

            return new List<long>([long.Parse(stoneLeft), long.Parse(stoneRight)]);
        }
        else
        {
            return new List<long>([stone * 2024]);
        }
    }
}