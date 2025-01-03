

namespace AOC.ConsoleApp.Models.Day23;

public class LanParty : IEquatable<LanParty>
{
    private readonly IDictionary<Computer, List<Computer>> _connections;
    private readonly IEnumerable<Computer> _computers;

    public static IDictionary<LanParty, IEnumerable<Computer>> InterconnectedGroupsMaxDictionary = new Dictionary<LanParty, IEnumerable<Computer>>();
    public static IDictionary<LanParty, List<List<Computer>>> InterconnectedGroupsDictionary = new Dictionary<LanParty, List<List<Computer>>>();

    public LanParty(IEnumerable<(Computer, Computer)> connections)
    {
        _computers = connections.SelectMany(connection => new List<Computer> { connection.Item1, connection.Item2 }).Distinct().OrderBy(computer => computer.Name);
        _connections = _computers.ToDictionary(computer => computer, computer => new List<Computer>());

        foreach (var (computer1, computer2) in connections)
        {
            _connections[computer1].Add(computer2);
            _connections[computer2].Add(computer1);
        }
        foreach (var connectionlist in _connections.Values)
        {
            connectionlist.Sort();
        }
    }

    public LanParty(IDictionary<Computer, List<Computer>> connections)
    {
        _connections = connections;
        _computers = connections.Keys.OrderBy(computer => computer.Name);
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

    public List<List<Computer>> GetInterconnectedGroups()
    {
        if (InterconnectedGroupsDictionary.TryGetValue(this, out var interconnectedGroups))
            return interconnectedGroups;

        interconnectedGroups = new List<List<Computer>>();
        foreach (var (computer, connectedComputers) in _connections)
        {
            if (connectedComputers.Count == 0)
                interconnectedGroups.Add(new List<Computer> { computer });

            var connections = connectedComputers.ToDictionary(
                connectedComputer => connectedComputer,
                connectedComputer => _connections[connectedComputer].Where(computer => connectedComputers.Contains(computer)).ToList());
            var lanParty = new LanParty(connections);

            interconnectedGroups.AddRange(lanParty.GetInterconnectedGroups().Select(group => group.Append(computer).ToList()));
        }
        InterconnectedGroupsDictionary.Add(this, interconnectedGroups);
        return interconnectedGroups;
    }

    public IEnumerable<Computer> GetInterconnectedGroupsMaxCount()
    {
        if (InterconnectedGroupsMaxDictionary.TryGetValue(this, out var maxGroup))
            return maxGroup;

        maxGroup = new List<Computer>();
        if (_computers.Count() <= 1)
        {
            return _computers;
        }
        foreach (var (computer, connectedComputers) in _connections)
        {
            if (connectedComputers.Count + 1 <= maxGroup.Count()) continue;

            var connections = connectedComputers.ToDictionary(
                connectedComputer => connectedComputer,
                connectedComputer => _connections[connectedComputer].Where(computer => connectedComputers.Contains(computer)).ToList());
            var lanParty = new LanParty(connections);

            var localMax = lanParty.GetInterconnectedGroupsMaxCount();
            if (localMax.Count() + 1 > maxGroup.Count())
                maxGroup = localMax.Append(computer);
        }
        InterconnectedGroupsMaxDictionary.Add(this, maxGroup);
        return maxGroup;
    }

    public bool Equals(LanParty? other)
    {
        return other is not null
            && other._computers.Count() == _computers.Count()
            && string.Join(',', _computers.Select(computer => computer.Name)) == string.Join(',', other._computers.Select(computer => computer.Name));
    }

    public override int GetHashCode()
    {
        return string.Join(',', _computers.Select(computer => computer.Name)).GetHashCode();
    }
}
