using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// 基础类命名空间
namespace CTMS.BaseClasses
{
    /// <summary>
    /// <see cref="Page"/>的扩展类
    /// </summary>
    public class PageEx : Page
    {
        /// <summary>
        /// 显示页面并处理
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
                Clear();
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
                }
                else
                {
                    Temp = Console.ReadLine();
                }
                Console.WriteLine();
                ThisProcessor.Invoke(Temp);
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
            Clear();
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
            Clear();
            ThisProcessors[PageIndex].Invoke();
            GC.Collect();
        }
        /// <summary>
        /// 普通页面所使用的委托
        /// </summary>
        /// <param name="Datas">输入数据</param>
        public delegate object Processor(object Datas);

        /// <summary>
        /// 多分支页面所使用的委托
        /// </summary>
        public delegate void SwitchProcessor();
    }
}
