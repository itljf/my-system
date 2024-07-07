using System.Globalization;

namespace L.Common
{
    /// <summary>
    /// TimeHelper
    /// </summary>
    public class TimeHelper
    {
        /// <summary>
        /// 一年中的第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int WeekOfYear(DateTime dt)
        {
            return WeekOfYear(dt, new CultureInfo("zh-CN"));
        }
        /// <summary>
        /// 一年中的第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ci"></param>
        /// <returns></returns>
        public static int WeekOfYear(DateTime dt, CultureInfo ci)
        {
            return ci.Calendar.GetWeekOfYear(dt, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
        }

        /// <summary>
        /// 计算差;...之前
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string str = null;
            try
            {
                TimeSpan span = (TimeSpan)(DateTime2 - DateTime1);
                if (span.Days >= 1) return (DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日");
                if (span.Hours > 1) return (span.Hours.ToString() + "小时前");
                str = span.Minutes.ToString() + "分钟前";
            }
            catch
            {
            }
            return str;
        }

        /// <summary>
        /// 计算差;
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static string DateDiffStr(DateTime DateTime1, DateTime DateTime2)
        {
            string str = null;
            try
            {
                TimeSpan span = (TimeSpan)(DateTime2 - DateTime1);

                if (span.Days >= 1)
                {
                    return string.Format("{0}天{1}小时{2}分{3}秒", span.Days, span.Hours, span.Minutes, span.Seconds);// (DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日");
                }
                if (span.Hours > 1)
                {
                    return string.Format("{0}小时{1}分{2}秒", span.Hours, span.Minutes, span.Seconds);// return (span.Hours.ToString() + "小时前");
                }
                return string.Format("{0}分{1}秒", span.Minutes, span.Seconds);
            }
            catch
            {
            }
            return str;
        }

        /// <summary>
        /// 两个日期的间隔
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static TimeSpan DateDiff2(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan span = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts = new TimeSpan(DateTime2.Ticks);
            return span.Subtract(ts).Duration();
        }

        public static DateTime ConvertLongToDateTime(long timeStamp, bool accurateToMilliseconds = false)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            if (accurateToMilliseconds)
            {
                return startTime.AddTicks(timeStamp * 10000);
            }
            else
            {
                return startTime.AddTicks(timeStamp * 10000000);
            }
        }

