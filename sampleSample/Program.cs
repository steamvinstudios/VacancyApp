// читаем путь

using System.Diagnostics;

//var path = "C:\\Users\\Steam\\Downloads\\super.txt"; //Console.ReadLine();
Console.Write("Введите путь к файлу: ");
var path = Console.ReadLine();
// создаем бенчмарк
var stopWatch = new Stopwatch();
stopWatch.Start();
// создаем обхект с текстом из файла
var textFile = File.ReadAllText(path);
// создаем коллекцию со словами
var words = textFile.Split("\n").ToList<string>();
// создаем коллекцию триплетов
var triplets = new List<string>();
// делим слова на триплеты и добавляем в коллекцию триплетов
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

// хранение объектов триплетов
var tripletCollection = new List<TripletObject>();

foreach (var k in groupedTriplets)
{
    tripletCollection.Add(new TripletObject { Name = k.Key, Rating = k.Count() });
}

foreach (var item in tripletCollection)
{
    Console.WriteLine($"{item.Name}: {item.Rating}");
}

// отсортируем коллекцию триплетов
var sortedTripletCollection = tripletCollection
    .AsParallel()
    .OrderBy(x => x.Rating)
    .Reverse()
    .ToList();

Console.WriteLine("Отсортированная коллекция триплетов:");
foreach (var item in sortedTripletCollection)
{
    Console.WriteLine($"{item.Name}: {item.Rating}");
}

// выводим топ 10 через запятую
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

class TripletObject
{
    public string Name { get; set; }
    public int Rating { get; set; }
}