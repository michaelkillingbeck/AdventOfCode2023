public class Day1
{
    public static void Part1()
    {
        string[] inputLines = File.ReadAllLines("Inputs/Day1.txt");

        int total = inputLines.Select(line => 
            line.Where(character => Char.IsDigit(character)))
            .Sum(line => int.Parse(line.First().ToString() + int.Parse(line.Last().ToString())));

        Console.WriteLine($"Day 1 Part 1 : {total}");
    }

    public static void Part2()
    {
        Dictionary<string, int> numericValues = new Dictionary<string, int>();
        numericValues.Add("one", 1);
        numericValues.Add("two", 2);
        numericValues.Add("three", 3);
        numericValues.Add("four", 4);
        numericValues.Add("five", 5);
        numericValues.Add("six", 6);
        numericValues.Add("seven", 7);
        numericValues.Add("eight", 8);
        numericValues.Add("nine", 9);
        numericValues.Add("1", 1);
        numericValues.Add("2", 2);
        numericValues.Add("3", 3);
        numericValues.Add("4", 4);
        numericValues.Add("5", 5);
        numericValues.Add("6", 6);
        numericValues.Add("7", 7);
        numericValues.Add("8", 8);
        numericValues.Add("9", 9);

        string[] inputLines = File.ReadAllLines("Inputs/Day1.txt");
        
        int total = 0;

        foreach(string line in inputLines)
        {
            List<Day1NumericStringFound> valuesFound = new List<Day1NumericStringFound>();

            foreach(KeyValuePair<string, int> kvp in numericValues)
            {
                int startingPoint = 0;

                while(startingPoint != -1 && startingPoint < line.Length)
                {
                    startingPoint = line.IndexOf(kvp.Key, startingPoint);
                    valuesFound.Add(new Day1NumericStringFound
                    {
                        Index = startingPoint,
                        Value = kvp.Value
                    });

                    if(startingPoint != -1)
                    {
                        startingPoint++;
                    }
                }
            }

            valuesFound = valuesFound
                .Where(value => value.Index > -1)
                .OrderBy(value => value.Index)
                .ToList();

            total += int.Parse(valuesFound.First().Value.ToString() + valuesFound.Last().Value.ToString());
        }

        Console.WriteLine($"Day 1 Part 2 : {total}");
    }

    public static void Run()
    {
        Part1();
        Part2();
    }
}