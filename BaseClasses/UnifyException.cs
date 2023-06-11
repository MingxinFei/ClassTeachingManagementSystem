﻿using System.Reflection;

// 基础类命名空间
namespace CTMS.BaseClasses;

/// <summary>
/// 此程序的统一处理异常
/// </summary>
[Kind("统一异常")]
public sealed class UnifyException : ApplicationException
{
    private Page errorPage;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="log">日志消息</param>
    /// <param name="type">抛出类</param>
    public UnifyException(string log, Type type) : base()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        var attr = type.GetCustomAttribute<KindAttribute>();
        errorPage = new Page(
            new string[]
            {
                $"由[{attr.Kind}]程序部分引发的运行时崩溃：",
                $"大致原因：{log}"
            },
            "按任意键退出"
        );
        errorPage.Show();
        errorPage = null;
    }
}
