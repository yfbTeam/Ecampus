﻿using SCDAL;
using SCIBLL;
using SCModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBLL
{
    public partial class Dorm_BuildingService : BaseService<Dorm_Building>, IDorm_BuildingService
    {
        Dorm_BuildingDal dal = new Dorm_BuildingDal();
        BLLCommon common = new BLLCommon();

        #region 编辑宿舍楼信息        
        public JsonModel EditDormBuilding(Dorm_Building model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.EditDormBuilding(model);
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
