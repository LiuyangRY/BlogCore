using System;

namespace Blog.Core.Common.Helper
{
    /// <summary>
    /// 类型转换工具类
    /// </summary>
    public static class UtilConvert
    {
        /// <summary>
        /// Object类型转Int
        /// </summary>
        /// <param name="thisValue">需要转换的对象</param>
        /// <returns>转换后的整型值，转换失败返回0</returns>
        public static int ObjToInt(this object thisValue)
        {
            return thisValue.ObjToInt(0);
        }
        
        /// <summary>
        /// Object类型转Int
        /// </summary>
        /// <param name="thisValue">需要转换的对象</param>
        /// <param name="errorValue">转换失败返回的值（默认为0）</param>
        /// <returns>转换成功返回转换后的值，否则返回指定值（默认为0）</returns>
        public static int ObjToInt(this object thisValue, int errorValue = 0)
        {
            int result = 0;
            if(thisValue == null || thisValue == DBNull.Value || !int.TryParse(thisValue.ToString(), out result))
            {
                return errorValue;
            }
            return result;
        }

        /// <summary>
        /// Object类型转Decimal
        /// </summary>
        /// <param name="thisValue">需要转换的对象</param>
        /// <returns>转换成功返回转换后的值，否则返回0</returns>
        public static decimal ObjToDecimal(this object thisValue)
        {
            return thisValue.ObjToDecimal(0);
        }

        /// <summary>
        /// Object类型转Decimal
        /// </summary>
        /// <param name="thisValue">需要转换的对象</param>
        /// <param name="errorValue">转换失败返回的值（默认为0）</param>
        /// <returns>转换成功返回转换后的值，否则返回指定的值</returns>
        public static decimal ObjToDecimal(this object thisValue, decimal errorValue = 0)
        {
            decimal result = 0;
            if(thisValue == null || thisValue == DBNull.Value || !decimal.TryParse(thisValue.ToString(), out result))
            {
                return errorValue;
            }
            return result;
        }

        /// <summary>
        /// Object类型转String
        /// </summary>
        /// <param name="thisValue">需要转换的对象</param>
        /// <returns>转换成功返回转换后的值，否则返回空字符串</returns>
        public static string ObjToString(this object thisValue)
        {
            return thisValue.ObjToString(string.Empty);
        }

        /// <summary>
        /// Object类型转String
        /// </summary>
        /// <param name="thisValue">需要转换的对象</param>
        /// <param name="errorValue">转换失败返回的值（默认为空字符串）</param>
        /// <returns>转换成功返回转换后的值，否则返回指定值</returns>
        public static string ObjToString(this object thisValue, string errorValue = "")
        {
            return thisValue?.ToString().Trim() ?? errorValue;
        }

        /// <summary>
        /// Object类型转DateTime
        /// </summary>
        /// <param name="thisValue">需要转换的对象</param>
        /// <returns>转换成功返回转换后的值，否则返回日期最小值</returns>
        public static DateTime ObjToDateTime(this object thisValue)
        {
            return thisValue.ObjToDateTime(DateTime.MinValue);
        }

        /// <summary>
        /// Object类型转DateTime
        /// </summary>
        /// <param name="thisValue">需要转换的对象</param>
        /// <param name="errorValue">转换失败返回的值</param>
        /// <returns>转换成功返回转换后的值，否则返回指定值</returns>
        public static DateTime ObjToDateTime(this object thisValue, DateTime errorValue)
        {
            DateTime result;
            if(thisValue == null || thisValue == DBNull.Value || !DateTime.TryParse(thisValue.ToString(), out result))
            {
                return errorValue;
            }
            return result;
        }

        /// <summary>
        /// Object类型转Bool
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool ObjToBool(this object thisValue)
        {
            bool result;
            if(thisValue == null || thisValue == DBNull.Value || !bool.TryParse(thisValue.ToString(), out result))
            {
                return false;
            }
            return result;
        }

        /// <summary>
        /// 获取当前时间的时间戳
        /// </summary>
        /// <param name="thisValue">当前时间</param>
        /// <returns>当前时间的时间戳</returns>
        public static string DateToTimeStamp(this DateTime thisValue)
        {
            TimeSpan timeSpan = thisValue - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(timeSpan.TotalSeconds).ToString();
        }
    }
}