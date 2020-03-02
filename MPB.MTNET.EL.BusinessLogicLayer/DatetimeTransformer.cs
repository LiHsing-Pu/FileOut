using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPB.MTNET.EL.BusinessLogicLayer
{
    /*年份轉換工具(Datetime_Transformer)詳細敘述如下*/
    public class DatetimeTransformer
    {
        /*西元年（DateTime）轉民國年（String）*/
        /// <summary>
        /// 西元年（DateTime）轉民國年（String）
        /// </summary>
        /// <param name="input_date">西元年，分隔符號為-（例：2012-02-29）</param>
        /// <returns>民國年，分隔符號為-（例：101-02-29）</returns>
        /// <info>Author: Kevin; Date: 2015/7/8 </info>
        /// <history>
        /// xx.  YYYY/MM/DD   Ver   Author      Comments
        /// ===  ==========  ====  ==========  ==========
        /// 01.  2015/7/8    1.00  Kevin  Created
        /// </history>
        public static string AD_To_TW(DateTime? input_date)
        {
            string output_date = "";
            if (input_date.HasValue)
            {

                int year_temp = input_date.Value.Year - 1911;

                if (year_temp < 100)
                {
                    output_date = "0" + year_temp.ToString() + input_date.Value.ToString("-MM-dd");
                }
                else
                {
                    output_date = year_temp.ToString() + input_date.Value.ToString("-MM-dd");
                }
            }
            return output_date;
        }
        /*民國年（String）轉西元年（DateTime）*/
        /// <summary>
        /// 民國年（String）轉西元年（DateTime）
        /// </summary>
        /// <param name="input_date">民國年，分隔符號為/（例：101/02/29）</param>
        /// <returns>西元年，分隔符號為-（例：2012-02-29）</returns>
        /// <info>Author: Kevin; Date: 2015/7/8 </info>
        /// <history>
        /// xx.  YYYY/MM/DD   Ver   Author      Comments
        /// ===  ==========  ====  ==========  ==========
        /// 01.  2015/7/8    1.00  Kevin  Created
        /// 02.  2018/8/7    2.00  Benson  Updated
        /// </history>
        public static DateTime? TW_To_AD(string input_date)
        {
            if (string.IsNullOrEmpty(input_date))
            {
                return null;
            }
            else
            {
                DateTime output_date = DateTime.Parse((int.Parse(input_date.Split('-')[0]) + 1911).ToString() + "-" + input_date.Split('-')[1] + "-" + input_date.Split('-')[2]);
                return output_date;
            }
        }
        /*西元年（DateTime）轉民國年（String）(只回傳年月)*/
        /// <summary>
        /// 西元年（DateTime）轉民國年（String）(只回傳年月)
        /// </summary>
        /// <param name="input_date">西元年，分隔符號為-（例：2012-02-29）</param>
        /// <returns>民國年，分隔符號為-（例：101-02-29）</returns>
        /// <info>Author: Ben; Date: 2015/09/02 </info>
        /// <history>
        /// xx.  YYYY/MM/DD   Ver   Author      Comments
        /// ===  ==========  ====  ==========  ==========
        /// 01.  2015/09/02  1.00    Ben        Created
        /// </history>
        public static string AD_To_TW_YYYMM(Nullable<DateTime> input_date)
        {
            string output_date = "";
            if (input_date.HasValue)
            {
                int year_temp = input_date.Value.Year - 1911;

                if (year_temp < 100)
                {
                    output_date = "0" + (input_date.Value.Year - 1911).ToString() + "-" + input_date.Value.Month.ToString("00");
                }
                else
                {
                    output_date = (input_date.Value.Year - 1911).ToString() + "-" + input_date.Value.Month.ToString("00");
                }
            }
            return output_date;
        }

        public static string AD_To_TW_HMS(DateTime? input_date)
        {
            string output_date = "";
            if (input_date.HasValue)
            {

                int year_temp = input_date.Value.Year - 1911;

                if (year_temp < 100)
                {
                    output_date = "0" + year_temp.ToString() + input_date.Value.ToString("-MM-dd HH:mm:ss");
                }
                else
                {
                    output_date = year_temp.ToString() + input_date.Value.ToString("-MM-dd HH:mm:ss");
                }
            }
            return output_date;
        }
        public static DateTime? TW_To_AD_HMS_MIN(string input_date)
        {
            if (string.IsNullOrEmpty(input_date))
            {
                return null;
            }
            else
            {
                DateTime output_date = DateTime.Parse((int.Parse(input_date.Split('-')[0]) + 1911).ToString() + "-" + input_date.Split('-')[1] + "-" + input_date.Split('-')[2] + " 00:00:00");
                return output_date;
            }
        }
        public static DateTime? TW_To_AD_HMS_MAX(string input_date)
        {
            if (string.IsNullOrEmpty(input_date))
            {
                return null;
            }
            else
            {
                DateTime output_date = DateTime.Parse((int.Parse(input_date.Split('-')[0]) + 1911).ToString() + "-" + input_date.Split('-')[1] + "-" + input_date.Split('-')[2] + " 23:59:59");
                return output_date;
            }
        }
    }
}
