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
    public partial class Com_AlbumService : BaseService<Com_Album>, ICom_AlbumService
    {
        Com_AlbumDal dal = new Com_AlbumDal();
        BLLCommon common = new BLLCommon();
        #region 编辑资讯        
        public JsonModel EditComAlbum(Com_Album model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.EditComAlbum(model);
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
