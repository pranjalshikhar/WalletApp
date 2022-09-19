using System;
using System.Collections;

namespace TestingLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new ArrayList();
            list.Add(true);
            list.Add("Success");

            var status = list[0];
            var message = list[1];
            Console.WriteLine(status);
            Console.WriteLine(message);
        }
    }
}
