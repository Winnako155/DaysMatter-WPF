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
using System.Windows.Shapes;

namespace daysMatterr
{
    /// <summary>
    /// itemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class itemWindow : Window
    {
        public item info;
        public int index;
        public itemWindow(int i)
        {
            InitializeComponent();
            index = i;
            info = ArchiveMaster.itemInfos[index];
            this.Left = info.x;
            this.Top = info.y;
            倒数日标题.Text = info.title;
            倒数日天数.Text = info.DiffDays.ToString();
        }

        private void 小组件拖拽区域_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            info.x = (int)this.Left;
            info.y = (int)this.Top;
            ArchiveMaster.itemInfos[index].x = info.x;
            ArchiveMaster.itemInfos[index].y = info.y;
            ArchiveMaster.writeArchive();
            ArchiveMaster.loadArchive();
        }
    }
}
