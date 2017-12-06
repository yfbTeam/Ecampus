using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCWeb.SystemSettings
{
    public partial class RoleSettings : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hid_UserIDCard.Value =UserInfo.UniqueNo;
            hid_LoginName.Value = UserInfo.LoginName;
        }
    }
}