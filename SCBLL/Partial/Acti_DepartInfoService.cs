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
    public partial class Acti_DepartInfoService : BaseService<Acti_DepartInfo>, IActi_DepartInfoService
    {
        Acti_DepartInfoDal dal = new Acti_DepartInfoDal();
        Acti_DepartMemberDal mem_dal = new Acti_DepartMemberDal();
        BLLCommon common = new BLLCommon();

        #region 编辑部门信息        
        public JsonModel EditDepartInfo(Acti_DepartInfo model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.EditDepartInfo(model);
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

        #region 添加或删除部门成员        
        public JsonModel AddOrDelDepartMember(int type, Acti_DepartMember model, string memberNos)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = mem_dal.AddOrDelDepartMember(type, model, memberNos);
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
