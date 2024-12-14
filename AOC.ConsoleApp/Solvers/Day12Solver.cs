using AOC.ConsoleApp.Models;
using AOC.ConsoleApp.Models.Day12;

namespace AOC.ConsoleApp.Solvers;

public class Day12Solver : AbstractSolver
{
    private IEnumerable<Region> _regions;
    public Day12Solver(IEnumerable<string> lines) : base(lines)
    {
        var plants = lines.Select(line => line.Select(character => new Plant(type: character)));
        var garden = new Grid<Plant>(plants);
        AssignRegions(garden);
        _regions = garden.Select(position => position.Value.Region!).Distinct();
    }

    public override string SolveFirstChallenge()
    {
        var totalPrice = _regions.Sum(region => region!.GetPrice());
        return totalPrice.ToString();
    }

    public override string SolveSecondChallenge()
    {
        var totalDiscountPrice = _regions.Sum(region => region!.GetDiscountPrice());
        return totalDiscountPrice.ToString();
    }

    private void AssignRegions(Grid<Plant> garden)
    {
        foreach (var plantPosition in garden)
        {
            var plant = plantPosition.Value;
            var neighbouringRegionsWithSamePlantType = plantPosition
                .GetAllDirectNeighbours()
                .Select(position => position.Value)
                .Where(neighbour => neighbour.HasRegion)
                .Where(neighbour => neighbour.Type == plant.Type)
                .Select(neighbour => neighbour.Region)
                .Distinct();

            switch (neighbouringRegionsWithSamePlantType.ToList())
            {
                case []:
                    Region.AddToNewRegion(plantPosition);
                    break;
                case [Region region]:
                    region!.AddGridPosition(plantPosition);
                    break;
                case [Region region1, Region region2]:
                    var newRegion = region1.Join(region2);
                    newRegion.AddGridPosition(plantPosition);
                    break;
                default:
                    throw new NotImplementedException("Unexpected situation.");
            }
        }
    }
}
