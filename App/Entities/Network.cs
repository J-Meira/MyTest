namespace App.Entities;
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

  private IEnumerable<int> GetIndirectlyConnections(int element)
  {
    var tested = new HashSet<int> { element };
    var toTest = new Stack<int>(GetConnections(element).Select(c => c.ElementA == element ? c.ElementB : c.ElementA));

    while (toTest.Count > 0)
    {
      var next = toTest.Pop();
      if (tested.Contains(next)) continue;

      tested.Add(next);
      yield return next;
      
      foreach (var connection in GetConnections(next))
      {
        var elementConnected = connection.ElementA == next ? connection.ElementB : connection.ElementA;
        if (!tested.Contains(elementConnected))
        {
          toTest.Push(elementConnected);
        }
      }
    }
  }

  public void Connect(int elementA, int elementB)
  {
    Validate(elementA, elementB);

    if (IsDirectlyConnected(elementA, elementB))
    {
      throw new InvalidOperationException("Elements are already directly connected");
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

    var indirectlyConnections = GetIndirectlyConnections(elementA);
    return indirectlyConnections.Contains(elementB);
  }
}
