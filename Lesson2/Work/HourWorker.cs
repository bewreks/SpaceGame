namespace Lesson2.Work
{
    public class HourWorker : Worker
    {
        public HourWorker(string name, int payment) : base(name, payment)
        {
        }

        public override float MonthPay()
        {
            return  20.8f * 8 * _payment;
        }
    }
}