using DataAccessLayer.Models;
using System;
using System.Collections;

namespace TestingLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var date = new ArrayList();
            date.Add("11-11-2022");
            date.Add("05-06-2025");

            var amount = new ArrayList();
            amount.Add("50");
            amount.Add("20");

            var info = new ArrayList();
            info.Add("wallet");
            info.Add("bank");

            var remark = new ArrayList();
            remark.Add(null);
            remark.Add("upi");

            var result = new ArrayList();
            result.Add(date);
            result.Add(amount);
            result.Add(info);
            result.Add(remark);

            int counter = 0;
            foreach(var x in result)
            {
                counter++;
            }
            //Console.WriteLine(counter);
            for(int i=0; i<counter; i++)
            {
                for(int j=0; j<i; j++)
                {
                    Console.WriteLine(result[j]);
                }
            }
        }
    }
}
