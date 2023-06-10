using CTMS.BaseClasses;
using CTMS.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

// 页面组命名空间
namespace CTMS.PageGroups
{
    /// <summary>
    /// 主类
    /// </summary>
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
                using (MainPageGroup MainMenu = new MainPageGroup())
                {
                    MainMenu.GuardedPageMain();
                }
            }
            catch (UnifyException)
            {
            }
        }
        /// <summary>
        /// 页面组主函数
        /// </summary>
        public void GuardedPageMain()
        {
            while (Exit == false)
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
                        () => Exit = true,
                        CreateProject,
                        DeleteProject,
                        () =>
                        {
                            using (ScorePageGroup ScoreMenu = new ScorePageGroup())
                            {
                                ScoreMenu.GuardedPageMain();
                            }
                        },
                        () =>
                        {
                            using (StatusPageGroup StatusMenu = new StatusPageGroup())
                            {
                                StatusMenu.GuardedPageMain();
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
                (object FileNameTemp) =>
                {
                    string NameTemp;
                    List<string> PersonsTemp = new List<string>();
                    while (true)
                    {
                        // 页面三
                        Set(
                            new string[] {
                                "请输入人员名称",
                            }
                        );
                        Set("输入栏");
                        NameTemp = (string)Show(false);
                        if (NameTemp == "None")
                        {
                            break;
                        }
                        PersonsTemp.Add(NameTemp);
                    }
                    // 创建项目
                    using (Manager ProcessWorker = new Manager(null, (string)FileNameTemp))
                    {
                        ProcessWorker.SetPersonConfig(PersonsTemp.ToArray());
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
            PersonsFileName = (string)Show(false);
            try
            {
                File.Delete("./Databases/" + PersonsFileName + ".persons");
            }
            catch (DirectoryNotFoundException)
            {
                throw new UnifyException("文件未找到", "Page");
            }
            catch (PathTooLongException)
            {
                throw new UnifyException("路径过长", "Page");
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
