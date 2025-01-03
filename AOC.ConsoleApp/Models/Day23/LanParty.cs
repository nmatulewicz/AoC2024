
namespace AOC.ConsoleApp.Models.Day23;

public class LanParty
{
    private readonly IDictionary<Computer, List<Computer>> _connections;
    private readonly IEnumerable<Computer> _computers;

    public LanParty(IEnumerable<(Computer, Computer)> connections)
    {
        _computers = connections.SelectMany(connection => new List<Computer> { connection.Item1, connection.Item2 }).Distinct();
        _connections = _computers.ToDictionary(computer => computer, computer => new List<Computer>());

        foreach (var (computer1, computer2) in connections)
        {
            _connections[computer1].Add(computer2);
            _connections[computer2].Add(computer1);
        }
    }

    public IEnumerable<(Computer, Computer, Computer)> GetAllInterconnectedGroupsOfThreeComputers()
    {
        var trios = new List<(Computer, Computer, Computer)>();
        foreach (var (computer, connectedComputers) in _connections)
        {
            var connectedComputersWithGreaterNameValue = connectedComputers
                .Where(connectedComputer => string.Compare(computer.Name, connectedComputer.Name) < 0).ToArray(); 
            for (var i = 0; i < connectedComputersWithGreaterNameValue.Length; i++)
            {
                var connectedComputer1 = connectedComputersWithGreaterNameValue[i];
                for (var j = i + 1; j < connectedComputersWithGreaterNameValue.Length; j++)
                {
                    var connectedComputer2 = connectedComputersWithGreaterNameValue[j];
                    if (AreConnected(connectedComputer1, connectedComputer2)) 
                        trios.Add((computer, connectedComputer1, connectedComputer2));
                }
            }
        }
        return trios;
    }

    public bool AreConnected(Computer computer1,  Computer computer2)
    {
        return _connections[computer1].Contains(computer2);
    }
}
