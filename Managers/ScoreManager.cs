using CTMS.BaseClasses;
using System;
using System.Collections.Generic;

// 管理器命名空间
namespace CTMS.Managers
{
    /// <summary>
    /// 成绩管理器
    /// </summary>
    public class ScoreManager : Manager, IConcreteManageable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="NewProjectFileName">项目配置文件</param>
        /// <param name="NewPersonsFileName">人员配置文件</param>
        /// <exception cref="UnifyException"></exception>
        public ScoreManager(string NewProjectFileName, string NewPersonsFileName) :
            base(NewProjectFileName, NewPersonsFileName)
        { }
        /// <summary>
        /// 创建一个项目配置文件
        /// </summary>
        /// <exception cref="UnifyException"></exception>
        public void CreateProject()
        {
            string[] Persons = GetPersonConfig();
            List<string> Temp = new List<string>();
            foreach (string Data in Persons)
            {
                Temp.Add(Data + ":None");
            }
            SetProjectConfig(Temp.ToArray());
        }
        /// <summary>
        /// 检查项目配置文件是否格式正确
        /// </summary>
        /// <exception cref="UnifyException"></exception>
        public void CheckFormat()
        {
            string[] Project = GetProjectConfig();
            string[] ProjectLineTemp;
            foreach (string Data in Project)
            {
                ProjectLineTemp = Data.Split(':');
                if (ProjectLineTemp[1] == "None")
                {
                    continue;
                }
                try
                {
                    Convert.ToInt32(ProjectLineTemp[1]);
                }
                catch (SystemException)
                {
                    throw new UnifyException("数据可能被损坏或为其他类型的项目", "Manager");
                }
            }
        }
        /// <summary>
        /// 设置成绩
        /// </summary>
        /// <param name="Index">人员序号</param>
        /// <param name="Value">成绩字符串</param>
        /// <exception cref="UnifyException"></exception>
        public void SetScore(int Index, string Value)
        {
            string[] Project = GetProjectConfig();
            try
            {
                Convert.ToInt32(Value);
            }
            catch (SystemException)
            {
                throw new UnifyException("数据可能被损坏", "Manager");
            }
            string[] ProjectDataTemp = Project[Index].Split(':');
            ProjectDataTemp[1] = Value;
            Project[Index] = ProjectDataTemp[0] + ":" + ProjectDataTemp[1];
            SetProjectConfig(Project);
        }
        /// <summary>
        /// 获取平均分
        /// </summary>
        /// <returns>平均分字符串</returns>
        /// <exception cref="UnifyException"></exception>
        public string GetAverageScore()
        {
            string[] Project = GetProjectConfig();
            float Sum = 0;
            int PersonSum = 0;
            string DataTemp;
            foreach (string Data in Project)
            {
                DataTemp = Data.Split(':')[1];
                if (DataTemp == "None")
                {
                    continue;
                }
                try
                {
                    Sum += Convert.ToSingle(DataTemp);
                    PersonSum++;
                }
                catch (SystemException)
                {
                    throw new UnifyException("数据可能被损坏", "Manager");
                }
            }
            if (PersonSum == 0)
            {
                return "未写入";
            }
            return Convert.ToString(Sum / PersonSum) + "分";
        }
    }
}
