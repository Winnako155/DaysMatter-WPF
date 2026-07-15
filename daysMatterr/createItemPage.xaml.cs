using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace daysMatterr
{
    /// <summary>
    /// createItemPage.xaml 的交互逻辑
    /// </summary>
    public partial class createItemPage : Page
    {
        public item infos = new item();
        public int index;
        public createItemPage(int state = -1) //状态  (-1:新建 ID:更改)
        {
            InitializeComponent();
            index = state;
            infos.date = DateTime.Today;
            Console.WriteLine("createItemPage:初始化成功");
            if (index != -1) //如果时更改模式
            {
                infos = ArchiveMaster.itemInfos[state]; //同步属性
                输入框_倒数日名称.Text = infos.title;
                输入框_倒数日天数.SelectedDate = infos.date;
                输入框_倒数日天数.Text = infos.date.ToString();
                输入框_倒数日天数.DisplayDate = infos.date;
            }
            refush();
        }
        public void refush() //竟然不用WPF的同步刷新，典型的WinForm大脑(
        {
            倒数日标题.Text = infos.title;
            倒数日天数.Text = infos.DiffDays.ToString();
            Console.WriteLine("refush:刷新成功");
        }

        private void 输入框_倒数日名称_TextChanged(object sender, TextChangedEventArgs e)
        {
            infos.title = 输入框_倒数日名称.Text;
            refush();
        }

        private void 输入框_倒数日天数_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                infos.date = (DateTime)输入框_倒数日天数.SelectedDate;
            }
            catch (Exception ex)
            {
                Console.WriteLine("输入框_倒数日天数:"+ex);
            }
            refush();
        }

        private void 输入框_倒数日天数_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                infos.date = (DateTime)输入框_倒数日天数.SelectedDate;
            }
            catch (Exception ex)
            {
                Console.WriteLine("输入框_倒数日天数:" + ex);
            }
            refush();
        }

        private void 按钮_小组件新建_Click(object sender, RoutedEventArgs e)
        {
            if (index == -1) //如果是新建模式
            {
                infos.x = 10; //设置默认x坐标
                infos.y = 10; //设置默认y坐标
                Console.WriteLine("按钮_小组件新建:小组件模式为新建");
                ArchiveMaster.itemInfos.Add(infos);
                ArchiveMaster.writeArchive();
                ArchiveMaster.loadArchive();
                Console.WriteLine("按钮_小组件新建:小组件新建完毕");
                PageController.changePage(new homePage());
            }
            else
            {
                Console.WriteLine("按钮_小组件新建:小组件模式为编辑");
                ArchiveMaster.itemInfos[index] = infos;
                ArchiveMaster.writeArchive();
                ArchiveMaster.loadArchive();
                Console.WriteLine("按钮_小组件新建:小组件更改完毕");
                PageController.changePage(new homePage());
            }
        }
    }
}
