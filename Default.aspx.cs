using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BDSystemSMBIEContext;


namespace WEB_SMBIE
{
    public partial class Default : System.Web.UI.Page
    {
        User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["token"] != null)
            {
                user = ClassHelper.Helper.Auth(Request.Cookies["token"].Value);
            }
            if (user != null)
            {
                Response.Redirect(@"PagesUser/RuleDepartments.aspx");
            }
        }

        protected void FormLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            user = ClassHelper.Helper.Auth(FormLogin.UserName, FormLogin.Password);
            if (user != null)
            {
                ClassHelper.Helper.UserUpdateToken(user);
                Response.Cookies.Remove("token");
                Response.Cookies.Add(new HttpCookie("token", user.Token));
                if (Request.UrlReferrer != null) { }
                //Response.Redirect(");
                else
                    Response.Redirect("BSlinks.aspx");
            }
            e.Authenticated = user != null;
        }
    }
}