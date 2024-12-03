public class Part2
{
    public int GetSolution()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
        int validReports = 0;

        foreach (string line in File.ReadLines(path))
        {
            List<int> report = line.Trim().Split(" ").Select(int.Parse).ToList();

            if (validateReport(report))
            {
                validReports++;
            }
            else if (validateReportWithOneErrorAllowed(report))
            {
                validReports++;
            }
        }

        return validReports;
    }

    private bool validateReport(List<int> report)
    {
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

    private bool validateReportWithOneErrorAllowed(List<int> report)
    {
        for (int i = 0; i < report.Count(); i++)
        {
            List<int> partialReport = report.ToList();

            partialReport.RemoveAt(i);

            if (validateReport(partialReport))
            {
                return true;
            }
        }

        return false;
    }

}