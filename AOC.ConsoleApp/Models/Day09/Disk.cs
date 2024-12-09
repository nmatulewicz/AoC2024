namespace AOC.ConsoleApp.Models.Day09;

public class Disk
{
    public DiskPosition[] Positions;

    public Disk(int[] diskMap)
    {
        SetPostions(diskMap);
    }

    public void RearrangeFiles()
    {
        var nextFreeSpacePosition = 0;
        var nextFilePostionToMove = Positions.Length - 1;
        while (true)
        {
            while (!Positions[nextFreeSpacePosition].IsFreeSpace) nextFreeSpacePosition++;
            while (!Positions[nextFilePostionToMove].IsFile) nextFilePostionToMove--;

            if (nextFreeSpacePosition >= nextFilePostionToMove) break;

            var freeSpacePosition = Positions[nextFreeSpacePosition];
            var filePosition = Positions[nextFilePostionToMove];

            freeSpacePosition.FillWithId(filePosition.IdNumber!.Value);
            filePosition.RemoveFile();

            nextFreeSpacePosition++;
            nextFilePostionToMove--;
        }
    }

    public long CalculateChecksum()
    {
        return Positions
            .Where(position => position.IsFile)
            .Sum(position => (long) position.IdNumber!.Value * (long) position.IndexOnDisk);
    }

    private void SetPostions(int[] diskMap)
    {
        var totalNumberOfDiskPositions = diskMap.Aggregate(0, (total, number) => total + number);
        Positions = new DiskPosition[totalNumberOfDiskPositions];

        var nextPositionToFill = 0;
        var isFreeSpace = false;
        var fileId = 0;
        foreach (var (length, index) in diskMap.Select((length, index) => (length, index)))
        {
            for (var position = nextPositionToFill; position < nextPositionToFill + length; position++)
            {
                Positions[position] = new DiskPosition
                {
                    IdNumber = isFreeSpace ? null : fileId,
                    IndexOnDisk = position,
                };
            }

            nextPositionToFill += length;
            if (isFreeSpace) fileId++;
            isFreeSpace = !isFreeSpace;
        }
    }
}
