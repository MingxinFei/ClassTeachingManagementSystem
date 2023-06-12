// 页面管理组命名空间
namespace CTMS.PageGroups;

/// <summary>
/// 具象化页面管理组类接口
/// </summary>
public interface IConcreteShowable
{
    /// <summary>
    /// 创建项目
    /// </summary>
    void CreateProject();

    /// <summary>
    /// 页面管理组主函数
    /// </summary>
    void GuardedPageMain();
}
