using CTMS.BaseClasses;
using CTMS.Managers;

// 页面管理组命名空间
namespace CTMS.PageGroups;

/// <summary>
/// 作业管理页面管理组
/// </summary>
[Kind("作业管理页面管理组")]
public class StatusPageGroup : PageGroup, IEditorShowable
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
        using (var processWorker = new StatusManager(projectFileName, personsFileName))
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
                "请输入作业项目配置文件名",
                "（*.managed文件，不带后缀名，且此文件必须在程序./Databases/Projects/目录下，否则会导致程序崩溃）"
            }
        );
        Set("输入栏");
        projectFileName = (string)Show(false);
        // 页面四
        Set(
            new string[]
            {
                "请输入人员配置文件名",
                "（*.persons文件，不带后缀名，且此文件必须在程序./Databases/目录下，否则会导致程序崩溃）"
            }
        );
        Set("输入栏");
        personsFileName = (string)Show(false);
        // 创建项目
        using (var processWorker = new StatusManager(projectFileName, personsFileName))
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
    /// <exception cref="UnifyException"></exception>
    public void ReadProject()
    {
        while (editorExit != true)
        {
            using (var processWorker = new StatusManager(projectFileName, personsFileName))
            {
                Set(
                    new string[]
                    {
                        "作业项目编辑器"
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
        using (var processWorker = new StatusManager(projectFileName, personsFileName))
        {
            Set(
                new string[]
                {
                    "作业项目编辑器"
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
            Set("请输入合格状态");
            SwitchShow(
                new string[]
                {
                    "未合格",
                    "已合格"
                },
                new PageEx.SwitchProcessor[]
                {
                    () => processWorker.SetStatus(personIndexTemp, false),
                    () => processWorker.SetStatus(personIndexTemp, true)
                }
            );
        }
    }

    /// <summary>
    /// 查看合格率
    /// </summary>
    private void GetQualifiedRate()
    {
        // 获取成绩
        using (var processWorker = new StatusManager(projectFileName, personsFileName))
        {
            string rateTemp = processWorker.GetQualifiedRate();
            Set(
                new string[]
                {
                    "完成率为" + rateTemp
                }
            );
            rateTemp = null;
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
                    "班级教学统一管理系统 - 作业管理部分",
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
                    "查看合格率"
                },
                new PageEx.SwitchProcessor[]
                {
                    () => exit = true,
                    LoadProject,
                    CreateProject,
                    DeleteProject,
                    ReadProject,
                    GetQualifiedRate
                }
            );
        }
        exit = false;
    }
}
