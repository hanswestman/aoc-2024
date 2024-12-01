public class Part2
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

        int similarityScore = 0;

        foreach (int left in leftList)
        {
            similarityScore += rightList.Count(right => right == left) * left;
        }

        return similarityScore;
    }

}