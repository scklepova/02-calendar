using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    class CalendarDrawer
    {

        private static int Left, Top, ImageSize, CalendarWidth, CellHeight, DateHeight, MonthHeight, CellWidth;
        private static Bitmap Bmp;
        private static Graphics G;
        private static StringFormat SFormat;


        public CalendarDrawer()
        {
            ImageSize = 800;
            Left = 120;
            Top = 20;
            CalendarWidth = 560;
            CellHeight = 80;
            MonthHeight = 80;
            DateHeight = 140;
            CellWidth = 80;

            Bmp = new Bitmap(ImageSize, ImageSize);
            G = Graphics.FromImage(Bmp);

            SFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }


        public void DrawCalendar(MyCalendar calendar)
        {
            DrawBackground();
            DrawDate(calendar);
            DrawMonth(calendar);
            DrawDaysOfWeek(calendar);
            DrawDays(calendar);
            Bmp.Save("calendar.png");
        }

        private static void DrawBackground()
        {
            G.FillRectangle(new SolidBrush(Color.FromArgb(8, 108, 179)), 0, 0, ImageSize, ImageSize);
        }


        private static void DrawDays(MyCalendar calendar)
        {            
            for (var col = 0; col < calendar.Calendar.Keys.Count; col++)
            {
                DrawAllWeekdayInMonth(calendar, calendar.Calendar.Keys.ElementAt(col), col);
            } 
        }

        private static void DrawAllWeekdayInMonth(MyCalendar calendar, DayOfWeek dayOfWeek, int col)
        {
            var row = 0;
            
            if (calendar.Calendar[dayOfWeek].First() > 1 &&
                (dayOfWeek < calendar.FirstDay().DayOfWeek && dayOfWeek != DayOfWeek.Sunday ||
                 calendar.FirstDay().DayOfWeek == DayOfWeek.Sunday))
            {
                DrawDay(row, col, "", Brushes.WhiteSmoke);
                row++;
            }


            foreach (var day in calendar.Calendar[dayOfWeek])
            {
                DrawDay(row, col, day.ToString(),
                    day == calendar.Date.Day ? new SolidBrush(Color.FromArgb(225, 225, 225)) : Brushes.WhiteSmoke);
                row++;
            }


            if (calendar.LastDay().DayOfWeek != DayOfWeek.Sunday && calendar.Calendar[dayOfWeek].Last() < calendar.LastDay().Day &&
                (dayOfWeek > calendar.LastDay().DayOfWeek || dayOfWeek == DayOfWeek.Sunday))
            {
                DrawDay(row, col, "", Brushes.WhiteSmoke);
            }
        }


        private static void DrawDay(int row, int col, string text, Brush background)
        {
            var day = new RectangleF(Left + col * CellWidth, Top + DateHeight + MonthHeight + CellHeight + row * CellHeight, CellWidth, CellHeight);
            G.FillRectangle(background, day);
            G.DrawString(text, new Font("Helvetica", 20), Brushes.Turquoise, day, SFormat);
        }


        private static void DrawDate(MyCalendar calendar)
        {
            var thisDate = new RectangleF(Left, Top, CalendarWidth, DateHeight);
            G.FillRectangle(new SolidBrush(Color.FromArgb(3, 201, 169)), thisDate);
            G.DrawString(calendar.Date.Day.ToString(), new Font("Helvetica", 60), Brushes.White, thisDate, SFormat);
        }


        private static void DrawMonth(MyCalendar calendar)
        {
            var thisMonth = new RectangleF(Left, Top + DateHeight, CalendarWidth, MonthHeight);
            G.FillRectangle(Brushes.White, thisMonth);
            G.DrawString(calendar.Date.ToString("MMMM", CultureInfo.GetCultureInfo("en-us")) + ", " + calendar.Date.Year.ToString(),
                new Font("Helvetica", 30), Brushes.Turquoise, thisMonth, SFormat);
        }


        private static void DrawDayOfWeek(int col, DayOfWeek dayOfWeek)
        {
            var thisDays = new RectangleF(Left + col * CellHeight, Top + DateHeight + MonthHeight, CellWidth, CellHeight);
            G.FillRectangle(Brushes.WhiteSmoke, thisDays);
            G.DrawString(dayOfWeek.ToString().Substring(0, 3), new Font("Helvetica", 20), Brushes.Turquoise, thisDays,
                SFormat);
        }


        private static void DrawDaysOfWeek(MyCalendar calendar)
        {
            var col = 0;
            foreach (var dayOfWeek in calendar.Calendar.Keys)
            {
                DrawDayOfWeek(col, (DayOfWeek)dayOfWeek);
                col++;
            }
        }
    }
}
