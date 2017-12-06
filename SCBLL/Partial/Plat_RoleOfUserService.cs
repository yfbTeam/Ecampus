using SCDAL;
using SCIBLL;
using SCModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBLL
{
    public partial class Plat_RoleOfUserService : BaseService<Plat_RoleOfUser>, IPlat_RoleOfUserService
    {
        Plat_RoleOfUserDal dal = new Plat_RoleOfUserDal();
        #region 删除关系数据， 删单条
        /// <summary>
        /// 删除关系数据， 删单条
        /// </summary>
        /// <returns>返回 JsonModel</returns>
        public JsonModel DeleteUserRelation(Plat_RoleOfUser roleu)
        {
            JsonModel jsonModel = null;
            try
            {
                bool result = dal.DeleteUserRelation(roleu);
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
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

        #region 数据导入
        /// <summary>
        /// 数据导入
        /// </summary>
        /// <returns></returns>
        public string ImportUser(DataTable dt)
        {
            string result = dal.ImportUser(dt);

            return result;
            /*JsonModel JsonModel=null;
            string result = dal.ImportUser(dt);
            if (result == "")
            {
                JsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "数据导入成功",
                    retData = ""
                };
            }
            else
            {
                JsonModel = new JsonModel()
                {
                    errNum =400,
                    errMsg = result,
                    retData = ""
                };
            }
            return JsonModel*/

        }

        #endregion

        public string GetUserOfAdmin()
        {
            string ReurnResult = dal.GetUserOfAdmin();
            return ReurnResult;
        }
    }
}
