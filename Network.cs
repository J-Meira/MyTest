namespace MyTest
{
  public class Network
  {
    private readonly int _count;
    private readonly List<Connection> _connections = [];

    public Network(int count)
    {
      if (count <= 0)
      {
        throw new ArgumentException("Parameter has to be  greater than 0", nameof(count));
      }
      _count = count;
    }

    private void Validate(int elementA, int elementB)
    {
      if (elementA <= 0)
      {
        throw new ArgumentOutOfRangeException(nameof(elementA), "Parameter must be greater than 0");
      }
      if (elementB <= 0)
      {
        throw new ArgumentOutOfRangeException(nameof(elementB), "Parameter must be greater than 0");
      }
      if (elementA == elementB)
      {
        throw new ArgumentException("Parameters has to be different", nameof(elementA));
      }
      if (elementA > _count)
      {
        throw new ArgumentOutOfRangeException(nameof(elementA), $"Parameter has to be less or equal to {_count}");
      }
      if (elementB > _count)
      {
        throw new ArgumentOutOfRangeException(nameof(elementB), $"Parameter has to be less or equal to {_count}");
      }
    }

    private bool IsDirectlyConnected(int elementA, int elementB)
    {
      return _connections
        .Where(x => (x.ElementA == elementA && x.ElementB == elementB) ||
                    (x.ElementA == elementB && x.ElementB == elementA)).Any();
    }

    private List<Connection> GetConnections(int element)
    {
      return _connections.Where(c => c.ElementA == element || c.ElementB == element).ToList();
    }

    private List<int> GetIndirectlyConnections(int element, List<int> tested)
    {
      var connections = GetConnections(element);
      var result = new List<int>();

      foreach (var item in connections)
      {
        var elementConnected = item.ElementA == element ? item.ElementB : element;
        if (!tested.Contains(elementConnected))
        {
          tested.Add(elementConnected);
          result.Add(elementConnected);
          result.AddRange(GetIndirectlyConnections(elementConnected, tested));
        }
      }

      return result;
    }

    public void Connect(int elementA, int elementB)
    {
      Validate(elementA, elementB);

      if (IsDirectlyConnected(elementA, elementB))
      {
        throw new InvalidOperationException("Elements are already connected");
      }

      _connections.Add(new Connection(elementA, elementB));
    }

    public bool Query(int elementA, int elementB)
    {
      Validate(elementA, elementB);

      if (IsDirectlyConnected(elementA, elementB))
      {
        return true;
      }

      var indirectlyConnections = GetIndirectlyConnections(elementA, [elementA]);
      return indirectlyConnections.Contains(elementB);
    }
  }
}
