using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BDSystemSMBIEContext;

namespace WEB_SMBIE
{
    public class UserPage : System.Web.UI.Page
    {
        public BDSystemSMBIEContext.User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["token"] != null)
            {
                user = ClassHelper.Helper.Auth(Request.Cookies["token"].Value);
            }
            if (user == null)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}