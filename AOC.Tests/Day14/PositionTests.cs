using AOC.ConsoleApp.Models.Day14;

namespace AOC.Tests.Day14;

public class RobotTests
{
    [Theory]
    [InlineData(0, 2, 4)]
    [InlineData(1, 4, 1)]
    [InlineData(2, 6, 5)]
    [InlineData(3, 8, 2)]
    [InlineData(4, 10, 6)]
    [InlineData(5, 1, 3)]
    public void PositionAfterNSeconds_ShouldReturnCorrectPosition(int seconds, int expectedX, int expectedY)
    {
        // Arrange
        var position = new Position
        {
            X = 2,
            Y = 4,
            SpaceWidth = 11,
            SpaceTallness = 7,
        };
        var velocity = new Velocity
        {
            DeltaX = 2,
            DeltaY = -3,
        };
        var robot = new Robot(position, velocity);

        // Act
        var endPosition = robot.PositionAfterNSeconds(seconds);

        // Assert
        Assert.Equal(expectedX, endPosition.X);
        Assert.Equal(expectedY, endPosition.Y);
    }
}
