using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Task2_1(40, 10);

            //Task2_3(40, 10);

            //Task3_1();

            Console.ReadKey();
        }

        /// <summary>
        /// Задание №1 пункт 1
        /// </summary>

        static void Task2_1(int listCount, int numRange)
        {

            var list = CreateList(listCount, numRange);

            for (var i = 0; i < numRange; i++)
            {
                var numFinder = list.FindAll(e => e == i);

                Print(i, numFinder.Count);
            }
        }

        /// <summary>
        /// Задание №1 пункт 3
        /// </summary>

        static void Task2_3(int listCount, int numRange)
        {
            var list = CreateList(listCount, numRange);

            for (var i = 0; i < numRange; i++)
            {
                var numFinder = from e in list
                                where e == i
                                select e;
                Print(i, numFinder.Count());
            }
        }

        /// <summary>
        /// Условие 3-го задания.
        /// </summary>

        static void Task3()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },
            };
            
            var d = dict.OrderBy(delegate(KeyValuePair<string,int> pair) { return pair.Value; });
            
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Решение 3-го задания пункта 1.
        /// </summary>
        
        static void Task3_1()
        {
            var dict = new Dictionary<string, int>()
            {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },
            };

            var d = dict.OrderBy(pair => pair.Value);

            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }


        #region Вспомогательные методы

        /// <summary>
        /// Создание коллекции случайных чисел.
        /// </summary>
        /// <param name="listCount">Размер коллекции</param>
        /// <param name="numRange">Диапазон случайных чисел.</param>
        /// <returns>Коллекция случайных чисел.</returns>

        static List<int> CreateList(int listCount, int numRange)
        {
            var rnd = new Random();

            var Integers = new List<int>();

            Console.WriteLine("\n\nКоллекция целых чисел содержит:\n\n");

            for (var i = 0; i < listCount; i++)
            {
                Integers.Add(rnd.Next(numRange));
                Console.Write($"{Integers[i]} ");
            }

            Console.WriteLine("\n");

            return Integers;

        }

        /// <summary>
        /// Вывод количества чисел содержащихся в коллекции.
        /// </summary>
        /// <param name="num">Число, которое искали.</param>
        /// <param name="count">Количество чисел, которое искали.</param>

        static void Print(int num, int count)
        {
            var countStr = "раз";

            if ((count > 1 && count < 5) || (count % 10 > 1 && count % 10 < 5))
            {
                countStr += "а";
            }

            Console.WriteLine($"Число {num} встречается {count} {countStr}.");
        }

        #endregion

        
    }
}
