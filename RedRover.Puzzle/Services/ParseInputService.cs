using RedRover.Puzzle.Models;

namespace RedRover.Puzzle.Services;

public class ParseInputService
{
    public List<ParsedData> ParseInput(string input)
    {
        if (input[0] != '(' || input[^1] != ')')
            throw new ArgumentException("Input must start with '(' and end with ')'.");
        
        if (!CheckParenthesesCount(input))
            throw new ArgumentException("Input must contain equal number of parentheses.");
        
        if (System.Text.RegularExpressions.Regex.IsMatch(input, @"\w\s+\w"))
            throw new ArgumentException("Input must be comma delimited.");
        
        if (System.Text.RegularExpressions.Regex.IsMatch(input, @",\s*\("))
            throw new ArgumentException("A '(' must be preceded by a name, not a comma.");
        
        string trimmed = input[1..^1];
        return ParseSegment(trimmed);
    }
    
    private List<ParsedData> ParseSegment(string segment)
    {
        List<ParsedData> results = [];
        int i = 0;

        while (i < segment.Length)
        {
            if (segment[i] == ',' || segment[i] == ' ')
            {
                i++;
                continue;
            }
            
            int nameStart = i;
            
            while (i < segment.Length && segment[i] != ',' && segment[i] != '(' && segment[i] != ')')
                i++;

            string name = segment[nameStart..i];
            
            if (i < segment.Length && segment[i] == '(')
            {
                int depth = 0;
                int groupStart = i;
                
                while (i < segment.Length)
                {
                    if (segment[i] == '(')
                        depth++;
                    
                    else if (segment[i] == ')')
                        depth--;
                    
                    i++;
                    
                    if (depth == 0)
                        break;
                }
                
                string innerSegment = segment[(groupStart + 1)..(i - 1)];

                if (!string.IsNullOrWhiteSpace(name))
                {
                    ParsedData parsedData = new ParsedData()
                    {
                        Name = name,
                        Attributes = ParseSegment(innerSegment) 
                    };
                
                    results.Add(parsedData);
                }
                else
                {
                    results.AddRange(ParseSegment(innerSegment));
                }
                
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    ParsedData parsedData = new ParsedData
                    {
                        Name = name,
                        Attributes = new List<ParsedData>()
                    };
                    results.Add(parsedData);
                }
            }
        }
        return results;
    }
    
    private static bool CheckParenthesesCount(string input)
    {
        int count = 0;
        foreach (char c in input)
        {
            if (c == '(') count++;
            else if (c == ')') count--;
        }
        return count == 0;
    }
}