using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCWeb
{
    public partial class _event : BasePage
    {
        public string Date { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Date = Request["date"];
            EventID.Value = Request["id"];
        }
    }
}