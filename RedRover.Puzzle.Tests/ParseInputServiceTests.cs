using RedRover.Puzzle.Models;
using RedRover.Puzzle.Services;
using Xunit;

namespace RedRover.Puzzle.Tests;

public class ParseInputServiceTests
{
    private ParseInputService _service = new();
        
    [Fact]
    public void ParseInputService_HappyPath()
    {
        // Arrange
        string input = "(id, things(thing1(name), thing2))";
        
        // Act
        List<ParsedData> output = _service.ParseInput(input);
        
        // Assert
        Assert.Equal(2, output.Count);
        
        Assert.Equal("id",  output[0].Name);
        Assert.Equal("things", output[1].Name);
        
        Assert.Empty(output[0].Attributes!);
        Assert.Equal(2, output[1].Attributes!.Count);
        
        Assert.Equal("thing1", output[1].Attributes![0].Name);
        Assert.Equal("thing2", output[1].Attributes![1].Name);

        Assert.Single(output[1].Attributes![0].Attributes!);
        Assert.Empty(output[1].Attributes![1].Attributes!);
        
        Assert.Equal("name", output[1].Attributes![0].Attributes![0].Name);
    }

    [Fact]
    public void StringDoesNotStartWithParentheses_ThrowsException()
    {
        // Arrange
        string input = "id, things(thing1(name), thing2";
        
        // Act + Assert
        Assert.Throws<ArgumentException>(() => _service.ParseInput(input));
    }
    
    [Fact]
    public void StringDoesNotContainMatchingParentheses_ThrowsException()
    {
        // Arrange
        string input = "(id, things(thing1(name(), thing2)";
        
        // Act + Assert
        Assert.Throws<ArgumentException>(() => _service.ParseInput(input));
    }
    
    [Fact]
    public void StringIsNotCommaDeliminated_ThrowsException()
    {
        // Arrange
        string input = "(id things(thing1(name), thing2))";
        
        // Act + Assert
        Assert.Throws<ArgumentException>(() => _service.ParseInput(input));
    }
}