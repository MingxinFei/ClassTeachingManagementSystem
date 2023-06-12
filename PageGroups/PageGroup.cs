using CTMS.BaseClasses;
using CTMS.Managers;

// 页面管理组命名空间
namespace CTMS.PageGroups;

/// <summary>
/// 页面管理组
/// </summary>
[Kind("基页面管理组")]
public abstract class PageGroup : PageEx
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
    /// 是否退出
    /// </summary>
    protected bool exit;

    /// <summary>
    /// 编辑页面是否退出
    /// </summary>
    protected bool editorExit;

    /// <summary>
    /// 构造函数
    /// </summary>
    public PageGroup()
    {
        personsFileName = null;
        projectFileName = null;
        exit = false;
        editorExit = false;
    }

    /// <summary>
    /// 析构函数
    /// 清理工作在<see cref="Dispose"/>实现
    /// </summary>
    ~PageGroup()
    {
        Dispose();
    }

    /// <summary>
    /// 释放函数
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();
        personsFileName = null;
        projectFileName = null;
    }

    /// <summary>
    /// 加载项目
    /// </summary>
    protected virtual void LoadProject()
    {
        // 页面一
        Set(
            new string[] {
                "请输入项目配置文件名",
                "（*.managed文件，不带后缀名，且此文件必须在程序./Databases/Projects/目录下，否则会导致程序崩溃）"
            }
        );
        Set("输入栏");
        Show(
            (object projectFileNameTemp) =>
            {
                if ((string)projectFileNameTemp == "None")
                {
                    projectFileName = null;
                }
                else
                {
                    projectFileName = (string)projectFileNameTemp;
                }
                return null;
            },
            false
        );
    }

    /// <summary>
    /// 删除项目
    /// </summary>
    /// <exception cref="UnifyException"></exception>
    protected virtual void DeleteProject()
    {
        using (var processWorker = new StatusManager(projectFileName, personsFileName))
        {
            try
            {
                File.Delete("./Databases/Projects/" + projectFileName + ".managed");
            }
            catch (DirectoryNotFoundException)
            {
                throw new UnifyException("文件未找到", GetType());
            }
            catch (PathTooLongException)
            {
                throw new UnifyException("路径过长", GetType());
            }
        }
        Set(
            new string[] {
                "删除完成！"
            }
        );
        Block();
    }
}
