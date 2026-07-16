using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace daysMatterr
{
    public class item
    {
        public string title { get; set; }
        public DateTime date { get; set; }
        public int x { get; set; }
        public int y { get; set; } //不想用Point了，反正到时候还得分割

        //↓RGBA颜色调配
        public Color backGroundColor { get; set; } = Color.FromArgb(255, 255, 255, 255);//这个必须要用 color了）直接用RGBA变量多死人
        public Color fontColor { get; set; } = Color.FromArgb(255, 0, 0, 0);//前景色

        public int a { get; set; } = 100;//设置透明度
        public int s { get; set; } //这个是倒数日小组件的大小 (0:通常 1:缩小)

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


        //↓专门给WPF资源绑定用的solidColorBrush:
        public SolidColorBrush wpfBackGroundColor => new SolidColorBrush(backGroundColor);
        public SolidColorBrush wpfFontColor => new SolidColorBrush(fontColor);
    }
}
