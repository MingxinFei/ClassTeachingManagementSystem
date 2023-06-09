using CTMS.BaseClasses;
using System;
using System.Collections.Generic;

// 管理器命名空间
namespace CTMS.Managers
{
    /// <summary>
    /// 作业管理器
    /// </summary>
    public class StatusManager : Manager, IConcreteManageable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="NewProjectFileName">项目配置文件</param>
        /// <param name="NewPersonsFileName">人员配置文件</param>
        /// <exception cref="UnifyException"></exception>
        public StatusManager(string NewProjectFileName, string NewPersonsFileName) :
            base(NewProjectFileName, NewPersonsFileName)
        { }
        /// <summary>
        /// 创建一个项目配置文件
        /// </summary>
        /// <param name="Persons">人员配置文件</param>
        /// <exception cref="UnifyException"></exception>
        public void CreateProject(string[] Persons)
        {
            List<string> Temp = new List<string>();
            foreach (string Data in Persons)
            {
                Temp.Add(Data + ":未合格");
            }
            SetProjectConfig(Temp.ToArray());
        }
        /// <summary>
        /// 检查项目配置文件是否格式正确
        /// </summary>
        /// <param name="Project">项目配置文件数据</param>
        /// <exception cref="UnifyException"></exception>
        public void CheckFormat(string[] Project)
        {
            string Temp;
            foreach (string Data in Project)
            {
                Temp = Data.Split(':')[1];
                if (Temp != "已合格" && Temp != "未合格")
                {
                    throw new UnifyException("数据可能被损坏或为其他类型的项目", "Manager");
                }
            }
        }
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="Project">项目配置文件数据</param>
        /// <param name="Index">人员序号</param>
        /// <param name="Value">状态</param>
        /// <exception cref="UnifyException"></exception>
        public void SetStatus(string[] Project, int Index, bool Value)
        {
            string[] ProjectDataTemp = Project[Index].Split(':');
            if (Value)
            {
                ProjectDataTemp[1] = "已合格";
            }
            else
            {
                ProjectDataTemp[1] = "未合格";
            }
            Project[Index] = ProjectDataTemp[0] + ":" + ProjectDataTemp[1];
            SetProjectConfig(Project);
        }
        /// <summary>
        /// 获取合格率
        /// </summary>
        /// <param name="Project">项目配置文件数据</param>
        /// <returns>合格率字符串</returns>
        public string GetQualifiedRate(string[] Project)
        {
            int Count = 0;
            foreach (string Data in Project)
            {
                if (Data.Split(':')[1] == "已合格")
                {
                    Count++;
                }
            }
            return Convert.ToString(Count / (float)Project.Length * 100f) + "%";
        }
    }
}
