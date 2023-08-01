using System;
using System.ComponentModel;
using TypeConverterNamespace;

namespace TypeConverter
{
    [TypeConverter(typeof(TypeConverterNew))]
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
