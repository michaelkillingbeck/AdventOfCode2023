public class Day4 : Day
{
    public Day4() : base(4)
    {
    }

    protected override void Part1()
    {
        string[] allLines = File.ReadAllLines("Inputs/Day4.txt");
        IEnumerable<Day4Scratchcard> scratchcards = CreateScratchcards(allLines);
        PrintOutput(1, scratchcards.Sum(scratchcard => scratchcard.TotalWinningPoints()));
    }

    protected override void Part2()
    {
        string[] allLines = File.ReadAllLines("Inputs/Day4.txt");
        IEnumerable<Day4Scratchcard> scratchcards = CreateScratchcards(allLines);

        foreach(Day4Scratchcard scratchcard in scratchcards)
        {
            int numberOfWinningNumbers = scratchcard.AmountOfWinningNumbers();

            scratchcards
                .Where(card => card.GameID > scratchcard.GameID
                        && card.GameID <= scratchcard.GameID + numberOfWinningNumbers)
                .ToList()
                .ForEach(card => card.Instances += scratchcard.Instances);
        }

        PrintOutput(2, scratchcards.Sum(scratchcard => scratchcard.Instances));
    }

    private IEnumerable<Day4Scratchcard> CreateScratchcards(string[] inputLines)
    {
        List<Day4Scratchcard> returnScratchcards = new List<Day4Scratchcard>();

        foreach(string line in inputLines)
        {
            returnScratchcards.Add(new Day4Scratchcard(line));
        }

        return returnScratchcards;
    }
}