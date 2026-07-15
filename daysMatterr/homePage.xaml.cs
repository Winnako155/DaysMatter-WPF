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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace daysMatterr
{
    /// <summary>
    /// homePage.xaml 的交互逻辑
    /// </summary>
    public partial class homePage : Page
    {
        public homePage() //页面加载时的事件
        {
            InitializeComponent(); //初始化
            Console.WriteLine("homePage:主页加载完毕");
            列表_倒数日.ItemsSource = ArchiveMaster.itemInfos;
            PageController.refushItems();
        }

        private void 按钮_添加倒数日_Click(object sender, RoutedEventArgs e) //添加倒数日的事件
        {
            Console.WriteLine("按钮_添加倒数日_Click:开始进入新建页");
            PageController.changePage(new createItemPage()); //切换页面为新建页
        }

        private void 列表_倒数日_SelectionChanged(object sender, SelectionChangedEventArgs e) //选择项更改事件
        {
            if(列表_倒数日.SelectedIndex != -1)
            {
                var an = (Storyboard)FindResource("focusItem");
                an.Begin();
                Console.WriteLine("列表_倒数日:选中按钮区显示");
            }
            else
            {
                var an = (Storyboard)FindResource("unfocusItem");
                an.Begin();
                Console.WriteLine("列表_倒数日:选中按钮区隐藏");
            }
        }

        private void 列表_倒数日_LostFocus(object sender, RoutedEventArgs e) //已弃用的代码
        {
            列表_倒数日.SelectedIndex = -1;
        }

        private void 按钮_删除内容_Click(object sender, RoutedEventArgs e) //删除小组件事件
        {
            Console.WriteLine("按钮_删除内容_Click:准备删除小组件");
            遮罩.Visibility = Visibility.Visible;
            var dlg = new dialog("警告", "您确认要删除该倒数日吗", true).ShowDialog();
            遮罩.Visibility = Visibility.Collapsed;
            if(dlg == true)
            {
                Console.WriteLine("删除按钮被点击");
                if (列表_倒数日.SelectedIndex != -1)
                {
                    Console.WriteLine("按钮_删除内容_Click:小组件存在选中");
                    ArchiveMaster.itemInfos.RemoveAt(列表_倒数日.SelectedIndex);
                    ArchiveMaster.writeArchive();
                    ArchiveMaster.loadArchive();
                    列表_倒数日.ItemsSource = null;
                    列表_倒数日.ItemsSource = ArchiveMaster.itemInfos;
                    Console.WriteLine("按钮_删除内容_Click:小组件删除成功");
                    PageController.refushItems();
                }
            }
        }

        private void 按钮_更改内容_Click(object sender, RoutedEventArgs e)
        {
            if (列表_倒数日.SelectedIndex != -1)
            {
                Console.WriteLine("按钮_更改内容_Click:小组件存在选中,准备进入编辑页面");
                PageController.changePage(new createItemPage(列表_倒数日.SelectedIndex));
            }
        }
    }
}
