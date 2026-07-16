using System.Windows;
using System.Windows.Controls;

namespace daysMatterr
{
    /// <summary>
    /// setPage.xaml 的交互逻辑
    /// </summary>
    public partial class setPage : Page
    {
        public setPage()
        {
            InitializeComponent();
            if (AutoStartHelper.IsAutoStartOn())
            {
                按钮_开机自启.Content = "点击关闭";
            }
            else
            {
                按钮_开机自启.Content = "点击开启";
            }
        }

        private void 按钮_开机自启_Click(object sender, RoutedEventArgs e)
        {
            if (AutoStartHelper.IsAutoStartOn())
            {
                AutoStartHelper.SetAutoStart(false);
                按钮_开机自启.Content = "点击开启";
            }
            else
            {
                AutoStartHelper.SetAutoStart(true);
                按钮_开机自启.Content = "点击关闭";
            }
        }
    }
}
