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
    public partial class Course_ChapterDal : BaseDal<Course_Chapter>, ICourse_ChapterDal
    {
        #region 章节查询
        /// <summary>
        ///  章节查询
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="RowCount"></param>
        /// <param name="IsPage"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage, string Where)
        {
            Where += " and IsDelete=0";
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                int StartIndex = 0;
                int EndIndex = 0;
                string CourseID = ht["CourseID"].SafeToString();
                string Pid = ht["Pid"].SafeToString();
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                if (CourseID.Length > 0)
                {
                    Where += " and CourseID=" + CourseID;
                }
               
                if (Pid.Length > 0)
                {
                    Pid = Pid.Substring(Pid.LastIndexOf("|") + 1, Pid.Length - Pid.LastIndexOf("|") - 1);
                    Where += " and Pid=" + Pid;
                }
                dt = SQLHelp.GetListByPage((string)ht["TableName"], Where, " Sort asc,ID desc", StartIndex,
                   EndIndex, IsPage, null, out RowCount);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }
        #endregion

        #region 章节删除
        /// <summary>
        /// 章节删除
        /// </summary>
        /// <param name="chapter"></param>
        /// <param name="ID">章节ID</param>
        /// <returns></returns>
        public string DelChapter(int ID)
        {
            SqlParameter[] param = { new SqlParameter("@ChapterID", ID) };
            object obj = SQLHelp.ExecuteScalar("DelChapter", CommandType.StoredProcedure, param);
            return obj.ToString();
        }
        #endregion

        #region 添加章节
        /// <summary>
        /// 添加章节
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override int Add(Course_Chapter entity)
        {
            int result = 0;
            SqlParameter[] param = { 
                                       new SqlParameter("@Name", entity.Name),
                                       new SqlParameter("@CourseID", entity.CourseID),
                                       new SqlParameter("@Pid", entity.Pid)
                                   };
            object obj = SQLHelp.ExecuteScalar("AddChapter", CommandType.StoredProcedure, param);

            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion

        #region 修改章节目录排序
        /// <summary>
        /// 修改章节目录排序
        /// </summary>
        /// <param name="ID">章节ID</param>
        /// <param name="Type">up down</param>
        /// <returns></returns>
        public string EditChapterSort(int ID, string Type)
        {
            string result = "";
            SqlParameter[] param = { 
                                       new SqlParameter("@ID", ID),
                                       new SqlParameter("@Type", Type)
                                   };
            object obj = SQLHelp.ExecuteScalar("EditChapterSort", CommandType.StoredProcedure, param);
            result = obj.SafeToString();
            return result;
        }
        #endregion
    }
}
