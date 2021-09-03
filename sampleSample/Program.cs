using System.Diagnostics;

Console.Write("Введите путь к файлу: ");
var path = Console.ReadLine();

var stopWatch = new Stopwatch();

stopWatch.Start();

var textFile = File.ReadAllText(path);

var words = textFile
    .Split("\n")
    .ToList<string>();

var triplets = new List<string>();

foreach (var word in words)
{
    var i = 0;
    while (word.Length - i > 3)
    {
        var tmpTriplet = "";
        tmpTriplet += (word[i].ToString());
        i++;
        tmpTriplet += (word[i].ToString());
        i++;
        tmpTriplet += (word[i].ToString());
        i++;
        triplets.Add(tmpTriplet);
    }
}

var groupedTriplets = triplets.GroupBy(x => x);

var tripletCollection = new List<TripletObject>();

foreach (var k in groupedTriplets)
{
    tripletCollection.Add(new TripletObject { Name = k.Key, Rating = k.Count() });
}

var sortedTripletCollection = tripletCollection
    .AsParallel()
    .OrderBy(x => x.Rating)
    .Reverse()
    .ToList();

Console.WriteLine("Топ 10");
Console.ForegroundColor = ConsoleColor.Blue;
for (int i = 0; i < 10; i++)
{
    Console.Write($"{sortedTripletCollection[i].Name}: {sortedTripletCollection[i].Rating}, ");
}
Console.ResetColor();

stopWatch.Stop();

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"\nВыполнено за {stopWatch.ElapsedMilliseconds} мс");
Console.ResetColor();

Console.ReadLine();

class TripletObject
{
    public string Name { get; set; }
    public int Rating { get; set; }
}