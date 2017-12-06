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
    public partial class Com_EvaTempService : BaseService<Com_EvaTemp>, ICom_EvaTempService
    {
        Com_EvaTempDal dal = new Com_EvaTempDal();
        Com_EvaBaseDal base_dal = new Com_EvaBaseDal();
        BLLCommon common = new BLLCommon();
        #region 编辑考评模板  
        public JsonModel EditComEvaTemp(Com_EvaTemp model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.EditComEvaTemp(model);
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

        #region 编辑考评基础项    
        public JsonModel EditComEvaBase(Com_EvaBase model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = base_dal.EditComEvaBase(model);
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
