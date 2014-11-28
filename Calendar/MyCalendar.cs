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

            const int imageSize = 800;
            var bmp = new Bitmap(imageSize, imageSize);
            const int widthCalendar = 560;

            var g = Graphics.FromImage(bmp);
            g.FillRectangle(new SolidBrush(Color.FromArgb(8, 108, 179)), 0, 0, imageSize, imageSize);

            var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            CreateDate(widthCalendar, g, stringFormat);
            CreateMonth(widthCalendar, g, stringFormat);
            CreateDaysOfWeek(g, stringFormat);
            CreateDays(g, stringFormat);
            bmp.Save("calendar.png");
        }

        private void CreateDays(Graphics g, StringFormat stringFormat)
        {    
            var col = 0;
            foreach (var dayOfWeek in Calendar.Keys)
            {
                var row = 0;
                if (Calendar[dayOfWeek].First() > 1 &&
                    ((DayOfWeek) dayOfWeek < FirstDay().DayOfWeek && (DayOfWeek) dayOfWeek != DayOfWeek.Sunday ||
                     FirstDay().DayOfWeek == DayOfWeek.Sunday))
                {
                    CreateDay(row, col, "", g, stringFormat, Brushes.WhiteSmoke);
                    row++;
                }


                foreach (var day in Calendar[dayOfWeek])
                {
                    CreateDay(row, col, day.ToString(), g, stringFormat,
                        day == Date.Day ? new SolidBrush(Color.FromArgb(225, 225, 225)) : Brushes.WhiteSmoke);
                    row++;
                }


                if (LastDay().DayOfWeek != DayOfWeek.Sunday && Calendar[dayOfWeek].Last() < LastDay().Day &&
                    ((DayOfWeek) dayOfWeek > LastDay().DayOfWeek || (DayOfWeek) dayOfWeek == DayOfWeek.Sunday))
                {
                    CreateDay(row, col, "", g, stringFormat, Brushes.WhiteSmoke);
                }
                col++;
            }
        }

        private static void CreateDaysOfWeek(Graphics g, StringFormat stringFormat)
        {
            var col = 0;
            for (var dayOfWeek = DayOfWeek.Monday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
            {
                CreateDayOfWeek(g, stringFormat, col, dayOfWeek);
                col++;
            }
            CreateDayOfWeek(g, stringFormat, col, DayOfWeek.Sunday);
        }


        private static void CreateDayOfWeek(Graphics g, StringFormat stringFormat, int col, DayOfWeek dayOfWeek)
        {
            var thisDays = new RectangleF(120 + col*80, 240, 80, 80);
            g.FillRectangle(Brushes.WhiteSmoke, thisDays);
            g.DrawString(dayOfWeek.ToString().Substring(0, 3), new Font("Helvetica", 20), Brushes.Turquoise, thisDays,
                stringFormat);
        }

        private static void CreateMonth(int widthCalendar, Graphics g, StringFormat stringFormat)
        {
            var thisMonth = new RectangleF(120, 160, widthCalendar, 80);
            g.FillRectangle(Brushes.White, thisMonth);
            g.DrawString(Date.ToString("MMMM", CultureInfo.GetCultureInfo("en-us")) + ", " + Date.Year.ToString(),
                new Font("Helvetica", 30), Brushes.Turquoise, thisMonth, stringFormat);
        }

        private static void CreateDate(int widthCalendar, Graphics g, StringFormat stringFormat)
        {
            var thisDate = new RectangleF(120, 20, widthCalendar, 140);
            g.FillRectangle(new SolidBrush(Color.FromArgb(3, 201, 169)), thisDate);
            g.DrawString(Date.Day.ToString(), new Font("Helvetica", 60), Brushes.White, thisDate, stringFormat);
        }

        static void CreateDay(int row, int col, string text, Graphics g, StringFormat stringFormat, Brush background)
        {
            var day = new RectangleF(120 + col * 80, 320 + row * 80, 80, 80);
            g.FillRectangle(background, day);
            g.DrawString(text, new Font("Helvetica", 20), Brushes.Turquoise, day, stringFormat );
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
