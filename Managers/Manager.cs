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
    /// 项目配置文件名
    /// </summary>
    protected string? projectFileName;

    /// <summary>
    /// 人员配置文件名
    /// </summary>
    protected string? personsFileName;

    /// <summary>
    /// 项目信息
    /// </summary>
    private string[]? projectInfo;

    /// <summary>
    /// 人员配置文件数据
    /// </summary>
    public string[]? PersonConfig
    {
        get
        {
            return ReadFile("./Databases/", personsFileName, ".persons");
        }
        set
        {
            WriteFile(value, "./Databases/", personsFileName, ".persons");
        }
    }

    /// <summary>
    /// 项目配置文件数据
    /// </summary>
    public string[]? ProjectConfig
    {
        get
        {
            return ReadFile("./Databases/Projects/", projectFileName, ".managed");
        }
        set
        {
            WriteFile(value, "./Databases/Projects/", projectFileName, ".managed");
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="inProjectFileName">新<see cref="projectFileName"/></param>
    /// <param name="inPersonsFileName">新<see cref="personsFileName"/></param>
    /// <exception cref="UnifyException"></exception>
    public Manager(string? inProjectFileName, string? inPersonsFileName)
    {
        projectFileName = inProjectFileName;
        personsFileName = inPersonsFileName;
        isDisposed = false;
    }

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
    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }
        GC.SuppressFinalize(this);
        projectFileName = null;
        personsFileName = null;
        projectInfo = null;
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
    private string[] ReadFile(string? filePath, string? fileName, string? fileFormat)
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
    private void WriteFile(string[]? value, string? filePath, string? fileName, string? fileFormat)
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
    /// 生成<see cref="projectInfo"/>
    /// </summary>
    /// <param name="isRespawn">是否重新生成</param>
    /// <returns>给用户显示的项目信息</returns>
    public string[] GenerateProjectInfo(bool isRespawn = false)
    {
        if (isRespawn || projectInfo == null)
        {
            string[] project = ProjectConfig;
            List<string> projectDataTemp = new List<string>();
            string[] projectLineTemp;
            for (int index = 0; index < project.Length; index++)
            {
                projectLineTemp = project[index].Split(':');
                projectDataTemp.Add($"序号{index}：{projectLineTemp[0]}：{projectLineTemp[1]}");
            }
            projectInfo = projectDataTemp.ToArray();
        }
        return projectInfo;
    }

    /// <summary>
    /// 创建项目
    /// </summary>
    /// <param name="data"></param>
    public void CreateProject(string data)
    {
        string[]? persons = PersonConfig;
        List<string> temp = new List<string>();
        foreach (string? line in persons)
        {
            temp.Add(line + ":" + data);
        }
        ProjectConfig = temp.ToArray();
    }

    /// <summary>
    /// 删除项目
    /// </summary>
    /// <exception cref="UnifyException"></exception>
    public void DeleteProject()
    {
        try
        {
            File.Delete("./Databases/Projects/" + projectFileName + ".managed");
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
