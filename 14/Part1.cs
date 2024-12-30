using System.Text.RegularExpressions;

public class Robot
{
    public int X;
    public int Y;
    public int VX;
    public int VY;
    public int Cols;
    public int Rows;
    public int Quadrant;

    public Robot(int x, int y, int vX, int vY, int cols, int rows)
    {
        X = x;
        Y = y;
        VX = vX;
        VY = vY;
        Cols = cols;
        Rows = rows;
        CalcQuadrant();
    }

    public void Step(int steps)
    {
        X = (X + steps * VX) % Cols;
        Y = (Y + steps * VY) % Rows;

        if (X < 0)
        {
            X = Cols + X;
        }

        if (Y < 0)
        {
            Y = Rows + Y;
        }
        CalcQuadrant();
    }

    private void CalcQuadrant()
    {
        bool isLeft = X < Cols / 2;
        bool isRight = X > Cols / 2;
        bool isTop = Y < Rows / 2;
        bool isBottom = Y > Rows / 2;

        if (isLeft && isTop)
        {
            Quadrant = 1;
        }
        else if (isRight && isTop)
        {
            Quadrant = 2;
        }
        else if (isLeft && isBottom)
        {
            Quadrant = 3;
        }
        else if (isRight && isBottom)
        {
            Quadrant = 4;
        }
        else
        {
            Quadrant = 0;
        }
    }

    public void Debug()
    {
        Console.WriteLine($"x:{X} y:{Y} vx:{VX} vy:{VY}: quadrant:{Quadrant}");
    }
}

public class Part1
{
    public const int Width = 101;
    public const int Height = 103;

    public List<Robot> Robots = new List<Robot>();

    public Part1()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
        List<string> lines = File.ReadAllLines(path).ToList();

        string robotPattern = @"^p=(\d+),(\d+) v=(-?\d+),(-?\d+)$";

        foreach (string line in lines)
        {
            Match match = Regex.Match(line, robotPattern);

            Robots.Add(
                new Robot(
                    int.Parse(match.Groups[1].Value),
                    int.Parse(match.Groups[2].Value),
                    int.Parse(match.Groups[3].Value),
                    int.Parse(match.Groups[4].Value),
                    Width,
                    Height
                )
            );
        }
    }

    public long GetSolution()
    {
        int safetyFactor = 1;

        foreach (Robot robot in Robots)
        {
            robot.Step(100);
            // robot.Debug();
        }

        var groupings = Robots.Where(robot => robot.Quadrant != 0)
            .GroupBy(robot => robot.Quadrant)
            .ToList();

        foreach (var grouping in groupings)
        {
            // Console.WriteLine($"Quadrant {grouping.Key} = {grouping.Count()}");
            safetyFactor *= grouping.Count();
        }

        return safetyFactor;
    }
}