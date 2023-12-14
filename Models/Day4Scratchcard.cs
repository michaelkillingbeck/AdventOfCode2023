public class Day4Scratchcard
{
    public int GameID { get; private set; }
    public int Instances { get; set; }
    public List<int> PlayerNumbers { get; private set; }
    public List<int> WinningNumbers { get; private set; }

    public Day4Scratchcard(string gameLine)
    {
        int colonIndex = gameLine.IndexOf(':');

        ParseGameID(gameLine, colonIndex);
        ParseNumbers(gameLine, colonIndex);

        Instances = 1;
    }

    private void ParseGameID(string gameLine, int colonIndex)
    {
        string gameIDString = gameLine.Substring(0, colonIndex);
        string[] gameIDParts = gameIDString.Split(' ');

        GameID = int.Parse(gameIDParts.Last());
    }

    private void ParseNumbers(string gameLine, int colonIndex)
    {
        string[] scratchcardParts = gameLine
            .Replace("  ", " ")
            .Substring(colonIndex + 1)
            .Trim()
            .Split('|');

        WinningNumbers = scratchcardParts[0]
            .Split(' ')
            .Where(stringPart => string.IsNullOrEmpty(stringPart) == false)
            .Select(number => int.Parse(number))
            .ToList();

        PlayerNumbers = scratchcardParts[1]
            .Split(' ')
            .Where(stringPart => string.IsNullOrEmpty(stringPart) == false)
            .Select(number => int.Parse(number))
            .ToList();
    }

    public int TotalWinningPoints()
    {
        return (int)Math.Pow(2, AmountOfWinningNumbers() - 1);
    }

    public int AmountOfWinningNumbers()
    {
        return PlayerNumbers
            .Count(number => WinningNumbers.Contains(number));
    }
}