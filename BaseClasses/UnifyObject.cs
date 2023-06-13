using System.Runtime.CompilerServices;

namespace CTMS.BaseClasses;

public class UnifyObject
{
    /// <summary>
    /// 是否已释放
    /// </summary>
    protected bool isDisposed;

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
}
