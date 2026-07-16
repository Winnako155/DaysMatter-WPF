using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace daysMatterr
{
    /// <summary>
    /// itemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class itemWindow : Window
    {
        public item info;
        public int index;
        public itemWindow(int i) //初始化事件(i 接受的是archiveMaster的索引，方便直接获取其相关值)
        {
            InitializeComponent(); //初始化
            index = i;  //赋值给全局
            info = ArchiveMaster.itemInfos[index]; //拿到当前信息
            this.Left = info.x; //获取小组件应在的X位置
            this.Top = info.y; //获取小组件应在的Y位置
            倒数日标题.Text = info.title; //获取小组件标题
            倒数日天数.Text = info.DiffDays.ToString(); //获取小组件天数
            小组件背景.Opacity = info.a/100.0;
            
            
            小组件背景.Background = new SolidColorBrush(info.backGroundColor);//设置背景颜色

            //↓设置前景颜色
            倒数日标题.Foreground = new SolidColorBrush(info.fontColor);
            倒数日天数.Foreground = new SolidColorBrush(info.fontColor);
            倒数日天.Foreground = new SolidColorBrush(info.fontColor);
            //↑设置前景颜色
        }

        private void 小组件拖拽区域_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove(); //拖拽
            info.x = (int)this.Left; //赋值X
            info.y = (int)this.Top; //赋值Y
            ArchiveMaster.itemInfos[index].x = info.x; //同步赋值
            ArchiveMaster.itemInfos[index].y = info.y; //同步赋值
            ArchiveMaster.writeArchive(); //存入值
            ArchiveMaster.loadArchive(); //重新读写值w
        }
    }
}
