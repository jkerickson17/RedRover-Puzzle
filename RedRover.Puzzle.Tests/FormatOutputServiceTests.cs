using RedRover.Puzzle.Models;
using RedRover.Puzzle.Services;
using Xunit;

namespace RedRover.Puzzle.Tests;

public class FormatOutputServiceTests
{
    [Fact]
    public void FormatOutput_HappyPath()
    {
        // Arrange
        string input = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";
        string expected =
            """
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
            """;
        
        ParseInputService inputService = new();
        FormatOutputService outputService = new();
        
        // Act
        List<ParsedData> parsedData = inputService.ParseInput(input);
        string result = outputService.FormatOutput(parsedData);
        
        // Assert
        Assert.Equal(expected, result);
    }
}