using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Upsinator.CustomControls
{


    public partial class DateTimeStringPicker : UserControl
    {
        public static readonly DependencyProperty DateTimeValueProperty =
            DependencyProperty.Register(nameof(Value),
                typeof(DateTime),
                typeof(DateTimeStringPicker),
                new PropertyMetadata(DateTime.MinValue));

        public DateTime Value
        {
            get
            {
                return this.CurrentDateTime;
            }
            set
            {
                this.SetValue(DateTimeStringPicker.DateTimeValueProperty, value);
            }
        }

        public string Date
        {
            get
            {
                return this.CurrentDateTime.ToString("yyyy-MM-dd");
            }
            set
            {
                DateTime output;

                if (DateTime.TryParse(value, out output))
                {
                    this.Value = DateTimeStringPicker.ConstructDateTimeFromDateAndTime(output, this.CurrentDateTime);
                }
            }
        }

        public string Time
        {
            get
            {
                return this.CurrentDateTime.ToString("HH:mm:ss");
            }
            set
            {
                // Validate the input by using the TimeSpan TryParse method
                TimeSpan output;

                if (TimeSpan.TryParse(value, out output))
                {
                    var dt = new DateTime(output.Ticks);

                    this.Value = DateTimeStringPicker.ConstructDateTimeFromDateAndTime(this.CurrentDateTime, dt);
                }
                else
                {
                    Console.WriteLine($"The input '{value}' is not a valid time.");
                }
            }
        }

        public DateTimeStringPicker()
        {
            this.InitializeComponent();

            this.RootUiElement.DataContext = this;
        }

        private DateTime CurrentDateTime
        {
            get
            {
                return (DateTime)this.GetValue(DateTimeStringPicker.DateTimeValueProperty);
            }
        }

        /// <summary>
        /// Construct a new DateTime object made from the date of one parameter and the time of another
        /// TODO: can be an extension of DateTime but for now a static here
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns>New DateTime with the date and time combined</returns>
        private static DateTime ConstructDateTimeFromDateAndTime(DateTime date, DateTime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
        }
    }
}
