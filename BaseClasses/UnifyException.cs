using System;

// 基础类命名空间
namespace CTMS.BaseClasses
{
    /// <summary>
    /// 此程序的统一处理异常
    /// </summary>
    public sealed class UnifyException : SystemException
    {
        private Page ErrorMenu;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Log">日志消息</param>
        /// <param name="Type">抛出类</param>
        public UnifyException(string Log, string Type) : base()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            ErrorMenu = new Page(
                new string[] {
                    $"[{Type} Error] 运行时崩溃，{Log}"
                },
                "按任意键退出"
            );
            ErrorMenu.Show();
            ErrorMenu = null;
        }
    }
}
