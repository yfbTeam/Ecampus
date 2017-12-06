using SCIDAL;
using SCModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCUtility;
namespace SCDAL
{
    public partial class Book_SubjectDal : BaseDal<Book_Subject>, IBook_SubjectDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage, string where)
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                int StartIndex = 0;
                int EndIndex = 0;
                if (ht["IsPage"].SafeToString().Length > 0)
                {
                    IsPage = Convert.ToBoolean(ht["IsPage"]);
                }
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].SafeToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].SafeToString());
                }

                StringBuilder sb = new StringBuilder();
                if (ht.ContainsKey("Distinct") && !string.IsNullOrEmpty(ht["Distinct"].SafeToString()))
                {
                    sb.Append("select distinct ID,Name from Book_Subject where 1=1 ");
                }
                else
                {
                    sb.Append("select sub.ID,sub.Name as SubJectName,grd.Name as GradeName,ver.Name as VersionName,sub.CreateTime from Book_Grade grd inner join Book_Subject sub on grd.ID=sub.GradeID inner join Book_Version ver on sub.VersionID=ver.ID  where 1=1 ");
                }
                if (ht.ContainsKey("VersionID") && !string.IsNullOrEmpty(ht["VersionID"].SafeToString()))
                {
                    sb.Append(" and VersionID=" + ht["VersionID"]);
                }
                if (ht.ContainsKey("GradeID") && !string.IsNullOrEmpty(ht["GradeID"].SafeToString()))
                {
                    sb.Append(" and GradeID=" + ht["GradeID"]);
                }
                dt = SQLHelp.GetListByPage("(" + sb.ToString() + ")", where, "", StartIndex, EndIndex, IsPage, null, out RowCount);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }

    }
}
