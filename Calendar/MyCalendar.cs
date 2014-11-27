using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class MyCalendar
    {
        Dictionary<Enum, List<int>> Calendar;
        static DateTime Date;

        public MyCalendar(DateTime date)
        {
            this.Calendar = new Dictionary<Enum, List<int>>();
            for (var day = DayOfWeek.Monday; day <= DayOfWeek.Saturday; day++)
                Calendar.Add(day, new List<int>());
            Calendar.Add(DayOfWeek.Sunday, new List<int>());
            Date = date;

            FillMonth();
        }

        void FillMonth()
        {
            for (var day = 1; day <= DateTime.DaysInMonth(Date.Year, Date.Month); day++)
            {
                var date = new DateTime(Date.Year, Date.Month, day);
                Calendar[date.DayOfWeek].Add(day);
            }
        }


        public void ShowCalendar()
        {

            foreach (var dayOfWeek in Calendar.Keys)
            {
                var view = dayOfWeek.ToString().ToLower().Substring(0, 3);
                if (Calendar[dayOfWeek].First() > 1 && ((DayOfWeek)dayOfWeek < FirstDay().DayOfWeek && (DayOfWeek)dayOfWeek != DayOfWeek.Sunday || FirstDay().DayOfWeek == DayOfWeek.Sunday))
                    view += "     ";
                foreach (var day in Calendar[dayOfWeek])
                {
                    var viewDay = day.ToString();
                    if (viewDay.Length == 1)
                        viewDay = " " + viewDay;

                    if (day == Date.Day)
                        viewDay = " [" + viewDay + "]";
                    else
                        viewDay = "  " + viewDay + " ";

                    view += viewDay;
                }


                if (view.EndsWith(" "))
                    view = view.Substring(0, view.Length - 1);

                Console.WriteLine(view);
            }
        }

        static DateTime FirstDay()
        {
            return new DateTime(Date.Year, Date.Month, 1);
        }

        static DateTime LastDay()
        {
            return new DateTime(Date.Year, Date.Month, DateTime.DaysInMonth(Date.Year, Date.Month));
        }


       
    }
}
