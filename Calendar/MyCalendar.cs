using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class MyCalendar
    {
        public Dictionary<DayOfWeek, List<int>> Calendar;
        public DateTime Date;


        public MyCalendar(DateTime date)
        {
            this.Calendar = new Dictionary<DayOfWeek, List<int>>();
            for (var day = DayOfWeek.Monday; day <= DayOfWeek.Saturday; day++)
                Calendar.Add(day, new List<int>());
            Calendar.Add(DayOfWeek.Sunday, new List<int>());
            Date = date;

            FillMonth();
        }


        private void FillMonth()
        {
            var dates = Enumerable.Range(1, DateTime.DaysInMonth(Date.Year, Date.Month)).Select(day => new DateTime(Date.Year, Date.Month, day)).ToArray();
            foreach (var date in dates)
            {
                Calendar[date.DayOfWeek].Add(date.Day);
            }
        }


        public DateTime FirstDay()
        {
            return new DateTime(Date.Year, Date.Month, 1);
        }



        public DateTime LastDay()
        {
            return new DateTime(Date.Year, Date.Month, DateTime.DaysInMonth(Date.Year, Date.Month));
        }


       
    }
}
