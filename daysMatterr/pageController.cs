using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace daysMatterr
{

    public static class PageController  //页面控制器 公开暴露方便子页面来调用进行页面切换
    {
        // 存放父窗口的Frame实例
        public static Frame MainFrame { get; set; }
        public static int nowIndex { get; set; }
        public static MainWindow MainWin { get; set; }

        public static List<itemWindow> items = new List<itemWindow>();


        public static void changePage(Page target)
        {
            Console.WriteLine("pageMaster.changePage():开始切换页面");

            Type pageType = target.GetType(); //获取页面类型

            //↓屎山：逐步判断页面并赋值给nowIndex(不会用switch 不想用三元判断 不想映射字典(bushi))
            if (pageType == typeof(homePage)) 
            {
                //如果是主页
                nowIndex = 0;
                Console.WriteLine("pageMaster.changePage():当前页面为主页 nowIndex:" + nowIndex);
            }
            else if(pageType == typeof(createItemPage))
            {
                //如果是新建页
                nowIndex = 1;
                Console.WriteLine("pageMaster.changePage():当前页面为新建页 nowIndex:" + nowIndex);
            }
            else if (pageType == typeof(setPage))
            {
                //如果是设置页
                nowIndex = 2;
                Console.WriteLine("pageMaster.changePage():当前页面为设置页 nowIndex:" + nowIndex);
            }
            //↑

            if(nowIndex != 0) //如果打开了任何页面
            {
                Console.WriteLine("pageMaster.changePage():开始执行播放展示返回按钮动画 MainWin.showBackButtonAn.Begin()");
                MainWin.showBackButtonAn.Begin();
            }
            else
            {
                Console.WriteLine("pageMaster.changePage():开始执行播放隐藏返回按钮动画 MainWin.hideBackButtonAn.Begin()");
                MainWin.hideBackButtonAn.Begin();
            }
            MainWin.changePageAn.Begin(); //播放切换页面的动画
            MainFrame.Navigate(target); //切换到目标页面
            MainFrame.NavigationService.RemoveBackEntry(); //销毁所有的frame视图
            Console.WriteLine("pageMaster.changePage():已经切换至目标页面并销毁缓存");
        }//切换页面所需要的函数

        public static void refushItems()
        {
            if(items != null)
            {
                foreach (var i in items)
                {
                    i.Close();
                }
            }
            items.Clear();
            for (int i = 0; i < ArchiveMaster.itemInfos.Count; i++)
            {
                items.Add(new itemWindow(i));
                items[i].Show();
            }
        }
    }
}

