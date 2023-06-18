using CTMS.BaseClasses;
using CTMS.Managers;
using System.Diagnostics;

// 页面管理组命名空间
namespace CTMS.PageGroups;

/// <summary>
/// 主类
/// </summary>
[Kind("主页面管理组")]
public sealed class MainPageGroup : PageGroup, IConcreteShowable
{
    /// <summary>
    /// 主函数
    /// </summary>
    [DebuggerHidden]
    public static void Main()
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Title = "班级教学统一管理系统";
            // 主页面
            using (MainPageGroup mainMenu = new MainPageGroup())
            {
                if (!OperatingSystem.IsWindows())
                {
                    throw new UnifyException("此程序只支持于Windows操作系统", mainMenu.GetType());
                }
                mainMenu.GuardedPageMain();
            }
        }
        catch (UnifyException)
        {
        }
    }

    /// <summary>
    /// 页面管理组主函数
    /// </summary>
    public void GuardedPageMain()
    {
        while (exit != true)
        {
            Set(
                new string[]
                {
                    "班级教学统一管理系统",
                    "著作权人 Mingxin.Fei，保留所有权利"
                }
            );
            Set("输入栏");
            SwitchShow(
                new string[]
                {
                    "退出",
                    "创建人员项目",
                    "删除人员项目",
                    "成绩管理",
                    "作业管理"
                },
                new SwitchProcessor[]
                {
                    () => exit = true,
                    CreateProject,
                    DeleteProject,
                    () =>
                    {
                        using (var manageMenu = new ScorePageGroup())
                        {
                            manageMenu.GuardedPageMain();
                        }
                    },
                    () =>
                    {
                        using (var manageMenu = new StatusPageGroup())
                        {
                            manageMenu.GuardedPageMain();
                        }
                    }
                }
            );
        }
        GC.Collect();
    }

    /// <summary>
    /// 创建人员项目
    /// </summary>
    /// <exception cref="UnifyException"></exception>
    public void CreateProject()
    {
        // 页面三
        Set(
            new string[] {
                "请输入人员项目配置文件名",
                "（*.persons文件，不带后缀名，且此文件必须在程序./Databases/目录下，否则会导致程序崩溃）"
            }
        );
        Set("输入栏");
        Show(
            (object fileNameTemp) =>
            {
                string nameTemp;
                List<string> personsTemp = new List<string>();
                while (true)
                {
                    // 页面三
                    Set(
                        new string[] {
                            "请输入人员名称",
                        }
                    );
                    Set("输入栏");
                    nameTemp = (string)Show(false);
                    if (nameTemp == "None")
                    {
                        break;
                    }
                    personsTemp.Add(nameTemp);
                }
                // 创建项目
                using (var processWorker = new PersonsManager((string)fileNameTemp))
                {
                    processWorker.CreareProject(personsTemp.ToArray());
                }
                return null;
            }
        );
        // 页面五
        Set(
            new string[] {
                "创建完成！"
            }
        );
        Block();
    }

    /// <summary>
    /// 删除项目
    /// </summary>
    /// <exception cref="UnifyException"></exception>
    protected override void DeleteProject()
    {
        Set(
            new string[] {
                "请输入人员配置文件名",
                "（*.persons文件，不带后缀名，且此文件必须在程序./Databases/目录下，否则会导致程序崩溃）"
            }
        );
        Set("输入栏");
        personsFileName = (string)Show(false);
        using (var processWorker = new PersonsManager(personsFileName))
        {
            processWorker.DeleteProject();
        }
        Set(
            new string[] {
                "删除完成！"
            }
        );
        Block();
    }
}
