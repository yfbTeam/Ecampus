using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCWeb.StuAssociate
{
    public partial class AllAssociateManage : BasePage
    {
        public string uniqueNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.UserInfo != null)
            {                
                uniqueNo = this.UserInfo.UniqueNo;
            }
        }
    }
}