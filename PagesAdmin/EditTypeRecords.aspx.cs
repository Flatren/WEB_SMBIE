using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BDSystemSMBIEContext;
namespace WEB_SMBIE.PagesAdmin
{
    public partial class EditRelationship : UserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (user.Department.TypeDepartment.Position.Rang != 0)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
           NullDataGridView();
            int id = (int)GridView1.DataKeys[e.NewEditIndex].Value;
            var type = (from re in ClassHelper.Helper.BDSystem.TypeRecords where re.Id == id select re).ToList()[0];
            foreach (var _item in type.Relationships)
            {
                int i = 0;
                bool next = true;
                for (; i < GridView2.DataKeys.Count && next; i++)
                {
                    next = (int)GridView2.DataKeys[i].Value != _item.IdField; 
                }
                if (!next)
                {
                    i--;
                    ((CheckBox)GridView2.Rows[i].Cells[3].Controls[1]).Checked = _item.Typefielder == 1;
                    ((CheckBox)GridView2.Rows[i].Cells[4].Controls[1]).Checked = _item.Typefielder == 2;
                }
            }
        }

        

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

            int id = (int)GridView1.DataKeys[GridView1.EditIndex].Value;
            TypeRecord type = (from re in ClassHelper.Helper.BDSystem.TypeRecords where id == re.Id select re).ToList()[0];
            int n = GridView2.Rows.Count;
            foreach(var item in type.Relationships)
                ClassHelper.Helper.BDSystem.Relationships.DeleteOnSubmit(item);
            
            for (int i = 0; i < n; i++)
            {
                if (((CheckBox)GridView2.Rows[i].Cells[3].Controls[1]).Checked)
                {
                    Relationship relationship = new Relationship();
                    relationship.IdField = (int)GridView2.DataKeys[i].Value;
                    relationship.IdType = id;
                    relationship.Typefielder = 1;
                    type.Relationships.Add(relationship);
                }
                else
                {
                    if (((CheckBox)GridView2.Rows[i].Cells[4].Controls[1]).Checked)
                    {
                        Relationship relationship = new Relationship();
                        relationship.IdField = (int)GridView2.DataKeys[i].Value;
                        relationship.IdType = id;
                        relationship.Typefielder = 2;
                        type.Relationships.Add(relationship);
                    }
                }
            }
            ClassHelper.Helper.Save();
            NullDataGridView();
        }

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            
            TypeRecord type = new TypeRecord();
            type.Name = TxtName.Text;
            type.Description = TxtDescription.Text;
            type.Sysname = TxtSysname.Text;
            int n = GridView2.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                if (((CheckBox)GridView2.Rows[i].Cells[3].Controls[1]).Checked)
                {


                    Relationship relationship = new Relationship();
                    relationship.IdField = (int)GridView2.DataKeys[i].Value;
                    relationship.Typefielder = 1;
                    type.Relationships.Add(relationship);
                }
                else
                {
                    if (((CheckBox)GridView2.Rows[i].Cells[4].Controls[1]).Checked)
                    {
                        Relationship relationship = new Relationship();
                        relationship.IdField = (int)GridView2.DataKeys[i].Value;
                        relationship.Typefielder = 2;
                        type.Relationships.Add(relationship);
                    }
                }
            }
            ClassHelper.Helper.BDSystem.TypeRecords.InsertOnSubmit(type);
            ClassHelper.Helper.Save();
            NullDataGridView();

        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            NullDataGridView();
        }
        void NullDataGridView()
        {
            foreach (GridViewRow item in GridView2.Rows)
            {
                ((CheckBox)item.Cells[3].Controls[1]).Checked = false;
                ((CheckBox)item.Cells[4].Controls[1]).Checked = false;
            }
        }

        protected void ButtonZero_Click(object sender, EventArgs e)
        {
            
            NullDataGridView();
        }
    }
}