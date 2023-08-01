using System;
using System.Collections.Generic;

namespace DoubleTest
{
    class Program
    {
        private static double[] array = new double[]
        {
            0.1, 0.2, 0.5, 1, 2, 3, 5, 10
        };

    static void Main(string[] args)
        {
            //foreach (var element in array)
            //{
            //    DoubleinBinaereundHexa(element);
            //}
            
            //Console.ReadLine();

            foreach (var element in array)
            {
                long m = BitConverter.DoubleToInt64Bits(element); // 将 double 转成 Int64
                string str = Convert.ToString(m, 2); // 将 Int64 转成二进制字符串
                Console.WriteLine(str);

                long n = Convert.ToInt64(str, 2); // 将二进制字符串转成 Int64
                double x = BitConverter.Int64BitsToDouble(n); // 将 Int64 转成 double
                Console.WriteLine(x);

            }

            string s = "Hello; World";

            s.Split(';', StringSplitOptions.RemoveEmptyEntries);

            //double i = 26.35;
            //long m = BitConverter.DoubleToInt64Bits(i); // 将 double 转成 Int64
            //string str = Convert.ToString(m, 2); // 将 Int64 转成二进制字符串
            //Console.WriteLine(str);

            //long n = Convert.ToInt64(str, 2); // 将二进制字符串转成 Int64
            //double x = BitConverter.Int64BitsToDouble(n); // 将 Int64 转成 double
            //Console.WriteLine(x);

            Console.ReadLine();
        }

    public static void DoubleinBinaereundHexa(double wert)
    {
        int bitCount = sizeof(double) * 8;
        char[] result = new char[bitCount];

        //long lgValue = BitConverter.ToInt64(BitConverter.GetBytes(wert), 0);

        // split the conversion into two operations
        Byte[] bytes = BitConverter.GetBytes(wert);
        // show each byte
        foreach (Byte b in bytes)
        {
            Console.WriteLine(Convert.ToString(b, 2).PadLeft(8, '0'));
        }

        long lgValue = BitConverter.ToInt64(bytes, 0);

        for (int bit = 0; bit < bitCount; ++bit)
        {
            // show each mask
            Console.WriteLine(Convert.ToString((1 << bit), 2).PadLeft(64, '0'));

            long maskwert = lgValue & (1 << bit);
            if (maskwert > 0)
            {
                maskwert = 1;
            }
            result[bitCount - bit - 1] = maskwert.ToString()[0];
        }
        Console.WriteLine("\n\nBinaere Darstellung:");

        for (int i = 0; i < 64; i++)
        {

            if (i % 4 == 0)
                Console.Write(" ");
            if (result[i] == '-')
            {
                result[i] = '1';
            }
            Console.Write(result[i]);
        }
    }
    }
}
