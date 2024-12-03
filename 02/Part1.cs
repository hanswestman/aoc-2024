public class Part1
{
    public int GetSolution()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
        int validReports = 0;

        foreach (string line in File.ReadLines(path))
        {
            if (validateReport(line.Trim()))
            {
                validReports++;
            }
        }

        return validReports;
    }

    private bool validateReport(string line)
    {
        List<int> report = line.Split(" ").Select(number => int.Parse(number)).ToList();

        int previousNumber = report[0];
        int startDirection = 0;

        for (int i = 1; i < report.Count(); i++)
        {
            if (report[i] == previousNumber)
            {
                return false;
            }

            int currentDirection = report[i] - previousNumber > 0 ? 1 : -1;

            if (startDirection == 0)
            {
                startDirection = currentDirection;
            }

            if (currentDirection != startDirection)
            {
                return false;
            }

            if (Math.Abs(report[i] - previousNumber) > 3)
            {
                return false;
            }

            previousNumber = report[i];
        }

        return true;
    }

}