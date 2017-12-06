using SCDAL;
using SCIBLL;
using SCModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBLL
{
    public partial class Com_NewInfoService : BaseService<Com_NewInfo>, ICom_NewInfoService
    {
        Com_NewInfoDal dal = new Com_NewInfoDal();
        BLLCommon common = new BLLCommon();
        #region 编辑资讯        
        public JsonModel EditComNewInfo(Com_NewInfo model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.EditComNewInfo(model);
                jsonModel = new JsonModel()
                {
                    errNum = result==-1?-1:0,
                    errMsg = "",
                    retData = result
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion

        #region 点赞        
        public JsonModel GoodClick(Com_GoodClick model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                string result = dal.GoodClick(model);
                jsonModel = new JsonModel()
                {
                    errNum = result.IndexOf("成功")!=-1?0:1,
                    errMsg = result,
                    retData = ""
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion
    }
}
