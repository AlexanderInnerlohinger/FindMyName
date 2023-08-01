using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2021
{
    class Reader
    {
        public static readonly Reader Default = new();
        private readonly List<string> m_inputList = new();

        public List<string> ReadInput(string path)
        {
            StreamReader streamReader = new StreamReader(path);

            while (!streamReader.EndOfStream)
            {
                m_inputList.Add(streamReader.ReadLine());
            }

            return m_inputList;
        }
    }
}
