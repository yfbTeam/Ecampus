using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCWeb.CommonPage
{
    public partial class Header : BasePage
    {
        public string userName;
        public string absHeadPic;
        public string uniqueNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.UserInfo != null)
            {
                userName = this.UserInfo.Name;
                absHeadPic = this.UserInfo.AbsHeadPic;
                uniqueNo = this.UserInfo.UniqueNo;
            }            
        }
    }
}