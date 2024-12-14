using System.ComponentModel.Design;

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

    private int GetNumberOfLines()
    {
        List<Edge> edges = GetEdges();

        var numberOfLines = 0;
        while (edges.Count() > 0)
        {
            numberOfLines++;
            var firstEdge = edges.First();
            var currentEdge = firstEdge;
            edges.Remove(currentEdge);
            while (!currentEdge.Touches(firstEdge))
            {
                var possibleNextEdges = edges.Where(edge => 
                {
                    if (currentEdge.Touches(edge)) return true;
                    edge.Turn(); 
                    return currentEdge.Touches(edge);
                });
                var nextEdge = ChooseNextEdge(currentEdge, possibleNextEdges);
                if (!nextEdge.HasSameDirectionAs(currentEdge)) numberOfLines++;
                currentEdge = nextEdge;
                edges.Remove(currentEdge);
            }
            if (!currentEdge.Touches(firstEdge)) throw new Exception("Unexpected behaviour: " +
                "last edge was expected to touch first edge, but this wasn't the case.");
            if (currentEdge.HasSameDirectionAs(firstEdge)) numberOfLines--;
        }
        return numberOfLines;
    }

    private Edge ChooseNextEdge(Edge currentEdge, IEnumerable<Edge> possibleNextEdges)
    {
        if (!possibleNextEdges.Any()) 
            throw new Exception("Dit had niet moeten gebeuren");

        if (possibleNextEdges.Count() <= 1)
            return possibleNextEdges.First();

        return possibleNextEdges.First(edge => !edge.HasSameDirectionAs(currentEdge));
    }

    private List<Edge> GetEdges()
    {
        var offsets = new List<(int row, int column)> { (-1, 0), (1, 0), (0, -1), (0, 1) };
        var edges = new List<Edge>();
        foreach (var position in GridPositions)
        {
            var topLeftCorner = (row: position.Row, column: position.Column);
            var topRightCorner = (row: position.Row, column: position.Column + 1);
            var bottomLeftCorner = (row: position.Row + 1, column: position.Column);
            var bottomRightCorner = (row: position.Row + 1, column: position.Column + 1);

            foreach (var offset in offsets)
            {
                var neighbour = position.GetNeighbour(offset);
                if (neighbour.IsValidPosition && neighbour.Value.Type == position.Value.Type) continue;

                switch (offset)
                {
                    case (0, -1):
                        edges.Add(new Edge(bottomLeftCorner, topLeftCorner));
                        break;
                    case (-1, 0):
                        edges.Add(new Edge(topLeftCorner, topRightCorner));
                        break;
                    case (0, 1):
                        edges.Add(new Edge(topRightCorner, bottomRightCorner));
                        break;
                    case (1, 0):
                        edges.Add(new Edge(bottomRightCorner, bottomLeftCorner));
                        break;
                }
            }
        }

        return edges;
    }

    public int GetPrice()
    {
        return GetArea() * GetPerimeter();
    }

    public int GetDiscountPrice()
    {
        return GetArea() * GetNumberOfLines();
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
