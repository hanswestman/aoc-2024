public class Part2 : Part1
{
    new public long GetSolution()
    {
        for (int step = 1; step <= 10000; step++)
        {
            foreach (Robot robot in Robots)
            {
                robot.Step(1);
            }

            if (CheckForChristmasTree())
            {
                Console.WriteLine($"Step {step}");
                Print();

                return step;
            }

            //System.Threading.Thread.Sleep(600);
        }

        return 0;
    }

    private bool CheckForChristmasTree()
    {
        return Robots.GroupBy(robot => $"{robot.X},{robot.Y}").All(grouping => grouping.Count() == 1);
    }

    private void Print()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                int robotCount = Robots.Where(robot => robot.X == x && robot.Y == y).Count();
                Console.Write(robotCount > 0 ? robotCount : ".");
            }

            Console.WriteLine();
        }
    }
}