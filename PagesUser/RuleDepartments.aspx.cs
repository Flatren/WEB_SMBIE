using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BDSystemSMBIEContext;
namespace WEB_SMBIE.PagesUser
{
    public partial class RuleDeparment : System.Web.UI.Page
    {
        BDSystemSMBIEContext.User user;
        Department department;
        TypeDepartment childtype;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["token"] != null)
            {
                user = ClassHelper.Helper.Auth(Request.Cookies["token"].Value);
            }
            if (user != null)
            {
                if (!Page.IsPostBack)
                {
                    TreeView1.Nodes.Add(SetTree(user.Department));
                }

                if (TreeView1.SelectedValue != "")
                {
                    department = ClassHelper.Helper.GetDepartment(int.Parse(TreeView1.SelectedValue));
                    Labelcorrent.Text = department.Name;
                }
                else
                    department = user.Department;

                if (department!=null)
                {
                    childtype = ClassHelper.Helper.GetTypeDepartmentChild(department.TypeDepartment);
                }

                if (childtype != null)
                {
                    ButtonAdd.Visible = true;
                    ButtonAdd.Text = "Создать \"" + childtype.Name + "\"";
                    if (department.IdUserHead == null)
                    {
                       // if (user.Department != department)
                            //ButtonAddPersonal.Text = "Назначить главу";
                    }
                    //ButtonAddPersonal.Visible = user.Department != department;
                }
                else
                {
                    ButtonAdd.Visible = false;
                    //ButtonAddPersonal.Text = "Назначить сотрудника";
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        TreeNode SetTree(Department unit)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = unit.Name;
            treeNode.Value = unit.Id.ToString();
            var units = (from re in ClassHelper.Helper.BDSystem.Departments where re.IdParentDep == unit.Id select re).ToList();
            foreach (var item in units)
            {
                treeNode.ChildNodes.Add(SetTree(item));
            }
            return treeNode;
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            StatusCreateUnit(true);           
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StatusCreateUnit(false);
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e )
        {
            var i = TreeView1.SelectedNode;

        }

        protected void ButtonInsert_Click(object sender, EventArgs e)
        {
            Department depnew = new Department();
            depnew.Name = txtName.Text;
            depnew.Description = txtDescription.Text;
            depnew.IdParentDep = department.Id;
            depnew.TypeDepartment = childtype;
            ClassHelper.Helper.BDSystem.Departments.InsertOnSubmit(depnew);
            ClassHelper.Helper.Save();
            GridView1.DataBind();
            TreeNode treeNode = new TreeNode();

            treeNode.Text = depnew.Name;
            treeNode.Value = depnew.Id.ToString();

            GetNode(TreeView1.Nodes, TreeView1.SelectedValue).ChildNodes.Add(
                treeNode
                );
            ButtonCancel_Click(sender, e);
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
           
        }

        TreeNode GetNode(TreeNodeCollection root,string key)
        {
            TreeNode res = null;
            foreach (TreeNode item in root)
            {
                if (item.Value == key)
                    return item;
                res = GetNode(item.ChildNodes, key);
                if (res != null)
                    return res;
            }
            return res;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Department deldep = ClassHelper.Helper.GetDepartment(int.Parse(e.Keys[0].ToString()));
            TreeNode treeNode = GetNode(TreeView1.Nodes, e.Keys[0].ToString());
            Delete(treeNode);
            treeNode.Parent.ChildNodes.Remove(treeNode);
        }

        void Delete(TreeNode node)
        {
            ClassHelper.Helper.BDSystem.Departments.DeleteOnSubmit(ClassHelper.Helper.GetDepartment(int.Parse(node.Value)));
            foreach (TreeNode item in node.ChildNodes)
            {
                Delete(item);
            }
        }

        void StatusCreatePerson()
        {
            //PanelTableUnits.Visible = false;
            PanelCreatePerson.Visible = true;
            PanelShowWorker.Visible = false;
            
        }

        void StatusShowData()
        {
            PanelCreatePerson.Visible = false;
            //PanelTableUnits.Visible = true;
            PanelShowWorker.Visible = true;

        }

        protected void ButtonAddPersonal_Click(object sender, EventArgs e)
        {
            StatusCreatePerson();
        }



        // Состояния

        //Создание подразделения

        void StatusCreateUnit(bool show)
        {
            PanelCreateEdit.Visible = show;
            PanelTableUnit.Visible = !show;
        }

        void StatusEditWorker(bool show)
        {
            //PanelTableUnits.Visible = !show;
            PanelShowWorker.Visible = show;
        }

        void StatusCreateWorker(bool show)
        {
            PanelCreatePerson.Visible = show;
            PanelShowWorker.Visible = !show;
        }

        protected void btnNextProfile_Click(object sender, EventArgs e)
        {
            //LinqDataSource3.WhereParameters[1].
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Click(object sender, EventArgs e)
        {

        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnEditHead_Click(object sender, EventArgs e)
        {
            PanelShowWorker.Visible = false;
            PanelTableUnit.Visible = false;
            PanelTableWorker.Visible = true;
        }

        protected void ButtonCancelSelectWork_Click(object sender, EventArgs e)
        {
            PanelShowWorker.Visible = true;
            PanelTableUnit.Visible = true;
            PanelTableWorker.Visible = false;
        }

        protected void CreatePerson_Click(object sender, EventArgs e)
        {
            PanelCreatePerson.Visible = true;
            PanelTableWorker.Visible = false;
        }

        protected void btnCancelPerson_Click(object sender, EventArgs e)
        {
            PanelCreatePerson.Visible = false;
            PanelTableWorker.Visible = true;
        }

        protected void btnCreatePerson_Click(object sender, EventArgs e)
        {
            BDSystemSMBIEContext.User usernew = new BDSystemSMBIEContext.User();
            usernew.Character = txtCharacter.Text;
            usernew.Department = department;            
            usernew.Phone = txtPhone.Text;
            usernew.Password = txtPassword.Text;
            usernew.Login = txtLogin.Text;
            usernew.FIO = txtFIO.Text;
            Folder folder = new Folder();
            folder.Name = usernew.Login + new Random().Next(1000, 10000).ToString();
            usernew.Folder = folder;
            ClassHelper.Helper.BDSystem.Users.InsertOnSubmit(usernew);
            ClassHelper.Helper.Save();
            department.IdUserHead = usernew.Id;
            PanelCreatePerson.Visible = false;
            PanelShowWorker.Visible = true;
            PanelTableUnit.Visible = true;
            PanelTableWorker.Visible = false;
            GridView2.DataBind();
        }
    }
}