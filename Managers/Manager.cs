using CTMS.BaseClasses;
using System.Runtime.CompilerServices;

// 管理器命名空间
namespace CTMS.Managers;

/// <summary>
/// 管理器
/// </summary>
[Kind("基管理器")]
public class Manager : IDisposable
{
    protected string projectFileName;
    protected string personsFileName;
    private string[] projectInfo;
    protected bool isDisposed;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="inProjectFileName">项目配置文件</param>
    /// <param name="inPersonsFileName">人员配置文件</param>
    /// <exception cref="UnifyException"></exception>
    public Manager(string inProjectFileName, string inPersonsFileName)
    {
        projectFileName = inProjectFileName;
        personsFileName = inPersonsFileName;
        isDisposed = false;
    }
    /// <summary>
    /// 析构函数
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
        isDisposed = true;
    }
    /// <summary>
    /// 检查对象是否为空
    /// </summary>
    /// <param name="data">检查对象</param>
    /// <exception cref="UnifyException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Check(object data)
    {
        if (data == null)
        {
            throw new UnifyException("检查到空对象", GetType());
        }
    }
    /// <summary>
    /// 设置人员配置文件数据
    /// </summary>
    /// <param name="texts">人员配置文件数据</param>
    /// <exception cref="UnifyException"></exception>
    public void SetPersonConfig(string[] texts)
    {
        Check(personsFileName);
        try
        {
            File.WriteAllLines("./Databases/" + personsFileName + ".persons", texts);
        }
        catch (FileNotFoundException)
        {
            throw new UnifyException("文件未找到", GetType());
        }
    }
    /// <summary>
    /// 获取人员配置文件数据
    /// </summary>
    /// <returns>人员配置文件数据</returns>
    /// <exception cref="UnifyException"></exception>
    public string[] GetPersonConfig()
    {
        Check(personsFileName);
        try
        {
            return File.ReadAllLines("./Databases/" + personsFileName + ".persons");
        }
        catch (FileNotFoundException)
        {
            throw new UnifyException("文件未找到", GetType());
        }
        catch
        {
            throw new UnifyException("未知错误", GetType());
        }
    }
    /// <summary>
    /// 设置项目配置文件数据
    /// </summary>
    /// <param name="texts">项目配置文件数据</param>
    /// <exception cref="UnifyException"></exception>
    public void SetProjectConfig(string[] texts)
    {
        Check(projectFileName);
        try
        {
            File.WriteAllLines("./Databases/Projects/" + projectFileName + ".managed", texts);
        }
        catch (FileNotFoundException)
        {
            throw new UnifyException("文件未找到", GetType());
        }
    }
    /// <summary>
    /// 获取项目配置文件数据
    /// </summary>
    /// <returns>项目配置文件数据</returns>
    /// <exception cref="UnifyException"></exception>
    public string[] GetProjectConfig()
    {
        Check(projectFileName);
        try
        {
            return File.ReadAllLines("./Databases/Projects/" + projectFileName + ".managed");
        }
        catch (FileNotFoundException)
        {
            throw new UnifyException("项目配置文件加载异常", typeof(Manager));
        }
    }
    /// <summary>
    /// 生成给用户显示的项目信息
    /// </summary>
    /// <param name="Project">项目配置文件数据</param>
    /// <param name="IsRespawn">是否重新生成</param>
    /// <returns>给用户显示的项目信息</returns>
    public string[] GenerateProjectInfo(bool IsRespawn = false)
    {
        if (IsRespawn || projectInfo == null)
        {
            string[] Project = GetProjectConfig();
            List<string> ProjectDataTemp = new List<string>();
            string[] ProjectLineTemp;
            for (int Index = 0; Index < Project.Length; Index++)
            {
                ProjectLineTemp = Project[Index].Split(':');
                ProjectDataTemp.Add($"序号{Index}：{ProjectLineTemp[0]}：{ProjectLineTemp[1]}");
            }
            projectInfo = ProjectDataTemp.ToArray();
        }
        return projectInfo;
    }
}
