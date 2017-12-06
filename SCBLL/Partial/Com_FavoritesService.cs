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
    public partial class Com_FavoritesService : BaseService<Com_Favorites>, ICom_FavoritesService
    {
        Com_FavoritesDal dal = new Com_FavoritesDal();
        BLLCommon common = new BLLCommon();
        #region 加入收藏夹      
        public JsonModel AddComFavorites(Com_Favorites model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                string result = dal.AddComFavorites(model);
                jsonModel = new JsonModel()
                {
                    errNum = result.IndexOf("成功") != -1 ? 0 : 1,
                    errMsg = result,
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
