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
    public partial class Dorm_RoomService : BaseService<Dorm_Room>, IDorm_RoomService
    {
        Dorm_RoomDal dal = new Dorm_RoomDal();
        BLLCommon common = new BLLCommon();

        #region 编辑宿舍信息        
        public JsonModel EditDormRoom(Dorm_Room model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.EditDormRoom(model);
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
