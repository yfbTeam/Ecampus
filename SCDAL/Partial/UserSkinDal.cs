using SCIDAL;
using SCModel;
using SCUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDAL
{
    public partial class UserSkinDal : BaseDal<UserSkin>, IUserSkinDal
    {
        public override bool Update(UserSkin entity, System.Data.SqlClient.SqlTransaction trans = null)
        {
            string strSql1 = "select count(1) from UserSkin where UserID='" + entity.UserID + "'";
            object obj = SQLHelp.ExecuteScalar(strSql1, CommandType.Text, null);
            if (obj.ToString() == "0")
            {
                string strSql2 = "insert into UserSkin(UserID,SkinImage)values('" + entity.UserID + "','" + entity.SkinImage + "')";
                SQLHelp.ExecuteNonQuery(strSql2, CommandType.Text);
            }
            else
            {
                string strSql3 = "update UserSkin set SkinImage='" + entity.SkinImage + "' where  UserID='" + entity.UserID + "'";
                SQLHelp.ExecuteNonQuery(strSql3, CommandType.Text);
            }
            return true;
        }
    }
}
