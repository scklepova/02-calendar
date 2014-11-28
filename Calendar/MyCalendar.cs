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

            Bitmap bmp = new Bitmap(800, 800);
            var width = 560;

            Graphics g = Graphics.FromImage(bmp);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //g.DrawString("yourText", new Font("Tahoma", 32), Brushes.Black, rectf);

            //g.Flush();
            g.FillRectangle(new SolidBrush(Color.FromArgb(8, 108, 179)), 0, 0, 800, 800);

            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            
            RectangleF thisDate = new RectangleF(120, 20, width, 140);
            g.FillRectangle(new SolidBrush(Color.FromArgb(3, 201, 169)), thisDate);
            g.DrawString(Date.Day.ToString(), new Font("Helvetica", 60), Brushes.White, thisDate, stringFormat);

            var thisMonth = new RectangleF(120, 160, width, 80);
            g.FillRectangle(Brushes.White, thisMonth);
            g.DrawString(Date.ToString("MMMM", CultureInfo.GetCultureInfo("en-us")) + ", " + Date.Year.ToString(), new Font("Helvetica", 30), Brushes.Turquoise, thisMonth, stringFormat);


            int col = 0;
           
            for (var dayOfWeek = DayOfWeek.Monday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
            {
                var thisDays = new RectangleF(120 + col * 80, 240, 80, 80);
                g.FillRectangle(Brushes.WhiteSmoke, thisDays);
                g.DrawString(dayOfWeek.ToString().Substring(0, 3), new Font("Helvetica", 20), Brushes.Turquoise, thisDays, stringFormat);
                col++;
            }

            var sunday  = new RectangleF(120 + col * 80, 240, 80, 80);
            g.FillRectangle(Brushes.WhiteSmoke, sunday);
            g.DrawString("Sun", new Font("Helvetica", 20), Brushes.Turquoise, sunday, stringFormat);
            //col++;


            var row = 0;
            col = 0;
            foreach (var dayOfWeek in Calendar.Keys)
            {
                row = 0;
                //var view = dayOfWeek.ToString().ToLower().Substring(0, 3);
                if (Calendar[dayOfWeek].First() > 1 &&
                    ((DayOfWeek) dayOfWeek < FirstDay().DayOfWeek && (DayOfWeek) dayOfWeek != DayOfWeek.Sunday ||
                     FirstDay().DayOfWeek == DayOfWeek.Sunday))
                {
                    //view += "     ";
                    createDay(row, col, "", g, stringFormat, Brushes.WhiteSmoke);
                    row++;

                }
                    

                
                foreach (var day in Calendar[dayOfWeek])
                {
                    var viewDay = day.ToString();
                    if (viewDay.Length == 1)
                        viewDay = " " + viewDay;

                    if (day == Date.Day)
                        viewDay = " [" + viewDay + "]";
                    else
                        viewDay = "  " + viewDay + " ";

              
                    createDay(row, col, day.ToString(), g, stringFormat, day == Date.Day ? new SolidBrush(Color.FromArgb(225, 225, 225)) : Brushes.WhiteSmoke);
                    //view += viewDay;
                    row++;

                }

                

//                if (view.EndsWith(" "))
//                    view = view.Substring(0, view.Length - 1);

                //Console.WriteLine(view);
                //g.DrawString("yourText", new Font("Tahoma", 32), Brushes.Black, rectf);

                

                if (LastDay().DayOfWeek != DayOfWeek.Sunday && Calendar[dayOfWeek].Last() < LastDay().Day &&
                    ((DayOfWeek) dayOfWeek > LastDay().DayOfWeek || (DayOfWeek) dayOfWeek == DayOfWeek.Sunday))
                {
                    createDay(row, col, "", g, stringFormat, Brushes.WhiteSmoke);
                }
                    //view += "     ";

                col++;
            }

            Console.WriteLine(LastDay().DayOfWeek);

            bmp.Save("calendar.png");
        }

        static void createDay(int row, int col, string text, Graphics g, StringFormat stringFormat, Brush background)
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
