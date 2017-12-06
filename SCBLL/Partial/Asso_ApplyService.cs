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
    public partial class Asso_ApplyService : BaseService<Asso_Apply>, IAsso_ApplyService
    {
        Asso_ApplyDal dal = new Asso_ApplyDal();
        BLLCommon common = new BLLCommon();

        #region 添加社团申请        
        public JsonModel AddAssoApply(Asso_Apply model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.AddAssoApply(model);
                jsonModel = new JsonModel()
                {
                    errNum = result,
                    errMsg = result == 0 ? "success" : "",
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

        #region 社团申请审核        
        public JsonModel AssoApply_Audit(Asso_Apply model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.AssoApply_Audit(model);
                jsonModel = new JsonModel()
                {
                    errNum = result,
                    errMsg = result == 0 ? "success" : "",
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
