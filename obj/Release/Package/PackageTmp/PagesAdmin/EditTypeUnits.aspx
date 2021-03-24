<%@ Page Language="C#" Title="Ред. Видов подразделений" MasterPageFile="~/PagesAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="EditTypeUnits.aspx.cs" Inherits="WEB_SMBIE.PagesAdmin.EditTypeDep" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <hr />
    <asp:Label ID="Label1" runat="server" CssClass="text-danger"></asp:Label>
    <asp:Table ID="Table1" CssClass="table" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label Text="Название" runat="server"/></asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="TxtName" MaxLength="256" Width="100%" runat="server"></asp:TextBox>
                </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell><asp:Label Text="Описание" runat="server"/></asp:TableCell><asp:TableCell>
                <asp:TextBox ID="TxtDescririon" TextMode="MultiLine" Rows="5" Columns="60" runat="server"></asp:TextBox>
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell><asp:Label Text="Должность при управлении" runat="server"/></asp:TableCell><asp:TableCell>
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="LinqDataSource2" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                <asp:LinqDataSource runat="server" EntityTypeName="" ID="LinqDataSource2" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" Select="new (Id, Name)" TableName="Positions"></asp:LinqDataSource>
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell><asp:Label Text="Родитель" SkinID="TxtParent" runat="server"/></asp:TableCell><asp:TableCell>
                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="LinqDataSource3" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                <asp:LinqDataSource runat="server" EntityTypeName="" ID="LinqDataSource3" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" Select="new (Id, Name)" TableName="TypeDepartments"></asp:LinqDataSource>
            </asp:TableCell></asp:TableRow></asp:Table><asp:Button ID="ButtonCreate" runat="server" Text="Создать" OnClick="ButtonCreate_Click" />

    <asp:Button ID="ButtonSave" runat="server" Text="Сохранить" Visible="false" OnClick="ButtonSave_Click" /><asp:Button ID="ButtonCancel" Visible="false" runat="server" Text="Отменить" OnClick="ButtonCancel_Click" />    <hr />
    <div>
        <asp:GridView ID="GridView1" CssClass="table table-striped" runat="server" AutoGenerateColumns="False" DataSourceID="LinqDataSource1" AllowPaging="True" AllowSorting="True" DataKeyNames="Id" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleted="GridView1_RowDeleted"><Columns>
                <asp:BoundField DataField="Id" Visible="false" HeaderText="Id" ReadOnly="True" SortExpression="Id" InsertVisible="False"></asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Название" SortExpression="Name"></asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Описание" SortExpression="Description"></asp:BoundField>
                <asp:BoundField DataField="Position.Name" HeaderText="Должность управления" SortExpression="Position.Name"></asp:BoundField>
                <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" SelectText="Изменить"></asp:CommandField>
            </Columns>

        </asp:GridView>
        <asp:LinqDataSource runat="server" EntityTypeName="" ID="LinqDataSource1" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" TableName="TypeDepartments" EnableDelete="True" EnableInsert="True" EnableUpdate="True"></asp:LinqDataSource>
    </div>

</asp:Content>
