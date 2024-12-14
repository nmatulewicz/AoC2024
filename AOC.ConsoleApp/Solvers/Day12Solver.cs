using AOC.ConsoleApp.Models;
using AOC.ConsoleApp.Models.Day12;

namespace AOC.ConsoleApp.Solvers;

public class Day12Solver : AbstractSolver
{
    private Grid<Plant> _garden;
    public Day12Solver(IEnumerable<string> lines) : base(lines)
    {
        var plants = lines.Select(line => line.Select(character => new Plant(type: character)));
        _garden = new Grid<Plant>(plants);
    }

    public override string SolveFirstChallenge()
    {
        AssignRegions();
        var regions = _garden.Select(position => position.Value.Region).Distinct();
        var totalPrice = regions.Sum(region => region!.GetPrice());
        return totalPrice.ToString();

        // 8788748 ==> Too high
        // 1477538 ==> Too low
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }

    private void AssignRegions()
    {
        foreach (var plantPosition in _garden)
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
