using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Reflection;

namespace daysMatterr
{
    public static class AutoStartHelper
    {
        // 注册表路径（当前用户，无需管理员）
        private const string RegPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
        // 自定义启动项名称，改成你的软件名字
        private const string AppName = "DaysMatter倒数日";

        /// <summary>
        /// 设置开机自启
        /// </summary>
        /// <param name="enable">true开启，false关闭</param>
        public static void SetAutoStart(bool enable)
        {
            string exePath = Assembly.GetEntryAssembly().Location;

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegPath, writable: true))
            {
                if (enable)
                {
                    // 路径带空格时，外层加英文双引号防止启动失败
                    key.SetValue(AppName, $"\"{exePath}\"");
                }
                else
                {
                    // 删除启动项，不存在也不会报错
                    key.DeleteValue(AppName, throwOnMissingValue: false);
                }
            }
        }

        /// <summary>
        /// 判断当前是否已经开启开机自启
        /// </summary>
        public static bool IsAutoStartOn()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegPath, writable: false))
            {
                return key.GetValue(AppName) != null;
            }
        }
    }
}
