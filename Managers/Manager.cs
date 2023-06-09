using CTMS.BaseClasses;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

// 管理器命名空间
namespace CTMS.Managers
{
    /// <summary>
    /// 成绩管理器
    /// </summary>
    public class Manager : IDisposable
    {
        protected string ProjectFileName;
        protected string PersonsFileName;
        protected bool IsDisposed;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="NewProjectFileName">项目配置文件</param>
        /// <param name="NewPersonsFileName">人员配置文件</param>
        /// <exception cref="UnifyException"></exception>
        public Manager(string NewProjectFileName, string NewPersonsFileName)
        {
            ProjectFileName = NewProjectFileName;
            PersonsFileName = NewPersonsFileName;
            IsDisposed = false;
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~Manager()
        {
            Dispose();
        }
        /// <summary>
        /// 释放函数
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            GC.SuppressFinalize(this);
            ProjectFileName = null;
            PersonsFileName = null;
            IsDisposed = true;
        }
        /// <summary>
        /// 检查对象是否为空
        /// </summary>
        /// <param name="Data">检查对象</param>
        /// <exception cref="UnifyException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Check(object Data)
        {
            if (Data == null)
            {
                throw new UnifyException("检查到空对象", "Manager");
            }
        }
        /// <summary>
        /// 设置人员配置文件数据
        /// </summary>
        /// <param name="Texts">人员配置文件数据</param>
        /// <exception cref="UnifyException"></exception>
        public void SetPersonConfig(string[] Texts)
        {
            Check(PersonsFileName);
            try
            {
                File.WriteAllLines("./Databases/" + PersonsFileName + ".persons", Texts);
            }
            catch (FileNotFoundException)
            {
                throw new UnifyException("文件未找到", "Manager");
            }
        }
        /// <summary>
        /// 获取人员配置文件数据
        /// </summary>
        /// <returns>人员配置文件数据</returns>
        /// <exception cref="UnifyException"></exception>
        public string[] GetPersonConfig()
        {
            Check(PersonsFileName);
            string[] Persons;
            try
            {
                Persons = File.ReadAllLines("./Databases/" + PersonsFileName + ".persons");
            }
            catch (FileNotFoundException)
            {
                throw new UnifyException("文件未找到", "Manager");
            }
            catch
            {
                throw new UnifyException("未知错误", "Manager");
            }
            return Persons;
        }
        /// <summary>
        /// 设置项目配置文件数据
        /// </summary>
        /// <param name="Texts">项目配置文件数据</param>
        /// <param name="NewProjectFileName">项目配置文件</param>
        /// <exception cref="UnifyException"></exception>
        public void SetProjectConfig(string[] Texts, string NewProjectFileName)
        {
            Check(NewProjectFileName);
            try
            {
                File.WriteAllLines("./Databases/Projects/" + NewProjectFileName + ".managed", Texts);
            }
            catch (FileNotFoundException)
            {
                throw new UnifyException("文件未找到", "Manager");
            }
        }
        /// <summary>
        /// 设置项目配置文件数据
        /// </summary>
        /// <param name="Texts">项目配置文件数据</param>
        /// <exception cref="UnifyException"></exception>
        [DebuggerHidden]
        public void SetProjectConfig(string[] Texts)
        {
            SetProjectConfig(Texts, ProjectFileName);
        }
        /// <summary>
        /// 获取项目配置文件数据
        /// </summary>
        /// <param name="NewProjectFileName">项目配置文件</param>
        /// <returns>项目配置文件数据</returns>
        /// <exception cref="UnifyException"></exception>
        public string[] GetProjectConfig(string NewProjectFileName)
        {
            Check(NewProjectFileName);
            string[] Project;
            try
            {
                Project = File.ReadAllLines("./Databases/Projects/" + NewProjectFileName + ".managed");
            }
            catch (FileNotFoundException)
            {
                throw new UnifyException("项目配置文件加载异常", "Manager");
            }
            return Project;
        }
        /// <summary>
        /// 获取项目配置文件数据
        /// </summary>
        /// <returns>项目配置文件数据</returns>
        /// <exception cref="UnifyException"></exception>
        [DebuggerHidden]
        public string[] GetProjectConfig()
        {
            return GetProjectConfig(ProjectFileName);
        }
    }
}
