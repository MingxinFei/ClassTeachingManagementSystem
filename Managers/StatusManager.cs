using CTMS.BaseClasses;

// 管理器命名空间
namespace CTMS.Managers;

/// <summary>
/// 作业管理器
/// </summary>
[Kind("作业管理器")]
public class StatusManager : Manager, IConcreteManageable
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="inProjectFileName">项目配置文件</param>
    /// <param name="inPersonsFileName">人员配置文件</param>
    /// <exception cref="UnifyException"></exception>
    public StatusManager(string inProjectFileName, string inPersonsFileName) :
        base(inProjectFileName, inPersonsFileName)
    { }

    /// <summary>
    /// 创建项目
    /// </summary>
    /// <exception cref="UnifyException"></exception>
    public void CreateProject()
    {
        string[]? persons = PersonConfig;
        List<string> temp = new List<string>();
        foreach (string? line in persons)
        {
            temp.Add(line + ":未合格");
        }
        ProjectConfig = temp.ToArray();
    }

    /// <summary>
    /// 检查项目配置文件格式是否正确
    /// </summary>
    /// <exception cref="UnifyException"></exception>
    public void CheckFormat()
    {
        string[] project = ProjectConfig;
        string temp;
        foreach (string line in project)
        {
            temp = line.Split(':')[1];
            if (temp != "已合格" && temp != "未合格")
            {
                throw new UnifyException("数据可能被损坏或为其他类型的项目", GetType());
            }
        }
    }

    /// <summary>
    /// 设置状态
    /// </summary>
    /// <param name="index">人员序号</param>
    /// <param name="value">状态</param>
    /// <exception cref="UnifyException"></exception>
    public void SetStatus(int index, bool value)
    {
        string[] project = ProjectConfig;
        string[] projectDataTemp = project[index].Split(':');
        if (value)
        {
            projectDataTemp[1] = "已合格";
        }
        else
        {
            projectDataTemp[1] = "未合格";
        }
        project[index] = projectDataTemp[0] + ":" + projectDataTemp[1];
        ProjectConfig = project;
    }

    /// <summary>
    /// 获取合格率
    /// </summary>
    /// <returns>合格率字符串</returns>
    public string GetQualifiedRate()
    {
        string[] project = ProjectConfig;
        int count = 0;
        foreach (string line in project)
        {
            if (line.Split(':')[1] == "已合格")
            {
                count++;
            }
        }
        return Convert.ToString(count / (float)project.Length * 100f) + "%";
    }
}
