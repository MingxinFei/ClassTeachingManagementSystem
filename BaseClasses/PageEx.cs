// 基础类命名空间
namespace CTMS.BaseClasses;

/// <summary>
/// <see cref="Page"/>的扩展类
/// </summary>
[Kind("扩展控制台页面")]
public class PageEx : Page
{
    /// <summary>
    /// 显示页面并处理
    /// </summary>
    /// <param name="thisProcessor">委托</param>
    /// <param name="isGetChar">是否仅获取字符</param>
    /// <exception cref="UnifyException"></exception>
    public void Show(Processor thisProcessor, bool isGetChar = true)
    {
        Check(texts);
        Check(inputText);
        Check(thisProcessor);
        try
        {
            Clear();
            Console.WriteLine();
            foreach (string line in texts)
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
            thisProcessor.Invoke(temp);
        }
        catch (IOException)
        {
            throw new UnifyException("输入输出错误", GetType());
        }
    }
    /// <summary>
    /// 显示分支页面并处理
    /// </summary>
    /// <param name="thisTexts">分支选项</param>
    /// <param name="thisProcessors">委托数组</param>
    public void SwitchShow(string[] thisTexts, SwitchProcessor[] thisProcessors)
    {
        Check(texts);
        Check(inputText);
        Check(thisTexts);
        Check(thisProcessors);
        if (thisTexts.Length >= 10)
        {
            throw new UnifyException("分支页面选项数量过多", GetType());
        }
        List<string> casesTemp = new List<string>();
        int pageIndex = -1;
        for (int index = 0; index < thisTexts.Length; index++)
        {
            casesTemp.Add($"{index}键：" + thisTexts[index]);
        }
        Clear();
        Console.WriteLine();
        Set(texts.Concat(casesTemp.ToArray()).ToArray());
        while (true)
        {
            try
            {
                Show(
                    (object pageIndexTemp) =>
                    {
                        pageIndex = Convert.ToInt32("" + pageIndexTemp);
                        return null;
                    }
                );
                if (pageIndex > thisProcessors.Length - 1)
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
        thisProcessors[pageIndex].Invoke();
        GC.Collect();
    }
    /// <summary>
    /// 普通页面所使用的委托
    /// </summary>
    /// <param name="datas">输入数据</param>
    public delegate object Processor(object datas);
    /// <summary>
    /// 多分支页面所使用的委托
    /// </summary>
    public delegate void SwitchProcessor();
}