        /// <summary>
        /// 获取月份
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="item">0: 缩写英文格式（例如：Jan.） 1: 全拼英文格式（例如：January) 2: 中文格式（例如：一月)</param>
        /// <returns>反回月份</returns>
        public static string GetMonth(DateTime date, int item)
        {
            string month = date.Month.ToString();
            if (item == 0)
            {
                switch (month)
                {
                    case "1":
                        month = "Jan.";
                        break;
                    case "2":
                        month = "Feb.";
                        break;
                    case "3":
                        month = "Mar.";
                        break;
                    case "4":
                        month = "Apr.";
                        break;
                    case "5":
                        month = "May.";
                        break;
                    case "6":
                        month = "Jun.";
                        break;
                    case "7":
                        month = "Jul.";
                        break;
                    case "8":
                        month = "Aug.";
                        break;
                    case "9":
                        month = "Sep.";
                        break;
                    case "10":
                        month = "Oct.";
                        break;
                    case "11":
                        month = "Nov.";
                        break;
                    case "12":
                        month = "Dec.";
                        break;
                    default:
                        month = "NULL";
                        break;
                }
            }
            else if (item == 1)
            {
                switch (month)
                {
                    case "1":
                        month = "January";
                        break;
                    case "2":
                        month = "February";
                        break;
                    case "3":
                        month = "March";
                        break;
                    case "4":
                        month = "April";
                        break;
                    case "5":
                        month = "May";
                        break;
                    case "6":
                        month = "June";
                        break;
                    case "7":
                        month = "July";
                        break;
                    case "8":
                        month = "August";
                        break;
                    case "9":
                        month = "September";
                        break;
                    case "10":
                        month = "October";
                        break;
                    case "11":
                        month = "November";
                        break;
                    case "12":
                        month = "December";
                        break;
                    default:
                        month = "NULL";
                        break;
                }
            }
            else if (item == 2)
            {
                switch (month)
                {
                    case "1":
                        month = "一月";
                        break;
                    case "2":
                        month = "二月";
                        break;
                    case "3":
                        month = "三月";
                        break;
                    case "4":
                        month = "四月";
                        break;
                    case "5":
                        month = "五月";
                        break;
                    case "6":
                        month = "六月";
                        break;
                    case "7":
                        month = "七月";
                        break;
                    case "8":
                        month = "八月";
                        break;
                    case "9":
                        month = "九月";
                        break;
                    case "10":
                        month = "十月";
                        break;
                    case "11":
                        month = "十一月";
                        break;
                    case "12":
                        month = "十二月";
                        break;
                    default:
                        month = "NULL";
                        break;
                }
            }
            else
            {
                month = "NULL";
            }
            return month;
        }

        /// <summary>
        /// 获取星期
        /// </summary>
        /// <param name="date">星期</param>
        /// <param name="item">0: 缩写英文格式（例如：Mon.） 1: 中文格式（例如：星期一) 2: 中文格式 如:周一</param>
        /// <returns></returns>
        public static string GetWeek(DateTime date, int item)
        {
            string week = date.DayOfWeek.ToString();
            if (item == 0)
            {
                switch (week)
                {
                    case "Monday":
                        week = "Mon.";
                        break;
                    case "Tuesday":
                        week = "Tues.";
                        break;
                    case "Wednesday":
                        week = "Wed.";
                        break;
                    case "Thursday":
                        week = "Thur.";
                        break;
                    case "Friday":
                        week = "Fri.";
                        break;
                    case "Saturday":
                        week = "Sat.";
                        break;
                    case "Sunday":
                        week = "Sun.";
                        break;
                    default:
                        week = "NULL";
                        break;
                }
            }
            else if (item == 1)
            {
                switch (week)
                {
                    case "Monday":
                        week = "星期一";
                        break;
                    case "Tuesday":
                        week = "星期二";
                        break;
                    case "Wednesday":
                        week = "星期三";
                        break;
                    case "Thursday":
                        week = "星期四";
                        break;
                    case "Friday":
                        week = "星期五";
                        break;
                    case "Saturday":
                        week = "星期六";
                        break;
                    case "Sunday":
                        week = "星期日";
                        break;
                    default:
                        week = "NULL";
                        break;
                }
            }
            else if (item == 2)
            {
                switch (week)
                {
                    case "Monday":
                        week = "周一";
                        break;
                    case "Tuesday":
                        week = "周二";
                        break;
                    case "Wednesday":
                        week = "周三";
                        break;
                    case "Thursday":
                        week = "周四";
                        break;
                    case "Friday":
                        week = "周五";
                        break;
                    case "Saturday":
                        week = "周六";
                        break;
                    case "Sunday":
                        week = "周日";
                        break;
                    default:
                        week = "NULL";
                        break;
                }
            }
            return week;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="week"></param>
        /// <param name="item">0: 缩写英文格式（例如：Mon.） 1: 中文格式（例如：星期一) 2: 中文格式 如:周一</param>
        /// <returns></returns>
        public static string GetWeek(DayOfWeek week, int item)
        {
            if (item == 0)
            {
                switch (week)
                {
                    case DayOfWeek.Monday:
                        return "Mon.";
                    case DayOfWeek.Tuesday:
                        return "Tues.";
                    case DayOfWeek.Wednesday:
                        return "Wed.";
                    case DayOfWeek.Thursday:
                        return "Thur.";
                    case DayOfWeek.Friday:
                        return "Fri.";
                    case DayOfWeek.Saturday:
                        return "Sat.";
                    case DayOfWeek.Sunday:
                        return "Sun.";
                }
            }
            else if (item == 1)
            {
                switch (week)
                {
                    case DayOfWeek.Monday:
                        return "星期一";
                    case DayOfWeek.Tuesday:
                        return "星期二";
                    case DayOfWeek.Wednesday:
                        return "星期三";
                    case DayOfWeek.Thursday:
                        return "星期四";
                    case DayOfWeek.Friday:
                        return "星期五";
                    case DayOfWeek.Saturday:
                        return "星期六";
                    case DayOfWeek.Sunday:
                        return "星期日";
                }
            }
            else if (item == 2)
            {
                switch (week)
                {
                    case DayOfWeek.Monday:
                        return "周一";
                    case DayOfWeek.Tuesday:
                        return "周二";
                    case DayOfWeek.Wednesday:
                        return "周三";
                    case DayOfWeek.Thursday:
                        return "周四";
                    case DayOfWeek.Friday:
                        return "周五";
                    case DayOfWeek.Saturday:
                        return "周六";
                    case DayOfWeek.Sunday:
                        return "周日";
                }
            }
            return null;
        }

        public static string GetDay(DateTime date, int item)
        {
            string day = date.Day.ToString();
            if (item == 0)
            {
                switch (day)
                {
                    case "1":
                        day += "st";
                        break;
                    case "2":
                        day += "nd";
                        break;
                    case "3":
                        day += "rd";
                        break;
                    case "21":
                        day += "st";
                        break;
                    case "22":
                        day += "nd";
                        break;
                    case "23":
                        day += "rd";
                        break;
                    case "31":
                        day += "st";
                        break;
                    default:
                        day += "th";
                        break;
                }
            }
            return day;
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateMode"></param>
        /// <returns></returns>
        public static string FormatDate(DateTime dateTime1, string dateMode)
        {
            switch (dateMode)
            {
                case "0":
                    return dateTime1.ToString("yyyy-MM-dd");

                case "1":
                    return dateTime1.ToString("yyyy-MM-dd HH:mm:ss");

                case "2":
                    return dateTime1.ToString("yyyy/MM/dd");

                case "3":
                    return dateTime1.ToString("yyyy年MM月dd日");

                case "4":
                    return dateTime1.ToString("MM-dd");

                case "5":
                    return dateTime1.ToString("MM/dd");

                case "6":
                    return dateTime1.ToString("MM月dd日");

                case "7":
                    return dateTime1.ToString("yyyy-MM");

                case "8":
                    return dateTime1.ToString("yyyy/MM");

                case "9":
                    return dateTime1.ToString("yyyy年MM月");
            }
            return dateTime1.ToString();
        }

        /// <summary>
        /// 将时间格式化成 年月日 的形式,如果时间为null，返回当前系统时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Separator"></param>
        /// <returns></returns>
        public string GetFormatDate(DateTime dt, char Separator)
        {
            if (!dt.Equals(DBNull.Value))
            {
                string format = string.Format("yyyy{0}MM{1}dd", Separator, Separator);
                return dt.ToString(format);
            }
            return this.GetFormatDate(DateTime.Now, Separator);
        }

        /// <summary>
        /// 将时间格式化成 时分秒 的形式,如果时间为null，返回当前系统时间 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Separator"></param>
        /// <returns></returns>
        public string GetFormatTime(DateTime dt, char Separator)
        {
            if (!dt.Equals(DBNull.Value))
            {
                string format = string.Format("hh{0}mm{1}ss", Separator, Separator);
                return dt.ToString(format);
            }
            return this.GetFormatDate(DateTime.Now, Separator);
        }

        /// <summary>
        /// 返回某年某月最后一天 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime time = new DateTime(year, month, new GregorianCalendar().GetDaysInMonth(year, month));
            return time.Day;
        }

        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime time = new DateTime();
            TimeSpan span = new TimeSpan(time1.Ticks - time2.Ticks);
            double totalSeconds = span.TotalSeconds;
            int num2 = 0;
            if (totalSeconds > 2147483647.0)
                num2 = 0x7fffffff;
            else if (totalSeconds < -2147483648.0)
                num2 = -2147483648;
            else
                num2 = (int)totalSeconds;
            if (num2 > 0)
                time = time2;
            else if (num2 < 0)
                time = time1;
            else
                return time1;
            int num3 = num2;
            if (num2 <= -2147483648) num3 = -2147483647;
            int num4 = random.Next(Math.Abs(num3));
            return time.AddSeconds((double)num4);
        }

        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <param name="Second"></param>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal d = Second / 60M;
            return Convert.ToInt32(Math.Ceiling(d));
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        public static long CurrentTimeStamp(bool isMinseconds = false)
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long times = Convert.ToInt64(isMinseconds ? ts.TotalMilliseconds : ts.TotalSeconds);
            return times;
        }

        public static long ConvertDateTimeIong(System.DateTime time)
        {
            var ts = time - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long times = Convert.ToInt64(ts.TotalMilliseconds);
            return times;
        }
        /// <summary>
        /// 判断两个方法是否交叉
        /// </summary>
        /// <param name="start1"></param>
        /// <param name="end1"></param>
        /// <param name="start2"></param>
        /// <param name="end2"></param>
        /// <returns></returns>
        public static bool IsTimeRangeOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            // 交错或包含
            if (end1<=start2 || end2<=start1)
            {//相连或不相交
                return false;
            }else if (start1<=start2 && end2<=end1)
            {//包含
                return true;
            }else if (start2<=start1 && end1<=end2)
            {//包含
                return true;
            }else if (start2<=start1 && start1<end2)
            {//交错
                return true;
            }else if (start2<end1 && end1<end2)
            {//交错
                return true;
            }
            return false;
        }


        #region 时间段格式化
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TimeInfo">["13:00 - 15:00","11:00 - 13:00","09:00 - 11:00"]</param>
        /// <returns></returns>
        public static string FormatTime(List<string> listInfo)
        {
           
            System.Text.StringBuilder _sb = new System.Text.StringBuilder();

            var __index = 0;
            while (__index < listInfo.Count)
            {
                var cur = listInfo[__index].Split('-').Select(m => m.Trim()).ToList();

                if (__index == 0)
                {
                    _sb.Append(cur[0]);
                    _sb.Append("-");
                }

                if (__index == listInfo.Count - 1)
                {
                    _sb.Append("-");
                    _sb.Append(cur[1]);
                    break;
                }

                var next = listInfo[__index + 1].Split('-').Select(m => m.Trim()).ToList();

                //当前结束时间
                var curEndTime = new TimeSpan(int.Parse(cur[1].Split(':')[0]), int.Parse(cur[1].Split(':')[1]), 0);
                //下一个开始时间
                var nextEndTime = new TimeSpan(int.Parse(next[0].Split(':')[0]), int.Parse(next[0].Split(':')[1]), 0);

                //相差小于120分钟
                var _min =30;
              

                if ((nextEndTime - curEndTime).TotalMinutes >= _min)
                {
                    _sb.Append(cur[1]);
                    _sb.Append(",");
                    _sb.Append(next[0]);
                }
                __index++;
            }

            return _sb.Replace("--", " - ").ToString();
        }

        #endregion

    }
}
