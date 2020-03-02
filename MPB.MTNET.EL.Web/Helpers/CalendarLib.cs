using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MPB.MTNET.EL.Web.Helpers
{
    public class CalendarLib
    {
        public CalendarLib() { }
      
    }
    public enum CalendarFun : int
    {
        /// <summary>前</summary>
        Before = 0,
        /// <summary>前(含當日)</summary>
        BeforeThisDay = 1,
        /// <summary>後</summary>
        After = 2,
        /// <summary>後(含當日)</summary>
        AfterThisDay = 3,
    }
    public enum CalendarType : int
    {
        /// <summary>來源：日曆日||回傳：日曆日</summary>
        CalendarToCalendar = 1,
        /// <summary>來源：日曆日||回傳：工作日(回傳前一日)</summary>
        CalendarToWorkdaysForBefore = 2,
        /// <summary>來源：日曆日||回傳：工作日(回傳後一日)</summary>
        CalendarToWorkdaysForAfter = 3,
        /// <summary>來源：工作日||回傳：工作日</summary>
        WorkdaysToWorkdays = 4,
    }
    public interface ICalendar
    {
        DateTime GetDate();
    }


    public class CalendarDoNet : ICalendar
    {

        private int days { set; get; }
        private CalendarType calendarType { set; get; }
        private CalendarFun calendarFun { set; get; }
        private DateTime? dtBase { set; get; }


        public CalendarDoNet(DateTime? dtBase, CalendarFun fun, CalendarType type, int days)
        {
            this.calendarFun = fun;
            this.calendarType = type;
            this.days = days;
            this.dtBase = dtBase;

        }
        public DateTime GetDate()
        {
            DateTime dt = new DateTime();

            switch (calendarFun)
            {
                case CalendarFun.Before://前
                    dt = dtBase.Value.AddDays(-days);
                    break;
                case CalendarFun.BeforeThisDay://前取當天
                    dt = dtBase.Value.AddDays((-days) - 1);
                    break;
                case CalendarFun.After://後
                    dt = dtBase.Value.AddDays(days);
                    break;
                case CalendarFun.AfterThisDay://後取當天
                    dt = dtBase.Value.AddDays((days - 1));
                    break;
            }

            return GetDateCal(dt);
        }

        private DateTime GetDateCal(DateTime dt)
        {
            DateTime result = new DateTime();
            bool crc = true;
            switch (calendarType)
            {
                case CalendarType.CalendarToCalendar://日曆日 轉 日曆日
                    result = dt;
                    break;
                case CalendarType.CalendarToWorkdaysForBefore://日曆日 轉 工作日  (前一個)
                    for (int i = 0; crc; i++)
                    {
                        result = dt.AddDays(-i);
                        crc = CalendarManager.IsHolidays(result);

                    }
                    break;
                case CalendarType.CalendarToWorkdaysForAfter://日曆日 轉 工作日   (後一個)

                    for (int i = 0; crc; i++)
                    {
                        result = dt.AddDays(i);
                        crc = CalendarManager.IsHolidays(result);

                    }
                    break;
                case CalendarType.WorkdaysToWorkdays://工作日 轉 日曆日

                    break;
            }
            return result;
        }
    }
   
    public class CalendarManager
    {
        public static bool IsHolidays(DateTime date)
        {
            // 週休二日
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            // 國定假日(國曆)
            if (date.ToString("MM/dd").Equals("01/01"))
            {
                return true;
            }
            if (date.ToString("MM/dd").Equals("02/28"))
            {
                return true;
            }
            if (date.ToString("MM/dd").Equals("04/04"))
            {
                return true;
            }
            if (date.ToString("MM/dd").Equals("04/05"))
            {
                return true;
            }
            if (date.ToString("MM/dd").Equals("05/01"))
            {
                return true;
            }
            if (date.ToString("MM/dd").Equals("10/10"))
            {
                return true;
            }

            //// 國定假日(農曆)
            //if (GetLeapDate(date, false).Equals("12/" + GetDaysInLeapMonth(date)))
            //{
            //    return true;
            //}
            //if (GetLeapDate(date, false).Equals("1/1"))
            //{
            //    return true;
            //}
            //if (GetLeapDate(date, false).Equals("1/2"))
            //{
            //    return true;
            //}
            //if (GetLeapDate(date, false).Equals("1/3"))
            //{
            //    return true;
            //}
            //if (GetLeapDate(date, false).Equals("1/4"))
            //{
            //    return true;
            //}
            //if (GetLeapDate(date, false).Equals("1/5"))
            //{
            //    return true;
            //}
            //if (GetLeapDate(date, false).Equals("5/5"))
            //{
            //    return true;
            //}
            //if (GetLeapDate(date, false).Equals("8/15"))
            //{
            //    return true;
            //}

            //// 公司假日
            ////日期是否在特定節日，資料庫有資料則為假日
            //if (CheckHoliday(date))
            //{
            //    return true;
            //}
            ////日期是否在公司設定節日，資料庫有資料則為假日
            //if (CheckDeductionSh(date))
            //{
            //    return true;
            //}

            return false;
        }
    }
    public class CalendarRep
    {
        public DateTime? Date { get; set; }
        //public List<DateTime> dtList { get; set; }
    }

}