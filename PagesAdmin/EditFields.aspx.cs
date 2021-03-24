using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BDSystemSMBIEContext;

namespace WEB_SMBIE.PagesAdmin
{
    public partial class EditFields : UserPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (user.Department.TypeDepartment.Position.Rang != 0)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            RecordField recordField = new RecordField();
            recordField.Name = TxtName.Text;
            recordField.Description = TxtDescription.Text;
            recordField.Sysname = TxtSysname.Text;
            ClassHelper.Helper.BDSystem.RecordFields.InsertOnSubmit(recordField);
            ClassHelper.Helper.Save();
            GridView1.DataBind();
        }
    }
}