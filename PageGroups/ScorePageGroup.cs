using CTMS.BaseClasses;
using CTMS.Managers;

// 页面管理组命名空间
namespace CTMS.PageGroups;

/// <summary>
/// 成绩管理页面管理组
/// </summary>
[Kind("成绩管理页面管理组")]
public class ScorePageGroup : PageGroup, IEditorShowable
{
    /// <summary>
    /// 加载项目
    /// </summary>
    protected override void LoadProject()
    {
        base.LoadProject();
        if (projectFileName == null)
        {
            return;
        }
        using (var processWorker = new ScoreManager(projectFileName, personsFileName))
        {
            processWorker.CheckFormat();
        }
    }

    /// <summary>
    /// 创建项目
    /// </summary>
    /// <exception cref="UnifyException"></exception>
    public void CreateProject()
    {
        // 页面三
        Set(
            new string[]
            {
                "请输入成绩项目配置文件名",
                "（*.managed文件，不带后缀名，且此文件必须在程序./Databases/Projects/目录下，否则会导致程序崩溃）"
            }
        );
        Set("输入栏");
        string projectFileNameTemp = (string)Show(false);
        // 页面四
        Set(
            new string[]
            {
                "请输入人员配置文件名",
                "（*.persons文件，不带后缀名，且此文件必须在程序./Databases/目录下，否则会导致程序崩溃）"
            }
        );
        Set("输入栏");
        string personsFileNameTemp = (string)Show(false);
        // 创建项目
        using (var processWorker = new ScoreManager(projectFileNameTemp, personsFileNameTemp))
        {
            processWorker.CreateProject();
        }
        // 页面五
        Set(
            new string[]
            {
                "创建完成！"
            }
        );
        Block();
    }

    /// <summary>
    /// 查看项目
    /// </summary>
    public void ReadProject()
    {
        while (editorExit != true)
        {
            using (var processWorker = new ScoreManager(projectFileName, personsFileName))
            {
                Set(
                    new string[]
                    {
                        "成绩项目编辑器"
                    }.Concat(processWorker.GenerateProjectInfo()).ToArray()
                );
                Set("输入栏");
            }
            SwitchShow(
                new string[]
                {
                    "返回",
                    "编辑项目"
                },
                new PageEx.SwitchProcessor[]
                {
                    () => editorExit = true,
                    EditProject
                }
            );
        }
        editorExit = false;
    }

    /// <summary>
    /// 编辑项目
    /// </summary>
    /// <exception cref="UnifyException"></exception>
    public void EditProject()
    {
        using (var processWorker = new ScoreManager(projectFileName, personsFileName))
        {
            try
            {
                Set(
                    new string[]
                    {
                        "成绩项目编辑器"
                    }.Concat(processWorker.GenerateProjectInfo()).ToArray()
                );
                Set("请输入序号");
                int personIndexTemp = Convert.ToInt32((string)Show(false));
                Set(
                    new string[]
                    {
                        $"序号{personIndexTemp}"
                    }
                );
                Set("请输入成绩");
                string scoreTemp = (string)Show(false);
                processWorker.SetScore(personIndexTemp, scoreTemp);
            }
            catch (SystemException)
            {
                throw new UnifyException("数据可能被损坏", GetType());
            }
        }
    }

    /// <summary>
    /// 查看平均成绩
    /// </summary>
    /// <exception cref="UnifyException"></exception>
    private void GetAverageScore()
    {
        // 获取成绩
        using (var processWorker = new ScoreManager(projectFileName, personsFileName))
        {
            string scoreTemp = processWorker.GetAverageScore();
            // 页面三
            Set(
                new string[]
                {
                    "班级平均分为" + scoreTemp
                }
            );
            scoreTemp = null;
        }
        Block();
    }

    /// <summary>
    /// 页面管理组主函数
    /// </summary>
    public void GuardedPageMain()
    {
        LoadProject();
        while (exit != true)
        {
            Set(
                new string[]
                {
                    "班级教学统一管理系统 - 成绩管理部分",
                    "著作权人 Mingxin.Fei，保留所有权利"
                }
            );
            Set("输入栏");
            SwitchShow(
                new string[]
                {
                    "返回",
                    "重新加载",
                    "创建项目",
                    "删除项目",
                    "查看项目",
                    "查看平均分"
                },
                new PageEx.SwitchProcessor[]
                {
                    () => exit = true,
                    LoadProject,
                    CreateProject,
                    DeleteProject,
                    ReadProject,
                    GetAverageScore
                }
            );
        }
        exit = false;
    }
}

