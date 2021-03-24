using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BDSystemSMBIEContext;

namespace WEB_SMBIE.PagesAdmin
{
    public partial class EditPosition : UserPage
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
            if (
                    !(string.IsNullOrWhiteSpace(TxtDescririon.Text)
                    || string.IsNullOrWhiteSpace(TxtName.Text)
                    || string.IsNullOrWhiteSpace(TxtRang.Text))
                )
            {

                Position position = new Position();

                position.Description = TxtDescririon.Text;
                position.Name = TxtName.Text;
                position.Rang = int.Parse(TxtRang.Text);

                TxtDescririon.Text="";
                TxtName.Text="";
                TxtRang.Text="";

                try
                {
                    ClassHelper.Helper.BDSystem.Positions.InsertOnSubmit(position);
                    ClassHelper.Helper.Save();
                    GridView1.DataBind();

                    TxtDescririon.Text = "";
                    TxtName.Text = "";
                    TxtRang.Text = "";
                    Label1.Text = "";
                }
                catch
                {
                    Label1.Text = "Данные не корректны";
                    
                }
                
            }
            else
            {
                Label1.Text = "Данные не корректны";
            }
        }
    }
}