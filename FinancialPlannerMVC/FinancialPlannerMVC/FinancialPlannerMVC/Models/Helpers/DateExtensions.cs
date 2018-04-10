using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace FinancialPlannerMVC.Models.Helpers
{
    public static class DateExtensions
    {
        // does the date selected happen on the same day as the target date
        public static bool IsInDaySpan(this DateTimeOffset dt, DateTimeOffset targetDate)
        {
            DateTimeOffset targetStart = targetDate.DayStart();
            TimeSpan targetSpan = targetDate.GetDaySpan();

            if (dt >= targetStart && dt <= targetStart + targetSpan) return true;
            return false;
        }

        // does the date selected happen in the same week as the target date
        public static bool IsInWeekSpan(this DateTimeOffset dt, DateTimeOffset targetDate)
        {
            DateTimeOffset targetStart = targetDate.FirstDayInWeek();
            TimeSpan targetSpan = targetDate.GetMonthSpan();

            if (dt >= targetStart && dt <= targetStart + targetSpan) return true;
            return false;
        }

        // does the date selected happen in the same week as the target date
        public static bool IsInMonthSpan(this DateTimeOffset dt, DateTimeOffset targetDate)
        {
            DateTimeOffset targetStart = targetDate.FirstDayInMonth();
            TimeSpan targetSpan = targetDate.GetMonthSpan();

            if (dt >= targetStart && dt <= targetStart + targetSpan) return true;
            return false;
        }

        // does the date selected happen in the same week as the target date
        public static bool IsInYearSpan(this DateTimeOffset dt, DateTimeOffset targetDate)
        {
            DateTimeOffset targetStart = targetDate.FirstDayOfYear();
            TimeSpan targetSpan = targetDate.GetYearSpan();

            if (dt >= targetStart && dt <= targetStart + targetSpan) return true;
            return false;
        }

        public static DateTimeOffset DayStart (this DateTimeOffset dt)
        {
            string firstMomentStr = dt.Year + "-" + dt.Month + "-"+dt.Day+" 00:00:00";
            var resultDt = new DateTimeOffset(DateTime.Parse(firstMomentStr)).AddMilliseconds(1);

            return resultDt;
        }

        public static DateTimeOffset DayEnd (this DateTimeOffset dt)
        {
            return dt.DayStart().AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(998);
        }

        public static TimeSpan GetDaySpan(this DateTimeOffset dt)
        {
            return dt.DayEnd() - dt.DayStart();
        }

        //based on the paramater date, sets the start of Sunday/the week to be 12:01 AM
        public static DateTimeOffset FirstDayInWeek(this DateTimeOffset dt)
        {
            while (dt.DayOfWeek != Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                dt = dt.AddDays(-1);

            dt = dt.AddMilliseconds(1) - dt.TimeOfDay;

            return dt;
        }

        //based on the parameter date, sets the end of the week/day to be 11:59 PM
        public static DateTimeOffset LastDayInWeek(this DateTimeOffset dt)
        {
            return FirstDayInWeek(dt).AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
        }

        public static TimeSpan GetWeekSpan(this DateTimeOffset dt)
        {
            return LastDayInWeek(dt) - FirstDayInWeek(dt);
        }

        public static DateTimeOffset FirstDayInMonth(this DateTimeOffset dt)
        {
            string firstMonthDayStr = dt.Year + "-" + dt.Month + "-01 00:00:00";

            var resultDt = new DateTimeOffset(DateTime.Parse(firstMonthDayStr)).AddMilliseconds(1);
            return resultDt;
        }

        public static DateTimeOffset LastDayInMonth(this DateTimeOffset dt)
        {
            int daysToAdd = 0;
            switch (dt.Month)
            {
                case 1:
                    daysToAdd = 31;
                    break;
                case 2:
                    daysToAdd = dt.Year % 4 == 0 ? 29 : 28;
                    break;
                case 3:
                    daysToAdd = 31;
                    break;
                case 4:
                    daysToAdd = 30;
                    break;
                case 5:
                    daysToAdd = 31;
                    break;
                case 6:
                    daysToAdd = 30;
                    break;
                case 7:
                    daysToAdd = 31;
                    break;
                case 8:
                    daysToAdd = 31;
                    break;
                case 9:
                    daysToAdd = 30;
                    break;
                case 10:
                    daysToAdd = 31;
                    break;
                case 11:
                    daysToAdd = 30;
                    break;
                case 12:
                    daysToAdd = 31;
                    break;
            }
            var resultDt = dt.FirstDayInMonth().AddDays(daysToAdd).AddMilliseconds(-2);
            return resultDt;
        }

        public static TimeSpan GetMonthSpan(this DateTimeOffset dt)
        {
            return dt.LastDayInMonth() - dt.FirstDayInMonth();
        }

        public static DateTimeOffset FirstDayOfYear(this DateTimeOffset dt)
        {
            var resultDt = new DateTimeOffset(DateTime.Parse("0001-01-01 00:00:00"));

            resultDt = resultDt.AddYears(dt.Year - 1).AddMilliseconds(1);

            return resultDt;
        }

        public static DateTimeOffset LastDayOfYear(this DateTimeOffset dt)
        {
            var resultDt = dt.Year%4==0?dt.FirstDayOfYear().AddYears(1).AddDays(1).AddMilliseconds(-2):dt.FirstDayOfYear().AddYears(1).AddMilliseconds(-2);
            return resultDt;
        }

        public static TimeSpan GetYearSpan(this DateTimeOffset dt)
        {
            return dt.LastDayOfYear() - dt.FirstDayOfYear();
        }
    }
}