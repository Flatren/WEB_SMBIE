<%@ Page Title="Редактирование должностей"  Language="C#" AutoEventWireup="true" CodeBehind="EditPosition.aspx.cs" MasterPageFile="~/PagesAdmin/Admin.Master" Inherits="WEB_SMBIE.PagesAdmin.EditPosition" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <hr />
    <asp:Label ID="Label1" runat="server" CssClass="text-danger"></asp:Label>
    <asp:Table CssClass="table" ID="Table1" runat="server">
        <asp:TableRow>
            <asp:TableCell><asp:Label Text="Название" runat="server"/></asp:TableCell><asp:TableCell><asp:TextBox ID="TxtName" MaxLength="256" Width="100%" runat="server"></asp:TextBox></asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell><asp:Label Text="Описание" runat="server"/></asp:TableCell><asp:TableCell><asp:TextBox ID="TxtDescririon" TextMode="MultiLine" Rows="5" Columns="60"  runat="server"></asp:TextBox></asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell><asp:Label Text="Ранг доступа" SkinID="TxtRang" runat="server"/></asp:TableCell><asp:TableCell><asp:TextBox ID="TxtRang"  runat="server" TextMode="Number"></asp:TextBox></asp:TableCell></asp:TableRow></asp:Table><br />
    <asp:Button ID="ButtonCreate" runat="server" Text="Создать должность" OnClick="ButtonCreate_Click" /><div>

    </div>
    <hr />
    <div>
        <asp:GridView ID="GridView1" CssClass="table table-striped" runat="server" AutoGenerateColumns="False" DataSourceID="LinqDataSource1" AllowPaging="True" AllowSorting="True" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Id" Visible="false" HeaderText="Id" ReadOnly="True" SortExpression="Id" InsertVisible="False"></asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>
                <asp:BoundField DataField="Rang" HeaderText="Rang" SortExpression="Rang"></asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"></asp:BoundField>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True"></asp:CommandField>
            </Columns>
        </asp:GridView>

        <asp:LinqDataSource runat="server" EntityTypeName="" ID="LinqDataSource1" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" TableName="Positions" EnableDelete="True" EnableInsert="True" EnableUpdate="True"></asp:LinqDataSource>
    </div>
   
</asp:Content>
