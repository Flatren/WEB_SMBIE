using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BDSystemSMBIEContext;

namespace WEB_SMBIE.PagesAdmin
{
    public partial class EditTypeDep : UserPage
    {

        TypeDepartment typeDepartment;

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
            typeDepartment = new TypeDepartment();
            typeDepartment.Name = TxtName.Text;
            typeDepartment.Description = TxtDescririon.Text;
            if (DropDownList2.SelectedValue != "")
                typeDepartment.IdParent = int.Parse(DropDownList2.SelectedValue);
            if (DropDownList1.SelectedValue != "")
                typeDepartment.RuleIdPosition = int.Parse(DropDownList1.SelectedValue);

            ClassHelper.Helper.BDSystem.TypeDepartments.InsertOnSubmit(typeDepartment);
            ClassHelper.Helper.Save();
            GridView1.DataBind();
            DropDownList2.DataBind();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = (int)GridView1.SelectedDataKey.Value;
            typeDepartment = ClassHelper.Helper.GetTypeDepartment(index);
            TxtName.Text = typeDepartment.Name;
            TxtDescririon.Text = typeDepartment.Description;
            DropDownList1.SelectedValue = typeDepartment.RuleIdPosition.ToString();
            if(typeDepartment.IdParent!=null)
                DropDownList2.SelectedValue = typeDepartment.IdParent.ToString();
            ButtonCancel.Visible = true;
            ButtonSave.Visible = true;
            ButtonCreate.Visible = false;

        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            int index = (int)GridView1.SelectedDataKey.Value;
            typeDepartment = ClassHelper.Helper.GetTypeDepartment(index);

            typeDepartment.Name = TxtName.Text;
            typeDepartment.Description = TxtDescririon.Text;
            typeDepartment.IdParent = (int)GridView1.SelectedDataKey.Value;
            typeDepartment.RuleIdPosition = (int)GridView1.SelectedDataKey.Value;
            ButtonCancel.Visible = false;
            ButtonSave.Visible = false;
            ButtonCreate.Visible =true;
            ClassHelper.Helper.Save();
            GridView1.DataBind();
            DropDownList2.DataBind();
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            TxtDescririon.Text = "";
            TxtName.Text = "";
            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            ButtonCancel.Visible = false;
            ButtonSave.Visible = false;
            ButtonCreate.Visible =true;
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            DropDownList2.DataBind();
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
        }
    }
}