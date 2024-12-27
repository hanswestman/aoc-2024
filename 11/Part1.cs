public class Part1
{
    protected List<long> Stones;

    public Part1()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
        Stones = File.ReadAllText(path).Split(" ").Select(long.Parse).ToList();
    }

    public long GetSolution()
    {
        long count = 0;

        foreach (long startStone in Stones)
        {
            List<long> stones = new List<long>([startStone]);

            for (int i = 0; i < 25; i++)
            {
                Blink(ref stones);
            }

            count += stones.Count();
        }

        return count;
    }

    protected void Blink(ref List<long> stonesPartition)
    {
        List<long> stones = new List<long>();

        foreach (long stone in stonesPartition)
        {
            string stoneStr = stone.ToString();
            if (stone == 0)
            {
                stones.Add(1);
            }
            else if (stoneStr.Length % 2 == 0)
            {
                string stoneLeft = stoneStr.Substring(0, stoneStr.Length / 2);
                string stoneRight = stoneStr.Substring(stoneStr.Length / 2);

                stones.Add(long.Parse(stoneLeft));
                stones.Add(long.Parse(stoneRight));
            }
            else
            {
                stones.Add(stone * 2024);
            }
        }

        stonesPartition = stones;
    }
}