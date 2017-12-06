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
    public partial class ResourcesInfoDal : BaseDal<ResourcesInfo>, IResourcesInfoDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage, string where)
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                string DocName = (string)ht["DocName"];
                string GroupName = ht["GroupName"].SafeToString();
                string Postfixs = ht["Postfixs"].SafeToString();
                //string CatagoryID = ht["CatagoryID"].SafeToString();
                string ChapterID = ht["ChapterID"].SafeToString();
                string IDCard = ht["IDCard"].SafeToString();
                int StartIndex = 0;
                int EndIndex = 0;
                StringBuilder sb = new StringBuilder();
                sb.Append("select re.ID,Name+postfix as Name,FileUrl,re.CreateTime,FileSize,FileGroup,postfix,detail.ClickNum,CASE postfix WHEN '' THEN 'file' else right(postfix,LEN(postfix) - 1) end  as postfix1 from ResourcesInfo re left join ClickDetail detail on re.ID=detail.ResourcesID and detail.ClickType=2 and detail.CreateUID='" +
                    IDCard + "' where 1=1 ");
                if (DocName.SafeToString().Length > 0)
                {
                    sb.Append(" and re.Name like '%" + DocName + "%'");
                }
                if (GroupName.Length > 0)
                {
                    sb.Append(" and FileGroup = '" + GroupName + "'");
                }
                if (Postfixs.Length > 0)
                {
                    sb.Append(" and postfix in (" + GetPostfixs(int.Parse(Postfixs)) + ")");
                }
                if (ChapterID.Length > 0)
                {
                    sb.Append(" and re.ChapterID=" + ChapterID);
                }
                //else if (ChapterID.Length == 0 && CatagoryID.Length > 0)
                //{
                //    sb.Append(" and '|'+re.CatagoryID+'|' like '|" + CatagoryID + "|%'");
                //}
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }

                dt = SQLHelp.GetListByPage("(" + sb.ToString() + ")", where, "", StartIndex, EndIndex, IsPage, null, out RowCount);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }
        private string GetPostfixs(int id)
        {
            ResourceTypeDal dal = new ResourceTypeDal();
            ResourceType mo = new ResourceType();
            mo = dal.GetEntityById(mo, id);
            return mo.Postfixs;
        }
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Delete(MyResource entity, int id)
        {
            string sql = string.Format("delete from ResourcesInfo where id=@Id; delete from ResourcesInfo where '|'+code+'|' like '%|" + id + "|%'");
            SqlParameter pms = new SqlParameter("@Id", id);
            return SQLHelp.ExecuteNonQuery(sql, CommandType.Text, pms) > 0;

        }
    }
}
