using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lesson2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
             * Соколовский Дмитрий
             */

            /*
             * Первое задание:
             * Добавить в программу коллекцию астероидов. Как только она заканчивается (все астероиды сбиты),
             * то формируется новая коллекция, в которой на 1 астероид больше.
             */
            Application.Run(new GameForm());

            /*
             * Второе задание:
             * Дана коллекция List<T>, требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции.
             * а) для целых чисел;
             * б) для обобщенной коллекции;
             * в)*используя Linq.
             */
            ListTest();

            /*
             * Третье задание:
             * Дан фрагмент программы:
             * а) Свернуть обращение к OrderBy с использованием лямбда-выражения$
             * б) *Развернуть обращение к OrderBy с использованием делегата Func<KeyValuePair<string, int>, int>.
             */
            FragmentTest();
        }

        private static void ListTest()
        {
            var list = new List<string>();
            list.Add("раз");
            list.Add("раз");
            list.Add("Два");
            list.Add("два");
            list.Add("Два");
            list.Add("три");
            list.Add("раз");

            foreach (var counter in list.GetCounters())
            {
                Console.WriteLine("{0} - {1}", counter.Key, counter.Value);
            }
        }

        private static void FragmentTest()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four", 4},
                {"two", 2},
                {"one", 1},
                {"three", 3},
            };
            Console.WriteLine("Начало:");
            PrintFragment(dict.OrderBy(delegate(KeyValuePair<string, int> pair) { return pair.Value; }));
            Console.WriteLine("Лямбда:");
            PrintFragment(dict.OrderBy(pair => pair.Value));
            Console.WriteLine("Func");
            Func<KeyValuePair<string, int>, int> func = Order;
            PrintFragment(dict.OrderBy(func));
        }

        private static int Order(KeyValuePair<string, int> arg)
        {
            return arg.Value;
        }

        private static void PrintFragment(IOrderedEnumerable<KeyValuePair<string, int>> d)
        {
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

        }
    }

    internal static class ListExtension
    {
        public static IEnumerable<KeyValuePair<T, int>> GetCounters<T>(this IEnumerable<T> list)
        {
            var enumerable = from i in list
                group i by i
                into temp
                select new KeyValuePair<T, int>(temp.Key, temp.Count());
            return enumerable;
        }

        public static IEnumerable<KeyValuePair<string, int>> GetCounters(this IEnumerable<string> list)
        {
            var enumerable = from i in list
                group i by i.ToLower()
                into temp
                select new KeyValuePair<string, int>(temp.Key, temp.Count());
            return enumerable;
        }
    }
}