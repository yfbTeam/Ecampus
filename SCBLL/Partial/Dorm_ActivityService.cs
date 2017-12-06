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
    public partial class Dorm_ActivityService : BaseService<Dorm_Activity>, IDorm_ActivityService
    {
        Dorm_ActivityDal dal = new Dorm_ActivityDal();
        BLLCommon common = new BLLCommon();
        #region 编辑宿舍活动        
        public JsonModel EditDormActivity(Dorm_Activity model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.EditDormActivity(model);
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
