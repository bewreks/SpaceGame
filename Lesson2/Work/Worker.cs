using System;
using System.Collections;

namespace Lesson2.Work
{
    public abstract class Worker : IComparer, IComparable
    {
        protected string _name;
        protected float _payment;

        public Worker(string name, float payment)
        {
            _name = name;
            _payment = payment;
        }

        public void Print()
        {
            Console.WriteLine(_name + " {0:C}", MonthPay());
        }

        public int Compare(object x, object y)
        {
            var f = (Worker) x;
            var s = (Worker) y;
            var result = String.CompareOrdinal(f._name, s._name);
            if (result == 0)
            {
                result = f.MonthPay().CompareTo(s.MonthPay());
            }

            return result;
        }
        
        public int CompareTo(object obj)
        {
            return Compare(this, obj);
        }

        public abstract float MonthPay();
    }
}