using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

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
            if (index != -1) //如果是更改模式
            {
                infos = ArchiveMaster.itemInfos[state]; //同步属性
                输入框_倒数日名称.Text = infos.title;
                输入框_倒数日天数.SelectedDate = infos.date;
                输入框_倒数日天数.Text = infos.date.ToString();
                输入框_倒数日天数.DisplayDate = infos.date;
                输入框_倒数日透明度.Value = infos.a;
                文本_组件透明度指示.Text = infos.a.ToString() + "%";
            }
            refush();
        }
        public void refush() //竟然不用WPF的同步刷新，典型的WinForm大脑(
        {
            倒数日标题.Text = infos.title; //设置标题
            倒数日天数.Text = infos.DiffDays.ToString(); //设置天数
            小组件背景.Background = new SolidColorBrush(infos.backGroundColor); //设置背景颜色
            //↓设置前景字体颜色
            倒数日标题.Foreground = new SolidColorBrush(infos.fontColor); 
            倒数日天数.Foreground = new SolidColorBrush(infos.fontColor);
            倒数日天.Foreground = new SolidColorBrush(infos.fontColor);
            //↑设置前景字体颜色


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
                Console.WriteLine("输入框_倒数日天数:" + ex);
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

        private void 按钮_小组件新建_Click(object sender, RoutedEventArgs e) //新建/更新事件
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
            else //如果是编辑模式
            {
                Console.WriteLine("按钮_小组件新建:小组件模式为编辑");
                ArchiveMaster.itemInfos[index] = infos;
                ArchiveMaster.writeArchive();
                ArchiveMaster.loadArchive();
                Console.WriteLine("按钮_小组件新建:小组件更改完毕");
                PageController.changePage(new homePage());
            }
        }

        private void 按钮_小组件前景颜色选择_Click(object sender, RoutedEventArgs e) //前景颜色选择事件
        {
            ColorDialog colorDialog = new ColorDialog(); //新建一个颜色对话框实例
            colorDialog.AllowFullOpen = true; //允许自定义颜色
            //↓弹出颜色对话框
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //获取用户选中的 WinForms 颜色
                System.Drawing.Color winFormsColor = colorDialog.Color;
                //应用前景颜色
                infos.fontColor = Color.FromArgb(255, winFormsColor.R, winFormsColor.G, winFormsColor.B);
            }
            refush(); //刷新
        }

        private void 按钮_小组件背景颜色选择_Click(object sender, RoutedEventArgs e) //背景颜色选择事件
        {
            ColorDialog colorDialog = new ColorDialog(); //新建一个颜色对话框实例
            colorDialog.AllowFullOpen = true; //允许自定义颜色
            //↓弹出颜色对话框
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //获取用户选中的 WinForms 颜色
                System.Drawing.Color winFormsColor = colorDialog.Color;
                //应用前景颜色
                infos.backGroundColor = Color.FromArgb(255, winFormsColor.R, winFormsColor.G, winFormsColor.B);
            }
            refush(); //刷新
        }

        private void 输入框_倒数日透明度_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) //透明度拖拽条拖拽事件
        {
            // 确保 infos 对象已初始化
            if (infos == null) return;
            // 确保 UI 控件（TextBlock）已经创建完成
            if (文本_组件透明度指示 == null) return;
            // 确保 Slider 控件不为 null
            if (输入框_倒数日透明度 == null) return;

            // 安全地执行业务逻辑
            infos.a = Convert.ToInt32(输入框_倒数日透明度.Value);
            文本_组件透明度指示.Text = (infos.a).ToString()+"%";
            小组件背景.Opacity = infos.a/100.0;
        }
    }
}
