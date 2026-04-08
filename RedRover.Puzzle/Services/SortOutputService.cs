using RedRover.Puzzle.Models;

namespace RedRover.Puzzle.Services;

public class SortOutputService
{
    public void SortData(List<ParsedData> list)
    {
        list.Sort(CompareName);
        
        foreach (ParsedData data in list)
        {
            if (data.Attributes!.Count != 0)
                SortData(data.Attributes);
        }
    }

    private int CompareName(ParsedData a, ParsedData b)
    {
        return string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase);
    }
}