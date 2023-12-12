using System.Numerics;

public class Day2 : Day
{
    public Day2() : base(2)
    {
    }

    protected override void Part1()
    {
        Dictionary<string, int> maxAllowedValues = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        string[] games = File.ReadAllLines("Inputs/Day2.txt");

        int validGamesTotal = 0;
        foreach(string game in games)
        {
            int gameID = ParseGameID(game);
            List<Tuple<string, int>> cubeReveals = ParseCubeValues(game);

            if(GameIsValid(maxAllowedValues, cubeReveals))
            {
                validGamesTotal += gameID;
            }
        }

        PrintOutput(1, validGamesTotal);
    }

    protected override void Part2()
    {
        string[] games = File.ReadAllLines("Inputs/Day2.txt");

        int runningPowersTotal = 0;
        foreach(string game in games)
        {
            int gameID = ParseGameID(game);
            List<Tuple<string, int>> cubeReveals = ParseCubeValues(game);

            runningPowersTotal += GetPowerForGame(cubeReveals);   
        }

        PrintOutput(2, runningPowersTotal);
    }

    private int ParseGameID(string gameString)
    {        
        int colonIndex = gameString.IndexOf(":");
        string gameIDString = gameString.Substring(0, colonIndex);

        return int.Parse(gameIDString.Split(' ')[1]);
    }

    private List<Tuple<string, int>> ParseCubeValues(string gameString)
    {
        int colonIndex = gameString.IndexOf(":");
        string cubeValuesString = gameString.Substring(colonIndex + 1);
        string[] cubeValues = cubeValuesString.Split(";");

        List<Tuple<string, int>> cubeValuesList = new List<Tuple<string, int>>();

        foreach(string cubeValue in cubeValues)
        {
            string[] cubeReveals = cubeValue.Split(",");

            foreach(string cubeReveal in cubeReveals)
            {
                string[] cubeRevealParts = cubeReveal.Trim().Split(" ");
                int amountRevealed = int.Parse(cubeRevealParts[0]);
                
                cubeValuesList.Add(new Tuple<string, int>(cubeRevealParts[1], amountRevealed));
            }
        }

        return cubeValuesList;
    }

    private bool GameIsValid(Dictionary<string, int> maxAllowedValues, List<Tuple<string, int>> cubeValues)
    {
        foreach(Tuple<string, int> cubeValue in cubeValues)
        {
            if(maxAllowedValues[cubeValue.Item1] < cubeValue.Item2)
            {
                return false;
            }
        }

        return true;
    }

    public int GetPowerForGame(List<Tuple<string, int>> cubeValues)
    {
        Dictionary<string, int> lowestValues = new Dictionary<string, int>
        {
            { "red", 0 },
            { "green", 0 },
            { "blue", 0 }
        };

        foreach(Tuple<string, int> cubeValue in cubeValues)
        {
            if(lowestValues[cubeValue.Item1] < cubeValue.Item2)
            {
                lowestValues[cubeValue.Item1] = cubeValue.Item2;
            }
        }

        return lowestValues.Values.Aggregate((x, y) => x * y);
    }
}