namespace Lesson2.Work
{
    public class MonthWorker : Worker
    {
        public MonthWorker(string name, int payment) : base(name, payment)
        {
        }

        public override float MonthPay()
        {
            return _payment;
        }
    }
}