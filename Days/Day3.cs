public class Day3 : Day
{
    public Day3() : base(3)
    {
    }

    protected override void Part1()
    {
        char[,] inputArray = BuildInputArray();
        PrintOutput(1, GetValidNumbersFromArray(inputArray));
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

    private int GetValidNumbersFromArray(char[,] inputArray)
    {
        List<int> validNumbers = new List<int>();
        long rows = inputArray.GetLongLength(0);
        long columns = inputArray.GetLongLength(1);

        int currentRow = 0, currentColumn = 0;

        while(currentRow < inputArray.GetLongLength(0))
        {
            Day3ParsedNumber parsedNumber = ParseNumber(currentRow, currentColumn, inputArray);

            if(parsedNumber.HasMatch)
            {
                if(IsValidNumber(parsedNumber, inputArray))
                {
                    validNumbers.Add(parsedNumber.Value);
                }
                else
                {
                    Console.WriteLine($"{parsedNumber.Value} is invalid");
                }
            }
            
            if(parsedNumber.EndColumn == inputArray.GetLongLength(1))
            {
                currentColumn = 0;
                currentRow = parsedNumber.EndRow + 1;
            }
            else
            {
                currentColumn = parsedNumber.EndColumn;
            }
        }

        return validNumbers.Sum();
    }

    private Day3ParsedNumber ParseNumber(int startingRow, int startingColumn, char[,] inputArray)
    {
        int currentRow = startingRow;
        int currentColumn = startingColumn;
        string currentNumber = string.Empty;

        while(currentRow < inputArray.GetLongLength(0)
            && currentColumn < inputArray.GetLongLength(1))
        {
            char nextChar = inputArray[currentRow, currentColumn];

            if(char.IsDigit(nextChar))
            {
                if(string.IsNullOrEmpty(currentNumber))
                {
                    startingRow = currentRow;
                    startingColumn = currentColumn;
                }

                currentNumber += nextChar;
            }
            else if(string.IsNullOrEmpty(currentNumber) == false)
            {
                break;
            }

            if(currentColumn < inputArray.GetLongLength(1))
            {
                currentColumn++;
            }
            else
            {
                break;
            }
        }

        if(string.IsNullOrEmpty(currentNumber))
        {
            return new Day3ParsedNumber
            {
                EndColumn = currentColumn,
                EndRow = currentRow,
                HasMatch = false,
                StartColumn = startingColumn,
                StartRow = startingRow
            };
        }

        return new Day3ParsedNumber
        {
            EndColumn = currentColumn,
            EndRow = currentRow,
            HasMatch = true,
            StartColumn = startingColumn,
            StartRow = startingRow,
            Value = int.Parse(currentNumber)
        };
    }

    private bool IsValidNumber(Day3ParsedNumber parsedNumber, char[,] inputArray)
    {
        if(parsedNumber.StartRow != 0)
        {
            if(CheckAbove(parsedNumber, inputArray))
            {
                return true;
            }
        }

        if(parsedNumber.EndRow != inputArray.GetLongLength(0) - 1)
        {
            if(CheckBelow(parsedNumber, inputArray))
            {
                return true;
            }
        }

        if(parsedNumber.StartColumn != 0)
        {
            if(CheckLeft(parsedNumber, inputArray))
            {
                return true;
            }
        }

        if(parsedNumber.EndColumn < inputArray.GetLongLength(1) - 1)
        {
            if(CheckRight(parsedNumber, inputArray))
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckAbove(Day3ParsedNumber parsedNumber, char[,] inputArray)
    {
        int startingColumn = 0, endingColumn = 0;

        if(parsedNumber.StartColumn != 0)
        {
            startingColumn = parsedNumber.StartColumn - 1;
        }

        if(parsedNumber.EndColumn != inputArray.GetLongLength(1))
        {
            endingColumn = parsedNumber.EndColumn + 1;
        }

        for(; startingColumn < endingColumn; startingColumn++)
        {
            char charToCheck = inputArray[parsedNumber.StartRow - 1, startingColumn];

            if(char.IsDigit(charToCheck) == false && charToCheck != '.')
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckBelow(Day3ParsedNumber parsedNumber, char[,] inputArray)
    {
        int startingColumn = 0;

        if(parsedNumber.StartColumn != 0)
        {
            startingColumn = parsedNumber.StartColumn - 1;
        }

        for(; startingColumn < parsedNumber.EndColumn; startingColumn++)
        {
            char charToCheck = inputArray[parsedNumber.EndRow + 1, startingColumn];

            if(char.IsDigit(charToCheck) == false && charToCheck != '.')
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckLeft(Day3ParsedNumber parsedNumber, char[,] inputArray)
    {
        int startingRow = 0, endingRow = 0;

        if(parsedNumber.StartRow != 0)
        {
            startingRow = parsedNumber.StartRow - 1;
        }

        if(parsedNumber.EndRow != inputArray.GetLongLength(0) - 1)
        {
            endingRow = parsedNumber.StartRow + 1;
        }

        for(; startingRow <= endingRow; startingRow++)
        {
            char charToCheck = inputArray[startingRow, parsedNumber.StartColumn - 1];

            if(char.IsDigit(charToCheck) == false && charToCheck != '.')
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckRight(Day3ParsedNumber parsedNumber, char[,] inputArray)
    {
        int startingRow = 0, endingRow = 0;

        if(parsedNumber.StartRow != 0)
        {
            startingRow = parsedNumber.StartRow - 1;
        }

        if(parsedNumber.EndRow != inputArray.GetLongLength(0) - 1)
        {
            endingRow = parsedNumber.StartRow + 1;
        }

        for(; startingRow <= endingRow; startingRow++)
        {
            char charToCheck = inputArray[startingRow, parsedNumber.EndColumn];

            if(char.IsDigit(charToCheck) == false && charToCheck != '.')
            {
                return true;
            }
        }

        return false;
    }
}