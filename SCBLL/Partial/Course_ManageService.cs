using SCDAL;
using SCIBLL;
using SCModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBLL
{
    public partial class Course_ManageService : BaseService<Course_Manage>, ICourse_ManageService
    {
        Course_ManageDal courcedal = new Course_ManageDal();

        #region 课程删除
        /// <summary>
        ///课程删除
        /// </summary>
        /// <param name="courseid"></param>
        /// <param name="stuno"></param>
        /// <param name="StuName"></param>
        /// <returns></returns>
        public string DelCourse(string courseid)
        {
            return courcedal.DelCourse(courseid);
        }
        #endregion

        #region 课程注册
        /// <summary>
        ///课程注册
        /// </summary>
        /// <param name="courseid"></param>
        /// <param name="stuno"></param>
        /// <param name="StuName"></param>
        /// <returns></returns>
        public string StuSingUp(string courseid, string stuno, int Rank)
        {
            return courcedal.StuSingUp(courseid, stuno, Rank);
        }
        #endregion

        #region 添加课程
        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string AddCourse(Course_Manage entity, string Course)
        {
            return courcedal.AddCourse(entity, Course);
        }
        #endregion

        #region 修改课程
        /// <summary>
        /// 修改课程
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string UpdateCourse(Course_Manage entity, string Course)
        {
            return courcedal.UpdateCourse(entity, Course);
        }
        #endregion

    }
}
