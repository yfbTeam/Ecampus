using SCIDAL;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDAL
{
    public partial class Book_VersionDal : BaseDal<Book_Version>, IBook_VersionDal
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
                sb.Append("select distinct ID,Name from Book_Version where 1=1 ");

                if (ht.ContainsKey("SubjectName") && !string.IsNullOrEmpty(ht["SubjectName"].SafeToString()))
                {
                    sb.Append(" and ID in (select VersionID from Book_Subject where  Name='" + ht["SubjectName"] + "')");
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
