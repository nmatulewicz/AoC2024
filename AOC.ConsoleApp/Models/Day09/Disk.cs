
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

    public void RearrangeFilesPart2()
    {
        var nextFilePostionToMove = Positions.Length - 1;
        while (nextFilePostionToMove > 0)
        {
            while (!Positions[nextFilePostionToMove].IsFile) nextFilePostionToMove--;
            var idOfFileToMove = Positions[nextFilePostionToMove].IdNumber!.Value;
            var lastIndexOfFileTooMove = nextFilePostionToMove;
            var firstIndexOfFileToMove = nextFilePostionToMove;
            while (firstIndexOfFileToMove > 0 && Positions[firstIndexOfFileToMove - 1].IdNumber == idOfFileToMove) firstIndexOfFileToMove--;
            var fileSize = lastIndexOfFileTooMove - firstIndexOfFileToMove + 1;

            var success = TryFindFreeSpaceOfAtLeastLength(fileSize, out var startIndexFreeSpace);
            if (success && startIndexFreeSpace < firstIndexOfFileToMove)
            {
                foreach (var position in Positions[startIndexFreeSpace..(startIndexFreeSpace + fileSize)])
                {
                    position.FillWithId(idOfFileToMove);
                }
                foreach (var position in Positions[firstIndexOfFileToMove..(lastIndexOfFileTooMove + 1)])
                {
                    position.RemoveFile();
                }
            }

            nextFilePostionToMove = firstIndexOfFileToMove - 1;
        }
    }

    private bool TryFindFreeSpaceOfAtLeastLength(int fileSize, out int startIndex)
    {
        startIndex = 0;
        while (true)
        {
            while (startIndex < Positions.Length 
                && !Positions[startIndex].IsFreeSpace) 
                startIndex++;

            if (startIndex >= Positions.Length) return false;

            var lastIndexFreeSpace = startIndex;
            while (lastIndexFreeSpace + 1 < Positions.Length
                && Positions[lastIndexFreeSpace + 1].IsFreeSpace) lastIndexFreeSpace++;

            var freeSpacelength = lastIndexFreeSpace - startIndex + 1;
            if (freeSpacelength >= fileSize) return true;
            if (lastIndexFreeSpace + 1 >= Positions.Length) return false;

            startIndex = lastIndexFreeSpace + 1;
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
