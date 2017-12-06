using SCDAL;
using SCIBLL;
using SCModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBLL
{
    public partial class Course_ChapterService : BaseService<Course_Chapter>, ICourse_ChapterService
    {
        Course_ChapterDal dal = new Course_ChapterDal();
        #region 删除章节
        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string DelChapter(int ID)
        {
            string Result = dal.DelChapter(ID);
            return Result;
        }
        #endregion

        #region 修改课程目录结构
        /// <summary>
        /// 修改课程目录结构
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string EditChapterSort(int ID, string Type)
        {
            string result = dal.EditChapterSort(ID, Type);
            return result;
        }
        #endregion
    }
}
