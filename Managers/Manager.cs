using CTMS.BaseClasses;

// 管理器命名空间
namespace CTMS.Managers;

/// <summary>
/// 管理器
/// </summary>
[Kind("基管理器")]
public class Manager : UnifyObject, IDisposable
{
    /// <summary>
    /// 析构函数
    /// 清理工作在<see cref="Dispose"/>实现
    /// </summary>
    ~Manager()
    {
        Dispose();
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
        isDisposed = true;
    }

    /// <summary>
    /// 读取文件
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="fileName">文件名</param>
    /// <param name="fileFormat">文件后缀名</param>
    /// <returns>文件数据</returns>
    /// <exception cref="UnifyException"></exception>
    protected string[] ReadFile(string? filePath, string? fileName, string? fileFormat)
    {
        Check(filePath);
        Check(fileName);
        Check(fileFormat);
        try
        {
            return File.ReadAllLines(filePath + fileName + fileFormat);
        }
        catch (FileNotFoundException)
        {
            throw new UnifyException("文件未找到", GetType());
        }
        catch (DirectoryNotFoundException)
        {
            throw new UnifyException("路径未找到", GetType());
        }
        catch (PathTooLongException)
        {
            throw new UnifyException("路径过长", GetType());
        }
        catch
        {
            throw new UnifyException("未知错误", GetType());
        }
    }

    /// <summary>
    /// 写入文件
    /// </summary>
    /// <param name="value">文件数据</param>
    /// <param name="filePath">文件路径</param>
    /// <param name="fileName">文件名</param>
    /// <param name="fileFormat">文件后缀名</param>
    /// <exception cref="UnifyException"></exception>
    protected void WriteFile(string[]? value, string? filePath, string? fileName, string? fileFormat)
    {
        Check(value);
        Check(filePath);
        Check(fileName);
        Check(fileFormat);
        try
        {
            File.WriteAllLines(filePath + fileName + fileFormat, value);
        }
        catch (FileNotFoundException)
        {
            throw new UnifyException("文件未找到", GetType());
        }
        catch (DirectoryNotFoundException)
        {
            throw new UnifyException("路径未找到", GetType());
        }
        catch (PathTooLongException)
        {
            throw new UnifyException("路径过长", GetType());
        }
        catch
        {
            throw new UnifyException("未知错误", GetType());
        }
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="fileName">文件名</param>
    /// <param name="fileFormat">文件后缀名</param>
    /// <exception cref="UnifyException"></exception>
    public void DeleteFile(string? filePath, string? fileName, string? fileFormat)
    {
        try
        {
            File.Delete(filePath + fileName + fileFormat);
        }
        catch (DirectoryNotFoundException)
        {
            throw new UnifyException("路径未找到", GetType());
        }
        catch (PathTooLongException)
        {
            throw new UnifyException("路径过长", GetType());
        }
        catch
        {
            throw new UnifyException("未知错误", GetType());
        }
    }
}
