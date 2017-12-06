using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCWeb.SystemSettings
{
    public partial class QueueSettings : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                hid_UserIDCard.Value = UserInfo.UniqueNo;
                hid_LoginName.Value = UserInfo.LoginName;
            }
        }
    }
}