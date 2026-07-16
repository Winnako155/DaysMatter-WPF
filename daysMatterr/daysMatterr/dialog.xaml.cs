using System;
using System.Windows;
using System.Windows.Input;

namespace daysMatterr
{
    /// <summary>
    /// dialog.xaml 的交互逻辑
    /// </summary>
    public partial class dialog : Window
    {
        public dialog(string title, string content, bool showCancelButton = false) //提示框加载事件
        {
            InitializeComponent(); //初始化
            标题.Text = title; //给标题赋值
            内容.Text = content; //给内容赋值
            if (showCancelButton)
            {
                Console.WriteLine("dialog:提示框显示取消按钮");
                按钮_取消.Visibility = Visibility.Visible;
            } //决定是否展示取消按钮
            else
            {
                Console.WriteLine("dialog:提示框隐藏取消按钮");
                按钮_取消.Visibility = Visibility.Collapsed;
            }
            Console.WriteLine("dialog:提示框初始化完成");
        }

        private void 拖拽区_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) //拖动事件
        {
            Console.WriteLine("dialog:提示框开始进行拖拽");
            DragMove();
        }

        private void 关闭按钮_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("dialog:提示框选择了关闭 DialogResult:false");
            DialogResult = false;
        }

        private void 按钮_确定_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("dialog:提示框选择了确认 DialogResult:true");
            //控制台输出dialog提示用户是否要关闭弹窗
            DialogResult = true;
        }

        private void 按钮_取消_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("dialog:提示框选择了取消 DialogResult:false");
            DialogResult = false;
        }
    }
}
