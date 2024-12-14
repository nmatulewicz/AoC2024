namespace AOC.ConsoleApp.Models.Day12;

public class Region
{
    public char PlantType { get; set; }

    private List<GridPosition<Plant>> _gridPositions;
    public IEnumerable<GridPosition<Plant>> GridPositions { get => _gridPositions; }

    public Region(char plantType)
    {
        PlantType = plantType;
        _gridPositions = [];
    }

    public static void AddToNewRegion(GridPosition<Plant> gridPosition)
    {
        var plant = gridPosition.Value;
        var newRegion = new Region(plant.Type)
        {
            _gridPositions = [gridPosition],
        };
        plant.Region = newRegion;
    }

    public int GetArea()
    {
        return GridPositions.Count();
    }

    public int GetPerimeter()
    {
        var perimeter = 0;
        foreach (var position in GridPositions)
        {
            var neighbours = position.GetAllDirectNeighbours();
            var outsideBorderCount = 4 - neighbours.Count();

            perimeter += outsideBorderCount;
            foreach (var neighbour in neighbours)
            {
                if (neighbour.Value.Type != position.Value.Type) perimeter++;
            }
        }
        return perimeter;
    }

    public int GetPrice()
    {
        return GetArea() * GetPerimeter();
    }

    public int GetDiscountPrice()
    {
        throw new NotImplementedException();
    }

    public void AddGridPosition(GridPosition<Plant> position)
    {
        _gridPositions.Add(position);
        position.Value.Region = this;
    }

    public Region Join(Region other)
    {
        if (this == other)
            throw new InvalidOperationException("Cannot join region with itself");

        if (PlantType != other.PlantType)
            throw new InvalidOperationException("You cannot join two regions with different plant types.");

        var newRegion = new Region(PlantType)
        {
            _gridPositions = [.. _gridPositions, .. other._gridPositions],
        };

        foreach (var plant in newRegion.GridPositions.Select(position => position.Value))
        {
            plant.Region = newRegion;
        }

        return newRegion;
    }
}
