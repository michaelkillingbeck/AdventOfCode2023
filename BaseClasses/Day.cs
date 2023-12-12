public abstract class Day : IDay
{
    private int _dayID = 0;

    public int DayID => _dayID;

    public Day(int dayID)
    {
        _dayID = dayID;
    }

    protected abstract void Part1();

    protected abstract void Part2();

    public void PrintOutput(int partID, object output)
    {
        Console.WriteLine($"Day {_dayID} Part {partID}: {output}");
    }

    public void Run()
    {
        Part1();
        Part2();
    }
}