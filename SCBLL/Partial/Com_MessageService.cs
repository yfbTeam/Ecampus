using SCDAL;
using SCIBLL;
using SCModel;
using SCUtility;
using SCUtility.Mail;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBLL
{
    public partial class Com_MessageService : BaseService<Com_Message>, ICom_MessageService
    {
        Com_MessageDal dal = new Com_MessageDal();
        BLLCommon common = new BLLCommon();
        public JsonModel ReaderMessage(Hashtable ht)
        {
            try
            {
                int result = dal.ReaderMessage(ht);
                //定义JSON标准格式实体中
                JsonModel jsonModel = new JsonModel();
                if (result > 0)
                    jsonModel.errMsg = "success";
                else
                    jsonModel.errMsg = "fail";
                return jsonModel;
            }
            catch (Exception ex)
            {
                JsonModel jsonModel = new JsonModel();
                jsonModel.status = "error";
                jsonModel.errMsg = ex.ToString();
                return jsonModel;
            }
        }

        public JsonModel SendMessage(Hashtable ht, List<Com_Message> list)
        {
            try
            {
                int result = dal.SendMessage(ht, list);
                //定义JSON标准格式实体中
                JsonModel jsonModel = new JsonModel();
                if (result > 0)
                {
                    jsonModel.errMsg = "success";
                    SendMail sm = new SendMail();
                    List<string> emailList = new List<string>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(list[i].ReceiverEmail))
                            emailList.Add(list[i].ReceiverEmail);
                    }
                    sm.listToUser = emailList;
                    sm.m_Body = Convert.ToString(ht["Contents"]);
                    sm.m_Subject = ht["Title"].ToString();
                    sm.MainCount = emailList.Count;
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(ht["FilePath"])))
                    {
                        sm.filePaths.Add(ht["FileEmailPath"].ToString());
                    }
                    if (!string.IsNullOrWhiteSpace(ht["isSendEmail"].ToString()) && Convert.ToBoolean(ht["isSendEmail"]) == true && !string.IsNullOrWhiteSpace(ht["Timing"].ToString()) && Convert.ToInt32(ht["Timing"]) == ((int)MessageTiming.立即发送))
                    {
                        sm.SendMsg();
                    }
                }
                else
                {
                    jsonModel.errMsg = "fail";
                }
                return jsonModel;
            }
            catch (Exception ex)
            {
                JsonModel jsonModel = new JsonModel();
                jsonModel.status = "error";
                jsonModel.errMsg = ex.ToString();
                return jsonModel;
            }
        }
    }
}
