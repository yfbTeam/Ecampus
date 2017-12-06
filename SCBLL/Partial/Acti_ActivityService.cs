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
    public partial class Acti_ActivityService : BaseService<Acti_Activity>, IActi_ActivityService
    {
        Acti_ActivityDal dal = new Acti_ActivityDal();
        Acti_RecruitApplyDal app_dal = new Acti_RecruitApplyDal();
        Acti_ProjectDal pro_dal = new Acti_ProjectDal();
        BLLCommon common = new BLLCommon();
        #region 编辑部门活动        
        public JsonModel EditDepartActivity(Acti_Activity model,string projectStr)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.EditDepartActivity(model,projectStr);
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

        #region 添加招新申请        
        public JsonModel AddActiRecruitApply(Acti_RecruitApply model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = app_dal.AddActiRecruitApply(model);
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

        #region 招新申请审核        
        public JsonModel ActiRecruitApply_Audit(Acti_RecruitApply model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = app_dal.ActiRecruitApply_Audit(model);
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

        #region 编辑部门项目    
        public JsonModel EditActiProject(Acti_Project model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = pro_dal.EditActiProject(model);
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
