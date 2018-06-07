using System.Windows.Forms;

namespace Lesson2
{
    public class Program
    {
        delegate T GenericDelegate<T>(T first, T second);

        public static void Main(string[] args)
        {
            /*
             * Соколовский Дмитрий
             */

            /*
             * Первое задание:
             * Добавить космический корабль
             */

            /*
             * Второе задание:
             * а) Добавить в игру “Астероиды” ведение журнала в консоль с помощью делегатов
             * б) и в файл.
             */

            /* Третье задание:
             * Добавить аптечки, которые добавляют энергии
             */

            /* Четвертое задание:
             * Добавить подсчет очков за сбитые астероиды
             */
            Application.Run(new GameForm());

            /* Пятое задание:
             * Добавить в пример Lesson3 обобщенный делегат
             * 
             * Так и не придумал где его в игре можно использовать
             */
            GenericDelegate<int> delegateTest = Sum;
            delegateTest(5, 6);
            delegateTest = Sub;
            delegateTest(5, 6);
        }

        private static int Sum(int first, int second)
        {
            return first + second;
        }

        private static int Sub(int first, int second)
        {
            return first - second;
        }
    }
}