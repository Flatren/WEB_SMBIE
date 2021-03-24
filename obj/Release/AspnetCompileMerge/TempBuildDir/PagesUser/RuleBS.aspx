<%@ Page Title="Управление БС" Language="C#" AutoEventWireup="true" MasterPageFile="~/PagesUser/User.Master" CodeBehind="RuleBS.aspx.cs" Inherits="WEB_SMBIE.PagesUser.RuleBS" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <asp:Table CssClass="table" ID="TableOne" runat="server" Height="138px" style="margin-bottom: 20px">
        <asp:TableRow >
            <asp:TableCell Width="25%">
                <div>
                    <asp:Label ID="Label1" runat="server" Text="Активная папка: "></asp:Label>
                     <asp:Label ID="LabeActivFolder" runat="server" Text=""></asp:Label>
                </div>

                <div>
                    <asp:Panel runat="server">
                        <asp:Button ID="ButtonCreate" runat="server" Style="margin-bottom:5px;" Width="100%" CssClass="btn" Text="Создать" OnClick="ButtonCreate_Click" /><br />
                        <asp:Button ID="ButtonEdit" runat="server" Style="margin-bottom:5px;" Width="100%" CssClass="btn" Text="Переименовать" OnClick="ButtonEdit_Click" /><br />
                        <asp:Button ID="ButtonDelete" runat="server" Width="100%" CssClass="btn" Text="Удалить" OnClick="ButtonDelete_Click"/><br />
                    </asp:Panel>
                </div>
                <asp:PlaceHolder Visible="false" ID="PlaceHolderCreate" runat="server">
                    <asp:Table ID="Table2" CssClass="table" runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="LabelTypeOperation" runat="server" Text="Создать папку"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="TextBoxNameFolder" runat="server"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button ID="ButtonOK" CssClass="btn"  runat="server" Text="Ок" OnClick="ButtonOK_Click" />
                                <asp:Button ID="ButtonRename" CssClass="btn" Visible="false" runat="server" Text="Ок" OnClick="ButtonRename_Click" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="ButtonCancel" CssClass="btn" runat="server" Text="Отмена" OnClick="ButtonCancel_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                
                </asp:PlaceHolder>

                <asp:TreeView OnSelectedNodeChanged="TreeViewFolder_SelectedNodeChanged" ID="TreeViewFolder" runat="server">
                            <Nodes>
                            </Nodes>
                </asp:TreeView>
            </asp:TableCell>

            <asp:TableCell Width="30%">
                <h3>Библиографические ссылки</h3>
               <%--OnRowCancelingEdit="GVBSLink_RowCancelingEdit"  OnRowUpdated="GVBSLink_RowUpdated" OnRowEditing="GVBSLink_RowEditing" OnRowUpdating="GVBSLink_RowUpdating"--%>
                <asp:GridView CssClass="table" runat="server" ID="GVBSLink" AutoGenerateColumns="false" AllowSorting="True" AllowPaging="True" OnRowDeleting="GVBSLink_RowDeleting" OnRowCommand="GVBSLink_SelectedIndexChanging"  OnRowDeleted="GVBSLink_RowDeleted"> 
                <Columns>
                    <asp:TemplateField HeaderText ="Выбрать">
                            <ItemTemplate>
                               <asp:CheckBox runat="server"></asp:CheckBox> 
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Id"  HeaderText="ID" Visible="true" ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="Title" HeaderText="Заголовок" ReadOnly="True" InsertVisible="False"></asp:BoundField>
                    <asp:BoundField DataField="Author" HeaderText="Автор" ReadOnly="True" InsertVisible="False"></asp:BoundField>
                    <asp:BoundField DataField="Tag" HeaderText="Тег" ReadOnly="True" InsertVisible="False"></asp:BoundField>
                    <asp:BoundField DataField="Year" HeaderText="Год" ReadOnly="True" InsertVisible="False"></asp:BoundField>
                    <asp:CommandField ShowSelectButton="true" ShowDeleteButton="true"></asp:CommandField>
                    <%--<asp:CommandField ShowDeleteButton="True"></asp:CommandField>--%>
                    
                </Columns>
                </asp:GridView>

            </asp:TableCell>

            <asp:TableCell HorizontalAlign="Center">
                
                <asp:Panel ID="PanelExport" BorderWidth="2px" Style="padding:5px;" BorderStyle="Dashed" runat="server">
                    <h4>Экспорт</h4>
                    <asp:Label Text="Название файла" runat="server" />
                    <asp:TextBox ID="TextBoxNameFile" runat="server" Text="default"></asp:TextBox>
                    <asp:RadioButtonList ID="RadioButtonListFormat" runat="server">
                        <asp:ListItem Selected="True" Value="bibtex"> 
                            .BibTex
                        </asp:ListItem>
                        <asp:ListItem Value="csv">
                            .CSV
                        </asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Button ID="ButtonExport" runat="server" CssClass="btn" Width="40%" Text="Экспорт" OnClick="ButtonExport_Click" /> 
                </asp:Panel>

                <asp:Panel ID="PanelImport" Style="margin-top:10px;padding:5px;" BorderWidth="2px" BorderStyle="Dashed" runat="server">
                    <h4>Импорт</h4>
                    <asp:FileUpload ID="FileUploadImport" CssClass="btn" runat="server"/>
                    <asp:Button ID="Button1" runat="server" CssClass="btn" Width="40%" Text="Импорт" OnClick="ButtonImport_Click"/>
                </asp:Panel>
                <br />
                <br />
                <asp:Panel ID="PanelCreatBS" Style="margin-top:10px;padding:5px;" BorderWidth="2px" BorderStyle="Dashed" runat="server">
                    <asp:Button ID="ButtonBeginCreateBS" CssClass="btn" runat="server" Width="40%"  Text="Создать БС" OnClick="ButtonButtonBeginCreateBS_Click"/>
                        <br />
                         <br />
                    <asp:Panel ID="PanelAdd"  runat="server" Visible="false">
                        <span>Тип записи:</span>
                        <asp:DropDownList ID="DDLTypeRecords" runat="server" DataSourceID="LDSRecordTypes" DataTextField="Sysname" DataValueField="Id" AutoPostBack="true" OnSelectedIndexChanged="DDLTypeRecords_SelectedIndexChanged"></asp:DropDownList>
                        <br />
                         <br />
                        <asp:LinqDataSource runat="server" EntityTypeName="" ID="LDSRecordTypes" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" TableName="TypeRecords"></asp:LinqDataSource>
                        <asp:Label ID="ErrorLabel" runat="server" CssClass="alert-danger"></asp:Label>
                        <asp:GridView ID="GridViewFielders" CssClass="table" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="LDSFieldRecords" >
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" Visible="false" InsertVisible="False" SortExpression="Id"></asp:BoundField>
                                <asp:BoundField DataField="RecordField.sysname" HeaderText="Название" SortExpression="Typefielder" ></asp:BoundField>
                                <asp:TemplateField HeaderText ="Основное">
                                        <ItemTemplate>
                                            <asp:Textbox Width="100%" runat="server"></asp:Textbox> 
                                        </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                                            <span>Привязать файл:</span>
                    <asp:FileUpload ID="FileUploadBS" CssClass="btn" runat="server"/>
                        <asp:Button ID="ButtonSaveData" runat="server" CssClass="btn" Width=""  Text="Добавить БС" OnClick="ButtonSaveData_Click"/> 
                        <asp:Button ID="ButtonCancelBS" runat="server" CssClass="btn" Width=""  Text="Отменить" OnClick="ButtonCancelBS_Click"/>
                        <asp:LinqDataSource runat="server" EntityTypeName="" ID="LDSFieldRecords" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" TableName="Relationships" Where="IdType == @IdType">
                            <WhereParameters>
                                <asp:ControlParameter ControlID="DDLTypeRecords" PropertyName="SelectedValue" Name="IdType" Type="Int32"></asp:ControlParameter>
                            </WhereParameters>
                        </asp:LinqDataSource>
                </asp:Panel>
                </asp:Panel>
                
                <asp:Panel ID="PanelEditBS" Visible="false" Style="margin-top:10px;padding:5px;" BorderWidth="2px" BorderStyle="Dashed" runat="server">
                     <asp:Label ID="LabelEdit" runat="server"> <h3>Редактирование записи</h3></asp:Label>
                     <asp:DropDownList ID="DropDownListDataTypesEdit" runat="server" DataSourceID="LDSRecordTypes" DataTextField="Sysname" DataValueField="Id" Enabled="false"></asp:DropDownList>
                    <asp:LinqDataSource runat="server" EntityTypeName="" ID="LinqDataSourceDataFieldsEdit" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" TableName="Relationships" Where="IdType == @IdType">
                        <WhereParameters>
                            <asp:ControlParameter ControlID="DropDownListDataTypesEdit" PropertyName="SelectedValue" DefaultValue="-1" Name="IdType" Type="Int32"></asp:ControlParameter>
                        </WhereParameters>
                    </asp:LinqDataSource>
                    <asp:GridView ID="GridViewDataEditBS" CssClass="table" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="LinqDataSourceDataFieldsEdit">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" Visible="false" InsertVisible="False" SortExpression="Id"></asp:BoundField>
                            <asp:BoundField DataField="RecordField.sysname" HeaderText="Название" SortExpression="Typefielder"></asp:BoundField>
                            <asp:TemplateField HeaderText="Основное">
                                <ItemTemplate>
                                    <asp:TextBox Width="100%" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>     
                    </asp:GridView>
                    <asp:Panel runat="server" BorderStyle="Double" Style="margin-bottom:5px;margin-top:5px; padding:5px;">
                        <asp:Label runat="server"><h4>Работа с файлом</h4></asp:Label>
                        <asp:Panel runat="server" ID="PanelHasFile">
                            <asp:Button ID="ButtonDownloadFile" runat="server" CssClass="btn" Width=""  Text="Скачать" OnClick="ButtonDownloadFile_Click"/>
                            <asp:Button ID="ButtonDeleteFile" runat="server" CssClass="btn" Width=""  Text="Удалить" OnClick="ButtonDeleteFile_Click"/>
                        </asp:Panel>
                        <asp:FileUpload ID="FileUploadUpFile" CssClass="btn" runat="server"/>
                    </asp:Panel>
                   <asp:Button ID="ButtonEditUpdata" runat="server" CssClass="btn" Width=""  Text="Обновить" OnClick="ButtonEditUpdata_Click"/> 
                        <asp:Button ID="ButtonEditCancel" runat="server" CssClass="btn" Width=""  Text="Отменить" OnClick="ButtonEditCancel_Click"/>
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

</asp:Content>