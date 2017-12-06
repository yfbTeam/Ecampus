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
    public partial class User_Model_RelService : BaseService<User_Model_Rel>, IUser_Model_RelService
    {
        User_Model_RelDal dal = new User_Model_RelDal();
        public bool SetDesk(string AddIDS, string DelIDS, string IDCard)
        {
            return dal.SetDesk(AddIDS, DelIDS, IDCard);
        }
    }
}
