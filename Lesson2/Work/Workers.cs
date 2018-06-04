using System;
using System.Collections;

namespace Lesson2.Work
{
    public class Workers : IEnumerable
    {
        private Worker[] _workers;


        public Workers()
        {
            var names = new string[]
            {
                "Иванов Иван Иванович",
                "Иванов Иван Иванович",
                "Иванова Елена Дмитриевна",
                "Петров Иван Сергеевич",
                "Русаков Юрий Ильич",
                "Авдеев Аркадий Федорович",
                "Кисилев Иннокентий Ильич",
                "Ситников Яков Ильич",
                "Сазонов Иван Петрович",
                "Константинова Марина Ярославовна",
                "Кудрявцева Людмила Егоровна"
            };

            _workers = new Worker[40];
            var random = new Random();
            for (int i = 0; i < _workers.Length / 2; i++)
            {
                _workers[i] = new MonthWorker(names[i%names.Length], random.Next(40000, 60000));
            }
            for (int i = _workers.Length / 2; i < _workers.Length; i++)
            {
                _workers[i] = new HourWorker(names[i%names.Length], random.Next(400, 600));
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Worker worker in _workers)
            {
                yield return worker;
            }
        }

        public Worker this[int i]
        {
            get { return _workers[i]; }
        }

        public void Sort()
        {
            Array.Sort(_workers);
        }
    }
}