using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCWeb.StuActivity
{
    public partial class EditActiActivity : BasePage
    {
        public string uniqueNo ="";
        protected void Page_Load(object sender, EventArgs e)
        {
            uniqueNo = UserInfo.UniqueNo;
        }
    }
}