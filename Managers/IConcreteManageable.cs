using System.Diagnostics;

// 管理器命名空间
namespace CTMS.Managers
{
    /// <summary>
    /// 具象化管理器类接口
    /// </summary>
    internal interface IConcreteManageable
    {
        /// <summary>
        /// 创建一个项目配置文件
        /// </summary>
        /// <param name="Project">项目配置文件数据</param>
        void CreateProject(string[] Project);
        /// <summary>
        /// 检查项目配置文件是否格式正确
        /// </summary>
        /// <param name="Project">项目配置文件数据</param>
        void CheckFormat(string[] Project);
    }
}
