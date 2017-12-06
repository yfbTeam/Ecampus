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
    public partial class Com_FavoritesDal : BaseDal<Com_Favorites>, ICom_FavoritesDal
    {
        #region 获取收藏夹表的分页数据
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
                sbSql4org.Append(@"select fav.* from Com_Favorites fav ");
                sbSql4org.Append(@" where 1=1 and fav.IsDelete=0 ");
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and fav.Name like N'%' + @Name + '%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                //0社团；1部门；2宿舍
                if (ht.ContainsKey("Type") && !string.IsNullOrEmpty(ht["Type"].ToString()))
                {
                    sbSql4org.Append(" and fav.Type=@Type ");
                    pms.Add(new SqlParameter("@Type", ht["Type"].ToString()));
                }
                if (ht.ContainsKey("RelationId") && !string.IsNullOrEmpty(ht["RelationId"].ToString()))
                {
                    sbSql4org.Append(" and fav.RelationId=@RelationId ");
                    pms.Add(new SqlParameter("@RelationId", ht["RelationId"].ToString()));
                }
                if (ht.ContainsKey("CreateUID") && !string.IsNullOrEmpty(ht["CreateUID"].ToString()))
                {
                    sbSql4org.Append(" and fav.CreateUID=@CreateUID ");
                    pms.Add(new SqlParameter("@CreateUID", ht["CreateUID"].ToString()));
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

        #region 加入收藏夹
        public string AddComFavorites(Com_Favorites model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Type", model.Type),
                    new SqlParameter("@RelationId", model.RelationId),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Href", model.Href),
                    new SqlParameter("@CreateUID", model.CreateUID),
                    new SqlParameter("@EditUID", model.EditUID??"")
            };
            object obj = SQLHelp.ExecuteScalar("AddComFavorites", CommandType.StoredProcedure, param);
            return obj.ToString();
        }
        #endregion
    }
}
