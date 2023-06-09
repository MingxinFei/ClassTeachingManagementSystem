using System.Collections.Generic;
using System.Linq;

// 基础类命名空间
namespace CTMS.BaseClasses
{
    /// <summary>
    /// <see cref="Page"/>的扩展类，专用于编辑项目
    /// </summary>
    public class PageEx : Page
    {
        private string[] ProjectStrings;
        /// <summary>
        /// 显示分支页面并处理
        /// </summary>
        /// <param name="Project">项目配置文件字符串</param>
        public void SwitchShow(string[] Project, SwitchProcessor[] Processors, bool IsRespawn = true)
        {
            Check(Project);
            Check(Texts);
            Check(InputText);
            // 重新生成，一般在使用此函数时项目用户文本并非最新值
            Set(Texts.Concat(SpawnProjectStrings(Project, IsRespawn).ToArray()).ToArray());
            base.SwitchShow(
                new string[]
                {
                    "返回",
                    "编辑项目"
                },
                Processors
            );
        }
        /// <summary>
        /// 生成给用户显示的项目信息
        /// </summary>
        /// <param name="Project">项目配置文件数据</param>
        /// <param name="IsRespawn">是否重新生成</param>
        /// <returns>给用户显示的项目信息</returns>
        public string[] SpawnProjectStrings(string[] Project, bool IsRespawn = false)
        {
            if (IsRespawn)
            {
                List<string> ProjectDataTemp = new List<string>();
                string[] ProjectLineTemp;
                for (int Index = 0; Index < Project.Length; Index++)
                {
                    ProjectLineTemp = Project[Index].Split(':');
                    ProjectDataTemp.Add($"序号{Index}：{ProjectLineTemp[0]}：{ProjectLineTemp[1]}");
                }
                ProjectStrings = ProjectDataTemp.ToArray();
            }
            return ProjectStrings;
        }
    }
}
