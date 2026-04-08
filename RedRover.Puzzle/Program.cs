using RedRover.Puzzle.Models;
using RedRover.Puzzle.Services;

Console.WriteLine("Hello! Thank you for running my program!");
Console.WriteLine();
Console.WriteLine("This program accepts a string input and then formats it into the expected output, as established by the requirements.");
Console.WriteLine();
Console.WriteLine("For example, this input: (id, name, email, type(id, name, customFields(c1, c2, c3)), externalId) will return the following:");
Console.WriteLine();
Console.WriteLine("""
                  - id
                  - name
                  - email
                  - type
                    - id
                    - name
                    - customFields
                      - c1
                      - c2
                      - c3
                  - externalId
                  """);
Console.WriteLine();
Console.WriteLine("Input must be surrounded by ( and ), must have an equal number of ( and ), and must be comma delimited.");
Console.WriteLine();
Console.WriteLine("Please type your input below, or type 'q' to quit:");

ParseInputService parseService = new ParseInputService();
FormatOutputService formatService = new FormatOutputService();
SortOutputService sortService = new SortOutputService();

while (true)
{
    List<ParsedData> list = new List<ParsedData>();
    
    while (true)
    {
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Input cannot be null or empty. Please try again.");
            Console.WriteLine();
            continue;
        }
        if (input == "q")
        { 
            Console.WriteLine();
            Console.WriteLine("Exiting program. Thank you for using me!");
            return;
        }
        
        
        try
        { 
            list = parseService.ParseInput(input);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine();
            Console.WriteLine($"{e.Message}");
            continue;
        }
        string output = formatService.FormatOutput(list);
        Console.WriteLine();
        Console.WriteLine(output);
        break;
    }
    
    Console.WriteLine();
    Console.WriteLine("Would you like to sort this list alphabetically? (y/n)");
    
    while (true)
    {
        string? input = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine();
            Console.WriteLine("Input cannot be null or empty. Please try again.");
            continue;
        }
        if (input == "q" || input == "n")
        { 
            Console.WriteLine();
            Console.WriteLine("Exiting program. Thank you for using me!");
            return;
        }
        
        sortService.SortData(list);
        
        string output = formatService.FormatOutput(list);
        Console.WriteLine();
        Console.WriteLine(output);
        Console.WriteLine();
        Console.WriteLine("There you are! If you would like to try another input, please type below. Otherwise, type 'q' to quit.");
        break;
    }
}