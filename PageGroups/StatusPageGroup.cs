using CTMS.BaseClasses;
using CTMS.Managers;
using System;
using System.Linq;

// 页面组命名空间
namespace CTMS.PageGroups
{
    /// <summary>
    /// 成绩管理页面组
    /// </summary>
    public class StatusPageGroup : PageGroup, IEditorShowable
    {
        protected bool EditorExit;
        /// <summary>
        /// 构造函数
        /// </summary>
        public StatusPageGroup()
        {
            EditorExit = false;
        }
        /// <summary>
        /// 加载项目
        /// </summary>
        protected override void LoadProject()
        {
            base.LoadProject();
            if (ProjectFileName == null)
            {
                return;
            }
            using (var ProcessWorker = new StatusManager(ProjectFileName, PersonsFileName))
            {
                ProcessWorker.CheckFormat();
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
            string ProjectFileNameTemp = (string)Show(false);
            // 页面四
            Set(
                new string[]
                {
                    "请输入人员配置文件名",
                    "（*.persons文件，不带后缀名，且此文件必须在程序./Databases/目录下，否则会导致程序崩溃）"
                }
            );
            Set("输入栏");
            string PersonsFileNameTemp = (string)Show(false);
            // 创建项目
            using (var ProcessWorker = new StatusManager(ProjectFileNameTemp, PersonsFileNameTemp))
            {
                ProcessWorker.CreateProject();
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
            while (EditorExit != true)
            {
                using (var ProcessWorker = new StatusManager(ProjectFileName, PersonsFileName))
                {
                    Set(
                        new string[]
                        {
                            "作业项目编辑器"
                        }.Concat(ProcessWorker.GenerateProjectInfo()).ToArray()
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
                        () => EditorExit = true,
                        EditProject
                    }
                );
            }
            EditorExit = false;
        }
        /// <summary>
        /// 编辑项目
        /// </summary>
        /// <exception cref="UnifyException"></exception>
        public void EditProject()
        {
            using (var ProcessWorker = new StatusManager(ProjectFileName, PersonsFileName))
            {
                Set(
                    new string[]
                    {
                        "作业项目编辑器"
                    }.Concat(ProcessWorker.GenerateProjectInfo()).ToArray()
                );
                Set("请输入序号");
                int PersonIndexTemp = Convert.ToInt32((string)Show(false));
                Set(
                    new string[]
                    {
                        $"序号{PersonIndexTemp}"
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
                        () => ProcessWorker.SetStatus(PersonIndexTemp, false),
                        () => ProcessWorker.SetStatus(PersonIndexTemp, true)
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
            using (var ProcessWorker = new StatusManager(ProjectFileName, PersonsFileName))
            {
                string RateTemp = ProcessWorker.GetQualifiedRate();
                Set(
                    new string[]
                    {
                        "完成率为" + RateTemp
                    }
                );
                RateTemp = null;
            }
            Block();
        }
        /// <summary>
        /// 页面组主函数
        /// </summary>
        public void GuardedPageMain()
        {
            LoadProject();
            while (Exit != true)
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
                        () => Exit = true,
                        LoadProject,
                        CreateProject,
                        DeleteProject,
                        ReadProject,
                        GetQualifiedRate
                    }
                );
            }
            Exit = false;
        }
    }
}
