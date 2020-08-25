using System;

namespace Blog.Core.Model
{
    /// <summary>
    /// 天气广播实体
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 温度(摄氏度)
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// 温度(华氏摄氏度)
        /// </summary>
        /// <returns></returns>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// 简介
        /// </summary>
        /// <value></value>
        public string Summary { get; set; }
    }
}
