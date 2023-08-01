using System.IO;

namespace AdventOfCode2021
{
    class Writer
    {
        public static readonly Writer Default = new();

        public void WriteOutput(string outputString, string path)
        {
            path = "Output_" + path;
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(outputString);
            }
        }
    }
}
