public class DateOperations
{
		/// <summary>
        /// Method to produce all the year folder that the report have been run
        /// </summary>
        /// <returns>A list of integers to bind</returns>
        private List<int> Years()
        {
            List<int> _years = new List<int>();
            int _currentYear = DateTime.Now.Year;

            //if the year is 2008 then just add that to the collection, as thats when the automated reports
            //will start
            if (_currentYear == _startYear)
            {
                _years.Add(_currentYear);
            }
            else
            {
                int _yearToAdd = _currentYear;
                //populate the collection with all years from the current one to the start of the reports
                //being run
                while (_yearToAdd > (_startYear - 1))
                {
                    _years.Add(_yearToAdd);
                    _yearToAdd = _yearToAdd - 1;
                }
                //reverse the order so they show in chronological order
                _years.Reverse();
            }

            return _years;
        }

        /// <summary>
        /// Method to produce months
        /// </summary>
        /// <returns>A list of month names as a collection of strings</returns>
        private List<int> Months()
        {
            List<int> _months = new List<int>();

            for (int i = 1; i < 13; i++)
            {
                //call the only simple way of getting the month name i know of without making your own
                //enumeration and to add to the list
                _months.Add(i);
            }

            return _months;
        }

        /// <summary>
        /// Method to return the weeks in a month
        /// </summary>
        /// <param name="Month">Integer value for the month</param>
        /// <param name="Year">Integer value for the year</param>
        /// <returns>A list of weeks for the year and month</returns>
        private List<int> Weeks(int Month, int Year)
        {
            List<int> _weeks = new List<int>();

            int _weekNo;

            //get all the full weeks in the months
            for (int i = 0; i < FullWeeksInMonth(Year, Month); i++)
            {
                _weeks.Add(i + 1);
                _weekNo++;
            }

            //now get the partial weeks
            for (int i = 0; i < PartialWeeksInMonth(Year, Month); i++)
            {
                _weeks.Add(_weekNo);
            }

            return _weeks;
        }

        /// <summary>
        /// Find the 1st Monday in a given month
        /// </summary>
        /// <param name="Year">Integer value for the Year</param>
        /// <param name="Month">Integer value for the Month</param>
        /// <param name="Day">Integer value for the Day</param>
        /// <returns>The day (1-7) that is the 1st Monday in the month in the given year</returns>
        private int FirstMonday(int Year, int Month, int Day)
        {
            DateTime FirstMonday = new DateTime(Year, Month, Day);
            while (!(FirstMonday.DayOfWeek == DayOfWeek.Monday))
            {
                FirstMonday = FirstMonday.AddDays(1);
            }
            return FirstMonday.Day;
        }

        /// <summary>
        /// Find the last Sunday in the given month
        /// </summary>
        /// <param name="Year">Integer value for the Year</param>
        /// <param name="Month">Integer value for the Month</param>
        /// <returns>The day that is the last Sunday in the month in the given year</returns>
        private int LastSunday(int Year, int Month)
        {
            int LastDay = DateTime.DaysInMonth(Year, Month);

            DateTime LastSunday = new DateTime(Year, Month, LastDay);
            while (!(LastSunday.DayOfWeek == DayOfWeek.Sunday))
            {
                LastSunday = LastSunday.AddDays(-1);
            }
            return LastSunday.Day;
        }

        /// <summary>
        /// Finds how many Full weeks are in a given month
        /// </summary>
        /// <param name="Year">Integer value for the Year</param>
        /// <param name="Month">Integer value for the Month</param>
        /// <returns>The number of full weeks in a month in the given year</returns>
        private int FullWeeksInMonth(int Year, int Month)
        {
            return ((LastSunday(Year, Month) - FirstMonday(Year, Month, 1)) / 7) + 1;
        }

        private int PartialWeeksInMonth(int Year, int Month)
        {
            int _partialWeeks;
            DateTime _monthStart = new DateTime(Year, Month, 1);
            DateTime _monthEnd = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
            if (_monthStart.DayOfWeek != DayOfWeek.Monday)
            {
                _partialWeeks += 1;
            }
            if (_monthEnd.DayOfWeek != DayOfWeek.Sunday)
            {
                _partialWeeks += 1;
            }
        }

