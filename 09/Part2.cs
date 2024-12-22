public class Part2 : Part1
{
    new public long GetSolution()
    {
        List<int> fileBlocks = ParseFileBlocks();

        // DebugLine(ref fileBlocks);

        ReorderFileBlocks(ref fileBlocks);

        // DebugLine(ref fileBlocks);

        return CalculateChecksum(ref fileBlocks);
    }

    private void ReorderFileBlocks(ref List<int> fileBlocks)
    {
        int fileId = fileBlocks[fileBlocks.Count() - 1];

        while (fileId >= 0)
        {

            int length = fileBlocks.Where(fileBlock => fileBlock == fileId).Count();

            int currentFileIdStartPosition = fileBlocks.FindIndex(fileBlock => fileBlock == fileId);

            int freeSpaceStartPosition = GetFreeSpaceStartPosition(ref fileBlocks, length, currentFileIdStartPosition);

            if (freeSpaceStartPosition != -1)
            {
                for (int i = freeSpaceStartPosition; i < freeSpaceStartPosition + length; i++)
                {
                    fileBlocks[i] = fileId;
                }

                for (int i = currentFileIdStartPosition; i < currentFileIdStartPosition + length; i++)
                {
                    fileBlocks[i] = -1;
                }
            }

            // DebugLine(ref fileBlocks);

            fileId--;
        }
    }

    private int GetFreeSpaceStartPosition(ref List<int> fileBlocks, int length, int rightLimit)
    {
        int startFreeSpace = -1;
        int currentFreeSpaceLength = 0;

        for (int i = 0 + 1; i < rightLimit; i++)
        {
            if (fileBlocks[i] == -1 && startFreeSpace == -1)
            {
                startFreeSpace = i;
            }

            if (fileBlocks[i] == -1)
            {
                currentFreeSpaceLength++;

                if (currentFreeSpaceLength == length)
                {
                    return startFreeSpace;
                }
            }
            else
            {
                currentFreeSpaceLength = 0;
                startFreeSpace = -1;
            }
        }

        return -1;
    }

    private void DebugLine(ref List<int> fileBlocks)
    {
        Console.WriteLine(
            String.Join(
                "",
                fileBlocks.Select(fileBlock => fileBlock > -1 ? fileBlock.ToString() : " ")
            )
        );
    }

    private long CalculateChecksum(ref List<int> fileBlocks)
    {
        long sum = 0;

        for (int i = 0; i < fileBlocks.Count(); i++)
        {
            if (fileBlocks[i] == -1)
            {
                continue;
            }

            sum += i * fileBlocks[i];
        }

        return sum;
    }
}