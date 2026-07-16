using System;
using System.Threading;
using System.Windows;

namespace daysMatterr
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        // 定义一个互斥量，保证全局唯一
        private static Mutex _mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            // 这里的名字可以自定义，但必须保证全局唯一（建议使用GUID或项目特定名称）
            string mutexName = "daysMatterr.WPF";

            // 尝试创建互斥量，isNewInstance 为 true 表示这是第一次启动
            _mutex = new Mutex(true, mutexName, out bool isNewInstance);

            if (!isNewInstance)
            {
                // 已经有程序在运行 → 弹出提示并关闭当前新实例
                new dialog("提示", "该软件已经在运行\n点击托盘图标即可显示主界面").ShowDialog();

                // 关闭当前这个新启动的进程
                Environment.Exit(0);
                return;
            }

            // 如果是第一次启动，正常执行后续逻辑
            base.OnStartup(e);
        }
    }
}
