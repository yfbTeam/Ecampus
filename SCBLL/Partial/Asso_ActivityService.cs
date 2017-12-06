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
    public partial class Asso_ActivityService : BaseService<Asso_Activity>, IAsso_ActivityService
    {
        Asso_ActivityDal dal = new Asso_ActivityDal();
        BLLCommon common = new BLLCommon();
        #region 编辑社团活动        
        public JsonModel EditAssoActivity(Asso_Activity model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.EditAssoActivity(model);
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
