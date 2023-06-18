namespace CTMS.Managers;

public class PersonsManager : Manager
{
    /// <summary>
    /// 人员配置文件名
    /// </summary>
    protected string? personsFileName;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="inProjectFileName">新<see cref="projectFileName"/></param>
    public PersonsManager(string? inPersonsFileName)
    {
        personsFileName = inPersonsFileName;
    }

    /// <summary>
    /// 析构函数
    /// 清理工作在<see cref="Dispose"/>实现
    /// </summary>
    ~PersonsManager()
    {
        Dispose();
    }

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
    /// 释放函数
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();
        personsFileName = null;
    }

    /// <summary>
    /// 创建项目
    /// </summary>
    /// <param name="data">写入数据</param>
    public void CreareProject(string[] data) => PersonConfig = data;

    /// <summary>
    /// 删除项目
    /// </summary>
    public void DeleteProject() => base.DeleteFile("./Databases/", personsFileName, ".persons");
}