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
    public partial class Dorm_RoomStuListDal : BaseDal<Dorm_RoomStuList>, IDorm_RoomStuListDal
    {
        #region 获取宿舍成员表的分页数据
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            List<SqlParameter> pms = new List<SqlParameter>();
            int StartIndex = 0;
            int EndIndex = 0;
            if (IsPage)
            {
                StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
            }
            try
            {
                StringBuilder sbSql4org = new StringBuilder();
                sbSql4org.Append(@"select mem.*,build.Name as BuildName,room.Name as RoomName,'舍员' as Position 
                                    from Dorm_RoomStuList mem
                                    left join Dorm_Room room on room.Id=mem.RoomId 
                                    left join Dorm_Building build on room.BuildId = build.Id ");
                sbSql4org.Append(@" where 1=1 ");
                if (ht.ContainsKey("RoomId") && !string.IsNullOrEmpty(ht["RoomId"].ToString()))
                {
                    sbSql4org.Append(" and mem.RoomId=@RoomId ");
                    pms.Add(new SqlParameter("@RoomId", ht["RoomId"].ToString()));
                }
                if (ht.ContainsKey("NewMemerDay") && !string.IsNullOrEmpty(ht["NewMemerDay"].ToString()))
                {
                    sbSql4org.Append(" and DateDiff(dd,mem.CreateTime,getdate())<=@NewMemerDay ");
                    pms.Add(new SqlParameter("@NewMemerDay", ht["NewMemerDay"].ToString()));
                }
                string orderby = "";
                if (ht.ContainsKey("OrderBy") && !string.IsNullOrEmpty(ht["OrderBy"].ToString()))
                {
                    orderby = ht["OrderBy"].ToString();
                }
                return SQLHelp.GetListByPage("(" + sbSql4org.ToString() + ")", Where, orderby, StartIndex, EndIndex, IsPage, pms.ToArray(), out RowCount);
            }
            catch (Exception ex)
            {
                //写入日志
                //throw;
                return null;
            }
        }
        #endregion
    }
}
