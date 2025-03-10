using App.Entities;

namespace Tests;

public class UnitTest
{
  private readonly Network _network = new(8);

  [Fact]
  public void Constructor_WithValidParameter_ShouldCreateNetwork()
  {
    // Act
    var network = new Network(8);

    // Assert
    Assert.NotNull(network);
    Assert.IsType<Network>(network);
  }

  [Fact]
  public void Constructor_WithInvalidParameter_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentException>(static () => new Network(0));
  }

  [Fact]
  public void Connect_WithValidParameters_ShouldConnect()
  {
    // Act
    _network.Connect(1, 2);

    // Assert
    Assert.True(_network.Query(1, 2));

  }

  [Fact]
  public void Connect_WithZeroAsFirstParameter_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentOutOfRangeException>(static () => new Network(8).Connect(0, 1));
  }

  [Fact]
  public void Connect_WithZeroAsSecondParameter_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentOutOfRangeException>(static () => new Network(8).Connect(1, 0));
  }

  [Fact]
  public void Connect_WithNegativeFirstParameter_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentOutOfRangeException>(static () => new Network(8).Connect(-1, 1));
  }

  [Fact]
  public void Connect_WithNegativeSecondParameter_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentOutOfRangeException>(static () => new Network(8).Connect(1, -1));
  }

  [Fact]
  public void Connect_WithEqualParameters_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentException>(static () => new Network(8).Connect(1, 1));
  }

  [Fact]
  public void Connect_WithFirstParameterGreaterThanCount_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentOutOfRangeException>(static () => new Network(8).Connect(9, 1));
  }

  [Fact]
  public void Connect_WithSecondParameterGreaterThanCount_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentOutOfRangeException>(static () => new Network(8).Connect(1, 9));
  }
  
  [Fact]
  public void Connect_WithAlreadyConnectedElements_ShouldThrowException()
  {
    // Arrange
    _network.Connect(1, 2);

    // Act & Assert
    Assert.Throws<InvalidOperationException>(() => _network.Connect(1, 2));
  }

  [Fact]
  public void Query_WithZeroAsFirstParameter_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentOutOfRangeException>(static () => new Network(8).Query(0, 1));
  }

  [Fact]
  public void Query_WithZeroAsSecondParameter_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentOutOfRangeException>(static () => new Network(8).Query(1, 0));
  }

  [Fact]
  public void Query_WithNegativeFirstParameter_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentOutOfRangeException>(static () => new Network(8).Query(-1, 1));
  }

  [Fact]
  public void Query_WithNegativeSecondParameter_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentOutOfRangeException>(static () => new Network(8).Query(1, -1));
  }

  [Fact]
  public void Query_WithEqualParameters_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentException>(static () => new Network(8).Query(1, 1));
  }

  [Fact]
  public void Query_WithFirstParameterGreaterThanCount_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentOutOfRangeException>(static () => new Network(8).Query(9, 1));
  }

  [Fact]
  public void Query_WithSecondParameterGreaterThanCount_ShouldThrowException()
  {
    // Act & Assert
    Assert
      .Throws<ArgumentOutOfRangeException>(static () => new Network(8).Query(1, 9));
  }

  [Fact]
  public void Query_WithValidParameters_ShouldReturnTrue()
  {
    // Arrange
    _network.Connect(1, 2);
    _network.Connect(2, 6);
    _network.Connect(2, 4);
    _network.Connect(5, 8);
    _network.Connect(4, 7);
    _network.Connect(3, 7);

    // Act & Assert
    Assert.True(_network.Query(1, 4));
    Assert.True(_network.Query(5, 8));
    Assert.True(_network.Query(7, 3));
    Assert.True(_network.Query(3, 1));
  }

  [Fact]
  public void Query_WithNotConnectedElements_ShouldReturnFalse()
  {
    // Arrange
    _network.Connect(1, 2);
    _network.Connect(2, 6);
    _network.Connect(2, 4);
    _network.Connect(5, 8);
    _network.Connect(4, 7);
    _network.Connect(3, 7);

    // Act & Assert
    Assert.False(_network.Query(1, 8));
    Assert.False(_network.Query(5, 7));
    Assert.False(_network.Query(8, 2));
    Assert.False(_network.Query(5, 3));

  }
}
