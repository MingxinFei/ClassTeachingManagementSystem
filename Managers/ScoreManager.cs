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
        /// <param name="Persons">人员配置文件</param>
        /// <exception cref="UnifyException"></exception>
        public void CreateProject(string[] Persons)
        {
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
        /// <param name="Project">项目配置文件数据</param>
        /// <exception cref="UnifyException"></exception>
        public void CheckFormat(string[] Project)
        {
            foreach (string Data in Project)
            {
                if (Data == "None")
                {
                    continue;
                }
                try
                {
                    Convert.ToInt32(Data.Split(':')[1]);
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
        /// <param name="Project">项目配置文件数据</param>
        /// <param name="Index">人员序号</param>
        /// <param name="Value">成绩字符串</param>
        /// <exception cref="UnifyException"></exception>
        public void SetScore(string[] Project, int Index, string Value)
        {
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
        /// <param name="Project">项目配置文件数据</param>
        /// <returns>平均分字符串</returns>
        /// <exception cref="UnifyException"></exception>
        public string GetAverageScore(string[] Project)
        {
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
