using RedRover.Puzzle.Models;
using RedRover.Puzzle.Services;
using Xunit;

namespace RedRover.Puzzle.Tests;

public class SortOutputServiceTests
{
    [Fact]
    public void SortOutputService_HappyPath()
    {
        // Arrange
        string input = "(c, a(d, b), b)";
        ParseInputService parseService = new ParseInputService();
        SortOutputService sortService = new SortOutputService();

        // Act
        List<ParsedData> list = parseService.ParseInput(input);
        sortService.SortData(list);

        // Assert
        Assert.Equal("a", list[0].Name);
        Assert.Equal("b", list[0].Attributes![0].Name);
        Assert.Equal("d", list[0].Attributes![1].Name);
        Assert.Equal("b", list[1].Name);
        Assert.Equal("c", list[2].Name);
    }

    [Fact]
    public void SortOutputService_Numbers_HappyPath()
    {
        // Arrange
        string input = "(4, 2(5, 1), 3)";
        ParseInputService parseService = new ParseInputService();
        SortOutputService sortService = new SortOutputService();

        // Act
        List<ParsedData> list = parseService.ParseInput(input);
        sortService.SortData(list);

        // Assert
        Assert.Equal("2", list[0].Name);
        Assert.Equal("1", list[0].Attributes![0].Name);
        Assert.Equal("5", list[0].Attributes![1].Name);
        Assert.Equal("3", list[1].Name);
        Assert.Equal("4", list[2].Name);
    }
}