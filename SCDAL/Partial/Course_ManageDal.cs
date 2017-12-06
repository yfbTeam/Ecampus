using SCIDAL;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDAL
{
    public partial class Course_ManageDal : BaseDal<Course_Manage>, ICourse_ManageDal
    {

        #region 分页获取课程信息重写
        /// <summary>
        /// 分页获取课程信息重写
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="IsPage"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage, string where)
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@"select * from Course a where 1=1");

                int StartIndex = 0;
                int EndIndex = 0;
                string Where = "";
                if (ht.ContainsKey("ID") && !string.IsNullOrEmpty(ht["ID"].SafeToString()))
                {
                    str.Append(" and a.ID = " + ht["ID"].SafeToString());
                }
                if (ht.ContainsKey("OperSymbol") && !string.IsNullOrEmpty(ht["OperSymbol"].SafeToString()))
                {
                    str.Append(" and a.EndTime" + ht["OperSymbol"].SafeToString() + "'" + DateTime.Now + "'");
                }
                if (ht.ContainsKey("StudyTerm") && !string.IsNullOrEmpty(ht["StudyTerm"].SafeToString()))
                {
                    str.Append(" and a.StudyTerm=" + ht["StudyTerm"].SafeToString());
                }                
                if (ht.ContainsKey("CourseType") && !string.IsNullOrEmpty(ht["CourseType"].SafeToString()))
                {
                    str.Append(" and a.CourceType=" + ht["CourseType"].SafeToString() + "");
                }
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].SafeToString()))
                {
                    str.Append(" and a.Name like '%" + ht["Name"].SafeToString() + "%'");
                }
              
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                dt = SQLHelp.GetListByPage("(" + str.ToString() + ")", Where, "", StartIndex,
                    EndIndex, IsPage, null, out RowCount);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }
        #endregion

        #region 报名
        /// <summary>
        /// 报名
        /// </summary>
        /// <param name="courseid">课程编号</param>
        /// <param name="stuno">学生编号</param>
        /// <returns></returns>
        public string StuSingUp(string courseid, string stuno,int Rank)
        {
            SqlParameter[] param = { 
                                       new SqlParameter("@CouseID",courseid),
                                       new SqlParameter("@StuNo", stuno),
                                       new SqlParameter("@Rank",Rank)
                                   };
            return SQLHelp.ExecuteScalar("StuSingUp", CommandType.StoredProcedure, param).ToString();

        }
        #endregion

        #region 自动调整报名人员（根据志愿顺序和报名顺序）
        /// <summary>
        /// 自动调整报名人员
        /// </summary>
        /// <param name="CouseIDArry">课程ID串</param>
        /// <returns></returns>
        public string StuSingUp(string CouseIDArry)
        {
            SqlParameter[] param = { 
                                       new SqlParameter("@CouseIDArry",CouseIDArry)
                                   };
            return SQLHelp.ExecuteScalar("StuAdJust", CommandType.StoredProcedure, param).ToString();

        }
        #endregion

        #region 添加课程
        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string AddCourse(Course_Manage entity, string@Class)
        {
            SqlParameter[] param = { 
                                       new SqlParameter("@Name",entity.Name),
                                       new SqlParameter("@ImageUrl", entity.ImageUrl),
                                       new SqlParameter("@CourceType",entity.CourceType),
                                       new SqlParameter("@Status", entity.Status),
                                       new SqlParameter("@StuMaxCount", entity.StuMaxCount),
                                       new SqlParameter("@CourseIntro",entity.CourseIntro), 
                                       new SqlParameter("@StudyTerm", entity.StudyTerm),
                                       new SqlParameter("@GradeID",entity.GradeID),
                                       new SqlParameter("@Class",Class),
                                       new SqlParameter("@WeekName",entity.WeekName),
                                       new SqlParameter("@CreateUID",entity.CreatUID),
                                       new SqlParameter("@StartTime",entity.StartTime),
                                       new SqlParameter("@EndTime",entity.EndTime),
                                       new SqlParameter("@EvalueStandard",entity.EvalueStandard),
                                       new SqlParameter("@CourseHardware",entity.CourseHardware),
                                       new SqlParameter("@StudyPlace",entity.StudyPlace),

                                   };
            object obj = SQLHelp.ExecuteScalar("AddCource", CommandType.StoredProcedure, param);
            string result = "";
            if (obj.ToString().Length > 0)
            {
                result = obj.ToString();
            }
            return result;
        }
        #endregion

        #region 课程删除
        public string DelCourse(string courseid)
        {
            SqlParameter[] param = {  new SqlParameter("@ID",courseid)};
            object obj = SQLHelp.ExecuteScalar("DelCourse", CommandType.StoredProcedure, param);
            string result = "";
            if (obj.ToString().Length > 0)
            {
                result = obj.ToString();
            }
            return result;
        }
        #endregion

        #region 修改课程
        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string UpdateCourse(Course_Manage entity, string Class)
        {
            SqlParameter[] param = { 
                                       new SqlParameter("@ID",entity.ID),
                                       new SqlParameter("@Name",entity.Name),
                                       new SqlParameter("@ImageUrl", entity.ImageUrl),
                                       new SqlParameter("@CourceType",entity.CourceType),
                                       new SqlParameter("@Status", entity.Status),
                                       new SqlParameter("@StuMaxCount", entity.StuMaxCount),
                                       new SqlParameter("@CourseIntro",entity.CourseIntro), 
                                       new SqlParameter("@StudyTerm", entity.StudyTerm),
                                       new SqlParameter("@GradeID",entity.GradeID),
                                       new SqlParameter("@WeekName",entity.WeekName),
                                       new SqlParameter("@StartTime",entity.StartTime),
                                       new SqlParameter("@EndTime",entity.EndTime),
                                       new SqlParameter("@EvalueStandard",entity.EvalueStandard),
                                       new SqlParameter("@CourseHardware",entity.CourseHardware),
                                       new SqlParameter("@StudyPlace",entity.StudyPlace),
                                       new SqlParameter("@Class",Class)
                                   };
            object obj = SQLHelp.ExecuteScalar("UpdateCourse", CommandType.StoredProcedure, param);
            string result = "";
            if (obj.ToString().Length > 0)
            {
                result = obj.ToString();
            }
            return result;
        }
        #endregion

    }
}
