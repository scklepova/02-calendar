using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputDate = new int[3];
            inputDate = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            var date = new DateTime(inputDate[2], inputDate[1], inputDate[0]);

            var calendar = new MyCalendar(date);
            calendar.ShowCalendar();
        }
    }
}
