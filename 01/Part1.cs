public class Part1
{
    public int GetSolution()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

        List<int> leftList = new List<int>();
        List<int> rightList = new List<int>();

        foreach (string line in File.ReadLines(path))
        {
            string[] stringValues = line.Trim().Split("   ");
            leftList.Add(int.Parse(stringValues[0]));
            rightList.Add(int.Parse(stringValues[1]));
        }

        leftList = leftList.OrderBy(i => i).ToList();
        rightList = rightList.OrderBy(i => i).ToList();

        int totalDiff = 0;

        for (int i = 0; i < leftList.Count(); i++)
        {
            totalDiff += Math.Abs(leftList[i] - rightList[i]);
        }

        return totalDiff;
    }

}