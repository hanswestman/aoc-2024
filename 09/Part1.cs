public class Part1
{
    List<char> Input;

    public Part1()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
        Input = File.ReadAllText(path).ToCharArray().ToList();
    }

    public long GetSolution()
    {
        List<int> fileBlocks = ParseFileBlocks();

        // Console.WriteLine(String.Join("", fileBlocks));

        ReorderFileBlocks(ref fileBlocks);

        // Console.WriteLine(String.Join("", fileBlocks));

        return CalculateChecksum(ref fileBlocks);
    }

    protected List<int> ParseFileBlocks()
    {
        List<int> fileBlocks = new List<int>();
        int index = 0;
        bool isFile = true;

        foreach (char inputPart in Input)
        {
            int amount = int.Parse(inputPart.ToString());
            int value = -1;

            if (isFile)
            {
                value = index;
                index++;
            }

            isFile = !isFile;

            for (int i = 0; i < amount; i++)
            {
                fileBlocks.Add(value);
            }
        }

        return fileBlocks;
    }

    private void ReorderFileBlocks(ref List<int> fileBlocks)
    {
        int leftIndex = 0;
        int rightIndex = fileBlocks.Count() - 1;

        while (leftIndex < rightIndex)
        {
            if (fileBlocks[leftIndex] != -1)
            {
                leftIndex++;

                continue;
            }

            while (fileBlocks[rightIndex] == -1)
            {
                rightIndex--;

                if (leftIndex >= rightIndex)
                {
                    return;
                }
            }

            int temp = fileBlocks[leftIndex];
            fileBlocks[leftIndex] = fileBlocks[rightIndex];
            fileBlocks[rightIndex] = temp;

            // Console.WriteLine(String.Join("", fileBlocks));

            leftIndex++;
            rightIndex--;
        }
    }

    private long CalculateChecksum(ref List<int> fileBlocks)
    {
        long sum = 0;

        for (int i = 0; i < fileBlocks.Count(); i++)
        {
            if (fileBlocks[i] == -1)
            {
                return sum;
            }

            sum += i * fileBlocks[i];
        }

        return sum;
    }
}