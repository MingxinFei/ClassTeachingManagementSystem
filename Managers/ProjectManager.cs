namespace CTMS.Managers;

public class ProjectManager : Manager
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
    /// 构造函数
    /// </summary>
    /// <param name="inProjectFileName">新<see cref="projectFileName"/></param>
    /// <param name="inPersonsFileName">新<see cref="personsFileName"/></param>
    public ProjectManager(string? inProjectFileName, string? inPersonsFileName)
    {
        projectFileName = inProjectFileName;
        personsFileName = inPersonsFileName;
    }

    /// <summary>
    /// 析构函数
    /// 清理工作在<see cref="Dispose"/>实现
    /// </summary>
    ~ProjectManager()
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
    /// 释放函数
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();
        projectFileName = null;
        personsFileName = null;
        projectInfo = null;
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
    /// <param name="data">写入数据</param>
    public void CreateProject(string data)
    {
        List<string> temp = new List<string>();
        foreach (string? person in PersonConfig)
        {
            temp.Add($"{person}:{data}");
        }
        ProjectConfig = temp.ToArray();
    }

    /// <summary>
    /// 删除项目
    /// </summary>
    public void DeleteProject() => DeleteFile("./Databases/Project/", projectFileName, ".managed");
}