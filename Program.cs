using MyTest;

var network = new Network(8);

network.Connect(1, 2);
network.Connect(2, 6);
network.Connect(2, 4);
network.Connect(5, 8);
network.Connect(4, 7);
network.Connect(3, 7);

Console.WriteLine(network.Query(1, 4));
Console.WriteLine(network.Query(5, 6));
Console.WriteLine(network.Query(7, 1));
Console.WriteLine(network.Query(3, 1));
