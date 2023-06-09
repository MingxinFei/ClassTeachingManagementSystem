using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

// 基础类命名空间
namespace CTMS.BaseClasses
{
    /// <summary>
    /// 页面类
    /// </summary>
    public class Page : IDisposable
    {
        protected string[] Texts;
        protected string InputText;
        protected bool IsDisposed;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="NewTexts">输出文字内容</param>
        /// <param name="NewInputText">输入提示文字内容</param>
        /// <exception cref="UnifyException"></exception>
        public Page(string[] NewTexts, string NewInputText)
        {
            Check(NewTexts);
            Check(NewInputText);
            Texts = NewTexts;
            InputText = NewInputText;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        protected Page()
        {
            Texts = null;
            InputText = null;
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~Page()
        {
            Dispose();
        }
        /// <summary>
        /// 执行系统命令
        /// </summary>
        /// <param name="Command">系统命令字符串</param>
        public static void Clear()
        {
            using (var CmdProcess = new Process())
            {
                CmdProcess.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                CmdProcess.StartInfo.Arguments = "/c" + "cls";
                CmdProcess.StartInfo.CreateNoWindow = false;
                CmdProcess.StartInfo.UseShellExecute = false;
                CmdProcess.Start();
                CmdProcess.WaitForExit();
            }
        }
        /// <summary>
        /// 释放函数
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            GC.SuppressFinalize(this);
            Texts = null;
            InputText = null;
            IsDisposed = true;
        }
        /// <summary>
        /// 检查对象是否为空
        /// </summary>
        /// <param name="Data">检查对象</param>
        /// <exception cref="UnifyException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Check(object Data)
        {
            if (Data == null)
            {
                throw new UnifyException("检查到空对象", "Page");
            }
        }
        /// <summary>
        /// 设置页面内容数据
        /// </summary>
        /// <param name="NewTexts">页面内容数据</param>
        /// <exception cref="UnifyException"></exception>
        public void Set(string[] NewTexts)
        {
            Check(NewTexts);
            Texts = NewTexts;
        }
        /// <summary>
        /// 设置页面输入栏数据
        /// </summary>
        /// <param name="NewInputText">页面输入栏数据</param>
        /// <exception cref="UnifyException"></exception>
        public void Set(string NewInputText)
        {
            Check(NewInputText);
            InputText = NewInputText;
        }
        /// <summary>
        /// 阻塞程序
        /// </summary>
        /// <exception cref="UnifyException"></exception>
        public void Block()
        {
            try
            {
                Set("按任意键返回");
                Show();
            }
            catch (IOException)
            {
                throw new UnifyException("输入输出错误", "Page");
            }
        }
        /// <summary>
        /// 显示页面并获取输入
        /// </summary>
        /// <param name="IsGetChar">是否仅获取字符</param>
        /// <returns>获取输入</returns>
        /// <exception cref="UnifyException"></exception>
        public object Show(bool IsGetChar = true)
        {
            Check(Texts);
            Check(InputText);
            try
            {
                Page.Clear();
                Console.WriteLine();
                foreach (string Line in Texts)
                {
                    Console.WriteLine(" " + Line + "\n");
                }
                Console.Write(" " + InputText + "：");
                object Temp;
                if (IsGetChar)
                {
                    Temp = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    return Temp;
                }
                else
                {
                    Temp = Console.ReadLine();
                    Console.WriteLine();
                    return Temp;
                }
            }
            catch (IOException)
            {
                throw new UnifyException("输入输出错误", "Page");
            }
        }
        /// <summary>
        /// 显示页面
        /// </summary>
        /// <exception cref="UnifyException"></exception>
        public void Show()
        {
            Check(Texts);
            Check(InputText);
            try
            {
                Page.Clear();
                Console.WriteLine();
                foreach (string Line in Texts)
                {
                    Console.WriteLine(" " + Line + "\n");
                }
                Console.Write(" " + InputText + "：");
                Console.ReadKey();
                Console.WriteLine();
            }
            catch (IOException)
            {
                throw new UnifyException("输入输出错误", "Page");
            }
        }
        /// <summary>
        /// 显示页面并获取输入
        /// </summary>
        /// <param name="ThisProcessor">委托</param>
        /// <param name="IsGetChar">是否仅获取字符</param>
        /// <exception cref="UnifyException"></exception>
        public void Show(Processor ThisProcessor, bool IsGetChar = true)
        {
            Check(Texts);
            Check(InputText);
            try
            {
                Page.Clear();
                Console.WriteLine();
                foreach (string Line in Texts)
                {
                    Console.WriteLine(" " + Line + "\n");
                }
                Console.Write(" " + InputText + "：");
                object Temp;
                if (IsGetChar)
                {
                    Temp = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    ThisProcessor.Invoke(Temp);
                }
                else
                {
                    Temp = Console.ReadLine();
                    Console.WriteLine();
                    ThisProcessor.Invoke(Temp);
                }
            }
            catch (IOException)
            {
                throw new UnifyException("输入输出错误", "Page");
            }
        }
        /// <summary>
        /// 显示分支页面并处理
        /// </summary>
        /// <param name="ThisTexts">分支选项</param>
        /// <param name="ThisProcessors">委托数组</param>
        public void SwitchShow(string[] ThisTexts, SwitchProcessor[] ThisProcessors)
        {
            Check(ThisTexts);
            Check(Texts);
            Check(InputText);
            if (ThisTexts.Length >= 10)
            {
                throw new UnifyException("分支页面选项数量过多", "Page");
            }
            List<string> CasesTemp = new List<string>();
            int PageIndex = -1;
            for (int Index = 0; Index < ThisTexts.Length; Index++)
            {
                CasesTemp.Add($"{Index}键：" + ThisTexts[Index]);
            }
            Page.Clear();
            Console.WriteLine();
            Set(Texts.Concat(CasesTemp.ToArray()).ToArray());
            while (true)
            {
                try
                {
                    Show(
                        (object PageIndexTemp) =>
                        {
                            PageIndex = Convert.ToInt32("" + PageIndexTemp);
                            return null;
                        }
                    );
                    if (PageIndex > ThisProcessors.Length - 1)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                catch (SystemException)
                {
                    continue;
                }
            }
            Page.Clear();
            ThisProcessors[PageIndex].Invoke();
            GC.Collect();
        }
        /// <summary>
        /// 多分支页面所使用的委托
        /// </summary>
        public delegate void SwitchProcessor();
        /// <summary>
        /// 普通页面所使用的委托
        /// </summary>
        /// <param name="Datas">输入数据</param>
        public delegate object Processor(object Datas);
    }
}
