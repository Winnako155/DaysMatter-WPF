using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Drawing;

namespace daysMatterr
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public Storyboard showBackButtonAn;
        public Storyboard hideBackButtonAn;
        public Storyboard changePageAn;
        private NotifyIcon _notifyIcon;


        //↓处理这个BYD系统托盘
        private void InitTrayIcon()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            _notifyIcon = new NotifyIcon();

            Assembly asm = Assembly.GetExecutingAssembly();
            Stream stream = asm.GetManifestResourceStream("daysMatterr.favicon.ico");
            if (stream != null)
            {
                MemoryStream ms = new MemoryStream();
                stream.CopyTo(ms);
                stream.Dispose();
                ms.Position = 0;
                _notifyIcon.Icon = new Icon(ms);
            }
            else
            {
                _notifyIcon.Icon = SystemIcons.Application;
            }

            _notifyIcon.Text = "倒数日";
            System.Windows.Forms.MenuItem openItem = new System.Windows.Forms.MenuItem("打开窗口");
            System.Windows.Forms.MenuItem exitItem = new System.Windows.Forms.MenuItem("退出程序");

            openItem.Click += (s, e) =>
            {
                Show();
                WindowState = WindowState.Normal;
                Activate();
            };

            exitItem.Click += (s, e) =>
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
                System.Windows.Application.Current.Shutdown();
            };

            _notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(new System.Windows.Forms.MenuItem[] { openItem, exitItem });
            _notifyIcon.DoubleClick += (s, e) =>
            {
                Show();
                WindowState = WindowState.Normal;
                Activate();
            };
            _notifyIcon.Visible = true;
        }
        private void ShowWindow()
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        protected override void OnClosed(EventArgs e)
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
            }
            base.OnClosed(e);
        }
        //↑处理这个BYD系统托盘





        public MainWindow() //窗口运行时的事件
        {
            InitializeComponent(); //初始化
            InitTrayIcon(); //启用系统托盘

            //声明动画变量↓
            showBackButtonAn = (Storyboard)FindResource("showBackButton");
            hideBackButtonAn = (Storyboard)FindResource("hideBackButton");
            changePageAn = (Storyboard)FindResource("changePage");
            //生命动画变量↑

            PageController.MainWin = this; //直接将自己给个实例
            PageController.MainFrame = 控制视图; //给全局frame赋值，方便全局调用
            PageController.changePage(new homePage());


            Console.WriteLine("mainWindow:初始化界面成功");
            Console.WriteLine("archiveMaster:当前环境工作目录:" + ArchiveMaster.appPath);


            //读写操作↓
            int readInfo = ArchiveMaster.loadArchive();
            if (readInfo!=-1) //如果没有成功读取文件
            {
                if(readInfo == -3)
                {
                    Console.WriteLine("MainWindow:小组件文件写入失败 (-3)");
                    遮罩.Visibility = Visibility.Visible; //设置遮罩可视
                    var dlg = new dialog("注意", "版本更新，请注意保存Info.inf中之前的倒数日信息\n关闭该提示框后，将会重写info.inf，会导致之前的倒数日丢失。").ShowDialog();
                    遮罩.Visibility = Visibility.Collapsed;
                }
                Console.WriteLine("MainWindow:读取小组件文件失败,准备写入文件");
                int wtinfo = ArchiveMaster.writeArchive(); //写入文件
                if (wtinfo == 1) //如果返回值为1 (成功写入了小组件文件)
                {
                    Console.WriteLine("MainWindow:写入小组件文件成功接受到了正确的返回值 (1)");
                    ArchiveMaster.loadArchive(); //再次读入文件刷新
                }
                else
                {
                    Console.WriteLine("MainWindow:小组件文件写入失败 (0)");
                    遮罩.Visibility = Visibility.Visible; //设置遮罩可视
                    var dlg = new dialog("错误", "小组件文件写入失败 (0) 请联系开发者。").ShowDialog();
                    遮罩.Visibility = Visibility.Collapsed;
                    this.Close();
                }
            }
            else
            {
                if(ArchiveMaster.itemInfos.Count != 0) //如果读取到了任何倒数日，那么说明已经使用过了，直接最小化
                {
                    this.Hide();
                }
            }
            /*遮罩.Visibility = Visibility.Visible; //设置遮罩可视
            var dlg2 = new dialog("提示", "本次找到了"+ArchiveMaster.itemInfos.Count.ToString()+"个小组件信息。\n用我一个个提示框提示小组件的信息吗?",true).ShowDialog();
            if(dlg2 == true)
            {
                foreach (var i in ArchiveMaster.itemInfos)
                {
                    new dialog("提示", "Title:" + i.title + "\nDate:" + i.date.ToString()).ShowDialog();
                }
            }
            遮罩.Visibility = Visibility.Collapsed;*/
            PageController.refushItems();
        }

        private void 关闭按钮_Click(object sender, RoutedEventArgs e) //关闭按钮事件
        {
            Console.WriteLine("开始进行关闭");
            this.Hide();
        }

        private void 拖拽区_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)  //拖拽事件
        {
            Console.WriteLine("开始进行拖拽");
            DragMove(); //拖拽窗口
        }

        private void 按钮_返回上级_Click(object sender, RoutedEventArgs e) //返回上级事件
        {
            Console.WriteLine("按钮_返回上级被点击");
            if (PageController.nowIndex == 1) //再次确认页面为新建页
            {
                Console.WriteLine("按钮_返回上级_Click:当前页面不为主页");
                遮罩.Visibility = Visibility.Visible; //设置遮罩可视
                var dlg = new dialog("警告", "您确定要返回吗？\n您更改的内容可能不会保存。", true).ShowDialog();
                遮罩.Visibility = Visibility.Collapsed;
                if (dlg == true) //如果确认返回页面
                {
                    Console.WriteLine("按钮_返回上级_Click:开始切换页面");
                    PageController.changePage(new homePage());
                }
            }
            else
            {
                PageController.changePage(new homePage());
            }
        }

        private void 按钮_设置_Click(object sender, RoutedEventArgs e)
        {
            PageController.changePage(new setPage());
        }
    }
}
