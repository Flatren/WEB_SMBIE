<%@ Page Language="C#" Title="Ред. полей библиографических ссылок" MasterPageFile="~/PagesAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="EditFields.aspx.cs" Inherits="WEB_SMBIE.PagesAdmin.EditFields" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <div>
        <asp:Table ID="Table1" CssClass="table " runat="server">
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
    <div>
        <asp:GridView ID="GridView1"  PageSize="25" CssClass="table table-striped" runat="server" AutoGenerateColumns="False" DataSourceID="LinqDataSource1" AllowPaging="True" AllowSorting="True" DataKeyNames="Id" ><Columns>
           
            <asp:BoundField DataField="Id" Visible="false" HeaderText="Id" ReadOnly="True" SortExpression="Id" InsertVisible="False"></asp:BoundField>
            <asp:BoundField DataField="Name" HeaderText="Название" SortExpression="Name"></asp:BoundField>
            <asp:BoundField DataField="Description" HeaderText="Описание" SortExpression="Description"></asp:BoundField>
            <asp:BoundField DataField="Sysname" HeaderText="Системное название" SortExpression="Sysname"></asp:BoundField>
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True"></asp:CommandField>
        </Columns>
        </asp:GridView>
        <asp:LinqDataSource runat="server" EntityTypeName="" ID="LinqDataSource1" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" TableName="RecordFields" EnableDelete="True" EnableInsert="True" EnableUpdate="True"></asp:LinqDataSource>
    </div>


</asp:Content>