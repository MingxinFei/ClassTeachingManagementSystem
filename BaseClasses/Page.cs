using System.Diagnostics;
using System.Runtime.CompilerServices;

// 基础类命名空间
namespace CTMS.BaseClasses;

/// <summary>
/// 页面类
/// </summary>
[Kind("控制台页面")]
public class Page : IDisposable
{
    /// <summary>
    /// 输出文字内容
    /// </summary>
    protected string[]? texts;

    /// <summary>
    /// 输入提示文字内容
    /// </summary>
    protected string? inputText;

    /// <summary>
    /// 是否已释放
    /// </summary>
    protected bool isDisposed;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="inTexts">新<see cref="texts"/></param>
    /// <param name="inInputText">新<see cref="inputText"/></param>
    /// <exception cref="UnifyException"></exception>
    public Page(string[] inTexts, string inInputText)
    {
        Check(inTexts);
        Check(inInputText);
        texts = inTexts;
        inputText = inInputText;
    }

    /// <summary>
    /// 构造函数
    /// 所有属性均初始化为<see cref="null"/>
    /// </summary>
    protected Page()
    {
        texts = null;
        inputText = null;
    }

    /// <summary>
    /// 析构函数
    /// 清理工作在<see cref="Dispose"/>实现
    /// </summary>
    ~Page()
    {
        Dispose();
    }

    /// <summary>
    /// 清空控制台
    /// 使用<see cref="Process"/>实现
    /// </summary>
    public static void Clear()
    {
        using (var commandProcess = new Process())
        {
            commandProcess.StartInfo.FileName = "cmd.exe";
            commandProcess.StartInfo.Arguments = "/c" + "cls";
            commandProcess.StartInfo.CreateNoWindow = false;
            commandProcess.StartInfo.UseShellExecute = false;
            commandProcess.Start();
            commandProcess.WaitForExit();
        }
    }

    /// <summary>
    /// 释放函数
    /// </summary>
    public virtual void Dispose()
    {
        if (isDisposed)
        {
            return;
        }
        GC.SuppressFinalize(this);
        texts = null;
        inputText = null;
        isDisposed = true;
    }

    /// <summary>
    /// 检查对象是否为空
    /// </summary>
    /// <param name="data">检查对象</param>
    /// <exception cref="UnifyException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void Check(object? data)
    {
        if (data == null)
        {
            throw new UnifyException("检查到空对象", GetType());
        }
    }

    /// <summary>
    /// 设置<see cref="texts"/>
    /// </summary>
    /// <param name="inTexts">新<see cref="texts"/></param>
    /// <exception cref="UnifyException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(string[] inTexts)
    {
        Check(inTexts);
        texts = inTexts;
    }

    /// <summary>
    /// 设置<see cref="inputText"/>
    /// </summary>
    /// <param name="inInputText">新<see cref="inputText"/></param>
    /// <exception cref="UnifyException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(string inInputText)
    {
        Check(inInputText);
        inputText = inInputText;
    }

    /// <summary>
    /// 阻塞程序
    /// </summary>
    /// <exception cref="UnifyException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Block()
    {
        try
        {
            Set("按任意键返回");
            Show();
        }
        catch (IOException)
        {
            throw new UnifyException("输入输出错误", GetType());
        }
    }

    /// <summary>
    /// 显示页面并获取输入
    /// </summary>
    /// <param name="isGetChar">是否仅获取字符</param>
    /// <returns>获取输入</returns>
    /// <exception cref="UnifyException"></exception>
    public object? Show(bool isGetChar = true)
    {
        Check(texts);
        Check(inputText);
        try
        {
            Clear();
            Console.WriteLine();
            foreach (string? line in texts)
            {
                Console.WriteLine(" " + line + "\n");
            }
            Console.Write(" " + inputText + "：");
            object temp;
            if (isGetChar)
            {
                temp = Console.ReadKey().KeyChar;
            }
            else
            {
                temp = Console.ReadLine();
            }
            Console.WriteLine();
            return temp;
        }
        catch (IOException)
        {
            throw new UnifyException("输入输出错误", GetType());
        }
    }

    /// <summary>
    /// 显示页面
    /// </summary>
    /// <exception cref="UnifyException"></exception>
    public void Show()
    {
        Check(texts);
        Check(inputText);
        try
        {
            Clear();
            Console.WriteLine();
            foreach (string? line in texts)
            {
                Console.WriteLine(" " + line + "\n");
            }
            Console.Write(" " + inputText + "：");
            Console.ReadKey();
            Console.WriteLine();
        }
        catch (IOException)
        {
            throw new UnifyException("输入输出错误", GetType());
        }
    }
}
