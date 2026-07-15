using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace daysMatterr
{
    public class item
    {
        public string title { get; set; }
        public DateTime date { get; set; }
        public int x { get; set; }
        public int y { get; set; } //不想用Point了，反正到时候还得分割

        public TimeSpan dateDiff
        {
            get
            {
                DateTime today = DateTime.Now.Date;
                Console.WriteLine("dateDiff:成功获取今天" + today);
                return date.Date - today;
            }
        }

        
        public int DiffDays => Math.Abs(dateDiff.Days); //获取距离的天数
    }
}
