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
    public partial class Dorm_RoomDal : BaseDal<Dorm_Room>, IDorm_RoomDal
    {
        #region 获取宿舍信息表的分页数据
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
                sbSql4org.Append(@"select room.*,build.Name as BuildName
                                ,(select count(1) from Dorm_RoomStuList where RoomId=room.Id) as MemCount
                                ,(select count(1) from Com_NewInfo where type=2 and RelationId=room.Id) as NewCount ");
                if (ht.ContainsKey("LoginUID") && !string.IsNullOrEmpty(ht["LoginUID"].ToString()))
                {
                    sbSql4org.Append(@" ,case when room.ManagerNo = @LoginUID then 1 else 0 end as IsLeader");
                    pms.Add(new SqlParameter("@LoginUID", ht["LoginUID"].ToString()));
                }
                sbSql4org.Append(@" from Dorm_Room room
                                    left join Dorm_Building build on room.BuildId=build.Id ");
                sbSql4org.Append(@" where 1=1 ");
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and room.Name like N'%' + @Name + '%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                if (ht.ContainsKey("IsDelete") && !string.IsNullOrEmpty(ht["IsDelete"].ToString()))
                {
                    sbSql4org.Append(" and room.IsDelete=@IsDelete ");
                    pms.Add(new SqlParameter("@IsDelete", ht["IsDelete"].ToString()));
                }
                if (ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].ToString()))
                {
                    sbSql4org.Append(" and room.Id=@Id ");
                    pms.Add(new SqlParameter("@Id", ht["Id"].ToString()));
                }
                if (ht.ContainsKey("BuildId") && !string.IsNullOrEmpty(ht["BuildId"].ToString()))
                {
                    sbSql4org.Append(" and room.BuildId=@BuildId ");
                    pms.Add(new SqlParameter("@BuildId", ht["BuildId"].ToString()));
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

        #region 编辑宿舍信息
        public int EditDormRoom(Dorm_Room model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@BuildId", model.BuildId),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@FloorNo", model.FloorNo),
                    new SqlParameter("@Beds", model.Beds),
                    new SqlParameter("@Introduce", model.Introduce),  
                    new SqlParameter("@PicURL",model.PicURL),                  
                    new SqlParameter("@CreateUID", model.CreateUID),
                    new SqlParameter("@EditUID", model.EditUID??"")
            };
            object obj = SQLHelp.ExecuteScalar("EditDormRoom", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion
    }
}
