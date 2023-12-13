using System.Text;

public class Day3 : Day
{
    public Day3() : base(3)
    {
    }

    protected override void Part1()
    {
        char[,] inputArray = BuildInputArray();
        List<Day3ParsedNumber> numbers = ParseNumbers(inputArray);
        List<Day3ParsedSymbol> symbols = ParseSymbols(inputArray);
        PrintOutput(1, GetSumOfValidNumbers(numbers, symbols));
    }

    protected override void Part2()
    {
        char[,] inputArray = BuildInputArray();
    }

    private char[,] BuildInputArray()
    {
        string[] allLines = File.ReadAllLines("Inputs/Day3.txt");

        char[,] inputArrray = new char[allLines[0].Length, allLines.Length];

        for(int row = 0; row < allLines.Length; row++)
        {
            for(int column = 0; column < allLines[0].Length; column++)
            {
                inputArrray[row, column] = allLines[row][column];
            }
        }

        return inputArrray;
    }

    private List<Day3ParsedNumber> ParseNumbers(char[,] inputArray)
    {
        List<Day3ParsedNumber> returnNumbers = new List<Day3ParsedNumber>();

        long rows = inputArray.GetLongLength(0) - 1;
        long columns = inputArray.GetLongLength(1) - 1;
        int currentRow = 0, currentColumn = 0;

        StringBuilder stringBuilder = new StringBuilder();

        int? startingColumn = null;

        while(currentRow <= rows)
        {
            char nextChar = inputArray[currentRow, currentColumn];

            if(char.IsDigit(nextChar))
            {
                stringBuilder.Append(nextChar);

                if(startingColumn == null)
                {
                    startingColumn = currentColumn;
                }
            }
            else if(stringBuilder.Length > 0 && startingColumn != null)
            {
                returnNumbers.Add(new Day3ParsedNumber
                {
                    EndColumn = currentColumn - 1,
                    Row = currentRow,
                    StartColumn = startingColumn.Value,
                    Value = int.Parse(stringBuilder.ToString())
                });

                startingColumn = null;
                stringBuilder.Clear();
            }

            if(currentColumn == columns)
            {
                if(stringBuilder.Length > 0 && startingColumn != null)
                {
                    returnNumbers.Add(new Day3ParsedNumber
                    {
                        EndColumn = currentColumn,
                        Row = currentRow,
                        StartColumn = startingColumn.Value,
                        Value = int.Parse(stringBuilder.ToString())
                    });

                    startingColumn = null;
                    stringBuilder.Clear();
                }

                currentColumn = 0;
                currentRow++;
            }
            else
            {
                currentColumn++;
            }
        }

        return returnNumbers;
    }

    private List<Day3ParsedSymbol> ParseSymbols(char[,] inputArray)
    {
        List<Day3ParsedSymbol> returnSymbols = new List<Day3ParsedSymbol>();

        long rows = inputArray.GetLongLength(0) - 1;
        long columns = inputArray.GetLongLength(1) - 1;
        int currentRow = 0, currentColumn = 0;

        while(currentRow <= rows)
        {
            char nextChar = inputArray[currentRow, currentColumn];

            if(char.IsDigit(nextChar) == false && nextChar != '.')
            {
                returnSymbols.Add(new Day3ParsedSymbol
                {
                    Column = currentColumn,
                    Row = currentRow,
                    Value = nextChar
                });
            }

            if(currentColumn == columns)
            {
                currentColumn = 0;
                currentRow++;
            }
            else
            {
                currentColumn++;
            }
        }

        return returnSymbols;
    }

    private int GetSumOfValidNumbers(List<Day3ParsedNumber> numbers, List<Day3ParsedSymbol> symbols)
    {
        int returnValue = 0;

        foreach(Day3ParsedNumber number in numbers)
        {
            bool symbolsInRowAbove = symbols
                .Any(symbol => symbol.Row == number.Row - 1 &&
                    (symbol.Column >= number.StartColumn - 1 && symbol.Column <= number.EndColumn + 1));

            bool symbolsInRowBelow = symbols
                .Any(symbol => symbol.Row == number.Row + 1 &&
                    (symbol.Column >= number.StartColumn - 1 && symbol.Column <= number.EndColumn + 1));

            bool symbolsInColumnToTheLeft = symbols
                .Any(symbol => symbol.Column == number.StartColumn - 1 &&
                    (symbol.Row >= number.Row - 1 && symbol.Row <= number.Row + 1));

            bool symbolsInColumnToTheRight = symbols
                .Any(symbol => symbol.Column == number.EndColumn + 1 &&
                    (symbol.Row >= number.Row - 1 && symbol.Row <= number.Row + 1));

            if(symbolsInRowAbove || symbolsInRowBelow
                || symbolsInColumnToTheLeft || symbolsInColumnToTheRight)
            {
                returnValue += number.Value;
            }
        }

        return returnValue;
    }
}