using CTMS.BaseClasses;

// 管理器命名空间
namespace CTMS.Managers;

/// <summary>
/// 成绩管理器
/// </summary>
[Kind("成绩管理器")]
public class ScoreManager : ProjectManager, IConcreteManageable
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="inProjectFileName">项目配置文件</param>
    /// <param name="inPersonsFileName">人员配置文件</param>
    /// <exception cref="UnifyException"></exception>
    public ScoreManager(string inProjectFileName, string inPersonsFileName) :
        base(inProjectFileName, inPersonsFileName)
    { }

    /// <summary>
    /// 创建项目
    /// </summary>
    /// <exception cref="UnifyException"></exception>
    public void CreateProject() => CreateProject("None");

    /// <summary>
    /// 检查项目配置文件格式是否正确
    /// </summary>
    /// <exception cref="UnifyException"></exception>
    public void CheckFormat()
    {
        string[] project = ProjectConfig;
        string[] temp;
        foreach (string line in project)
        {
            temp = line.Split(':');
            if (temp[1] == "None")
            {
                continue;
            }
            try
            {
                Convert.ToInt32(temp[1]);
            }
            catch (SystemException)
            {
                throw new UnifyException("数据可能被损坏或为其他类型的项目", GetType());
            }
        }
    }

    /// <summary>
    /// 设置成绩
    /// </summary>
    /// <param name="index">人员序号</param>
    /// <param name="value">成绩字符串</param>
    /// <exception cref="UnifyException"></exception>
    public void SetScore(int index, string value)
    {
        string[] project = ProjectConfig;
        try
        {
            Convert.ToInt32(value);
        }
        catch (SystemException)
        {
            throw new UnifyException("数据可能被损坏", GetType());
        }
        string[] projectDataTemp = project[index].Split(':');
        projectDataTemp[1] = value;
        project[index] = projectDataTemp[0] + ":" + projectDataTemp[1];
        ProjectConfig = project;
    }

    /// <summary>
    /// 获取平均成绩
    /// </summary>
    /// <returns>平均成绩字符串</returns>
    /// <exception cref="UnifyException"></exception>
    public string GetAverageScore()
    {
        string[] project = ProjectConfig;
        float sum = 0;
        int personSum = 0;
        string lineTemp;
        foreach (string line in project)
        {
            lineTemp = line.Split(':')[1];
            if (lineTemp == "None")
            {
                continue;
            }
            try
            {
                sum += Convert.ToSingle(lineTemp);
                personSum++;
            }
            catch (SystemException)
            {
                throw new UnifyException("数据可能被损坏", GetType());
            }
        }
        if (personSum == 0)
        {
            return "未写入";
        }
        return Convert.ToString(sum / personSum) + "分";
    }
}
