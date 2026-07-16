using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


/* 储存格式示例：
 * ——DaysMatter倒数日储存的信息——
 * xxx|2000-1-1|X|Y|BR|BG|BB|FR|FG|FB|A|S
 * ...
*/


namespace daysMatterr
{
    public class ArchiveMaster //文档管理器，负责读写每个倒数日的数据
    {
        public static string appPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location); //取当前工作目录
        public static string infosPath = appPath + @"\infos.inf"; //取倒数日小组件信息的储存位置


        public static List<item> itemInfos { get; set; } = new List<item>();//这是一个公开的集合,用于存放每一个倒数日小组件的信息


        public static int loadArchive() //读取储存的倒数日小组件信息  (-2:不是正确小组件文件格式 -1:权限不够 0:没找到文件 1:成功)
        {
            itemInfos.Clear();
            string[] itemInfosStr;
            bool isTargetFile = false;
            try
            {
                Console.WriteLine("loadArchive:开始尝试读取小组件信息");
                if (File.Exists(infosPath)) //如果小组件信息存在
                {
                    Console.WriteLine("loadArchive:小组件文件存在");
                    itemInfosStr = File.ReadAllLines(infosPath); //尝试读取小组件信息 并储存在字符串数组中
                    if (itemInfosStr[0] == "——DaysMatter倒数日储存的信息——") //判断是不是小组件储存信息的文件
                    {
                        Console.WriteLine("loadArchive:此文件属于正统的储存信息 isTargetFile:True");
                        isTargetFile = true; //是我们要找的储存文件，直接批准
                    }

                    if (isTargetFile) //如果是小组件文件
                    {
                        try
                        {
                            for (int i = 1; i < itemInfosStr.Length; i++) //直接让i=1 跳过读取文件头
                            {
                                string[] infos = itemInfosStr[i].Split('|'); //按照|来分割字符串
                                var result = new item(); //新建一个模板
                                result.title = infos[0]; //设置标题
                                result.date = Convert.ToDateTime(infos[1]); //设置日期
                                result.x = int.Parse(infos[2]); //设置x坐标
                                result.y = int.Parse(infos[3]); //设置y坐标
                                result.backGroundColor = System.Windows.Media.Color.FromArgb(255, byte.Parse(infos[4]), byte.Parse(infos[5]), byte.Parse(infos[6])); //设置背景颜色
                                result.fontColor = System.Windows.Media.Color.FromArgb(255, byte.Parse(infos[7]), byte.Parse(infos[8]), byte.Parse(infos[9])); //设置前景颜色
                                result.a = int.Parse(infos[10]); //设置透明度
                                result.s = int.Parse(infos[11]); //设置大小模式
                                itemInfos.Add(result); //添加结果
                                Console.WriteLine("loadArchive:title:" + result.title + "|date:" + result.date + "|X:" + result.x + "|Y:" + result.y + "|BR:" + result.backGroundColor.R + "|BG:" + result.backGroundColor.G + "|BB:" + result.backGroundColor.B + "|FR:" + result.fontColor.R + "|FG:" + result.fontColor.G + "|FB:" + result.fontColor.B + "|A" + result.a + "|S" + result.s);
                            }
                        }
                        catch
                        {
                            Console.WriteLine("loadArchive:执行读取操作失败 (-3)"); //版本更新导致的格式变动
                            return -3;
                        }
                    }
                    else
                    {
                        Console.WriteLine("loadArchive:读取失败 找到了小组件文件但不是正确的格式 (-2)");
                        return -2;
                    }

                    Console.WriteLine("loadArchive:读取小组件信息成功 (1)");
                    return 1; //成功
                }
                else //遇到没找到文件时
                {
                    Console.WriteLine("loadArchive:没找到小组件文件 (0)");
                    return 0; 
                }
            }
            catch //遇到执行失败时(例如管理员权限不够)
            {
                Console.WriteLine("loadArchive:执行读取操作失败 (-1)");
                return -1;
            }
        }

        public static int writeArchive() //写入储存的倒数日小组件信息 (0:失败 1:成功)
        {
            Console.WriteLine("writeArchive:尝试写入文件");
            try
            {
                string head = "——DaysMatter倒数日储存的信息——"; //写入的头文本
                string[] itemInfosStr = new string[itemInfos.Count + 1];
                itemInfosStr[0] = head;
                if (itemInfos.Count > 0) //如果itemInfos里面不是啥也没有
                {
                    for (int i = 1; i < itemInfos.Count+1; i++)
                    {
                        itemInfosStr[i] = itemInfos[i - 1].title + "|" + itemInfos[i - 1].date.ToString() + "|" + itemInfos[i-1].x.ToString() + "|" + itemInfos[i-1].y.ToString() + "|" + itemInfos[i-1].backGroundColor.R.ToString() + "|" + itemInfos[i - 1].backGroundColor.G.ToString() + "|" + itemInfos[i - 1].backGroundColor.B.ToString() + "|" + itemInfos[i - 1].fontColor.R.ToString() + "|" + itemInfos[i - 1].fontColor.G.ToString() + "|" + itemInfos[i - 1].fontColor.B.ToString() + "|" + itemInfos[i - 1].a.ToString() + "|" + itemInfos[i - 1].s.ToString(); //把其拼接成字符串
                        Console.WriteLine("writeArchive:" + itemInfosStr[i]);
                    }
                }
                else
                {
                    Console.WriteLine("writeArchive:itemInfos里面啥也没有");
                }
                File.WriteAllLines(infosPath, itemInfosStr); //尝试写入文件
                Console.WriteLine("writeArchive:写入文件成功");
                return 1;
            }
            catch (Exception er)
            {
                Console.WriteLine("writeArchive:文件写入失败:" + er);
                //然后进行关闭操作（这个dlg变量可以直接省略但是保留调试）
                var dlg = new dialog("错误", "文件写入失败，请联系开发者。\n" + er).ShowDialog(); //展示错误
                //返回值0
                //返回值0表示错误信息
                return 0;
            }
        }
    }
    
}
