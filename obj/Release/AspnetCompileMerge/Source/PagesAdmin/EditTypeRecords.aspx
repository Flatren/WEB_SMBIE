<%@ Page Title="Ред. типов БС" Language="C#" MasterPageFile="~/PagesAdmin/Admin.Master"  AutoEventWireup="true" CodeBehind="EditTypeRecords.aspx.cs" Inherits="WEB_SMBIE.PagesAdmin.EditRelationship" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>

 <div>
        <asp:Table ID="Table2" CssClass="table " runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label Text="Название" runat="server"/></asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="TxtName" MaxLength="50" Width="100%" runat="server"></asp:TextBox>
                    </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label Text="Системное название" runat="server"/></asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="TxtSysname" MaxLength="50" Width="100%" runat="server"></asp:TextBox>
                    </asp:TableCell></asp:TableRow><asp:TableRow>
                    <asp:TableCell>
                        <asp:Label Text="Описание" runat="server"/></asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="TxtDescription" TextMode="MultiLine" Rows="5" Columns="60" MaxLength="256" runat="server"></asp:TextBox>
                    </asp:TableCell></asp:TableRow></asp:Table><asp:Button ID="ButtonCreate" runat="server" Text="Создать" OnClick="ButtonCreate_Click" />
        <hr />
    </div>

    <asp:Table ID="Table1" CssClass="table" runat="server">

        <asp:TableRow>
            <asp:TableCell Width="75%">
<asp:GridView ID="GridView1"   CssClass="table table-striped" runat="server" AutoGenerateColumns="False" DataSourceID="LinqDataSource1" AllowPaging="True" AllowSorting="True" DataKeyNames="Id" ViewStateMode="Inherit" OnRowEditing="GridView1_RowEditing" OnRowUpdated="GridView1_RowUpdated" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit"><Columns>
            <asp:BoundField DataField="Id" Visible="false" HeaderText="Id" ReadOnly="True" SortExpression="Id" InsertVisible="False"></asp:BoundField>
            <asp:BoundField DataField="Name" HeaderText="Название" SortExpression="Name"></asp:BoundField>
            <asp:BoundField DataField="Description" HeaderText="Описание" SortExpression="Description"></asp:BoundField>
            <asp:BoundField DataField="Sysname" HeaderText="Системное имя" SortExpression="Sysname"></asp:BoundField>
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True"></asp:CommandField>
        </Columns>
        </asp:GridView>
        <asp:LinqDataSource runat="server" EntityTypeName="" ID="LinqDataSource1" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" TableName="TypeRecords" EnableDelete="True" EnableInsert="True" EnableUpdate="True"></asp:LinqDataSource>

            </asp:TableCell>
            <asp:TableCell> 
                <asp:Button ID="ButtonZero" runat="server" Text="Сборос" OnClick="ButtonZero_Click"/>
                <asp:GridView CssClass="table" ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="LinqDataSource2">
                    <Columns>
                        <asp:BoundField DataField="Id" Visible="false" HeaderText="Id" ReadOnly="True" SortExpression="Id" InsertVisible="False"></asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>
                        <asp:BoundField DataField="Sysname" HeaderText="Sysname" SortExpression="Sysname"></asp:BoundField>
                        <asp:TemplateField HeaderText ="Основное">
                            <ItemTemplate>
                               <asp:CheckBox runat="server"></asp:CheckBox> 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText ="Доп.">
                            <ItemTemplate>
                               <asp:CheckBox runat="server"></asp:CheckBox> 
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource runat="server" EntityTypeName="" ID="LinqDataSource2" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" TableName="RecordFields" OrderBy="Sysname" Select="new (Name, Sysname, Id)"></asp:LinqDataSource>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
