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
    public partial class User_Model_RelDal : BaseDal<User_Model_Rel>, IUser_Model_RelDal
    {
        public bool SetDesk(string AddIDS, string DelIDS, string IDCard)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string[] addArry = AddIDS.Split(',');
            for (int i = 0; i < addArry.Length; i++)
            {
                if (addArry[i].Length > 0)
                {
                    stringBuilder.Append("insert into User_Model_Rel(ModelID,UserIDCard) values(" + addArry[i] + ",'" + IDCard + "');");
                }
            }
            string[] delArry = DelIDS.Split(',');
            for (int i = 0; i < delArry.Length; i++)
            {
                if (delArry[i].Length > 0)
                {
                    stringBuilder.Append("delete from User_Model_Rel where ModelID=" + delArry[i] + " and UserIDCard='" + IDCard + "';");
                }
            }
            int result = SQLHelp.ExecuteNonQuery(stringBuilder.ToString(), CommandType.Text, null);
            if (result > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
