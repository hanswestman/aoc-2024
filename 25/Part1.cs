public class Lock
{
    public int[] Pins;

    public Lock(int[] pins)
    {
        Pins = pins;
    }

    public bool OverlapsWithKey(Key key)
    {
        for (int column = 0; column < 5; column++)
        {
            if (Pins[column] + key.Heights[column] > 5)
            {
                return true;
            }
        }

        return false;
    }
}

public class Key
{
    public int[] Heights;

    public Key(int[] heights)
    {
        Heights = heights;
    }
}

public class Part1
{
    List<Lock> Locks = new List<Lock>();
    List<Key> Keys = new List<Key>();

    public Part1()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

        List<string> keyLockLines = new List<string>(7);
        int lines = 0;

        foreach (string line in File.ReadAllLines(path))
        {
            if (line.Length > 0)
            {
                keyLockLines.Add(line);
                lines++;

                if (lines == 7)
                {
                    ParseKeyLockLines(keyLockLines);
                    keyLockLines.Clear();
                    lines = 0;
                }
            }
        }
    }

    public long GetSolution()
    {
        int fittingCombinations = 0;

        /*foreach (Lock l in Locks)
        {
            Console.WriteLine($"Lock: {String.Join(", ", l.Pins)}");
        }

        foreach (Key k in Keys)
        {
            Console.WriteLine($"Key: {String.Join(", ", k.Heights)}");
        }*/

        foreach(Lock l in Locks){
            foreach(Key k in Keys){
                if(!l.OverlapsWithKey(k)){
                    fittingCombinations++;

                    //Console.WriteLine($"Lock: {String.Join(", ", l.Pins)}");
                    //Console.WriteLine($"Key: {String.Join(", ", k.Heights)}");
                }
            }
        }

        return fittingCombinations;
    }

    private void ParseKeyLockLines(List<string> lines)
    {
        bool isLock = lines[0] == "#####";
        int[] columnValues = new int[5];

        for (int column = 0; column < 5; column++)
        {
            int columnValue = 0;

            for (int lineNr = 1; lineNr <= 5; lineNr++)
            {
                columnValue += lines[lineNr][column] == '#' ? 1 : 0;
            }

            columnValues[column] = columnValue;
        }

        if (isLock)
        {
            Locks.Add(new Lock(columnValues));
        }
        else
        {
            Keys.Add(new Key(columnValues));
        }
    }
}