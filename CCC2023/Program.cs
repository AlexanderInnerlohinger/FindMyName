// See https://aka.ms/new-console-template for more information

string path = @"C:\Users\A.Innerlohinger\source\repos\Playground\CCC2023\obj\Debug\input_tutorial.txt";

char[,] map = GetMap(path);
List<Tuple<int, int>> coordinates = FillCoordinates(path);
List<char> treasures = FindTreasures(map, coordinates);

Console.Read();

char[,] GetMap(string path)
{
    var wholeInput = File.ReadAllLines(path);
    int sizeOfMap = Convert.ToInt32(wholeInput[0]);
    char[,] map = new char[sizeOfMap, sizeOfMap];
    
    for (int i = 0; i < sizeOfMap; i++)
    {
        int counter = 0;
        foreach (char s in wholeInput[i + 1])
        {
            map[i, counter] = s;
            counter++;
        }
    }
    return map;
}

List<Tuple<int, int>> FillCoordinates(string s)
{
    var wholeInput = File.ReadAllLines(path);
    int sizeOfMap = Convert.ToInt32(wholeInput[0]);
    int numberOfCoordinates = Convert.ToInt32(wholeInput[sizeOfMap + 1]);
    List<Tuple<int, int>> coordinates = new List<Tuple<int, int>>();

    for (int i = 0; i < numberOfCoordinates; i++)
    {
        string[] currentCoordinate = wholeInput[sizeOfMap + 2 + i].Split(',');
        Tuple<int, int> currentTuple = new Tuple<int, int>(Convert.ToInt32(currentCoordinate[0]), Convert.ToInt32(currentCoordinate[1]));
        coordinates.Add(currentTuple);
    }

    return coordinates;
}

List<char> FindTreasures(char[,] chars, List<Tuple<int, int>> tuples)
{
    List<char> treasures = new List<char>();
    
    for (int i = 0; i < tuples.Count; i++)
    {
        treasures.Add(map[tuples[i].Item2, tuples[i].Item1]);
    }

    return treasures;
}
