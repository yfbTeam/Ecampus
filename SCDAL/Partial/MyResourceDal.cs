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
using System.Data.SqlClient;
namespace SCDAL
{
    public partial class MyResourceDal : BaseDal<MyResource>, IMyResourceDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage, string where)
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                int StartIndex = 0;
                int EndIndex = 0;
                string OrderBy = "";

                string DocName = ht["DocName"].SafeToString();
                string IsFolder = ht["IsFolder"].SafeToString();
                string Pid = ht["Pid"].SafeToString();
                string IDS = ht["IDS"].SafeToString();
                string ID = ht["ID"].SafeToString();
                string Time = ht["CreateTime"].SafeToString();
                string CreateUID = ht["CreateUID"].SafeToString();
                string Where = " and CreateUID='" + CreateUID + "'";
                string Postfixs = ht["Postfixs"].SafeToString();
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                if (ID.Length > 0)
                {
                    Where += " and ID=" + ID;
                }
                if (Postfixs.Length > 0)
                {
                    Where += " and postfix in (" + GetPostfixs(int.Parse(Postfixs)) + ")";
                    if (Pid.Length > 0 && Pid != "0")
                    {
                        Where += " and Pid=" + Pid;
                    }
                }
                else
                {
                    if (Pid.Length > 0)
                    {
                        Where += " and Pid=" + Pid;
                    }
                }
                if (DocName.Length > 0)
                {
                    Where += " and Name like '%" + DocName + "%'";
                }
                if (IDS.Length > 0)
                {
                    Where += " and ID in (" + IDS + ")";

                }
                if (IsFolder.SafeToString().Length > 0)
                {
                    Where += " and IsFolder = " + IsFolder;
                }
                if (Time.Length > 0)
                {
                    if (Convert.ToDateTime(Time) > DateTime.Now.AddMonths(-7))
                    {
                        Where += " and CreateTime> '" + Time + "'";
                    }
                }
                if (ht.ContainsKey("OrderBy") && !string.IsNullOrEmpty(ht["OrderBy"].SafeToString()))
                {
                    OrderBy = ht["OrderBy"].SafeToString();
                }
                dt = SQLHelp.GetListByPage((string)ht["TableName"], Where, OrderBy, StartIndex, EndIndex, IsPage, null, out RowCount,
                    "ID,Name+postfix as Name,Pid,FileUrl,CreateTime,EditTime,FileSize,code,postfix,IsFolder,CASE postfix WHEN '' THEN 'file' else right(postfix,LEN(postfix) - 1) end  as postfix1");
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Delete(MyResource entity, int id)
        {
            string sql = string.Format("delete from MyResource where id=@Id; delete from MyResource where '|'+code+'|' like '%|" + id + "|%'");
            SqlParameter pms = new SqlParameter("@Id", id);
            return SQLHelp.ExecuteNonQuery(sql, CommandType.Text, pms) > 0;
        }

        private string GetPostfixs(int id)
        {
            ResourceTypeDal dal = new ResourceTypeDal();
            ResourceType mo = new ResourceType();
            mo = dal.GetEntityById(mo, id);
            return mo.Postfixs;
        }
    }
}
