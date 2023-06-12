// 页面管理组命名空间
namespace CTMS.PageGroups;

/// <summary>
/// 具象化项目编辑器页面管理组类接口
/// </summary>
public interface IEditorShowable : IConcreteShowable
{
    /// <summary>
    /// 查看项目
    /// </summary>
    void ReadProject();

    /// <summary>
    /// 编辑项目
    /// </summary>
    void EditProject();
}
