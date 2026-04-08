using System.Text;
using RedRover.Puzzle.Models;

namespace RedRover.Puzzle.Services;

public class FormatOutputService
{
    public string FormatOutput(List<ParsedData> data)
    {
        StringBuilder output = new();
        FormatOutput(data, 0, output);
        return output.ToString().TrimEnd();
    }
    
    private void FormatOutput(List<ParsedData> input, int depth, StringBuilder output)
    {
        foreach (ParsedData data in input)
        {
            output.Append(' ', depth*2);
            output.AppendLine($"- {data.Name}");
            
            if (data.Attributes!.Count > 0)
                FormatOutput(data.Attributes, depth + 1, output);
        }
    }
}