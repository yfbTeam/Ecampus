using SCIDAL;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDAL
{
    public partial class Com_MessageDal : BaseDal<Com_Message>, ICom_MessageDal
    {
        public int ReaderMessage(Hashtable ht)
        {
            using (SqlTransaction trans = SQLHelp.BeginTransaction())
            {
                try
                {
                    StringBuilder sbSql4org = new StringBuilder();
                    if (ht.ContainsKey("Status") && !string.IsNullOrWhiteSpace(ht["Status"].ToString()))
                    {
                        sbSql4org.Append("update Com_Message set [Status]=" + ht["Status"].ToString() + " where Id in (");
                    }
                    else if (ht.ContainsKey("IsDelete") && !string.IsNullOrWhiteSpace(ht["IsDelete"].ToString()))
                    {
                        sbSql4org.Append("update Com_Message set [IsDelete]=" + ht["IsDelete"].ToString() + " where Id in (");
                    }
                    List<SqlParameter> list = new List<SqlParameter>();
                    if (!string.IsNullOrWhiteSpace(ht["ids"].ToString()))
                    {
                        var ids = ht["ids"].ToString().Split(',');
                        var strsql = "";
                        for (int i = 0; i < ids.Length; i++)
                        {
                            strsql += "@ids" + i + ",";
                            list.Add(new SqlParameter("@ids" + i, ids[i]));
                        }
                        if (!string.IsNullOrWhiteSpace(strsql)) strsql = strsql.Substring(0, strsql.Length - 1);
                        sbSql4org.Append(strsql + ")");
                        if (ht.ContainsKey("Receiver") && !string.IsNullOrEmpty(ht["Receiver"].ToString()))
                        {
                            sbSql4org.Append(" and Receiver=@Receiver");
                            list.Add(new SqlParameter("@Receiver", ht["Receiver"].ToString()));
                        }
                        int number = SQLHelp.ExecuteNonQuery(trans, System.Data.CommandType.Text, sbSql4org.ToString(), list.ToArray());
                        if (number > 0)
                        {
                            trans.Commit();
                            return number;
                        }
                        else
                        {
                            trans.Rollback();
                            return -1;
                        }
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return -1;
                }
            }
            return 0;
        }

        public int SendMessage(Hashtable ht, List<Com_Message> list)
        {
            using (SqlTransaction trans = SQLHelp.BeginTransaction())
            {
                try
                {
                    StringBuilder sbSql4org = new StringBuilder();
                    List<SqlParameter> spList = new List<SqlParameter>();
                    sbSql4org.Append("INSERT INTO Com_Message(Title,Contents,[Type],IsDelete,CreateTime,CreateUID,Receiver,Href,Status,IsSend,ReceiverEmail,CreateName,ReceiverName,Timing,FilePath)");
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (i + 1 == list.Count)//最后一条
                            sbSql4org.Append(@" select @Title" + i + ",@Contents" + i + ",@Type" + i + ",@IsDelete" + i + ",@CreateTime" + i + ",@CreateUID" + i + ",@Receiver" + i + ",@Href" + i + ",@Status" + i + ",@IsSend" + i + ",@ReceiverEmail" + i + ",@CreateName" + i + ",@ReceiverName" + i + ",@Timing" + i + ",@FilePath" + i);
                        else
                            sbSql4org.Append(@" select @Title" + i + ",@Contents" + i + ",@Type" + i + ",@IsDelete" + i + ",@CreateTime" + i + ",@CreateUID" + i + ",@Receiver" + i + ",@Href" + i + ",@Status" + i + ",@IsSend" + i + ",@ReceiverEmail" + i + ",@CreateName" + i + ",@ReceiverName" + i + ",@Timing" + i + ",@FilePath" + i + " union all ");
                        spList.Add(new SqlParameter("@Title" + i, ht["Title"].ToString()));
                        string href = "";
                        if (!string.IsNullOrWhiteSpace(list[i].Href))
                            href = "<br/><h3><a href=" + list[i].Href + ">点击此处可查看详细信息</a></h3>";
                        spList.Add(new SqlParameter("@Contents" + i, ht["Contents"].ToString() + href));
                        spList.Add(new SqlParameter("@Type" + i, ht["Type"].ToString()));
                        spList.Add(new SqlParameter("@IsDelete" + i, ((int)SysStatus.正常).ToString()));
                        spList.Add(new SqlParameter("@CreateTime" + i, ht["CreateTime"].ToString()));
                        spList.Add(new SqlParameter("@CreateUID" + i, ht["CreateUID"].ToString()));
                        spList.Add(new SqlParameter("@Receiver" + i, list[i].Receiver));
                        spList.Add(new SqlParameter("@Href" + i, list[i].Href));
                        spList.Add(new SqlParameter("@Status" + i, ((int)MessageStatus.未读).ToString()));
                        spList.Add(new SqlParameter("@IsSend" + i, ((int)MessageIsSend.未发送).ToString()));
                        spList.Add(new SqlParameter("@ReceiverEmail" + i, list[i].ReceiverEmail));
                        spList.Add(new SqlParameter("@CreateName" + i, ht["CreateName"].ToString()));
                        spList.Add(new SqlParameter("@ReceiverName" + i, list[i].ReceiverName));
                        spList.Add(new SqlParameter("@Timing" + i, ht["Timing"].ToString()));
                        spList.Add(new SqlParameter("@FilePath" + i, Convert.ToString(ht["FilePath"])));
                    }
                    int number = SQLHelp.ExecuteNonQuery(trans, System.Data.CommandType.Text, sbSql4org.ToString(), spList.ToArray());
                    if (number > 0)
                    {
                        trans.Commit();
                        return number;
                    }
                    else
                    {
                        trans.Rollback();
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return -1;
                }
            }

        }
    }
}
