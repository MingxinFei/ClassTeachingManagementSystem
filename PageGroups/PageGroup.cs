using CTMS.BaseClasses;
using CTMS.Managers;
using System;
using System.IO;

// 页面组命名空间
namespace CTMS.PageGroups
{
    /// <summary>
    /// 管理页面组
    /// </summary>
    public abstract class PageGroup : PageEx
    {
        protected string PersonsFileName;
        protected string ProjectFileName;
        protected bool Exit;
        /// <summary>
        /// 构造函数
        /// </summary>
        public PageGroup()
        {
            PersonsFileName = null;
            ProjectFileName = null;
            Exit = false;
        }
        /// <summary>
        /// 析构函数
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
            PersonsFileName = null;
            ProjectFileName = null;
        }
        /// <summary>
        /// 加载项目
        /// </summary>
        protected virtual void LoadProject()
        {
            PersonsFileName = null;
            // 页面一
            Set(
                new string[] {
                    "请输入项目配置文件名",
                    "（*.managed文件，不带后缀名，且此文件必须在程序./Databases/Projects/目录下，否则会导致程序崩溃）"
                }
            );
            Set("输入栏");
            Show(
                (object ProjectFileNameTemp) =>
                {
                    if ((string)ProjectFileNameTemp == "None")
                    {
                        ProjectFileName = null;
                    }
                    else
                    {
                        ProjectFileName = (string)ProjectFileNameTemp;
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
            using (var ProcessWorker = new StatusManager(ProjectFileName, PersonsFileName))
            {
                try
                {
                    File.Delete("./Databases/Projects/" + ProjectFileName + ".managed");
                }
                catch (DirectoryNotFoundException)
                {
                    throw new UnifyException("文件未找到", "Page");
                }
                catch (PathTooLongException)
                {
                    throw new UnifyException("路径过长", "Page");
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
}
