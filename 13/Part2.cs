public class Part2 : Part1
{
    new public long GetSolution()
    {
        long tokens = 0;

        foreach (Machine machine in Machines)
        {
            machine.PrizeX += 10_000_000_000_000;
            machine.PrizeY += 10_000_000_000_000;

            (decimal a, decimal b) = machine.FindButtonIntersect();

            if (decimal.IsInteger(a) && decimal.IsInteger(b))
            {
                tokens += (long)a * 3 + (long)b;
            }
        }

        return tokens;
    }
}