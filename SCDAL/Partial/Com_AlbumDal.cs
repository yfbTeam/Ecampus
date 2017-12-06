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
    public partial class Com_AlbumDal : BaseDal<Com_Album>, ICom_AlbumDal
    {
        #region 获取相册表的分页数据
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
                sbSql4org.Append(@"select alb.*,(select count(1) from Com_AlbumPic where AlbumId=alb.Id) as PicCount
                       ,(select top 1 PicUrl from Com_AlbumPic where AlbumId=alb.Id)as FirstPicUrl  from Com_Album alb ");
                sbSql4org.Append(@" where 1=1 and alb.IsDelete=0 ");
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and alb.Name like N'%' + @Name + '%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                //0社团；1部门；2宿舍
                if (ht.ContainsKey("Type") && !string.IsNullOrEmpty(ht["Type"].ToString()))
                {
                    sbSql4org.Append(" and alb.Type=@Type ");
                    pms.Add(new SqlParameter("@Type", ht["Type"].ToString()));
                }
                if (ht.ContainsKey("RelationId") && !string.IsNullOrEmpty(ht["RelationId"].ToString()))
                {
                    sbSql4org.Append(" and alb.RelationId=@RelationId ");
                    pms.Add(new SqlParameter("@RelationId", ht["RelationId"].ToString()));
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

        #region 编辑相册
        public int EditComAlbum(Com_Album model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Type", model.Type),
                    new SqlParameter("@RelationId", model.RelationId),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@CreateUID", model.CreateUID),
                    new SqlParameter("@EditUID", model.EditUID??"")
            };
            object obj = SQLHelp.ExecuteScalar("EditComAlbum", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion
    }
}
