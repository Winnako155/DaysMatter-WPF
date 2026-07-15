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
using Microsoft.Win32;
using System.Reflection;

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