        /// <summary>
        /// Get the start and end date for a given week in a month and year
        /// </summary>
        /// <param name="Year">Integer value for the Year</param>
        /// <param name="Month">Integer value for the Month</param>
        /// <param name="Week">Integer value for the Week</param>
        /// <returns>Integer array with the start date and end date of the week</returns>
        private int[] WeekStartEnd(int Year, int Month, int Week)
        {
            int[] _weekSpan = new int[2];
            switch (Week)
            {
                case 1:
                    DateTime _weekStart = new DateTime(Year, Month, 1);

                    //_weekSpan[0] = FirstMonday(Year, Month, 1);
                    break;
                case 2:
                    _weekSpan[0] = FirstMonday(Year, Month, 8);
                    break;
                case 3:
                    _weekSpan[0] = FirstMonday(Year, Month, 15);
                    break;
                case 4:
                    _weekSpan[0] = FirstMonday(Year, Month, 22);
                    break;
                case 5:
                    _weekSpan[0] = FirstMonday(Year, Month, 29);
                    break;
            }

            _weekSpan[1] = _weekSpan[0] + 6;

            return _weekSpan;
        }
		/// <summary>
        /// Method to return the weeks in a month
        /// </summary>
        /// <param name="Month">Integer value for the month</param>
        /// <param name="Year">Integer value for the year</param>
        /// <returns>A list of weeks for the year and month</returns>
        private List<int> Weeks(int Month, int Year)
        {
            List<int> _weeks = new List<int>();

            //Create a Totals array one Int node for each full week
            ArrayList Totals = new ArrayList(FullWeeksInMonth(Year, Month));
            for (int i = 0; i < FullWeeksInMonth(Year, Month); i++)
            {
                Totals.Add(new int());
            }
 
            //Itterate over each days of the full weeks
            for (int i = FirstMonday(Year, Month); i <= LastSunday(Year, Month); i++)
            {
                DateTime CurrentDay = new DateTime(Year, Month, i);
                int CurrentWeekNumber = WeekNumber(CurrentDay);
                int CurrentWeekTotal = (int)Totals[CurrentWeekNumber-1];
                CurrentWeekTotal += GetTotalForDay(CurrentDay);
                Totals[CurrentWeekNumber - 1] = CurrentWeekTotal;
 
            }
 
            //now we write out the data of our Totals List to the dictionary object
 
            for (int i = 0; i < Totals.Count; i++)
            {
                _weeks.Add(i + 1, Totals[i].ToString());
            }

            return _weeks;
        }
 
        private int GetTotalForDay(DateTime CurrentDay)
        {
            int outValue = 0;
            //Get Total for CurrentDay
            return outValue;
        }
 
        private int FirstMonday(int Year, int Month)
        {
            DateTime FirstMonday = new DateTime(Year, Month, 1);
            while (!(FirstMonday.DayOfWeek == DayOfWeek.Monday))
            {
                FirstMonday = FirstMonday.AddDays(1);
            }
            return FirstMonday.Day;
        }
 
        private int LastSunday(int Year, int Month)
        {
            int LastDay = DateTime.DaysInMonth(Year, Month);
 
            DateTime LastSunday = new DateTime(Year, Month, LastDay);
            while (!(LastSunday.DayOfWeek == DayOfWeek.Sunday))
            {
                LastSunday = LastSunday.AddDays(-1);
            }
            return LastSunday.Day;
        }
 
        private int WeekNumber(DateTime theDate)
        {
            int DaysAfterFirstMonday = theDate.Day - FirstMonday(theDate.Year,theDate.Month);
 
            return (DaysAfterFirstMonday / 7)+1;
        }
 
        private int FullWeeksInMonth(int Year, int Month)
        {
            return ((LastSunday(Year, Month) - FirstMonday(Year, Month)) / 7) + 1;
        }
		
		public DateTime RoundUp(this DateTime dateTime, TimeSpan timeSpan)
		{
			return new DateTime(((dateTime.Ticks + timeSpan.Ticks) / timeSpan.Ticks) * timeSpan.Ticks);
		}
}