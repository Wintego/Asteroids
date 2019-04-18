using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01
{
    public delegate KeyValuePair<string, int> pair();
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(1);
            list.Add(4);
            list.Add(4);
            foreach (var item in list.Distinct())
            {
                Console.WriteLine($"{item} - {list.Where(val => val == item).Count()} раз");
            }
            Console.WriteLine();
            foreach (var x in list.GroupBy(x => x))
                Console.WriteLine($"{x.Key} - {x.Count()} раз");           

            Task3();
            Console.ReadKey();
        }
        static void Task3()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
  {
    {"four",4 },
    {"two",2 },
    { "one",1 },
    {"three",3 },
  };
            var a = dict.OrderBy(pair => pair.Value);
            foreach (var pair in a) Console.WriteLine("{0} - {1}", pair.Key, pair.Value);   		

        }
    }
}
