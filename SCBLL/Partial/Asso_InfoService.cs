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
    public partial class Asso_InfoService : BaseService<Asso_Info>, IAsso_InfoService
    {
        Asso_InfoDal dal = new Asso_InfoDal();
        Asso_MemberDal mem_dal = new Asso_MemberDal();
        BLLCommon common = new BLLCommon();

        #region 编辑社团信息        
        public JsonModel EditAssoInfo(Asso_Info model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.EditAssoInfo(model);
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

        #region 添加或删除社团成员        
        public JsonModel AddOrDelAssoMember(int type, Asso_Member model, string memberNos)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = mem_dal.AddOrDelAssoMember(type, model, memberNos);
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

        #region 获取社团报名时间        
        public JsonModel GetAssoEnteredData()
        {
            return GetJsonModelByDataTable(dal.GetAssoEnteredData());
        }
        #endregion

        #region 社团报名时间设置        
        public JsonModel SetAssoEntered(Asso_EnteredSet model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.SetAssoEntered(model);
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
