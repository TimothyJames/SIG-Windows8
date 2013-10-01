using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DatumPrikker.UI.Common.DateTimePicker
{
    public sealed partial class DatePicker : UserControl
    {
        #region Events

        public delegate void dateChanged(DateTime newDate);
        /// <summary>
        /// Occurs when User changes the Date.
        /// </summary>
        public event dateChanged DateChanged = null;

        #endregion

        #region Properties

        #region Displayed Date
        /// <summary>
        /// Creates a Property for changing the Displayed Date through XAML
        /// </summary>
        public static readonly DependencyProperty DisplayedDateProperty =
            DependencyProperty.Register("Displayed Date", typeof(DateTime), typeof(DatePicker),
            new PropertyMetadata(DateTime.Now.Date, new PropertyChangedCallback(OnActualDateChanged)));

        /// <summary>
        /// Gets and Sets the Displayed Date
        /// </summary>
        public DateTime DisplayedDate
        {
            get { return (DateTime)GetValue(DisplayedDateProperty); }
            set { SetValue(DisplayedDateProperty, value.Date); }
        }

        /// <summary>
        /// Handles the Date-Changing through XAML
        /// </summary>
        /// <param name="d">The DatePicker Instance which has thrown this Event</param>
        /// <param name="e">The new DateTime object</param>
        private static void OnActualDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DatePicker tmpDP = (DatePicker)d;
            DateTime? tmpDT = (DateTime?)e.NewValue;

            ///Checks if both Parameters are correct
            if (tmpDP == null || tmpDT == null)
                return;

            if (tmpDP.DateChanged != null)
                tmpDP.DateChanged(tmpDP.DisplayedDate.Date);

            tmpDP.setDate(tmpDP.DisplayedDate.Date);
        }
        #endregion

        #region Template
        /// <summary>
        /// The last DateTemplate ensure that the Date is read exactly in the same way like it was written
        /// </summary>
        private String lastDateTemplate = null;
        #endregion

        #region max-min year to display
        
        /*---------------------------MaxYear----------------------------*/

        /// <summary>
        /// Creats a Property for changing the maximal Year
        /// </summary>
        public static readonly DependencyProperty MaxYearProperty =
            DependencyProperty.Register("Max Year", typeof(int), typeof(DatePicker),
            new PropertyMetadata(DateTime.Now.Year + 10));

        /// <summary>
        /// Gets and Sets the Maximal Year
        /// </summary>
        public int MaxYear
        {
            get { return (int)GetValue(MaxYearProperty); }
            set
            {
                if (value >= DateTime.Now.Year && value <= DateTime.Now.Year + 200) SetValue(MaxYearProperty, value);
                else SetValue(MaxYearProperty, DateTime.Now.Year + 10);
            }
        }


        /*---------------------------MinYear----------------------------*/

        /// <summary>
        /// Creates are Property for changing the minimal Year
        /// </summary>
        public static readonly DependencyProperty MinYearProperty =
            DependencyProperty.Register("Min Year", typeof(int), typeof(DatePicker),
            new PropertyMetadata(DateTime.Now.Year - 10));

        /// <summary>
        /// Gets and Sets the Minmal Year
        /// </summary>
        public int MinYear
        {
            get { return (int)GetValue(MinYearProperty); }
            set
            {
                if (value <= DateTime.Now.Year && value >= DateTime.Now.Year - 100) SetValue(MinYearProperty, value);
                else SetValue(MaxYearProperty, DateTime.Now.Year - 10);
            }
        }

        #endregion

        #region Day light saving time

        /*---------------------------DayLightSavingFlag------------------*/

        /// <summary>
        /// Creates are Property to Display the Daylight Saving Flag or not
        /// </summary>
        public static readonly DependencyProperty DaylightSavingFlagProperty =
            DependencyProperty.Register("Daylight Saving Flag", typeof(bool), typeof(DatePicker),
            new PropertyMetadata(true));

        /// <summary>
        /// Gets and Sets the Daylight Saving Flag
        /// </summary>
        public bool DaylightSavingFlag
        {
            get { return (bool)GetValue(DaylightSavingFlagProperty); }
            set { SetValue(DaylightSavingFlagProperty, value); }
        }

        #endregion

        #region Leap year

        /*---------------------------LeapYearFlag------------------*/

        /// <summary>
        /// Creates are Property to Display the Leap Year Flag or not
        /// </summary>
        public static readonly DependencyProperty LeapYearFlagProperty =
            DependencyProperty.Register("Leap Year Flag", typeof(bool), typeof(DatePicker),
            new PropertyMetadata(true));

        /// <summary>
        /// Gets and Sets the Leap Year Flag
        /// </summary>
        public bool LeapYearFlag
        {
            get { return (bool)GetValue(LeapYearFlagProperty); }
            set { SetValue(LeapYearFlagProperty, value); }
        }

        #endregion

        #endregion

        public DatePicker()
        {
            this.InitializeComponent();
        }

        #region setDate
        private void DatePickerLoaded(object sender, RoutedEventArgs e)
        {
            setDate(DisplayedDate);
        }
        /// <summary>
        /// setDate sets the day, month and year to the correct position like it is displayed on Windows itself
        /// </summary>
        /// <param name="time">The time to display</param>
        private void setDate(DateTime time)
        {
            ///Gets the DateTimeFormat used in Windows
            lastDateTemplate = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            ///Seperates the different Date Sections ( Year , Month , Day )
            String[] sections = lastDateTemplate.Split(new char[] { '/', '-', '.' });

            ///The For loop chooses the correct Date Section for the Comboboxes
            for (int x = 0, y = 0; x < 3; x++, y++)
            {
                switch (sections[y])
                {
                    case "M":
                        loadSection(time, (section)x, kindOfDisplay.month);
                        break;
                    case "MM":
                    case "MMM":
                        loadSection(time, (section)x, kindOfDisplay.month, true);
                        break;
                    case "d":
                        loadSection(time, (section)x, kindOfDisplay.day);
                        break;
                    case "dd":
                        loadSection(time, (section)x, kindOfDisplay.day, true);
                        break;
                    case "yyyy":
                        loadSection(time, (section)x, kindOfDisplay.year);
                        break;
                    case "yy":
                        loadSection(time, (section)x, kindOfDisplay.year, true);
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// loadSection loads on ComboBox with an specific DateSection ( Year , Month , Day )
        /// </summary>
        /// <param name="time">The time to display</param>
        /// <param name="section">The ComboBox position</param>
        /// <param name="kindOfDisplay">The DateSection to display</param>
        /// <param name="additionalZero">Display Day and Month with addition Zero (02 / 2) and the Year long or short ( 13 / 2013 )</param>
        private void loadSection(DateTime time, section section, kindOfDisplay kindOfDisplay, bool additionalZero = false)
        {
            ///For the correct ComboBox or TextBlock
            ComboBox sectionBox = null;
            TextBlock sectionText = null;
            ///ComboBox Item for adding new Items to the list
            ComboBoxItem item = null;
            ///The Selection Changed Event Handler to delete and add again
            ///so the Event is not raised while adding items and set the 
            ///selected item
            SelectionChangedEventHandler handler = null;

            ///Each section has its own ComboBox, TextBox and Selection Changed Handler
            ///The switch ensures getting the wright one
            switch (section)
            {
                case section.first:
                    sectionBox = firstSectionComboBox;
                    sectionText = firstSectionTextBlock;
                    handler = firstSectionComboBox_SelectionChanged;
                    sectionBox.SelectionChanged -= handler;
                    break;

                case section.second:
                    sectionBox = secondSectionComboBox;
                    sectionText = secondSectionTextBlock;
                    handler = secondSectionComboBox_SelectionChanged;
                    sectionBox.SelectionChanged -= handler;
                    break;

                case section.third:
                    sectionBox = thirdSectionComboBox;
                    sectionText = thirdSectionTextBlock;
                    handler = thirdSectionComboBox_SelectionChanged;
                    sectionBox.SelectionChanged -= handler;
                    break;
            }

            ///Clears the sectionBox before recreating
            sectionBox.Items.Clear();
            ///Creates different items for different Date Sections
            switch (kindOfDisplay)
            {
                case kindOfDisplay.day:
                    ///Creates the Days for the Month ( 31 , 30 , 29 , 28 )
                    for (int i = 1; i <= DateTime.DaysInMonth(time.Year, time.Month); i++)
                    {
                        item = new ComboBoxItem();

                        ///Displayes a Day with an addition Zero or not ( 8 , 08 )
                        if (additionalZero && i < 10)
                            item.Content += "0";
                        item.Content += i.ToString();

                        ///marks the correct day as selected
                        if (i == time.Day)
                            item.IsSelected = true;

                        ///Adds the item
                        sectionBox.Items.Add(item);
                    }
                    ///Adds the Day of the Week ( Monday ... )
                    sectionText.Text = time.DayOfWeek.ToString();
                    break;

                case kindOfDisplay.month:
                    ///Creats the different Month
                    for (int i = 1; i <= 12; i++)
                    {
                        item = new ComboBoxItem();

                        ///Displayes a Month with an addition Zero or not ( 8 , 08 )
                        if (additionalZero && i < 10)
                            item.Content += "0";
                        item.Content += i.ToString();

                        ///marks the correct month as selected
                        if (i == time.Month)
                            item.IsSelected = true;

                        ///Adds the item
                        sectionBox.Items.Add(item);
                    }

                    ///Adds the Month as its long form ( January )
                    sectionText.Text = time.ToString("MMM");
                    break;

                case kindOfDisplay.year:
                    ///Creates the Yearitems
                    for (int i = MinYear; i <= MaxYear; i++)
                    {
                        item = new ComboBoxItem();

                        ///Displayes a Year in two different ways ( 13 , 2013 )
                        if (additionalZero)
                            item.Content += i.ToString().Substring(3);
                        else
                            item.Content += i.ToString();

                        ///marks the correct year as selected
                        if (i == time.Year)
                            item.IsSelected = true;

                        ///Adds the item
                        sectionBox.Items.Add(item);
                    }
                    ///Clears the sectionText
                    sectionText.Text = "";

                    ///Adds the DayLightSaving Flag
                    if (DaylightSavingFlag)
                    {
                        if (time.IsDaylightSavingTime())
                            sectionText.Text += "S";
                        else
                            sectionText.Text += "N";
                    }

                    ///Adds the Leap Year Flag if it is a LeapYear
                    if (LeapYearFlag && DateTime.IsLeapYear(time.Year))
                        sectionText.Text += "  L";

                    break;
            }
            sectionBox.SelectionChanged += handler;
        }

        private enum kindOfDisplay
        {
            day,
            month,
            year
        }
        private enum section
        {
            first = 0,
            second = 1,
            third = 2
        }
        #endregion

        #region dateChanging

        private void firstSectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changeDate((sender as ComboBox).SelectedIndex, section.first);
        }

        private void secondSectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changeDate((sender as ComboBox).SelectedIndex, section.second);
        }

        private void thirdSectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changeDate((sender as ComboBox).SelectedIndex, section.third);
        }

        private void changeDate(int newIndex, section section)
        {
            String[] sections = lastDateTemplate.Split(new char[] { '/', '-', '.' });

            switch (sections[(int)section])
            {
                case "M":
                case "MM":
                    DisplayedDate = DisplayedDate.AddMonths((newIndex + 1) - DisplayedDate.Month);
                    break;
                case "d":
                case "dd":
                    DisplayedDate = DisplayedDate.AddDays((newIndex + 1) - DisplayedDate.Day);
                    break;
                case "yyyy":
                case "yy":
                    DisplayedDate = DisplayedDate.AddYears((newIndex + MinYear) - DisplayedDate.Year);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
