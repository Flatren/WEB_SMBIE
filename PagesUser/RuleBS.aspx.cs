using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BDSystemSMBIEContext;
using System.Text;

namespace WEB_SMBIE.PagesUser
{
    public partial class RuleBS : System.Web.UI.Page
    {
        BDSystemSMBIEContext.User user;

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
                   TreeViewFolder.Nodes.Clear();
                    TreeViewFolder.Nodes.Add(SetTree(user.Folder));
                    //UpdateGridViewBS();
                }
                //UpdateGridViewBS();
                ButtonCreate.Visible = TreeViewFolder.SelectedNode != null;
                ButtonEdit.Visible = TreeViewFolder.SelectedNode != null;
                ButtonDelete.Visible = TreeViewFolder.SelectedNode != null;
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        TreeNode SetTree(Folder folder)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = folder.Name+" ("+folder.FolderFiles.Count.ToString()+")";
            treeNode.Value = folder.Id.ToString();
            var folders = (from re in ClassHelper.Helper.BDSystem.Folders where re.IdParent == folder.Id select re).ToList();
            foreach (var item in folders)
            {
                treeNode.ChildNodes.Add(SetTree(item));
            }
            return treeNode;
        }

        void UpdateGridViewBS()
        {
            if (TreeViewFolder.SelectedNode != null)
            {
                int id = int.Parse(TreeViewFolder.SelectedNode.Value);

                var folders = (from re in ClassHelper.Helper.BDSystem.Folders where re.Id == id select re).ToList()[0];
                var list = new List<ClassHelper.CBSLink>();
                foreach (var item in folders.FolderFiles)
                {
                    list.Add(new ClassHelper.CBSLink(item.BSLink));
                }
                GVBSLink.DataSource = list;
                GVBSLink.DataBind();
            }
        }

        protected void TreeViewFolder_SelectedNodeChanged(object sender, EventArgs e)
        {
            PanelCreatBS.Visible = true;
            PanelExport.Visible = true;
            PanelImport.Visible = true;
            PanelEditBS.Visible = false;
            LabeActivFolder.Text = TreeViewFolder.SelectedNode.Text;
            TreeViewFolder.Target = TreeViewFolder.SelectedNode.Value;
            TreeViewFolder.SelectedNode.Selected = true;
            ButtonDelete.Visible = TreeViewFolder.SelectedNode.Parent != null;
            UpdateGridViewBS();
        }

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {

            ButtonOK.Visible = true;
            ButtonRename.Visible = false;
            PlaceHolderCreate.Visible = true;
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            PlaceHolderCreate.Visible = false;
        }

        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            
            PlaceHolderCreate.Visible = true;
            ButtonOK.Visible = false;
            ButtonRename.Visible = true;
           // TextBoxNameFolder.Text = TreeViewFolder.;

        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonOK_Click(object sender, EventArgs e)
        {
            var newfolder = new Folder();
            newfolder.Name = TextBoxNameFolder.Text;
            if (string.IsNullOrEmpty(TreeViewFolder.Target))
            {
                newfolder.IdParent = user.IdMainFolder;
            }
            else
                newfolder.IdParent = int.Parse(TreeViewFolder.Target);
            ClassHelper.Helper.BDSystem.Folders.InsertOnSubmit(newfolder);
            ClassHelper.Helper.Save();
        }

        protected void ButtonRename_Click(object sender, EventArgs e)
        {
            Folder folder;
            int id = 0;
            if (TreeViewFolder.Target == null)
                folder = user.Folder;
            else
            {
                id = int.Parse(TreeViewFolder.Target);
                folder = (from re in ClassHelper.Helper.BDSystem.Folders where re.IdParent == id select re).ToList()[0];
            }
            folder.Name = TextBoxNameFolder.Text;
            ClassHelper.Helper.Save();
        }

        protected void ButtonImport_Click(object sender, EventArgs e)
        {
            if (TreeViewFolder.SelectedNode == null) { return; }
            string extension = System.IO.Path.GetExtension(FileUploadImport.FileName);
            var bytes = FileUploadImport.FileBytes; // файл в битовом представлении
          
            if (extension == ".bib" || extension == ".bibtex")
            {
                var to_s = System.Text.Encoding.UTF8.GetString(bytes); //преобразование в строку
                var list = ClassHelper.CBSLink.GetListBsLink(to_s);
                int id_folder = int.Parse(TreeViewFolder.SelectedValue);
                var folder = (from re in ClassHelper.Helper.BDSystem.Folders where re.Id == id_folder select re).ToList()[0];
                foreach (var item in list)
                {
                    FolderFile folderFile = new FolderFile();
                    folderFile.BSLink = item.bSLink;
                    folderFile.Folder = folder;
                    ClassHelper.Helper.BDSystem.FolderFiles.InsertOnSubmit(folderFile);
                    ClassHelper.Helper.Save();
                }
               // ClassHelper.Helper.Save();
                UpdateGridViewBS();
            }
            if (extension == ".csv")
            {
                using(var stream = new System.IO.MemoryStream(bytes))
                using (var reader = new System.IO.StreamReader(stream))
                using (var csv = new CsvHelper.CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
                {
                    bool is_header= false;
                    int id_folder = int.Parse(TreeViewFolder.SelectedValue);
                    var folder = (from re in ClassHelper.Helper.BDSystem.Folders where re.Id == id_folder select re).ToList()[0];
                    while (csv.Read())
                    {
                        if (!is_header)
                        {
                            csv.ReadHeader();
                            is_header = true;
                        }
                        if (is_header)
                        {
                            string type = csv.GetField("Type");
                            ClassHelper.CBSLink bSLink = new ClassHelper.CBSLink();
                            if (bSLink.SetTypeRecord(type))
                            {
                                string value = "";
                                foreach (var item in ClassHelper.Helper.BDSystem.RecordFields)
                                {
                                    value = csv.GetField(item.Sysname);
                                    if (!string.IsNullOrWhiteSpace(value))
                                        bSLink.SetFiled(item.Sysname, value);
                                }                                
                                bSLink.Save();
                                FolderFile folderFile = new FolderFile();
                                folderFile.BSLink = bSLink.bSLink;
                                folderFile.Folder = folder;
                                ClassHelper.Helper.BDSystem.FolderFiles.InsertOnSubmit(folderFile);
                            }
                        }
                    }
                    ClassHelper.Helper.Save();
                    UpdateGridViewBS();

                }
            }
        }

        protected void DDLTypeRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void ButtonButtonBeginCreateBS_Click(object sender, EventArgs e)
        {
            PanelAdd.Visible = true;
            ButtonBeginCreateBS.Visible = false;
        }

        protected void ButtonSaveData_Click(object sender, EventArgs e)
        {
            string sid_typeRecord = DDLTypeRecords.SelectedItem.Value;
            int id_typeRecord = int.Parse(sid_typeRecord);
            TypeRecord typeRecord = (from re in ClassHelper.Helper.BDSystem.TypeRecords where re.Id == id_typeRecord select re).ToList()[0];
            ClassHelper.CBSLink bSLink = new ClassHelper.CBSLink();
            bSLink.bSLink.TypeRecord = typeRecord;
            foreach (GridViewRow item in GridViewFielders.Rows)
            {
                string sysname = item.Cells[1].Text;
                string value = ((TextBox)item.Cells[2].Controls[1]).Text;
                bSLink.SetFiled(sysname, value);
            }
            string error = bSLink.CheckedNorm();
            if (!string.IsNullOrWhiteSpace(error))
            {
                ErrorLabel.Text = "Заполнены не все необходимые поля\n"+error;
                return;
            }
            if (string.IsNullOrEmpty(TreeViewFolder.SelectedValue))
            {
                ErrorLabel.Text += "\n Не задана папка добавления";
                return;
            }
            int id_folder = int.Parse(TreeViewFolder.SelectedValue);
            var folder = (from re in ClassHelper.Helper.BDSystem.Folders where re.Id == id_folder select re).ToList()[0];
            if (FileUploadBS.HasFile)
            {
                Document document = new Document();
                document.DataFile = FileUploadBS.FileBytes;
                document.Name = FileUploadBS.FileName;
                bSLink.bSLink.Document = document;
            }
            FolderFile folderFile = new FolderFile();
            folderFile.BSLink = bSLink.bSLink;
            folderFile.Folder = folder;
            bSLink.Save();
            ClassHelper.Helper.BDSystem.FolderFiles.InsertOnSubmit(folderFile);
            ClassHelper.Helper.Save();
            UpdateGridViewBS();
            PanelAdd.Visible = false;
            ButtonBeginCreateBS.Visible = true;
        }

        protected void ButtonCancelBS_Click(object sender, EventArgs e)
        {
            PanelAdd.Visible = false;
            ButtonBeginCreateBS.Visible = true;
        }

        protected void GVBSLink_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void GVBSLink_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            
        }

        protected void GVBSLink_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sid_bslink = e.Values[0].ToString();
            int id_bs = int.Parse(sid_bslink);
            var bSLinks = (from re in ClassHelper.Helper.BDSystem.BSLinks where re.Id == id_bs select re).ToList();
            if (bSLinks.Count > 0)
            {

                ClassHelper.Helper.BDSystem.FolderFiles.DeleteOnSubmit(bSLinks[0].FolderFiles[0]);
                ClassHelper.Helper.BDSystem.BSLinks.DeleteOnSubmit(bSLinks[0]);

                ClassHelper.Helper.Save();
               
            }
            UpdateGridViewBS();
        }

        protected void GVBSLink_RowEditing(object sender, GridViewEditEventArgs e)
        {
         
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            
        }

        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            string format = RadioButtonListFormat.SelectedItem.Value;
            string namefile = TextBoxNameFile.Text;
            if (namefile == "") namefile = "default";
            List<ClassHelper.CBSLink> bSLinks = new List<ClassHelper.CBSLink>();
            foreach (GridViewRow item in GVBSLink.Rows)
            {
                if (((CheckBox)item.Cells[0].Controls[1]).Checked)
                {
                    int id_link = int.Parse(item.Cells[1].Text);
                    BSLink bSLink = (from re in ClassHelper.Helper.BDSystem.BSLinks where id_link == re.Id select re).ToList()[0];
                    bSLinks.Add(new ClassHelper.CBSLink(bSLink));
                }
            }
            switch (format)
            {
                case "bibtex":
                    {
                        StringBuilder builder = new StringBuilder();
                        foreach (var item in bSLinks)
                        {
                            builder.Append(item.ToBibTex());
                            builder.AppendLine();
                            builder.AppendLine();
                        }
                        byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
                        SendFile(namefile + "." + format, data);
                    }
                    break;
                case "csv":
                    {
                        byte[] data;
                        using (var stream = new System.IO.MemoryStream())
                        {
                            using (var writer = new System.IO.StreamWriter(stream))
                            using (var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
                            {
                                if (bSLinks.Count > 0)
                                    bSLinks[0].ToCVS(csv, true);

                                for (int i = 1; i < bSLinks.Count; i++)
                                {
                                    bSLinks[i].ToCVS(csv);
                                }
                            }
                            data = stream.GetBuffer();
                            SendFile(namefile + "." + format, data);
                        }
                        
                    }
                    break;
                default:
                    break;
            }
        }

        protected void SendFile(string namefile, byte[] data)
        { 
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + namefile);
            Response.ContentType = "application/octet-strean";
            Response.OutputStream.Write(data, 0, data.Length);
            Response.End();
        }

        protected void GVBSLink_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GVBSLink_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            PanelCreatBS.Visible = true;
            PanelExport.Visible = true;
            PanelImport.Visible = true;
            PanelEditBS.Visible = false;
        }

        protected void GVBSLink_SelectedIndexChanging(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                PanelCreatBS.Visible = false;
                PanelExport.Visible = false;
                PanelImport.Visible = false;
                PanelEditBS.Visible = true;
                int id_bs = int.Parse(GVBSLink.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text);
                BSLink sLink = (from re in ClassHelper.Helper.BDSystem.BSLinks where re.Id == id_bs select re).ToList()[0];
                DropDownListDataTypesEdit.SelectedValue = sLink.TypeRecord.Id.ToString();
                ClassHelper.CBSLink cBSLink = new ClassHelper.CBSLink(sLink);
                var i = GridViewDataEditBS.Rows;
                PanelHasFile.Visible = sLink.Document != null;
                
                foreach (GridViewRow item in GridViewDataEditBS.Rows)
                {
                    ((TextBox)item.Cells[2].Controls[1]).Text = cBSLink.GetFiled(item.Cells[1].Text);
                }
            }            
        }

        protected void ButtonEditCancel_Click(object sender, EventArgs e)
        {
            PanelCreatBS.Visible = true;
            PanelExport.Visible = true;
            PanelImport.Visible = true;
            PanelEditBS.Visible = false;
        }

        protected void ButtonDeleteFile_Click(object sender, EventArgs e)
        {
           var id = GVBSLink.Rows[GVBSLink.SelectedIndex].Cells[1].Text;
           BSLink sLink = ClassHelper.Helper.GetBSLink(int.Parse(id));
           ClassHelper.Helper.BDSystem.Documents.DeleteOnSubmit(sLink.Document);
           sLink.Document = null;
           PanelHasFile.Visible = false;
           ClassHelper.Helper.Save();
        }

        protected void ButtonDownloadFile_Click(object sender, EventArgs e)
        {
            var id = GVBSLink.Rows[GVBSLink.SelectedIndex].Cells[1].Text;
            BSLink sLink = ClassHelper.Helper.GetBSLink(int.Parse(id));
            SendFile(sLink.Document.Name, sLink.Document.DataFile);
        }

        protected void ButtonEditUpdata_Click(object sender, EventArgs e)
        {
            var id = GVBSLink.Rows[GVBSLink.SelectedIndex].Cells[1].Text;
            BSLink sLink = ClassHelper.Helper.GetBSLink(int.Parse(id));
            ClassHelper.CBSLink cBSLink = new ClassHelper.CBSLink(sLink);
            if (FileUploadUpFile.HasFile)
            {
                if(sLink.Document!=null)
                    ButtonDeleteFile_Click(sender, e); 
                Document document = new Document();
                document.DataFile = FileUploadUpFile.FileBytes;
                document.Name = FileUploadUpFile.FileName;
                sLink.Document = document;
            }
            foreach (GridViewRow item in GridViewDataEditBS.Rows)
            {
                cBSLink.SetFiled( item.Cells[1].Text,((TextBox)item.Cells[2].Controls[1]).Text);
            }
            cBSLink.Save();
            ClassHelper.Helper.Save();
            ButtonEditCancel_Click(sender, e);
            UpdateGridViewBS();
        }
    }
}