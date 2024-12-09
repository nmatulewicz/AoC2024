namespace AOC.ConsoleApp.Models.Day09;

public class DiskPosition
{
    public int? IdNumber { get; set; }
    public int IndexOnDisk { get; set; }
    public bool IsFreeSpace => !IdNumber.HasValue;
    public bool IsFile => !IsFreeSpace;

    public void FillWithId(int id) { IdNumber = id; }
    public void RemoveFile() { IdNumber = null; }
}
