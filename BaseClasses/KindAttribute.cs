// 基础类命名空间
namespace CTMS.BaseClasses
{
    /// <summary>
    /// 分类特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    [Kind("修饰特性")]
    public class KindAttribute : Attribute
    {
        private string kind;

        public string Kind
        {
            get
            {
                if (kind == null)
                {
                    throw new UnifyException("检查到空对象", GetType());
                }
                return kind;
            }
            set
            {
                if (value == null) {
                    throw new UnifyException("检查到空对象", GetType());
                }
                kind = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inKind">分类文本</param>
        public KindAttribute(string inKind)
        {
            Kind = inKind;
        }
    }
}
